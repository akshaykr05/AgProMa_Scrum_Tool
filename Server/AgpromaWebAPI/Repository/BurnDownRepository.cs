//using Microsoft.EntityFrameworkCore;
//using AgpromaWebAPI.model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AgpromaWebAPI.Repository
//{
//    public interface IBurndownRepository
//    {
//        List<ReleasePlan> GetProjectData(int projectId);
//        List<TaskBacklog> GetTasks(int userId);
//        List<Sprint> GetSprintDetails(int projectId);
//    }
//    public class BurndownRepository : IBurndownRepository
//    {
//        public AgpromaDbContext _context;
//        public BurndownRepository(AgpromaDbContext context)
//        {
//                _context=context;
//        }

//        //get project details for a project.
//        public List<ReleasePlan> GetProjectData(int projectId)
//        {
//            return _context.Releasepl.Include(m=>m.Sprints).ThenInclude(m=>m.Tasks).Where(m => m.ProjectId == projectId).ToList();
//        }

//        public List<Sprint> GetSprintDetails(int projectId)
//        {
//            return _context.Sprintbl.Include(n=>n.Tasks).Where(m=>m.ProjectId==projectId).ToList();
//        }

//        //get all tasks assigned to a user
//        public List<TaskBacklog> GetTasks(int userId)
//        {
//            return _context.Taaskbl.Include(n=>n.SprintBacklogs.ProjectMaster).Where(m => m.PersonId == userId && m.Status==TaskBacklogStatus.Completed).ToList();
//        }
//    }
//}
