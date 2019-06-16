using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Drawing.Printing;
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
        const int VK_TAB = 0x09; //up key
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;

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
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        DataGridViewButtonColumn ButtonColumn = new DataGridViewButtonColumn();

        //Print
        private PrintDocument printDocument1 = new PrintDocument();
        Bitmap memoryImage;
        //

        private void AgricoveBilling_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void cleanform()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else if (control is NumericUpDown)
                        (control as NumericUpDown).Value = 0;
                    else
                        func(control.Controls);
            };

            func(Controls);
        }
        private void disable_rows()
        {
            foreach(var desc in descBox)
            {
                desc.Enabled = false;
            }
            foreach (var q in qtyBox)
            {
                q.Enabled = false;
            }
            foreach (var u in uBox)
            {
                u.Enabled = false;
            }
            descBox1.Enabled = true;
        }
        private void form_load()
        {
            //vScrollBar1.Enabled = true;
            //invno.Text = "#INV" + System.DateTime.Now.Date.ToString("dd") + System.DateTime.Now.Date.ToString("MM") + System.DateTime.Now.Date.ToString("yy");
            //invdt.Text = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
            DateTime foo = DateTime.UtcNow;
            long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            invno.Text = "#INV" + unixTime.ToString();
            populate_list();
            disctype.SelectedIndex = 0;
            discval.Controls[0].Visible = false;
            txrt.Controls[0].Visible = false;
            cleanform();
            disable_rows();
            save.Enabled = false;
            print.Enabled = false;
            this.ActiveControl = descBox1;
            searchpanel.Visible = false;
        }
        private void initiate_label()
        {
            label_agri_addr.Text = ConfigurationManager.AppSettings["address"];
            label_agri_url.Text = ConfigurationManager.AppSettings["website"];
            label_agri_email.Text = ConfigurationManager.AppSettings["email"];
            label_ph.Text = ConfigurationManager.AppSettings["phone"];
            label_terms1.Text = ConfigurationManager.AppSettings["terms1"];
            label_terms2.Text = ConfigurationManager.AppSettings["terms2"];
        }
        private void datagridview_style(DataGridView d)
        {
            d.BorderStyle = BorderStyle.None;
            d.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            d.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            d.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            d.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            d.BackgroundColor = Color.White;

            d.EnableHeadersVisualStyles = false;
            d.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            d.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            d.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            d.RowHeadersVisible = false;
        }
        private void add_tag(TextBox[] t)
        {
            int i = 0;
            foreach (var v in t)
            {
                v.Tag = i.ToString();
                i = i + 1;
            }
        }
        private void add_tag(Label[] t)
        {
            int i = 0;
            foreach (var v in t)
            {
                v.Tag = i.ToString();
                i = i + 1;
            }
        }
        private void add_tag(NumericUpDown[] t)
        {
            int i = 0;
            foreach (var v in t)
            {
                v.Tag = i.ToString();
                i = i + 1;
            }
        }
        private void add_context(TextBox[] T)
        {
            foreach(var v in T)
            {
                v.ContextMenuStrip = copypastemenu;
            }
        }
        private void AgricoveBilling_Load(object sender, EventArgs e)
        {
            add_tag(descBox);
            add_tag(qtyBox);
            add_tag(uBox);
            add_tag(tBox);
            form_load();
            initiate_label();

            //Print
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.DefaultPageSettings.Landscape = true;
            /*PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
            PaperSize sizeA4 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A4); // setting paper size to A4 size
            */
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custom", (this.Height + 20), (this.Width + 47));
            //

            datestart.Enabled = false;
            dateend.Enabled = false;
            dtrange.Checked = false;
            add_context(descBox);
            find_gridview.Columns.Add(ButtonColumn);
            datasrc.Items.Add("Invoice");
            datasrc.Items.Add("Items");
            //datasrc.SelectedIndex = 0;
            ButtonColumn.Text = "deledit";
            ButtonColumn.Name = "deledit";
            ButtonColumn.UseColumnTextForButtonValue = true;
            datagridview_style(find_gridview);
        }
        private void populate_list()
        {
            using (var dataContext = new DBConnection())
            {
                var items = dataContext.Items.ToList();
                var desc = items.Select(t => Tuple.Create(t.ItemName)).ToList();
                foreach (var descval in desc)
                {
                    foreach (var dBox in descBox)
                    {
                        dBox.AutoCompleteCustomSource.Add(descval.Item1);
                    }
                }
                var invoice = dataContext.Invoice.ToList();
                var names = invoice.Select(n => Tuple.Create(n.BillToName)).ToList();
                var adds = invoice.Select(a => Tuple.Create(a.BillToAdd)).ToList();
                foreach (var name in names)
                {
                    billname.AutoCompleteCustomSource.Add(name.Item1);
                }
                foreach (var add in adds)
                {
                    billaddr.AutoCompleteCustomSource.Add(add.Item1);
                }
                //dataContext.SaveChanges();                
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
                //dataContext.SaveChanges();
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
                        var item = (from x in items
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
                var invoices = dataContext.Invoice.ToList();
                var invoice = (from x in invoices where x.InvoiceNo == invno.Text select x).FirstOrDefault();
                if(invoice != null)
                {
                    dataContext.Invoice.Remove(invoice);
                }
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
                    Paid = paid.Value,
                    Due = decimal.Parse(balancedue.Text.ToString())
                }); ;
                dataContext.SaveChanges();
            }
        }
        
        private void save_Click(object sender, EventArgs e)
        {
            save_item();
            save_invoice();
        }

        private void recursive_up(int i)
        {
            if (descBox[i + 1].Text.Length > 0)
            {
                descBox[i].Text = descBox[i + 1].Text;
                qtyBox[i].Value = qtyBox[i + 1].Value;
                uBox[i].Value = uBox[i + 1].Value;
                tBox[i].Text = tBox[i + 1].Text;
                descBox[i + 1].Clear();
                qtyBox[i + 1].Value = 0;
                uBox[i + 1].Value = 0;
                tBox[i + 1].Text = "0.00";

                descBox[i].Enabled = true;
                qtyBox[i].Enabled = true;
                uBox[i].Enabled = true;

                descBox[i+1].Enabled = true;
                qtyBox[i+1].Enabled = false;
                uBox[i+1].Enabled = false;

                try
                {
                    descBox[i + 2].Enabled = false;
                }
                catch
                {
                    return;
                }

                recursive_up(i + 1);
            }


        }
        private void descBox_LostFocus(object sender, EventArgs e)
        {
            int i = Int32.Parse((sender as TextBox).Tag.ToString());
            uBox[i].Value = fetch_price(descBox[i].Text);
            qtyBox[i].Value = 1;
            tBox[i].Text = (qtyBox[i].Value * uBox[i].Value).ToString("0.00");
            if (descBox[i].Text.Length > 0)
            {
                qtyBox[i].Enabled = true;
                uBox[i].Enabled = true;
                try
                {
                    descBox[i + 1].Enabled = true;
                }
                catch { }
                qtyBox[i].Focus();
            }
            else
            {
                qtyBox[i].Value = 0;
                uBox[i].Value = 0;
                qtyBox[i].Enabled = false;
                uBox[i].Enabled = false;
                try
                {
                    descBox[i + 1].Enabled = false;
                    recursive_up(i);
                }
                catch { }
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

        private void close_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void minimise_Click_1(object sender, EventArgs e)
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
                print_Click(sender ,e);
            }
            if (e.Control && e.KeyCode == Keys.S)
            {                
                Form dlg1 = new Form();
                dlg1.ShowDialog();
            }
            if (e.Control && e.KeyCode == Keys.N)
            {
                newinv_Click(sender, e);
            }
            if (e.Control && e.KeyCode == Keys.F)
            {
                find_Click(sender, e);
            }
            if (e.Alt && e.KeyCode == Keys.F4)
            {
                Close();
            }
        }

        private void newinv_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            form_load();
            Cursor.Current = Cursors.Default;
        }

        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                keybd_event((byte)VK_TAB, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
                return;
            }
        }
        private void gridview_format(DataGridView d, string s)
        {
            try
            {
                if (s == "invoice")
                {
                    d.Columns[0].HeaderText = "";
                    ButtonColumn.Text = "Edit";
                    d.Columns[1].HeaderText = "Invoice No.";
                    d.Columns[2].HeaderText = "Invoice Date";
                    d.Columns[3].HeaderText = "Due Date";
                    d.Columns[4].HeaderText = "Customer Name";
                    d.Columns[4].Width = 200;
                    d.Columns[5].HeaderText = "Due";
                    d.Columns[5].DefaultCellStyle.Format = "0.00##";
                    d.AllowUserToDeleteRows = false;

                }
                else if (s == "items")
                {
                    d.Columns[0].HeaderText = "";
                    ButtonColumn.Text = "Delete";
                    d.Columns[1].HeaderText = "Item Name";
                    d.Columns[1].Width = 500;
                    d.Columns[2].HeaderText = "Item Price";
                    d.Columns[2].Width = 100;
                    d.AllowUserToDeleteRows = true;
                }
            }
            catch { }
        }
        private void gridview_load(DataGridView d, string s)
        {
            using (var dataContext = new DBConnection())
            {
                if (s == "invoice")
                {
                    var invoice = dataContext.Invoice.ToList();
                    var data = invoice
                                //.Where( x => x.Due > 0 )
                                .Select( x => new { x.InvoiceNo, x.InvoiceDt, x.DueDt, x.BillToName, x.Due }).ToList(); 
                    d.DataSource = data;
                    gridview_format(d, "invoice");
                }
                else if( s == "items")
                {
                    var items = dataContext.Items.ToList();
                    var data = items.Select(x => new { x.ItemName, x.ItemPrice }).ToList();
                    d.DataSource = data;
                    gridview_format(d, "items");
                }
            }
        }
        private void find_Click(object sender, EventArgs e)
        {
            if(searchpanel.Visible)
            {
                searchpanel.Visible = false;
                save.Enabled = true;
                print.Enabled = true;
                search.Text = "&Find";
                newinv.Enabled = true;
                this.AcceptButton = null;
                find_gridview.DataSource = null;
                descBox1.Focus();
            }
            else
            {
                searchpanel.Visible = true;
                save.Enabled = false;
                print.Enabled = false;
                search.Text = "Back";
                newinv.Enabled = false;
                this.AcceptButton = search_btn;
                search_inv_box.Focus();
                if (datasrc.SelectedIndex == 0)
                {
                    gridview_load(find_gridview, "invoice");
                }
                datasrc.SelectedIndex = 0;
            }
        }

        //Print
        private void print_Click(object sender, EventArgs e)
        {
            save_invoice();
            CaptureScreen();
            printDocument1.Print();
        }


        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }

        private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }
        //

        private void Dtrange_CheckedChanged(object sender, EventArgs e)
        {
            if (dtrange.Checked)
            {
                datestart.Enabled = true;
                dateend.Enabled = true;
                search_inv_box.Enabled = false;
                search_inv_box.Text = "";
            }
            else
            {
                datestart.Enabled = false;
                dateend.Enabled = false;
                search_inv_box.Enabled = true;
            }
        }

        private void Datasrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(datasrc.SelectedIndex == 0)
            {
                name_search_label.Text = "Client Name";
                search_name_box.Width = 262;
                invno_search_label.Visible = true;
                search_inv_box.Visible = true;
                dtrange.Visible = true;
                datestart.Visible = true;
                dateend.Visible = true;
                dtrange.Checked = false;
                datestart.Enabled = false;
                dateend.Enabled = false;
                gridview_load(find_gridview, "invoice");
            }
            else
            {
                name_search_label.Text = "Item Name";
                search_name_box.Width = 540;
                invno_search_label.Visible = false;
                search_inv_box.Visible = false;
                dtrange.Visible = false;
                datestart.Visible = false;
                dateend.Visible = false;
                gridview_load(find_gridview, "items");
            }
        }
        private void search_fill(DataGridView d)
        {
            using (var dataContext = new DBConnection())
            {  
                if (datasrc.SelectedIndex == 0)
                {
                    var dts = datestart.Value;
                    var dte = dateend.Value;
                    if (search_inv_box.Text.Length > 0)
                    {
                        var var_invno = search_inv_box.Text.ToString();

                        var invoice = dataContext.Invoice.ToList();
                        var data = invoice.Where(x => x.InvoiceNo == var_invno)
                                        .Select(x => new { x.InvoiceNo, x.InvoiceDt, x.DueDt, x.BillToName, x.Due }).ToList();

                        d.DataSource = data;
                    }
                    else if (search_name_box.Text.Length > 0)
                    {
                        var var_name = search_name_box.Text.ToString();
                        if (dtrange.Checked)
                        {
                            var invoice = dataContext.Invoice.ToList();
                            var data = invoice.Where(x => x.BillToName.ToLower().Contains(var_name.ToLower()))
                                            .Where(x => DateTime.ParseExact(x.InvoiceDt, "dd/MM/yyyy", null) >= dts)
                                            .Where(x => DateTime.ParseExact(x.InvoiceDt, "dd/MM/yyyy", null) <= dte)
                                            .Select(x => new { x.InvoiceNo, x.InvoiceDt, x.DueDt, x.BillToName, x.Due }).ToList();
                            d.DataSource = data;
                        }
                        else
                        {
                            var invoice = dataContext.Invoice.ToList();
                            var data = invoice.Where(x => x.BillToName.ToLower().Contains(var_name.ToLower()))
                                            .Select(x => new { x.InvoiceNo, x.InvoiceDt, x.DueDt, x.BillToName, x.Due }).ToList();
                            d.DataSource = data;
                        }
                    }
                    else
                    {
                        if (dtrange.Checked)
                        {
                            var invoice = dataContext.Invoice.ToList();
                            var data = invoice
                                        .Where(x => DateTime.ParseExact(x.InvoiceDt, "dd/MM/yyyy", null) >= dts)
                                        .Where(x => DateTime.ParseExact(x.InvoiceDt, "dd/MM/yyyy", null) <= dte)
                                        .Select(x => new { x.InvoiceNo, x.InvoiceDt, x.DueDt, x.BillToName, x.Due }).ToList();
                            d.DataSource = data;
                        }
                        else
                        {
                            var invoice = dataContext.Invoice.ToList();
                            var data = invoice.Select(x => new { x.InvoiceNo, x.InvoiceDt, x.DueDt, x.BillToName, x.Due }).ToList();
                            d.DataSource = data;
                        }
                    }
                    gridview_format(d, "invoice");
                }
                else
                {
                    var items = dataContext.Items.ToList();
                    var data = items.Where(x => x.ItemName.ToLower().Contains(search_name_box.Text.ToLower()))
                                .Select(x => new { x.ItemName, x.ItemPrice }).ToList();
                    d.DataSource = data;
                    gridview_format(d, "items");
                }
            }
        }
        private void Search_btn_Click(object sender, EventArgs e)
        {
            search_fill(find_gridview);
        }

        private void Search_name_box_Enter(object sender, EventArgs e)
        {
            search_inv_box.Text = "";
        }

        private void Search_inv_box_Enter(object sender, EventArgs e)
        {
            search_name_box.Text = "";
        }
        private void load_data(Invoice inv)
        {
            invno.Text = inv.InvoiceNo;
            invdt.Value = DateTime.ParseExact(inv.InvoiceDt, "dd/MM/yyyy", null);
            ddt.Value = DateTime.ParseExact(inv.DueDt, "dd/MM/yyyy", null); 
            billname.Text = inv.BillToName;
            billaddr.Text = inv.BillToAdd;
            descBox1.Text = inv.ItemID1Name;
            uBox1.Value = inv.ItemID1Price;
            qtyBox1.Value = inv.ItemID1Qty;
            descBox2.Text = inv.ItemID2Name;
            uBox2.Value = inv.ItemID2Price;
            qtyBox2.Value = inv.ItemID2Qty;
            descBox3.Text = inv.ItemID3Name;
            uBox3.Value = inv.ItemID3Price;
            qtyBox3.Value = inv.ItemID3Qty;
            descBox4.Text = inv.ItemID4Name;
            uBox4.Value = inv.ItemID4Price;
            qtyBox4.Value = inv.ItemID4Qty;
            descBox5.Text = inv.ItemID5Name;
            uBox5.Value = inv.ItemID5Price;
            qtyBox5.Value = inv.ItemID5Qty;
            descBox6.Text = inv.ItemID6Name;
            uBox6.Value = inv.ItemID6Price;
            qtyBox6.Value = inv.ItemID6Qty;
            descBox7.Text = inv.ItemID7Name;
            uBox7.Value = inv.ItemID7Price;
            qtyBox7.Value = inv.ItemID7Qty;
            descBox8.Text = inv.ItemID8Name;
            uBox8.Value = inv.ItemID8Price;
            qtyBox8.Value = inv.ItemID8Qty;
            descBox9.Text = inv.ItemID9Name;
            uBox9.Value = inv.ItemID9Price;
            qtyBox9.Value = inv.ItemID9Qty;
            descBox10.Text = inv.ItemID10Name;
            uBox10.Value = inv.ItemID10Price;
            qtyBox10.Value = inv.ItemID10Qty;
            descBox11.Text = inv.ItemID11Name;
            uBox11.Value = inv.ItemID11Price;
            qtyBox11.Value = inv.ItemID11Qty;
                    
            disctype.SelectedIndex = inv.DiscountType;
            discval.Value = inv.DiscountValue;
            txrt.Value = inv.TaxRate;
            paid.Value = inv.Paid;
        }
        private void find_gridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                if (datasrc.SelectedIndex == 0)
                {
                    using (var dataContext = new DBConnection())
                    {
                        var invoice = dataContext.Invoice.ToList();
                        var inv = (from x in invoice
                                    where x.InvoiceNo == find_gridview.Rows[e.RowIndex].Cells[1].Value.ToString()
                                    select x).FirstOrDefault();
                        load_data(inv);
                        find_Click(sender, e);
                    }
                }
                else
                {
                    using (var dataContext = new DBConnection())
                    {
                        var items = dataContext.Items.ToList();
                        var item = (from x in items
                                    where x.ItemName == find_gridview.Rows[e.RowIndex].Cells[1].Value.ToString()
                                    where x.ItemPrice == find_gridview.Rows[e.RowIndex].Cells[2].Value.ToString()
                                    select x).FirstOrDefault();
                        dataContext.Items.Remove(item);
                        dataContext.SaveChanges();
                        gridview_load(find_gridview, "items");
                    }
                }
            }
        }

        private void refresh_search_Click(object sender, EventArgs e)
        {
            Datasrc_SelectedIndexChanged(sender, e);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (sender as ToolStripItem);
            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    Clipboard.SetText(owner.SourceControl.Text);
                }
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (sender as ToolStripItem);
            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    TextBox sourceControl = owner.SourceControl as TextBox;
                    sourceControl.SelectedText = Clipboard.GetText();
                }
            }
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (sender as ToolStripItem);
            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    DataGridView dg = owner.SourceControl as DataGridView;
                    if (dg.CurrentCell.RowIndex != 0)
                    {
                        Clipboard.SetText(dg.CurrentCell.Value.ToString());
                    }
                }
            }
        }

        private void find_gridview_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                find_gridview.CurrentCell = find_gridview.Rows[0].Cells[1];
            }
            catch { }
        }

        private void searchbar_Enter(object sender, EventArgs e)
        {
            //MessageBox.Show("0");
            this.AcceptButton = search_btn;
        }

        private void searchbar_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }
    }
}
