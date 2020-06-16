using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;

namespace Journal.Models
{
    
    public class JournalModel
    {
        public string Day { get; set; }
        public string Name { get; set; }
        
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Symptoms { get; set; }
        public string Body { get; set; }

    }
}