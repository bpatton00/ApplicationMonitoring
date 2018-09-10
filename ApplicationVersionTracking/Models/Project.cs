using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationVersionTracking.Models
{
    public class Project
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string TFprojID { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}
