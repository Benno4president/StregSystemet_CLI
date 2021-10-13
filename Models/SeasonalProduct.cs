using System;

namespace Stregsystem.Models
{
    public class SeasonalProduct : Product
    {
   
        public override bool Active { get; set; }

        public DateTime SeasonStartDate { get; set; }
        public DateTime SeasonEndDate { get; set; }

        public SeasonalProduct(int id, string name, decimal price, bool active, bool canBeBoughtOnCredit, DateTime seasonStartDate, DateTime seasonEndDate) 
            : base(id, name, price, active, canBeBoughtOnCredit)
        {
            Id = (id > 0) ? id : throw new ArgumentOutOfRangeException();
            Name = name ?? throw new ArgumentNullException();
            Price = price;
            Active = IsActive(active, seasonStartDate, seasonEndDate);
            CanBeBoughtOnCredit = canBeBoughtOnCredit;
            SeasonStartDate = seasonStartDate;
            SeasonEndDate = seasonEndDate;
        }

        private bool IsActive(bool active, DateTime Start, DateTime End)
        {
            DateTime now = DateTime.Now;
            if (now < End && now > Start && active == true)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            var spaces = 5 - Id.ToString().Length;
            var curSpaces = 6 - (Price / 100).ToString().Length;
            var sp = "";
            var curSp = "";

            for (int i = 0; i < spaces; i++)
            {
                sp += " ";
            }
            for (int i = 0; i < curSpaces; i++)
            {
                curSp += " ";
            }

            return $"ID:{sp}{Id} |{curSp}{Price / 100} kr| {Name}   (Ends: {SeasonEndDate})";
        }
    }
}