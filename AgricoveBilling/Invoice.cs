using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricoveBilling
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDt { get; set; }
        public string DueDt { get; set; }
        public string BillToName { get; set; }
        public string BillToAdd { get; set; }
        public string ItemID1Name { get; set; }
        public decimal ItemID1Price { get; set; }
        public int ItemID1Qty { get; set; }
        public string ItemID2Name { get; set; }
        public decimal ItemID2Price { get; set; }
        public int ItemID2Qty { get; set; }
        public string ItemID3Name { get; set; }
        public decimal ItemID3Price { get; set; }
        public int ItemID3Qty { get; set; }
        public string ItemID4Name { get; set; }
        public decimal ItemID4Price { get; set; }
        public int ItemID4Qty { get; set; }
        public string ItemID5Name { get; set; }
        public decimal ItemID5Price { get; set; }
        public int ItemID5Qty { get; set; }
        public string ItemID6Name { get; set; }
        public decimal ItemID6Price { get; set; }
        public int ItemID6Qty { get; set; }
        public string ItemID7Name { get; set; }
        public decimal ItemID7Price { get; set; }
        public int ItemID7Qty { get; set; }
        public string ItemID8Name { get; set; }
        public decimal ItemID8Price { get; set; }
        public int ItemID8Qty { get; set; }
        public string ItemID9Name { get; set; }
        public decimal ItemID9Price { get; set; }
        public int ItemID9Qty { get; set; }
        public string ItemID10Name { get; set; }
        public decimal ItemID10Price { get; set; }
        public int ItemID10Qty { get; set; }
        public string ItemID11Name { get; set; }
        public decimal ItemID11Price { get; set; }
        public int ItemID11Qty { get; set; }
        public decimal DiscountValue { get; set; }
        public int DiscountType { get; set; }
        public decimal TaxRate { get; set; }
        public decimal Paid { get; set; }
        public decimal Due { get; set; }
    }
    
}
