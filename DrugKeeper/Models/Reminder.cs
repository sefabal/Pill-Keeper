using System;

namespace DrugKeeper.Models
{
    public class Reminder : BaseModel
    {
        public Pill Pill { get; set; }

        public int Amount { get; set; }

        public int FrequencyHour { get; set; }

        public DateTime StartingDate { get; set; }

        public double Dose { get; set; }
    }
}
