using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;

namespace AgricoveBilling
{
    public partial class Printpreview : Form
    {
        reporter rep;
        public Printpreview(reporter r)
        {
            InitializeComponent();
            rep = r;      
        }

        private void Printpreview_Load(object sender, EventArgs e)
        {
            ReportDocument rpt = new ReportDocument();
            rpt.Load(@"..\printform.rpt");
            //rpt.SetDataSource(rep);
            rpt.SetParameterValue("_InvoiceNo", rep.InvoiceNo);
            rpt.SetParameterValue("_InvoiceDt", rep.InvoiceDt);
            rpt.SetParameterValue("_InvoiceDueDt", rep.DueDt);
            rpt.SetParameterValue("_BillToName", rep.BillToName);
            rpt.SetParameterValue("_BillToAdd", rep.BillToAdd);

            rpt.SetParameterValue("_Item1Name", rep.ItemID1Name);
            rpt.SetParameterValue("_Item1Qty", rep.ItemID1Qty);
            rpt.SetParameterValue("_Item1Price", rep.ItemID1Price);
            rpt.SetParameterValue("_Item1Total", rep.ItemID1Total);
            rpt.SetParameterValue("_Item2Name", rep.ItemID2Name);
            rpt.SetParameterValue("_Item2Qty", rep.ItemID2Qty);
            rpt.SetParameterValue("_Item2Price", rep.ItemID2Price);
            rpt.SetParameterValue("_Item2Total", rep.ItemID2Total);
            rpt.SetParameterValue("_Item3Name", rep.ItemID3Name);
            rpt.SetParameterValue("_Item3Qty", rep.ItemID3Qty);
            rpt.SetParameterValue("_Item3Price", rep.ItemID3Price);
            rpt.SetParameterValue("_Item3Total", rep.ItemID3Total);
            rpt.SetParameterValue("_Item4Name", rep.ItemID4Name);
            rpt.SetParameterValue("_Item4Qty", rep.ItemID4Qty);
            rpt.SetParameterValue("_Item4Price", rep.ItemID4Price);
            rpt.SetParameterValue("_Item4Total", rep.ItemID4Total);
            rpt.SetParameterValue("_Item5Name", rep.ItemID5Name);
            rpt.SetParameterValue("_Item5Qty", rep.ItemID5Qty);
            rpt.SetParameterValue("_Item5Price", rep.ItemID5Price);
            rpt.SetParameterValue("_Item5Total", rep.ItemID5Total);
            rpt.SetParameterValue("_Item6Name", rep.ItemID6Name);
            rpt.SetParameterValue("_Item6Qty", rep.ItemID6Qty);
            rpt.SetParameterValue("_Item6Price", rep.ItemID6Price);
            rpt.SetParameterValue("_Item6Total", rep.ItemID6Total);
            rpt.SetParameterValue("_Item7Name", rep.ItemID7Name);
            rpt.SetParameterValue("_Item7Qty", rep.ItemID7Qty);
            rpt.SetParameterValue("_Item7Price", rep.ItemID7Price);
            rpt.SetParameterValue("_Item7Total", rep.ItemID7Total);
            rpt.SetParameterValue("_Item8Name", rep.ItemID8Name);
            rpt.SetParameterValue("_Item8Qty", rep.ItemID8Qty);
            rpt.SetParameterValue("_Item8Price", rep.ItemID8Price);
            rpt.SetParameterValue("_Item8Total", rep.ItemID8Total);
            rpt.SetParameterValue("_Item9Name", rep.ItemID9Name);
            rpt.SetParameterValue("_Item9Qty", rep.ItemID9Qty);
            rpt.SetParameterValue("_Item9Price", rep.ItemID9Price);
            rpt.SetParameterValue("_Item9Total", rep.ItemID9Total);
            rpt.SetParameterValue("_Item10Name", rep.ItemID10Name);
            rpt.SetParameterValue("_Item10Qty", rep.ItemID10Qty);
            rpt.SetParameterValue("_Item10Price", rep.ItemID10Price);
            rpt.SetParameterValue("_Item10Total", rep.ItemID10Total);
            rpt.SetParameterValue("_Item11Name", rep.ItemID11Name);
            rpt.SetParameterValue("_Item11Qty", rep.ItemID11Qty);
            rpt.SetParameterValue("_Item11Price", rep.ItemID11Price);
            rpt.SetParameterValue("_Item11Total", rep.ItemID11Total);

            rpt.SetParameterValue("_Subtotal", rep.Subtotal);
            rpt.SetParameterValue("_DiscountValue", rep.DiscountValue);
            rpt.SetParameterValue("_DiscountType", rep.DiscountType);
            rpt.SetParameterValue("_SubtotalLessDiscount", rep.SubtotalLessDiscount);
            rpt.SetParameterValue("_TaxRate", rep.TaxRate);
            rpt.SetParameterValue("_TotalTax", rep.Totaltax);
            rpt.SetParameterValue("_GrossTotal", rep.GrossTotal);
            rpt.SetParameterValue("_Paid", rep.Paid);
            rpt.SetParameterValue("_Due", rep.Due);

            print_crystalreport.ReportSource = rpt;            
            print_crystalreport.Zoom(55);
            print_crystalreport.Refresh();
            foreach (Control c1 in print_crystalreport.Controls)
            {
                if (c1 is CrystalDecisions.Windows.Forms.PageView)
                {
                    PageView pv = (PageView)c1;
                    foreach (Control c2 in pv.Controls)
                    {
                        if (c2 is TabControl)
                        {
                            TabControl tc = (TabControl)c2;
                            tc.ItemSize = new Size(0, 1);
                            tc.SizeMode = TabSizeMode.Fixed;
                        }
                    }
                }
            }
        }
    }
}
