namespace ToDoList.Models
{
    public class TaskModel
    {
        public string TaskData { get; set; } 
        public string dd { get; set; } 
        public string addtasks { get; set; } 
        public string TaskTime { get; set; } 
        public string UpdatetaskTime { get; set; } 
        public string TaskTDime { get; set; } 
        public bool TaskCheck { get; set; } 
        public bool TaskDTimecheck { get; set; } 
        public bool TaskDCheck { get; set; } 
        public bool TaskACheck { get; set; } 
        public bool TaskUCheck { get; set; } 
        public bool TaskTCheck { get; set; } 
        public bool TaskDDCheck { get; set; } 
        public string Message { get; set; }
        public string Updatetask { get; set; }
        public string istaskcompletedcheckbox { get; set; }
        public string istaskcompletedcheckboxstatus { get; set; }
        public string UserID { get; set; }
        public List<TaskModel> list1 = new List<TaskModel>();
        public List<TaskModel> list2;


        //Display
        public string taskshow { get; set; }
        public string deletetask { get; set; }
        public string tasdID { get; set; }
        public int taskId { get; set; }
    }
}
