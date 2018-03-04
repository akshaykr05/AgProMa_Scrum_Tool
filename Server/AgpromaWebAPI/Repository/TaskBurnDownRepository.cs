//using AgpromaWebAPI.model;
//using AgpromaWebAPI.Service;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AgpromaWebAPI.Repository
//{
//    public interface ITaskBurnDownRepository
//    {
//        void Add_DailyData(TaskBurnDown task);
//        List<TaskBurnDown> Get();
//    }
//    public class TaskBurnDownRepository:ITaskBurnDownRepository
//    {
//        private AgpromaDbContext _context;
//        private ICheckListService _checklistservice;
//        public TaskBurnDownRepository(AgpromaDbContext context,ICheckListService checklistservice)
//        {
//            _context = context;
//            _checklistservice = checklistservice;
//        }
//        public void Add_DailyData(TaskBurnDown task)
//        {
//            List<ChecklistBacklog> checklist=_checklistservice.Get();
//            task.completedSize = 0;
//            int remainingSize = 0;
//            foreach (var data in checklist)
//            {
//                completedSize = completedSize + data.CompletedSize;
//                remainingSize = remainingSize + data.RemainingSize;
//            }
//            _context.TaskBurnDowns.Add(task);

//        }
//        public List<TaskBurnDown> Get()
//        {
//            return 
//        }

//    }
//}
