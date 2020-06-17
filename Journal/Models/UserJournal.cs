using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Journal.Models
{
    public class UserJournal
    {
        public string UserId { get; set; }

        public User User { get; set; }
        public int JournalId { get; set; }

        public JournalModel Journal { get; set; }

    }
}


