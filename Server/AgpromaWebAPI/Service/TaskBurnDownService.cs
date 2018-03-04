//using AgpromaWebAPI.model;
//using AgpromaWebAPI.Repository;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AgpromaWebAPI.Service
//{
//    public interface ITaskBurnDownService
//    {
//        void Add_DailyData(TaskBurnDown task);
//        List<TaskBurnDown> Get();
//    }
//    public class TaskBurnDownService : ITaskBurnDownService
//    {
//        private ITaskBurnDownRepository _taskburndownrepository;
//        public TaskBurnDownService(ITaskBurnDownRepository taskburndownrepository)
//        {
//            _taskburndownrepository = taskburndownrepository;
//        }
//        //add daily status of a task
//        public void Add_DailyData(TaskBurnDown task)
//        {
//            _taskburndownrepository.Add_DailyData(task);

//        }
//        //get all the details 
//        public List<TaskBurnDown> Get()
//        {
//            return
//        }
//    }
