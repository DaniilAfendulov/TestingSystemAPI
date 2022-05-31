using Common.Data.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingSystemAPI.Models
{
    public class ModuleInfo
    {
        public Module Module { get; set; }
        public List<Lesson> Lessons { get; set; }        
    }
}
