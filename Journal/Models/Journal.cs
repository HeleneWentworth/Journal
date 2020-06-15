using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;

namespace Journal.Models
{
    public class Journal
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