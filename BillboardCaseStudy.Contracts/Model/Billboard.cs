using System.Diagnostics;

namespace BillboardCaseStudy.Contracts.Model
{
    [DebuggerDisplay("No = {No}, Price = {Price}")]
    public class Billboard
    {
        public Billboard()
        {
            No = string.Empty;
        }
        
        public string No { get; set; }

        public decimal Price { get; set; }

        public double Latidude { get; set; }

        public double Longitude { get; set; }
    }
}
