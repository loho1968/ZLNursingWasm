using System;
namespace NursingModel.TaskModels
{
    public class TaskFormList
    {
        public string Id { get; set; }
        public string FormId { get; set; }
        public bool? Scope { get; set; }
        public string TaskEventCode { get; set; }
        public string FormTimingId { get; set; }
        public string FormName { get; set; }
        
        public string WardId { get; set; }
    }
}
