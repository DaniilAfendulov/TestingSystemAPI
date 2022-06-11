using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingSystemAPI.Models
{
    public class AnswerDTO
    {
        public Guid ID { get; set; }
        public IEnumerable<string> Answers { get; set; }
    }
}
