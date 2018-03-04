//using AgpromaWebAPI.model;
//using AgpromaWebAPI.Repository;
//using AgpromaWebAPI.Viewmodel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AgpromaWebAPI.Service
//{
//    public interface IBurndownService
//    {
//        List<ReleasePlan> GetProjectData(int projectId);
//        List<UserBurnDown> GetTasks(int userId);
//        List<Sprint> GetSprintDetails(int projectId);
//    }

//    public class BurndownService : IBurndownService
//    {
//        public IBurndownRepository _repository;
//        public BurndownService(IBurndownRepository repository)
//        {
//            _repository = repository;
//        }

//        //get all tasks assign to a user
//        public List<UserBurnDown> GetTasks(int userId)
//        {
//            List<UserBurnDown> dropdown = new List<UserBurnDown>();
//            //get all tasks for a user
//            List<TaskBacklog> tasks= _repository.GetTasks(userId);
//            tasks.ForEach(m => {
//                UserBurnDown userbd = new UserBurnDown();
//                userbd.TaskId = m.TaskId;
//                userbd.TaskName = m.TaskName;
//                userbd.ExpectedDate = m.EndDate.Subtract(m.StartDate).TotalHours;
//                userbd.ActualDate = m.ActualEndDate.Subtract(m.StartDate).TotalHours;
//                userbd.ProjectId =m.SprintBacklogs.ProjectId;
//                userbd.ProjectName = m.SprintBacklogs.ProjectMaster.Name;
//                userbd.SprintName=m.SprintBacklogs.SprintName;
//                //Add each task details for a user
//                dropdown.Add(userbd);
//            });
//            return dropdown;
//        }

//        //get all project details specific to a project.
//        public List<ReleasePlan> GetProjectData(int projectId)
//        {
//            var projectData= _repository.GetProjectData(projectId);
//            return projectData;
//        }

//        public List<Sprint> GetSprintDetails(int projectId)
//      {
//            return _repository.GetSprintDetails(projectId);
//        }
//    }
//}
