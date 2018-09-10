using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationVersionTracking.Models
{
    public class Application
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [ForeignKey("Project")]
        public long ProjectID { get; set; }
        public Project Project { get; set; }
        [ForeignKey("StatusType")]
        public int StatusTypeID { get; set; }
        public StatusType StatusType { get; set; }
        public string ActiveTFRelease { get; set; }

        public string DisplayName { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
