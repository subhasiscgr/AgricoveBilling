using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace AgricoveBilling
{
    public partial class AgricoveBilling : Form
    {
        TextBox[] descBox;
        NumericUpDown[] qtyBox;
        NumericUpDown[] uBox;
        Label[] tBox;

        public AgricoveBilling()
        {
            InitializeComponent();
            descBox = new TextBox[] { descBox1, descBox2, descBox3, descBox4, descBox5, descBox6, descBox7, descBox8, descBox9, descBox10, descBox11 };
            qtyBox = new NumericUpDown[] { qtyBox1, qtyBox2, qtyBox3, qtyBox4, qtyBox5, qtyBox6, qtyBox7, qtyBox8, qtyBox9, qtyBox10, qtyBox11 };
            uBox = new NumericUpDown[] { uBox1, uBox2, uBox3, uBox4, uBox5, uBox6, uBox7, uBox8, uBox9, uBox10, uBox11 };
            tBox = new Label[] { tBox1, tBox2, tBox3, tBox4, tBox5, tBox6, tBox7, tBox8, tBox9, tBox10, tBox11 };

            this.AutoScroll = true;
            this.AutoSize = true;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void AgricoveBilling_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void AgricoveBilling_Load(object sender, EventArgs e)
        {

            //vScrollBar1.Enabled = true;
            invno.Text = "#INV" + System.DateTime.Now.Date.ToString("dd") + System.DateTime.Now.Date.ToString("MM") + System.DateTime.Now.Date.ToString("yy");
            //invdt.Text = System.DateTime.Now.Date.ToString("dd/MM/yy");
            label_agri_addr.Text = ConfigurationManager.AppSettings["address"];
            label_agri_url.Text = ConfigurationManager.AppSettings["website"];
            label_agri_email.Text = ConfigurationManager.AppSettings["email"];
            label_ph.Text = ConfigurationManager.AppSettings["phone"];
            label_terms1.Text = ConfigurationManager.AppSettings["terms1"];
            label_terms2.Text = ConfigurationManager.AppSettings["terms2"];
            populate_list();
            disctype.SelectedIndex = 0;
            discval.Controls[0].Visible = false;
            txrt.Controls[0].Visible = false;
            this.ActiveControl = descBox1;

        }
        private void populate_list()
        {
            using (var dataContext = new DBConnection())
            {
                var items = dataContext.Items.ToList();
                var desc = items.Select(t => Tuple.Create(t.ItemName)).ToList();
                foreach (var descval in desc)
                {
                    descBox1.AutoCompleteCustomSource.Add(descval.Item1);
                    descBox2.AutoCompleteCustomSource.Add(descval.Item1);
                    descBox3.AutoCompleteCustomSource.Add(descval.Item1);
                    descBox4.AutoCompleteCustomSource.Add(descval.Item1);
                    descBox5.AutoCompleteCustomSource.Add(descval.Item1);
                    descBox6.AutoCompleteCustomSource.Add(descval.Item1);
                    descBox7.AutoCompleteCustomSource.Add(descval.Item1);
                    descBox8.AutoCompleteCustomSource.Add(descval.Item1);
                    descBox9.AutoCompleteCustomSource.Add(descval.Item1);
                    descBox10.AutoCompleteCustomSource.Add(descval.Item1);
                    descBox11.AutoCompleteCustomSource.Add(descval.Item1);
                }
                dataContext.SaveChanges();                
            }
        }
        private decimal fetch_price(string s)
        {
            decimal price = 0;
            using (var dataContext = new DBConnection())
            {
                var items = dataContext.Items.ToList();
                var p = from x in items where x.ItemName == s select x.ItemPrice;
                try
                {
                    price = decimal.Parse(p.Single());
                }
                catch
                { }
                dataContext.SaveChanges();
            }
            return price;
        }

        private void save_item()
        {
            using (var dataContext = new DBConnection())
            {
                var items = dataContext.Items.ToList();
                int i = 0;
                foreach(var v in descBox)
                {
                    if (v.Text.Length > 0)
                    {
                        Items item = (from x in items
                                      where x.ItemName == v.Text
                                      select x).FirstOrDefault();
                        if (item != null)
                        {
                            item.ItemPrice = uBox[i].Value.ToString();
                        }
                        else
                        {
                            Items item2 = new Items();
                            item2.ItemName = v.Text.ToString();
                            item2.ItemPrice = uBox[i].Value.ToString();
                            dataContext.Items.Add(item2);
                        }
                    }
                    i = i + 1;
                }
                dataContext.SaveChanges();
            }
        }
        private void save_invoice()
        {
            using (var dataContext = new DBConnection())
            {
                dataContext.Invoice.Add(new Invoice() {
                    InvoiceNo = invno.Text,
                    InvoiceDt = invdt.Text,
                    DueDt = ddt.Text,
                    BillToName = billname.Text,
                    BillToAdd = billaddr.Text,
                    ItemID1Name = descBox1.Text,
                    ItemID1Price = uBox1.Value,
                    ItemID1Qty = Convert.ToInt32(qtyBox1.Value),
                    ItemID2Name = descBox2.Text,
                    ItemID2Price = uBox2.Value,
                    ItemID2Qty = Convert.ToInt32(qtyBox2.Value),
                    ItemID3Name = descBox3.Text,
                    ItemID3Price = uBox3.Value,
                    ItemID3Qty = Convert.ToInt32(qtyBox3.Value),
                    ItemID4Name = descBox4.Text,
                    ItemID4Price = uBox4.Value,
                    ItemID4Qty = Convert.ToInt32(qtyBox4.Value),
                    ItemID5Name = descBox5.Text,
                    ItemID5Price = uBox5.Value,
                    ItemID5Qty = Convert.ToInt32(qtyBox5.Value),
                    ItemID6Name = descBox6.Text,
                    ItemID6Price = uBox6.Value,
                    ItemID6Qty = Convert.ToInt32(qtyBox6.Value),
                    ItemID7Name = descBox7.Text,
                    ItemID7Price = uBox7.Value,
                    ItemID7Qty = Convert.ToInt32(qtyBox7.Value),
                    ItemID8Name = descBox8.Text,
                    ItemID8Price = uBox8.Value,
                    ItemID8Qty = Convert.ToInt32(qtyBox8.Value),
                    ItemID9Name = descBox9.Text,
                    ItemID9Price = uBox9.Value,
                    ItemID9Qty = Convert.ToInt32(qtyBox9.Value),
                    ItemID10Name = descBox10.Text,
                    ItemID10Price = uBox10.Value,
                    ItemID10Qty = Convert.ToInt32(qtyBox10.Value),
                    ItemID11Name = descBox11.Text,
                    ItemID11Price = uBox11.Value,
                    ItemID11Qty = Convert.ToInt32(qtyBox11.Value),
                    DiscountValue = discval.Value,
                    DiscountType = disctype.SelectedIndex,
                    TaxRate = txrt.Value,
                    Paid = paid.Value
                });
                dataContext.SaveChanges();
            }
        }
        
        private void save_Click(object sender, EventArgs e)
        {
            save_item();
            save_invoice();
        }

        private void descBox_TextChanged(object sender, EventArgs e)
        {
            int i = Int32.Parse((sender as TextBox).Tag.ToString());
            uBox[i].Value = fetch_price(descBox[i].Text);
            qtyBox[i].Value = 1;
            tBox[i].Text = (qtyBox[i].Value * uBox[i].Value).ToString("0.00");
            if (descBox[i].Text.Length > 0)
            {
                qtyBox[i].Enabled = true;
                uBox[i].Enabled = true;
                descBox[i + 1].Enabled = true;
                qtyBox[i].Focus();
            }
            else
            {
                qtyBox[i].Value = 0;
                uBox[i].Value = 0;
                qtyBox[i].Enabled = false;
                uBox[i].Enabled = false;
            }
        }
        private void qtyBox_ValueChanged(object sender, EventArgs e)
        {
            int i = Int32.Parse((sender as NumericUpDown).Tag.ToString());
            tBox[i].Text = (qtyBox[i].Value * uBox[i].Value).ToString("0.00");
        }

        private void uBox_ValueChanged(object sender, EventArgs e)
        {
            int i = Int32.Parse((sender as NumericUpDown).Tag.ToString());
            tBox[i].Text = (qtyBox[i].Value * uBox[i].Value).ToString("0.00");
        }

        private void tBox_TextChanged(object sender, EventArgs e)
        {
            float sum = 0;
            foreach ( var v in tBox )
            {
                sum = sum + float.Parse(v.Text);
            }
            subtotal.Text = sum.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }



        private void disctype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(disctype.SelectedIndex == 0)
            {
                discval.Value = 0;
                discval.DecimalPlaces = 2;
                discval.Maximum = 900000;
            }
            else if (disctype.SelectedIndex == 1)
            {
                discval.Value = 0;
                discval.DecimalPlaces = 0;
                discval.Maximum = 100;
            }
            disc_calc(sender, e);
        }

        private void disc_calc(object sender, EventArgs e)
        {
            if (disctype.SelectedIndex == 0)
            {
                subttllssdisc.Text = (decimal.Parse(subtotal.Text.ToString()) - discval.Value).ToString("0.00");
            }
            else if (disctype.SelectedIndex == 1)
            {
                subttllssdisc.Text = (decimal.Parse(subtotal.Text.ToString()) * (1 - discval.Value / 100)).ToString("0.00");
            }
        }

        private void txrt_ValueChanged(object sender, EventArgs e)
        {
            ttltax.Text = (decimal.Parse(subttllssdisc.Text.ToString()) * txrt.Value / 100).ToString("0.00");
            grssttl.Text = (decimal.Parse(subttllssdisc.Text.ToString()) * (1 + txrt.Value / 100)).ToString("0.00");
        }

        private void grssttl_TextChanged(object sender, EventArgs e)
        {
            balancedue.Text = (decimal.Parse(grssttl.Text.ToString()) - decimal.Parse(paid.Text.ToString())).ToString("0.00");
        }

        private void balancedue_TextChanged(object sender, EventArgs e)
        {
            save.Enabled = true;
            print.Enabled = true;
        }

        private void AgricoveBilling_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                Form dlg1 = new Form();
                dlg1.ShowDialog();
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                Form dlg1 = new Form();
                dlg1.ShowDialog();
            }
        }
    }
}
