using System.Collections.Generic;

namespace DrugKeeper.Models
{
    public class OnlinePharmacy
    {
        public List<Data> data;

        public class Data
        {
            public string konum;

            public string phone;

            public string eczane_adres;

            public string eczane_adi;
        }
    }
}
