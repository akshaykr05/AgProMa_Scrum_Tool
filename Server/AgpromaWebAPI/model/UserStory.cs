using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgpromaWebAPI.model
{
    public enum StoryStatus
    {
        New,
        Ready,
        Inprogress,
        Completed
    }

    public class UserStory
    {
        [Key]
        public int StoryId { get; set; }

        public string StoryName { get; set; }

        public int Priority { get; set; }

        public string Comments { get; set; }

        public string Rationale { get; set; }

        public int PlannedSize { get; set; }

        public int ActualSize { get; set; }

        public int UserId { get; set; }

        [EnumDataType(typeof(StoryStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public StoryStatus Status { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectMaster ProjectMaster { get; set; }

        public virtual List<TaskBacklog> Tasks { get; }
    }
}
