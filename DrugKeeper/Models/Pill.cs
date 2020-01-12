using System;

namespace DrugKeeper.Models
{
    public class Pill : BaseModel
    {
        public string Name { get; set; }

        public string Details { get; set; }

        public string SideEffects { get; set; }

    }
}
