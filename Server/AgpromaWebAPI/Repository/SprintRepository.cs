using Microsoft.EntityFrameworkCore;
using AgpromaWebAPI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Repository
{
    public interface ISprintRepository
    {
        List<Sprint> GetAll(int projectId);
        Sprint Get(int sprintId);
        void Add(Sprint sprint);
        void Update(int sprintId, UserStory sprint);
        void Delete(int sprintId);
        void UpdateConnectionId(string connectionid, int memberid);
        List<SignalRMaster> CreateGroup(int projectid);
    }

    public class SprintRepository : ISprintRepository
    {
        public AgpromaDbContext _context;
        public SprintRepository(AgpromaDbContext context)
        {
            _context = context;
        }

        //add a new sprint.
        public void Add(Sprint sprint)
        {
            sprint.ExpectedEndDate = sprint.StartDate.AddDays(sprint.TotalDays-1);
            sprint.Status = SprintStatus.Unplanned;
            _context.Sprints.Add(sprint);
            _context.SaveChanges();
        }

        //delete particular sprint.
        public void Delete(int sprintId)
        {
            Sprint sprint = _context.Sprints.FirstOrDefault(m => m.SprintId == sprintId);
            _context.Sprints.Remove(sprint);
            _context.SaveChanges();
        }

        //get the sprint backlog.
        public Sprint Get(int sprintId)
        {
            return _context.Sprints.FirstOrDefault(m => m.SprintId == sprintId);
        }

        //get all sprint backlogs.
        public List<Sprint> GetAll(int projectId)
        {
            return _context.Sprints.Where(m => m.ProjectId == projectId).ToList();
        }

        //updates sprint details of the specific sprint.
        public void Update(int sprintId, UserStory sprint)
        {
            UserStory sprints = _context.Userstories.FirstOrDefault(m => m.StoryId == sprint.StoryId);
            Sprint Sprints = _context.Sprints.FirstOrDefault(p => p.SprintId == sprintId);
            Sprints.Status = SprintStatus.Inprogress;
            sprints.InSprintNo = sprintId;
            _context.SaveChanges();
        }

        public void UpdateConnectionId(string connectionid, int memberid)
        {
            SignalRMaster signalr = _context.SignalRDb.FirstOrDefault(m => m.MemberId == memberid);
            signalr.ConnectionId = connectionid;
            signalr.HubCode = HubCode.sprint;
            signalr.Online = true;
            _context.SaveChanges();

        }
        public List<SignalRMaster> CreateGroup(int projectid)
        {
            List<Projectmembers> members = _context.Projectmembers.Where(p => p.ProjectId == projectid).ToList();

            List<SignalRMaster> onlinemembers = _context.SignalRDb.Where(m => m.Online == true && m.HubCode == HubCode.sprint).ToList();
            var data = members.Select(m => m.MemberId).ToList();
            var users = onlinemembers.Where(om => data.Contains(om.MemberId)).ToList();
            return users;
        }









    }
}

