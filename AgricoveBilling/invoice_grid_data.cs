using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricoveBilling
{
    class invoice_grid_data
    {
        public string InvoiceNo { get; set; }
        public string InvoiceDt { get; set; }
        public string DueDt { get; set; }
        public string BillToName { get; set; }
        public decimal due { get; set; }
    }
}
