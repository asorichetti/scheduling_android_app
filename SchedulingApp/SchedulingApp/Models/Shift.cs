using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingApp.Models
{
    public class Shift
    {
        public int Id {  get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location {  get; set; }
        public int SlotsAvailable { get; set; }
        public int SlotsTotal { get; set; }
        public string OrganizationName { get; set; }
    }
}
