using System.Collections.Generic;

namespace DrugKeeper.Models
{
    public class User : BaseModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public List<Reminder> Reminders { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }
    }
}
