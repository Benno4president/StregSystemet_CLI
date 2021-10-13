using System;

namespace Stregsystem.Models
{
    public class Product
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual bool Active { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }

        public Product(int id, string name, decimal price, bool active, bool canBeBoughtOnCredit)
        {
            Id = (id > 0) ? id : throw new ArgumentOutOfRangeException();
            Name = name ?? throw new ArgumentNullException();
            Price = price;
            Active = active;
            CanBeBoughtOnCredit = canBeBoughtOnCredit;
        }

        public override string ToString()
        {
            var spaces = 5 - Id.ToString().Length;
            var curSpaces = 6 - (Price/100).ToString().Length;
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

            return $"ID:{sp}{Id} |{curSp}{Price/100} kr| {Name}";
        }

    }
}