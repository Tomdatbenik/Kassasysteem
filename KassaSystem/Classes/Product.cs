using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystem.Classes
{
    class Product
    {
        public string PBarcode { get; }
        public string POmschrijving { get; }
        public int PPrijs { get; }

        public Product(string barcode, string omschrijving, int prijs)
        {
            this.PBarcode = barcode;
            this.POmschrijving = omschrijving;
            this.PPrijs = prijs;
        }
    }
}
