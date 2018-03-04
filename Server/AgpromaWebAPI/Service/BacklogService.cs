using AgpromaWebAPI.model;
using AgpromaWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.Service
{
    public interface IBacklogServices
    {
        void setConnectionId(string connecitonId, int memberId);
        List<SignalRMaster> JoinGroup(int projectId);
        List<UserStory> GetAll(int  id);
        void Add(UserStory backlog);
        UserStory Update(int id, UserStory res);
        void Delete(int id);
    }

    public class BacklogService : IBacklogServices
    {
      private  IBacklogRepository _repository;
        public BacklogService( IBacklogRepository repository)
        {
            _repository = repository;
        }
        //for adding new user story
        public void Add(UserStory backlog)
        {
            _repository.Add(backlog);
        }

        //for deleting particular user story based on storyid
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        //for getting all unassingned story
        public List<UserStory> GetAll(int  id)
        {
            return _repository.GetAll(id).ToList();
        }

        // for update  a user story based on storyid
        public UserStory Update(int id, UserStory res)
        {
            return _repository.Update(id, res);
        }

        //update the connection for the user
        public void setConnectionId(string connectionId, int memberId)
        {
            _repository.setConnectionId(connectionId,memberId);
        }

        //get the online members.
        public List<SignalRMaster> JoinGroup(int projectId)
        {
            return _repository.JoinGroup(projectId);
        }
    }
}
