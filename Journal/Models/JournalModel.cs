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
        public int Name { get; set; }
        public string Surname { get; set; }
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Title { get; set; }
        public decimal PostNumber { get; set; }
    }
}