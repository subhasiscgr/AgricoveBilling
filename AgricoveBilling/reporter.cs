using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricoveBilling
{
    public class reporter
    {
        public string InvoiceNo;
        public string InvoiceDt;
        public string DueDt;
        public string BillToName;
        public string BillToAdd;
        public string ItemID1Name;
        public decimal ItemID1Price;
        public decimal ItemID1Qty;
        public decimal ItemID1Total;
        public string ItemID2Name;
        public decimal ItemID2Price;
        public decimal ItemID2Qty;
        public decimal ItemID2Total;
        public string ItemID3Name;
        public decimal ItemID3Price;
        public decimal ItemID3Qty;
        public decimal ItemID3Total;
        public string ItemID4Name;
        public decimal ItemID4Price;
        public decimal ItemID4Qty;
        public decimal ItemID4Total;
        public string ItemID5Name;
        public decimal ItemID5Price;
        public decimal ItemID5Qty;
        public decimal ItemID5Total;
        public string ItemID6Name;
        public decimal ItemID6Price;
        public decimal ItemID6Qty;
        public decimal ItemID6Total;
        public string ItemID7Name;
        public decimal ItemID7Price;
        public decimal ItemID7Qty;
        public decimal ItemID7Total;
        public string ItemID8Name;
        public decimal ItemID8Price;
        public decimal ItemID8Qty;
        public decimal ItemID8Total;
        public string ItemID9Name;
        public decimal ItemID9Price;
        public decimal ItemID9Qty;
        public decimal ItemID9Total;
        public string ItemID10Name;
        public decimal ItemID10Price;
        public decimal ItemID10Qty;
        public decimal ItemID10Total;
        public string ItemID11Name;
        public decimal ItemID11Price;
        public decimal ItemID11Qty;
        public decimal ItemID11Total;

        public decimal Subtotal;
        public string DiscountType;
        public decimal DiscountValue;
        public decimal SubtotalLessDiscount;
        public decimal TaxRate;
        public decimal Totaltax;
        public decimal GrossTotal;
        public decimal Paid;
        public decimal Due;

        public reporter(
            string invno,
            string invdt,
            string ddt,
            string billname,
            string billaddr,

            string descBox1,
            decimal uBox1,
            decimal qtyBox1,
            decimal tBox1,
            string descBox2,
            decimal uBox2,
            decimal qtyBox2,
            decimal tBox2,
            string descBox3,
            decimal uBox3,
            decimal qtyBox3,
            decimal tBox3,
            string descBox4,
            decimal uBox4,
            decimal qtyBox4,
            decimal tBox4,
            string descBox5,
            decimal uBox5,
            decimal qtyBox5,
            decimal tBox5,
            string descBox6,
            decimal uBox6,
            decimal qtyBox6,
            decimal tBox6,
            string descBox7,
            decimal uBox7,
            decimal qtyBox7,
            decimal tBox7,
            string descBox8,
            decimal uBox8,
            decimal qtyBox8,
            decimal tBox8,
            string descBox9,
            decimal uBox9,
            decimal qtyBox9,
            decimal tBox9,
            string descBox10,
            decimal uBox10,
            decimal qtyBox10,
            decimal tBox10,
            string descBox11,
            decimal uBox11,
            decimal qtyBox11,
            decimal tBox11,
            decimal subtotal,
            string disctype,
            decimal discval,
            decimal subttllssdisc,
            decimal txrt,
            decimal ttltax,
            decimal grssttl,
            decimal paid,
            decimal balancedue
        )
        {
            InvoiceNo = invno;
            InvoiceDt = invdt;
            DueDt = ddt;
            BillToName = billname;
            BillToAdd = billaddr;
            ItemID1Name = descBox1;
            ItemID1Price = uBox1;
            ItemID1Qty = qtyBox1;
            ItemID1Total = tBox1;
            ItemID2Name = descBox2;
            ItemID2Price = uBox2;
            ItemID2Qty = qtyBox2;
            ItemID2Total = tBox2;
            ItemID3Name = descBox3;
            ItemID3Price = uBox3;
            ItemID3Qty = qtyBox3;
            ItemID3Total = tBox3;
            ItemID4Name = descBox4;
            ItemID4Price = uBox4;
            ItemID4Qty = qtyBox4;
            ItemID4Total = tBox4;
            ItemID5Name = descBox5;
            ItemID5Price = uBox5;
            ItemID5Qty = qtyBox5;
            ItemID5Total = tBox5;
            ItemID6Name = descBox6;
            ItemID6Price = uBox6;
            ItemID6Qty = qtyBox6;
            ItemID6Total = tBox6;
            ItemID7Name = descBox7;
            ItemID7Price = uBox7;
            ItemID7Qty = qtyBox7;
            ItemID7Total = tBox7;
            ItemID8Name = descBox8;
            ItemID8Price = uBox8;
            ItemID8Qty = qtyBox8;
            ItemID8Total = tBox8;
            ItemID9Name = descBox9;
            ItemID9Price = uBox9;
            ItemID9Qty = qtyBox9;
            ItemID9Total = tBox9;
            ItemID10Name = descBox10;
            ItemID10Price = uBox10;
            ItemID10Qty = qtyBox10;
            ItemID10Total = tBox10;
            ItemID11Name = descBox11;
            ItemID11Price = uBox11;
            ItemID11Qty = qtyBox11;
            ItemID11Total = tBox8;

            Subtotal = subtotal;
            DiscountType = disctype;
            DiscountValue = discval;
            SubtotalLessDiscount = subttllssdisc;
            TaxRate = txrt;
            Totaltax = ttltax;
            GrossTotal = grssttl;
            Paid = paid;
            Due = balancedue;
        }
    }
}
/*
 * 
 * 
public class reporter : IEnumerable<reporter>
{
    public string InvoiceNo { get; set; }
    public string InvoiceDt { get; set; }
    public string DueDt { get; set; }
    public string BillToName { get; set; }
    public string BillToAdd { get; set; }

    public string ItemID1Name { get; set; }
    public decimal ItemID1Price { get; set; }
    public decimal ItemID1Qty { get; set; }
    public decimal ItemID1Total { get; set; }
    public string ItemID2Name { get; set; }
    public decimal ItemID2Price { get; set; }
    public decimal ItemID2Qty { get; set; }
    public decimal ItemID2Total { get; set; }
    public string ItemID3Name { get; set; }
    public decimal ItemID3Price { get; set; }
    public decimal ItemID3Qty { get; set; }
    public decimal ItemID3Total { get; set; }
    public string ItemID4Name { get; set; }
    public decimal ItemID4Price { get; set; }
    public decimal ItemID4Qty { get; set; }
    public decimal ItemID4Total { get; set; }
    public string ItemID5Name { get; set; }
    public decimal ItemID5Price { get; set; }
    public decimal ItemID5Qty { get; set; }
    public decimal ItemID5Total { get; set; }
    public string ItemID6Name { get; set; }
    public decimal ItemID6Price { get; set; }
    public decimal ItemID6Qty { get; set; }
    public decimal ItemID6Total { get; set; }
    public string ItemID7Name { get; set; }
    public decimal ItemID7Price { get; set; }
    public decimal ItemID7Qty { get; set; }
    public decimal ItemID7Total { get; set; }
    public string ItemID8Name { get; set; }
    public decimal ItemID8Price { get; set; }
    public decimal ItemID8Qty { get; set; }
    public decimal ItemID8Total { get; set; }
    public string ItemID9Name { get; set; }
    public decimal ItemID9Price { get; set; }
    public decimal ItemID9Qty { get; set; }
    public decimal ItemID9Total { get; set; }
    public string ItemID10Name { get; set; }
    public decimal ItemID10Price { get; set; }
    public decimal ItemID10Qty { get; set; }
    public decimal ItemID10Total { get; set; }
    public string ItemID11Name { get; set; }
    public decimal ItemID11Price { get; set; }
    public decimal ItemID11Qty { get; set; }
    public decimal ItemID11Total { get; set; }

    public decimal Subtotal { get; set; }
    public string DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal SubtotalLessDiscount { get; set; }
    public decimal TaxRate { get; set; }
    public decimal Totaltax { get; set; }
    public decimal GrossTotal { get; set; }
    public decimal Paid { get; set; }
    public decimal Due { get; set; }

    public IEnumerator<reporter> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}*/

