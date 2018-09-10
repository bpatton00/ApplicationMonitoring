using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApplicationVersionTracking.Models
{
    public class Event
    {
        public long ID { get; set; }
        [ForeignKey("Event")]
        public int EventTypeID { get; set; }
        public EventType EventType { get; set; }
        [ForeignKey("Application")]
        public int ApplicationID { get; set; }
        public Application Application { get; set; }
        [ForeignKey("Server")]
        public int ServerID { get; set; }
        public Server Server { get; set; }
        [Required]
        public string msiFile { get; set; }
        [RegularExpression(@"^(?!REL_|rel_|Rel_).+")]
        [Required]
        public string Version { get; set; }
        [ForeignKey("EnvironmentType")]
        public int EnvironmentTypeID { get; set; }
        public EnvironmentType EnvironmentType { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public string Installer { get; set; }       
    }

    public class EventCountModel
    {
        public string s_eventDate { get; set; }
        public DateTime d_eventDate { get; set; }
        public int eventCount { get; set; }        
        public int developmentCount { get; set; }
        public int testCount { get; set; }
        public int productionCount { get; set; }
    }
}
