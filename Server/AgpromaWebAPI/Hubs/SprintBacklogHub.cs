using Microsoft.AspNetCore.SignalR;
using AgpromaWebAPI.model;
using AgpromaWebAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Hubs
{
    public class SprintBacklogHub : Hub
    {
        private ISprintService _service;
        private IBacklogServices _backlogService;

        public SprintBacklogHub(ISprintService service,IBacklogServices backlogService)
        {
            _service = service;
            _backlogService = backlogService;
        }

        //call method to add memberinfo into db with connectionid and memberid
        public void SetConnectionId(int memberId)
        {
            _service.UpdateConnectionId(Context.ConnectionId, memberId);
        }

        //create a group and member join it each time they visited this Hub
        public void CreateGroup(int projectid)
        {
            var users = _service.CreateGroup(projectid);
            foreach (var user in users)
            {
                Groups.AddAsync(user.ConnectionId, "SprintGroup");
            }

        }

        //get all the sprints specific to projectId
        public void GetSprints(int projectid)
        {
            List<Sprint> data = _service.GetAll(projectid);
            Clients.Client(Context.ConnectionId).InvokeAsync("getSprints",data);
        }

        //add the sprint
        public Task AddSprint(Sprint sprint)
        {
            CreateGroup(sprint.ProjectId);
            _service.Add(sprint);
            return Clients.Group("SprintGroup").InvokeAsync("postSprints", sprint);
        }

        //update the sprint(assign story to sprint).
        public void UpdateStoryInSprint(UserStory sprint, int sprintid,int projectid)
        {
            CreateGroup(projectid);
            _service.Update(sprintid,sprint);
            GetAllBacklogs(projectid);
        }

        //get all the backlogs specific to projectId
        public Task GetAllBacklogs(int projectId)
        {
            CreateGroup(projectId);
            List<UserStory> backlogs=_backlogService.GetAll(projectId);
            return Clients.Group("SprintGroup").InvokeAsync("getBacklogs", backlogs);
        }

    }
}
