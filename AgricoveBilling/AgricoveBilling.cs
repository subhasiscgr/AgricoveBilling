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
using Be.Timvw.Framework.ComponentModel;
using System.Globalization;

namespace AgricoveBilling
{
    public partial class AgricoveBilling : Form
    {
        TextBox[] descBox;
        NumericUpDown[] qtyBox;
        NumericUpDown[] uBox;
        Label[] tBox;

        bool paid_token = false;
        decimal paid_max = 0;
        ulong edit_check = 0;

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

            //this.AutoScroll = true;
            this.AutoSize = false;
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
        //private PrintDocument printDocument1 = new PrintDocument();
        //Bitmap memoryImage;
        //

        Color[,] bgColors = new Color[12, 4] {
            { SystemColors.ControlDarkDark , SystemColors.ControlDarkDark, SystemColors.ControlDarkDark, SystemColors.ControlDarkDark },
            { SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ControlDarkDark },
            { SystemColors.ButtonFace , SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ControlDarkDark },
            { SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ControlDarkDark },
            { SystemColors.ButtonFace , SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ControlDarkDark },
            { SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ControlDarkDark },
            { SystemColors.ButtonFace , SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ControlDarkDark },
            { SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ControlDarkDark },
            { SystemColors.ButtonFace , SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ControlDarkDark },
            { SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ControlDarkDark },
            { SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ControlDarkDark },
            { SystemColors.ButtonFace , SystemColors.ButtonFace, SystemColors.ButtonFace, SystemColors.ControlDarkDark }
        };

        private void invoice_table_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            using (var b = new SolidBrush(bgColors[e.Row, e.Column]))
            {
                e.Graphics.FillRectangle(b, e.CellBounds);
            }
        }

        private decimal decimal_parse(string s)
        {
            decimal value = 0;
            var allowedStyles = (NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
            if(s.Length == 0)
            {
                MessageBox.Show("Empty String");
                return 0;
            }
            else if (Decimal.TryParse(s, allowedStyles, CultureInfo.GetCultureInfo("EN-us"), out value))
            {
                return value;
            }
            else
            {
                MessageBox.Show("Not a number");
                return 0;
            }
        }
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
            paid_token = false;
            edit_check = 0;
            search.Enabled = true;

            Action<Control.ControlCollection> func = null;
            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                    {
                        (control as TextBox).Clear();
                    }
                    else if (control is NumericUpDown)
                    {
                        (control as NumericUpDown).Value = 0;
                    }
                    else if (control is DateTimePicker)
                    {
                        (control as DateTimePicker).Value = DateTime.Now;
                    }
                    else if(control is CheckBox)
                    {
                        (control as CheckBox).Checked = false;
                    }
                    else if(control is ComboBox)
                    {
                        if ((control as ComboBox).Items.Count > 0)
                        {
                            (control as ComboBox).SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        func(control.Controls);
                    }
            };
   
            func(Controls);

            //Assign default values of fields after this line
            search.Text = "&Find";
        }

        private void disable_rows()
        {
            foreach (var desc in descBox)
            {
                desc.Enabled = false;
                var v = Convert.ToInt32(desc.Tag.ToString());
                bgColors[v + 1, 0] = SystemColors.ButtonFace;
            }
            foreach (var q in qtyBox)
            {
                q.Enabled = false;
                var v = Convert.ToInt32(q.Tag.ToString());
                bgColors[v + 1, 1] = SystemColors.ButtonFace;
            }
            foreach (var u in uBox)
            {
                u.Enabled = false;
                var v = Convert.ToInt32(u.Tag.ToString());
                bgColors[v + 1, 2] = SystemColors.ButtonFace;
            }
            descBox1.Enabled = true;
            bgColors[1, 0] = SystemColors.ControlLightLight;
            invoice_table.Refresh();
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
            d.DefaultCellStyle.SelectionBackColor = Color.CadetBlue;
            d.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            d.BackgroundColor = Color.White;

            d.EnableHeadersVisualStyles = false;
            d.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            d.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            d.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            d.RowHeadersVisible = false;
            d.RowTemplate.Height = 35;
            d.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //d.AllowUserToResizeRows = false;
            d.ColumnHeadersHeight = 35;
            d.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            d.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 8F, FontStyle.Bold);
            d.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 8F, FontStyle.Bold);
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
            foreach (var v in T)
            {
                v.ContextMenuStrip = copypastemenu;
            }
        }

        //Print
        public static PaperSize GetPaperSize(string Name)
        {
            PaperSize size1 = null;
            Name = Name.ToUpper();
            PrinterSettings settings = new PrinterSettings();
            foreach (PaperSize size in settings.PaperSizes)
                if (size.Kind.ToString().ToUpper() == Name)
                {
                    size1 = size;
                    break;
                }
            return size1;
        }
        //
        private void add_tabindex()
        {
            int index = 1;
            foreach(var desc in descBox)
            {
                desc.TabIndex = index;
                index = index + 3;
            }
            index = 2;
            foreach (var q in qtyBox)
            {
                q.TabIndex = index;
                index = index + 3;
            }
            index = 3;
            foreach (var u in uBox)
            {
                u.TabIndex = index;
                index = index + 3;
            }
            discval.TabIndex = index;
            index++;
            disctype.TabIndex = index;
            index++;
            txrt.TabIndex = index;
            index++;
            paid.TabIndex = index;
            index++;
            billname.TabIndex = index;
            index++;
            billaddr.TabIndex = index;
            index++;
            invdt.TabIndex = index;
            index++;
            ddt.TabIndex = index;
        }

        private void AgricoveBilling_Load(object sender, EventArgs e)
        {
            add_tabindex();
            add_tag(descBox);
            add_tag(qtyBox);
            add_tag(uBox);
            add_tag(tBox);
            form_load();
            initiate_label();

            //Print
            //printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            //printDocument1.DefaultPageSettings.Landscape = true;
            //PrinterSettings ps = new PrinterSettings();
            //printDocument1.PrinterSettings = ps;

            //printDocument1.DefaultPageSettings.PaperSize = GetPaperSize("A4");
            //

            //MessageBoxManager.OK = "Alright";
            MessageBoxManager.Yes = "Print";
            //MessageBoxManager.No = "Nope";
            MessageBoxManager.Register();

            datestart.Enabled = false;
            dateend.Enabled = false;
            dtrange.Checked = false;
            search_duedt.Checked = false;
            add_context(descBox);
            find_gridview.Columns.Add(ButtonColumn);
            datasrc.Items.Add("Invoice");
            datasrc.Items.Add("Items");
            //datasrc.SelectedIndex = 0;

            ButtonColumn.Text = "deledit";
            ButtonColumn.Name = "deledit";
            ButtonColumn.UseColumnTextForButtonValue = true;
            ButtonColumn.FlatStyle = FlatStyle.Standard;
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
        private decimal? fetch_price(string s)
        {
            using (var dataContext = new DBConnection())
            {
                var items = dataContext.Items.ToList();
                var p = from x in items where x.ItemName == s select (decimal?) x.ItemPrice;
                decimal? result = p.FirstOrDefault();
                return result;
                //dataContext.SaveChanges();
            }         
        }

        private void save_item()
        {
            using (var dataContext = new DBConnection())
            {
                var items = dataContext.Items.ToList();
                int i = 0;
                foreach (var v in descBox)
                {
                    if (v.Text.Length > 0)
                    {
                        var item = (from x in items
                                    where x.ItemName == v.Text
                                    select x).FirstOrDefault();
                        if (item != null)
                        {
                            item.ItemPrice = uBox[i].Value;
                        }
                        else
                        {
                            Items item2 = new Items();
                            item2.ItemName = v.Text.ToString();
                            item2.ItemPrice = uBox[i].Value;
                            dataContext.Items.Add(item2);
                        }
                    }
                    i = i + 1;
                }
                try
                {
                    dataContext.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("       Database write permission not available.     ");
                }
            }
        }
        private void save_invoice()
        {
            using (var dataContext = new DBConnection())
            {
                var invoices = dataContext.Invoice.ToList();
                var invoice = (from x in invoices where x.InvoiceNo == invno.Text select x).FirstOrDefault();
                if (invoice != null)
                {
                    dataContext.Invoice.Remove(invoice);
                }
                dataContext.Invoice.Add(new Invoice()
                {
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
                    //Due = decimal_parse(balancedue.Text.ToString())
                });;
                try
                {
                    dataContext.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("       Database write permission not available.     ");
                }
            }
        }

        private void print_dialogue()
        {
            DialogResult dlgResult = MessageBox.Show("invoice has been saved successfully", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if(dlgResult == DialogResult.Yes)
            {
                load_crystalreports();
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            save_item();
            save_invoice();
            print_dialogue();
            newinv_Click(sender, e);
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
                tBox[i + 1].Text = "#,##0.00";

                descBox[i].Enabled = true;
                bgColors[i + 1, 0] = SystemColors.ControlLightLight;
                qtyBox[i].Enabled = true;
                bgColors[i + 1, 1] = SystemColors.ControlLightLight;
                uBox[i].Enabled = true;
                bgColors[i + 1, 2] = SystemColors.ControlLightLight;

                descBox[i + 1].Enabled = true;
                bgColors[i + 2, 0] = SystemColors.ControlLightLight;
                qtyBox[i + 1].Enabled = false;
                bgColors[i + 2, 1] = SystemColors.ButtonFace;
                uBox[i + 1].Enabled = false;
                bgColors[i + 2, 2] = SystemColors.ButtonFace;

                try
                {
                    descBox[i + 2].Enabled = false;
                    bgColors[i + 3, 0] = SystemColors.ButtonFace;
                }
                catch
                {
                    return;
                }
                invoice_table.Refresh();
                recursive_up(i + 1);
            }
        }

        private void descBox_Enter(object sender, EventArgs e)
        {
            int i = Int32.Parse((sender as TextBox).Tag.ToString());
            qtyBox[i].Enabled = true;
            bgColors[i + 1, 1] = SystemColors.ControlLightLight;
            uBox[i].Enabled = true;
            bgColors[i + 1, 2] = SystemColors.ControlLightLight;
            invoice_table.Refresh();
            descBox[i].SelectAll();
        }

        private void descBox_LostFocus(object sender, EventArgs e)
        {
            int i = Int32.Parse((sender as TextBox).Tag.ToString());
            if (descBox[i].Text.Length > 0)
            {
                if (fetch_price(descBox[i].Text) != null)
                {
                    uBox[i].Value = (decimal) fetch_price(descBox[i].Text);
                }
                if (qtyBox[i].Value == 0)
                {
                    qtyBox[i].Value = 1;
                }
                tBox[i].Text = (qtyBox[i].Value * uBox[i].Value).ToString("#,##0.00");
                try
                {
                    descBox[i + 1].Enabled = true;
                    bgColors[i + 2, 0] = SystemColors.ControlLightLight;
                }
                catch { }
            }
            else
            {
                qtyBox[i].Value = 0;
                uBox[i].Value = 0;
                qtyBox[i].Enabled = false;
                bgColors[i + 1, 1] = SystemColors.ButtonFace;
                uBox[i].Enabled = false;
                bgColors[i + 1, 2] = SystemColors.ButtonFace;
                try
                {
                    descBox[i + 1].Enabled = false;
                    bgColors[i + 2, 0] = SystemColors.ButtonFace;
                    recursive_up(i);
                }
                catch { }
            }
            invoice_table.Refresh();
        }

        private void qtyBox_ValueChanged(object sender, EventArgs e)
        {
            int i = Int32.Parse((sender as NumericUpDown).Tag.ToString());
            tBox[i].Text = (qtyBox[i].Value * uBox[i].Value).ToString("#,##0.00");
            numbox_TextChanged(sender, (ulong)(i + 13));
        }

        private void uBox_ValueChanged(object sender, EventArgs e)
        {
            int i = Int32.Parse((sender as NumericUpDown).Tag.ToString());
            tBox[i].Text = (qtyBox[i].Value * uBox[i].Value).ToString("#,##0.00");
            numbox_TextChanged(sender, (ulong)(i + 24));
        }

        private void tBox_TextChanged(object sender, EventArgs e)
        {
            decimal sum = 0;
            foreach (var v in tBox)
            {
                sum = sum + decimal_parse(v.Text.ToString());
            }
            subtotal.Text = sum.ToString("#,##0.00");
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
            if (disctype.SelectedIndex == 0)
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

        private void updown_leave(object sender, EventArgs e)
        {
            if (sender.GetType().ToString() == "System.Windows.Forms.NumericUpDown")                    //Necessary to avoid collision with disctype combobox
            {
                NumericUpDown obj = new NumericUpDown();
                obj = (NumericUpDown)sender;
                if (obj.Text == "")                         //Don't allow blank in numericupdown
                {
                    obj.Value = 0;
                    obj.Text = obj.Value.ToString();
                }
            }
        }
        private void disc_calc(object sender, EventArgs e)
        {
            decimal value = 0;
            value = decimal_parse(subtotal.Text.ToString());

            if (disctype.SelectedIndex == 0)
            {
                subttllssdisc.Text = (value - discval.Value).ToString("#,##0.00");
            }
            else if (disctype.SelectedIndex == 1)
            {
                subttllssdisc.Text = (value * (1 - discval.Value / 100)).ToString("#,##0.00");
            }

            numbox_TextChanged(sender, 36);                 //send for edit log
        }

        private void txrt_ValueChanged(object sender, EventArgs e)
        {
            decimal value = 0;
            value = decimal_parse(subttllssdisc.Text.ToString());

            ttltax.Text = (value * txrt.Value / 100).ToString("#,##0.00");
            grssttl.Text = (value * (1 + txrt.Value / 100)).ToString("#,##0.00");

            numbox_TextChanged(sender, 37);                 //Send for edit log
        }

        private void grssttl_TextChanged(object sender, EventArgs e)
        {
            decimal value = 0;
            value = decimal_parse(grssttl.Text.ToString());

            if (!paid_token)            //value loads automatically only if user hasn;t set it manually
            {
                paid.Value = value;
            }
            else if(paid_max >= value)      //value locked to the max that user had entered
            {
                paid.Value = value;
            }
            else if(paid_max < value)       //value loads automatically up to the max that user had entered
            {
                paid.Value = paid_max;
            }
            /*if (paid.Value > value)
            {
                paid.Value = value;
            }*/

            if (value == 0)
            {
                save.Enabled = false;
                //print.Enabled = false;
            }
            else
            {
                save.Enabled = true;
                //print.Enabled = true;
            }

            balancedue.Text = (value - paid.Value).ToString("#,##0.00");
        }

        private void paid_ValueChanged()
        {
            decimal value = 0;
            value = decimal_parse(grssttl.Text.ToString());

            if (paid.Value > value)
            {
                //MessageBox.Show("         You can't pay more than what's due      ");
                paid.Value = value;
            }
            balancedue.Text = (value - paid.Value).ToString("#,##0.00");
        }

        private void paid_ValueChanged(object sender, KeyEventArgs e)
        {
            paid_ValueChanged();
            paid_token = true;                      //paid_token sets only when value is changed manually
            paid_max = paid.Value;                  //save the max value user has typed
            numbox_TextChanged(sender, 38);           //send for edit log
        }

        private void paid_ValueChanged(object sender, EventArgs e)
        {
            paid_ValueChanged();
            numbox_TextChanged(sender, 38);       //send for edit log
        }

        //Set keyboard shortcuts
        private void AgricoveBilling_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)           //Ctrl+P for print
            {
                print_Click(sender, e);
            }   
            if (e.Control && e.KeyCode == Keys.S)          //Ctrl+S for Save
            {
                Form dlg1 = new Form();
                dlg1.ShowDialog();
            }
            if (e.Control && e.KeyCode == Keys.N)         //Ctrl+N for New Invoice (same as refresh)
            {
                newinv_Click(sender, e);
            }
            if (e.KeyCode == Keys.F5)                   //F5 for refresh
            {
                newinv_Click(sender, e);
            }
            if (e.Control && e.KeyCode == Keys.F)       //Ctrl+F for search
            {
                find_Click(sender, e);
            }
            if (e.Alt && e.KeyCode == Keys.F4)          //Alt+F4 for close without saving
            {
                Close();
            }
        }

        //refresh invoice
        private void newinv_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;        //set cursor to busy until form loads
            form_load();
            Cursor.Current = Cursors.Default;
        }

        //Disable mouse drag on table
        private void invoice_table_MouseDown(object sender, MouseEventArgs e)
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
                    d.Columns[0].Width = 60;
                    d.Columns[1].Visible = true;
                    d.Columns[1].HeaderText = "Invoice No.";
                    d.Columns[1].Width = 120;
                    d.Columns[2].HeaderText = "Invoice Date";
                    d.Columns[2].Width = 115;
                    d.Columns[3].HeaderText = "Due Date";
                    d.Columns[4].HeaderText = "Customer Name";
                    d.Columns[4].Width = 200;
                    d.Columns[5].HeaderText = "Due";
                    d.Columns[5].DefaultCellStyle.Format = "0.00##";
                    d.AllowUserToDeleteRows = false;
                    d.ReadOnly = true;
                    d.Sort(d.Columns["Due"], ListSortDirection.Descending);         //Order by due

                    foreach (DataGridViewRow v in d.Rows)           // Set due rows red and no Due rows green
                    {
                        if (decimal_parse(v.Cells[5].Value.ToString()) > 0)
                        {
                            v.Cells[1].Style.BackColor = Color.FromArgb(255, 204, 204);
                            v.Cells[2].Style.BackColor = Color.FromArgb(255, 204, 204);
                            v.Cells[3].Style.BackColor = Color.FromArgb(255, 204, 204);
                            v.Cells[4].Style.BackColor = Color.FromArgb(255, 204, 204);
                            v.Cells[5].Style.BackColor = Color.FromArgb(255, 204, 204);
                        }
                        else
                        {
                            v.Cells[1].Style.BackColor = Color.FromArgb(153, 255, 204);
                            v.Cells[2].Style.BackColor = Color.FromArgb(153, 255, 204);
                            v.Cells[3].Style.BackColor = Color.FromArgb(153, 255, 204);
                            v.Cells[4].Style.BackColor = Color.FromArgb(153, 255, 204);
                            v.Cells[5].Style.BackColor = Color.FromArgb(153, 255, 204);
                        }
                    }
                }
                else if (s == "items")
                {
                    d.Columns[0].HeaderText = "";
                    ButtonColumn.Text = "Delete";
                    d.Columns[0].Width = 60;
                    d.Columns[1].Visible = false;                           //Hide the ItemID column
                    d.Columns[2].HeaderText = "Item Name";
                    d.Columns[2].Width = 500;
                    d.Columns[3].HeaderText = "Item Price";
                    d.Columns[3].Width = 135;
                    d.ReadOnly = false;
                    d.AllowUserToDeleteRows = true;
                }
            }
            catch { }
        }

        //calculate due in runtime
        private decimal calc_due(decimal paid, decimal price1, int qty1, decimal price2, int qty2, decimal price3, int qty3, decimal price4, int qty4, decimal price5, int qty5, decimal price6, int qty6, decimal price7, int qty7, decimal price8, int qty8, decimal price9, int qty9, decimal price10, int qty10, decimal price11, int qty11)
        {
            return ((price1 * qty1) + (price2 * qty2) + (price3 * qty3) + (price4 * qty4) + (price5 * qty5) + (price6 * qty6) + (price7 * qty7) + (price8 * qty8) + (price9 * qty9) + (price10 * qty10) + (price11 * qty11) - paid);
        }

        //Default gridview load before search
        private void gridview_load(DataGridView d, string s)
        {
            using (var dataContext = new DBConnection())
            {
                if (s == "invoice")
                {
                    var invoice = dataContext.Invoice.ToList();
                    
                    var data = invoice
                                //.Where( x => x.Due > 0 )
                                .Select(x => new
                                {
                                    x.InvoiceNo,
                                    x.InvoiceDt,
                                    x.DueDt,
                                    x.BillToName,
                                    due = calc_due(x.Paid, x.ItemID1Price, x.ItemID1Qty, x.ItemID2Price, x.ItemID2Qty, x.ItemID3Price, x.ItemID3Qty, x.ItemID4Price, x.ItemID4Qty, x.ItemID5Price, x.ItemID5Qty, x.ItemID6Price, x.ItemID6Qty, x.ItemID7Price, x.ItemID7Qty, x.ItemID8Price, x.ItemID8Qty, x.ItemID9Price, x.ItemID9Qty, x.ItemID10Price, x.ItemID10Qty, x.ItemID11Price, x.ItemID11Qty)
                                }).ToList();

                    //Move everything to a sortable list for easy gridview header click
                    List<invoice_grid_data> datalist = data.Select(p => new invoice_grid_data() { InvoiceNo = p.InvoiceNo, InvoiceDt = p.InvoiceDt, BillToName = p.BillToName, DueDt = p.DueDt, due =p.due }).ToList();                   
                    SortableBindingList <invoice_grid_data> sorteddata = new SortableBindingList<invoice_grid_data>(datalist);
                    d.DataSource = sorteddata;
                    gridview_format(d, "invoice");
                }
                else if (s == "items")
                {
                    var items = dataContext.Items.ToList();
                    var data = items.Select(x => new { x.ItemID, x.ItemName, x.ItemPrice }).ToList();

                    //Move everything to a sortable list for easy gridview header click
                    List<items_grid_data> datalist = data.Select(p => new items_grid_data() { ItemID = p.ItemID, ItemName = p.ItemName, ItemPrice = p.ItemPrice }).ToList();
                    SortableBindingList<items_grid_data> sorteddata = new SortableBindingList<items_grid_data>(datalist);
                    d.DataSource = sorteddata;
                    gridview_format(d, "items");
                }
            }
        }

        //Toggle search UI
        private void find_Click(object sender, EventArgs e)
        {
            if (searchpanel.Visible)
            {
                searchpanel.Visible = false;
                if (balancedue.Text.Length > 0)
                {
                    save.Enabled = true;
                    print.Enabled = true;
                }
                if (edit_check == 1)
                {
                    search.Text = "Back to Results";
                }
                else
                {
                    search.Text = "&Find";
                }
                newinv.Enabled = true;
                this.AcceptButton = null;
                find_gridview.DataSource = null;
                descBox1.Focus();                           //return default focus
            }
            else
            {
                searchpanel.Visible = true;
                save.Enabled = false;
                print.Enabled = false;
                search.Text = "Back to Invoice";
                newinv.Enabled = false;
                this.AcceptButton = search_btn;
                search_inv_box.Focus();

                if(datasrc.SelectedItem == null)                    // if loading for teh first time
                {
                    datasrc.SelectedIndex = 0;                     //Necessary to ensure data loads for the first time                    
                }
                search_fill(find_gridview);                     //load last search                    
            }
        }

        //Print
        private void load_crystalreports()
        {
            reporter r = new reporter(
                invno.Text,
                invdt.Text,
                ddt.Text,
                billname.Text,
                billaddr.Text,
                descBox1.Text,
                uBox1.Value,
                qtyBox1.Value,
                decimal_parse(tBox1.Text),
                descBox2.Text,
                uBox2.Value,
                qtyBox2.Value,
                decimal_parse(tBox2.Text),
                descBox3.Text,
                uBox3.Value,
                qtyBox3.Value,
                decimal_parse(tBox3.Text),
                descBox4.Text,
                uBox4.Value,
                qtyBox4.Value,
                decimal_parse(tBox4.Text),
                descBox5.Text,
                uBox5.Value,
                qtyBox5.Value,
                decimal_parse(tBox5.Text),
                descBox6.Text,
                uBox6.Value,
                qtyBox6.Value,
                decimal_parse(tBox6.Text),
                descBox7.Text,
                uBox7.Value,
                qtyBox7.Value,
                decimal_parse(tBox7.Text),
                descBox8.Text,
                uBox8.Value,
                qtyBox8.Value,
                decimal_parse(tBox8.Text),
                descBox9.Text,
                uBox9.Value,
                qtyBox9.Value,
                decimal_parse(tBox9.Text),
                descBox10.Text,
                uBox10.Value,
                qtyBox10.Value,
                decimal_parse(tBox10.Text),
                descBox11.Text,
                uBox11.Value,
                qtyBox11.Value,
                decimal_parse(tBox11.Text),
                decimal_parse(subtotal.Text),
                disctype.Text,
                discval.Value,
                decimal_parse(subttllssdisc.Text),
                txrt.Value,
                decimal_parse(ttltax.Text),
                decimal_parse(grssttl.Text),
                paid.Value,
                decimal_parse(balancedue.Text)
                );

            /*var list = new List<reporter>();
            list.Add( new reporter {
                InvoiceNo = invno.Text,
                InvoiceDt = invdt.Text,
                DueDt = ddt.Text,
                BillToName = billname.Text,
                BillToAdd = billaddr.Text,
                ItemID1Name = descBox1.Text,
                ItemID1Price = uBox1.Value,
                ItemID1Qty = qtyBox1.Value,
                ItemID1Total = decimal_parse(tBox1.Text),
                ItemID2Name = descBox2.Text,
                ItemID2Price = uBox2.Value,
                ItemID2Qty = qtyBox2.Value,
                ItemID2Total = decimal_parse(tBox2.Text),
                ItemID3Name = descBox3.Text,
                ItemID3Price = uBox3.Value,
                ItemID3Qty = qtyBox3.Value,
                ItemID3Total = decimal_parse(tBox3.Text),
                ItemID4Name = descBox4.Text,
                ItemID4Price = uBox4.Value,
                ItemID4Qty = qtyBox4.Value,
                ItemID4Total = decimal_parse(tBox4.Text),
                ItemID5Name = descBox5.Text,
                ItemID5Price = uBox5.Value,
                ItemID5Qty = qtyBox5.Value,
                ItemID5Total = decimal_parse(tBox5.Text),
                ItemID6Name = descBox6.Text,
                ItemID6Price = uBox6.Value,
                ItemID6Qty = qtyBox6.Value,
                ItemID6Total = decimal_parse(tBox6.Text),
                ItemID7Name = descBox7.Text,
                ItemID7Price = uBox7.Value,
                ItemID7Qty = qtyBox7.Value,
                ItemID7Total = decimal_parse(tBox7.Text),
                ItemID8Name = descBox8.Text,
                ItemID8Price = uBox8.Value,
                ItemID8Qty = qtyBox8.Value,
                ItemID8Total = decimal_parse(tBox8.Text),
                ItemID9Name = descBox9.Text,
                ItemID9Price = uBox9.Value,
                ItemID9Qty = qtyBox9.Value,
                ItemID9Total = decimal_parse(tBox9.Text),
                ItemID10Name = descBox10.Text,
                ItemID10Price = uBox10.Value,
                ItemID10Qty = qtyBox10.Value,
                ItemID10Total = decimal_parse(tBox10.Text),
                ItemID11Name = descBox11.Text,
                ItemID11Price = uBox11.Value,
                ItemID11Qty = qtyBox11.Value,
                ItemID11Total = decimal_parse(tBox8.Text),

                Subtotal = decimal_parse(subtotal.Text),
                DiscountType = disctype.Text,
                DiscountValue = discval.Value,
                SubtotalLessDiscount = decimal_parse(subttllssdisc.Text),
                TaxRate = txrt.Value,
                Totaltax = decimal_parse(ttltax.Text),
                GrossTotal = decimal_parse(grssttl.Text),
                Paid = paid.Value,
                Due = decimal_parse(balancedue.Text)
                });
                */

            Printpreview pv = new Printpreview(r);
            pv.ShowDialog();                                        //ShowDialog to make sure user can't interact with parent window
        }
        private void print_Click(object sender, EventArgs e)        //deprecated
        {
            save_item();
            save_invoice();
            load_crystalreports();
            newinv_Click(sender, e);

            //CaptureScreen();
            //this.Enabled = false;
            //printDocument1.Print();
            /*PrintDialog pdi = new PrintDialog();
            pdi.Document = printDocument1;
            if (pdi.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            else
            {
                MessageBox.Show("                Print Cancelled         ");
            }*/
        }


        private void CaptureScreen()                //deprecated
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            //memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            //Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            //memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }

        private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)           //deprecated
        {
            //e.Graphics.DrawImage(memoryImage, 0, 0);
        }
        //

        private void Dtrange_CheckedChanged(object sender, EventArgs e)
        {
            //toggle
            if(search_duedt.Checked && dtrange.Checked)
            {
                search_duedt.Checked = false;
            }

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

        private void search_duedt_CheckedChanged(object sender, EventArgs e)
        {
            //toggle
            if(search_duedt.Checked && dtrange.Checked)
            {
                dtrange.Checked = false;
            }

            if (search_duedt.Checked)
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

        //switch between invoice and items
        private void Datasrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (datasrc.SelectedIndex == 0)
            {
                name_search_label.Text = "Client Name";
                search_name_box.Width = 262;
                invno_search_label.Visible = true;
                search_inv_box.Visible = true;
                dtrange.Visible = true;
                search_duedt.Visible = true;
                datestart.Visible = true;
                dateend.Visible = true;
                dtrange.Checked = false;
                search_duedt.Checked = false;
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
                search_duedt.Visible = false;
                datestart.Visible = false;
                dateend.Visible = false;
                gridview_load(find_gridview, "items");
            }
        }

        //load search result in gridview
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
                                        .Select(x => new
                                        {
                                            x.InvoiceNo,
                                            x.InvoiceDt,
                                            x.DueDt,
                                            x.BillToName,
                                            due = calc_due(x.Paid, x.ItemID1Price, x.ItemID1Qty, x.ItemID2Price, x.ItemID2Qty, x.ItemID3Price, x.ItemID3Qty, x.ItemID4Price, x.ItemID4Qty, x.ItemID5Price, x.ItemID5Qty, x.ItemID6Price, x.ItemID6Qty, x.ItemID7Price, x.ItemID7Qty, x.ItemID8Price, x.ItemID8Qty, x.ItemID9Price, x.ItemID9Qty, x.ItemID10Price, x.ItemID10Qty, x.ItemID11Price, x.ItemID11Qty)
                                        }).ToList();

                        //Move everything to a sortable list for enabling gridview header click
                        List<invoice_grid_data> datalist = data.Select(p => new invoice_grid_data() { InvoiceNo = p.InvoiceNo, InvoiceDt = p.InvoiceDt, BillToName = p.BillToName, DueDt = p.DueDt, due = p.due }).ToList();
                        SortableBindingList<invoice_grid_data> sorteddata = new SortableBindingList<invoice_grid_data>(datalist);
                        d.DataSource = sorteddata;
                    }
                    else if (search_name_box.Text.Length > 0)
                    {
                        var var_name = search_name_box.Text.ToString();
                        if (dtrange.Checked)
                        {
                            var invoice = dataContext.Invoice.ToList();
                            var data = invoice.Where(x => x.BillToName.ToLower().Contains(var_name.ToLower()))                                  //moving everything to lowercase for case insensitive search
                                            .Where(x => DateTime.ParseExact(x.InvoiceDt, "dd/MM/yyyy", null) >= dts.AddDays(-1))                //For some reason subtracting 1 is required to include start date in the search
                                            .Where(x => DateTime.ParseExact(x.InvoiceDt, "dd/MM/yyyy", null) <= dte)
                                            .Select(x => new
                                            {
                                                x.InvoiceNo,
                                                x.InvoiceDt,
                                                x.DueDt,
                                                x.BillToName,
                                                due = calc_due(x.Paid, x.ItemID1Price, x.ItemID1Qty, x.ItemID2Price, x.ItemID2Qty, x.ItemID3Price, x.ItemID3Qty, x.ItemID4Price, x.ItemID4Qty, x.ItemID5Price, x.ItemID5Qty, x.ItemID6Price, x.ItemID6Qty, x.ItemID7Price, x.ItemID7Qty, x.ItemID8Price, x.ItemID8Qty, x.ItemID9Price, x.ItemID9Qty, x.ItemID10Price, x.ItemID10Qty, x.ItemID11Price, x.ItemID11Qty)
                                            }).ToList();

                            //Move everything to a sortable list
                            List<invoice_grid_data> datalist = data.Select(p => new invoice_grid_data() { InvoiceNo = p.InvoiceNo, InvoiceDt = p.InvoiceDt, BillToName = p.BillToName, DueDt = p.DueDt, due = p.due }).ToList();
                            SortableBindingList<invoice_grid_data> sorteddata = new SortableBindingList<invoice_grid_data>(datalist);
                            d.DataSource = sorteddata;
                        }
                        else if (search_duedt.Checked)
                        {
                            var invoice = dataContext.Invoice.ToList();
                            var data = invoice.Where(x => x.BillToName.ToLower().Contains(var_name.ToLower()))                      //moving everything to lowercase for case insensitive search
                                            .Where(x => DateTime.ParseExact(x.DueDt, "dd/MM/yyyy", null) >= dts.AddDays(-1))                    //For some reason subtracting 1 is required to include start date in the search
                                            .Where(x => DateTime.ParseExact(x.DueDt, "dd/MM/yyyy", null) <= dte)
                                            .Select(x => new
                                            {
                                                x.InvoiceNo,
                                                x.InvoiceDt,
                                                x.DueDt,
                                                x.BillToName,
                                                due = calc_due(x.Paid, x.ItemID1Price, x.ItemID1Qty, x.ItemID2Price, x.ItemID2Qty, x.ItemID3Price, x.ItemID3Qty, x.ItemID4Price, x.ItemID4Qty, x.ItemID5Price, x.ItemID5Qty, x.ItemID6Price, x.ItemID6Qty, x.ItemID7Price, x.ItemID7Qty, x.ItemID8Price, x.ItemID8Qty, x.ItemID9Price, x.ItemID9Qty, x.ItemID10Price, x.ItemID10Qty, x.ItemID11Price, x.ItemID11Qty)
                                            }).ToList();

                            //Move everything to a sortable list
                            List<invoice_grid_data> datalist = data.Select(p => new invoice_grid_data() { InvoiceNo = p.InvoiceNo, InvoiceDt = p.InvoiceDt, BillToName = p.BillToName, DueDt = p.DueDt, due = p.due }).ToList();
                            SortableBindingList<invoice_grid_data> sorteddata = new SortableBindingList<invoice_grid_data>(datalist);
                            d.DataSource = sorteddata;
                        }
                        else
                        {
                            var invoice = dataContext.Invoice.ToList();
                            var data = invoice.Where(x => x.BillToName.ToLower().Contains(var_name.ToLower()))
                                            .Select(x => new
                                            {
                                                x.InvoiceNo,
                                                x.InvoiceDt,
                                                x.DueDt,
                                                x.BillToName,
                                                due = calc_due(x.Paid, x.ItemID1Price, x.ItemID1Qty, x.ItemID2Price, x.ItemID2Qty, x.ItemID3Price, x.ItemID3Qty, x.ItemID4Price, x.ItemID4Qty, x.ItemID5Price, x.ItemID5Qty, x.ItemID6Price, x.ItemID6Qty, x.ItemID7Price, x.ItemID7Qty, x.ItemID8Price, x.ItemID8Qty, x.ItemID9Price, x.ItemID9Qty, x.ItemID10Price, x.ItemID10Qty, x.ItemID11Price, x.ItemID11Qty)
                                            }).ToList();

                            //Move everything to a sortable list
                            List<invoice_grid_data> datalist = data.Select(p => new invoice_grid_data() { InvoiceNo = p.InvoiceNo, InvoiceDt = p.InvoiceDt, BillToName = p.BillToName, DueDt = p.DueDt, due = p.due }).ToList();
                            SortableBindingList<invoice_grid_data> sorteddata = new SortableBindingList<invoice_grid_data>(datalist);
                            d.DataSource = sorteddata;
                        }
                    }
                    else
                    {
                        if (dtrange.Checked)
                        {
                            var invoice = dataContext.Invoice.ToList();
                            var data = invoice
                                        .Where(x => DateTime.ParseExact(x.InvoiceDt, "dd/MM/yyyy", null) >= dts.AddDays(-1))                //For some reason subtracting 1 is required to include start date in the search
                                        .Where(x => DateTime.ParseExact(x.InvoiceDt, "dd/MM/yyyy", null) <= dte)
                                        .Select(x => new
                                        {
                                            x.InvoiceNo,
                                            x.InvoiceDt,
                                            x.DueDt,
                                            x.BillToName,
                                            due = calc_due(x.Paid, x.ItemID1Price, x.ItemID1Qty, x.ItemID2Price, x.ItemID2Qty, x.ItemID3Price, x.ItemID3Qty, x.ItemID4Price, x.ItemID4Qty, x.ItemID5Price, x.ItemID5Qty, x.ItemID6Price, x.ItemID6Qty, x.ItemID7Price, x.ItemID7Qty, x.ItemID8Price, x.ItemID8Qty, x.ItemID9Price, x.ItemID9Qty, x.ItemID10Price, x.ItemID10Qty, x.ItemID11Price, x.ItemID11Qty)
                                        }).ToList();

                            //Move everything to a sortable list
                            List<invoice_grid_data> datalist = data.Select(p => new invoice_grid_data() { InvoiceNo = p.InvoiceNo, InvoiceDt = p.InvoiceDt, BillToName = p.BillToName, DueDt = p.DueDt, due = p.due }).ToList();
                            SortableBindingList<invoice_grid_data> sorteddata = new SortableBindingList<invoice_grid_data>(datalist);
                            d.DataSource = sorteddata;
                        }
                        else
                        {
                            var invoice = dataContext.Invoice.ToList();
                            var data = invoice.Select(x => new
                            {
                                x.InvoiceNo,
                                x.InvoiceDt,
                                x.DueDt,
                                x.BillToName,
                                due = calc_due(x.Paid, x.ItemID1Price, x.ItemID1Qty, x.ItemID2Price, x.ItemID2Qty, x.ItemID3Price, x.ItemID3Qty, x.ItemID4Price, x.ItemID4Qty, x.ItemID5Price, x.ItemID5Qty, x.ItemID6Price, x.ItemID6Qty, x.ItemID7Price, x.ItemID7Qty, x.ItemID8Price, x.ItemID8Qty, x.ItemID9Price, x.ItemID9Qty, x.ItemID10Price, x.ItemID10Qty, x.ItemID11Price, x.ItemID11Qty)
                            }).ToList();

                            //Move everything to a sortable list
                            List<invoice_grid_data> datalist = data.Select(p => new invoice_grid_data() { InvoiceNo = p.InvoiceNo, InvoiceDt = p.InvoiceDt, BillToName = p.BillToName, DueDt = p.DueDt, due = p.due }).ToList();
                            SortableBindingList<invoice_grid_data> sorteddata = new SortableBindingList<invoice_grid_data>(datalist);
                            d.DataSource = sorteddata;
                        }
                    }
                    gridview_format(d, "invoice");
                }
                else
                {
                    var items = dataContext.Items.ToList();
                    var data = items.Where(x => x.ItemName.ToLower().Contains(search_name_box.Text.ToLower()))
                                .Select(x => new { x.ItemName, x.ItemPrice }).ToList();

                    //Move everything to a sortable list
                    List<items_grid_data> datalist = data.Select(p => new items_grid_data() { ItemName = p.ItemName, ItemPrice = p.ItemPrice }).ToList();
                    SortableBindingList<items_grid_data> sorteddata = new SortableBindingList<items_grid_data>(datalist);
                    d.DataSource = sorteddata;
                    gridview_format(d, "items");
                }
            }
        }

        //Search Button 
        private void Search_btn_Click(object sender, EventArgs e)
        {
            search_fill(find_gridview);
        }

        //Load data to invoice editor
        private void load_data(Invoice inv)
        {
            invno.Text = inv.InvoiceNo;
            invdt.Value = DateTime.ParseExact(inv.InvoiceDt, "dd/MM/yyyy", null);       //Indian format 
            ddt.Value = DateTime.ParseExact(inv.DueDt, "dd/MM/yyyy", null);             //Indian format
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

        // Search result button click
        private void find_gridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                if (datasrc.SelectedIndex == 0)                     //load invoice from search result
                {
                    using (var dataContext = new DBConnection())
                    {
                        var invoice = dataContext.Invoice.ToList();
                        var inv = (from x in invoice
                                   where x.InvoiceNo == find_gridview.Rows[e.RowIndex].Cells[1].Value.ToString()
                                   select x).FirstOrDefault();
                        load_data(inv);
                        print.Enabled = true;
                        edit_check = 1;
                        find_Click(sender, e);
                    }
                }
                else
                {                                                                               //delete item
                    using (var dataContext = new DBConnection())
                    {
                        var items = dataContext.Items.ToList();
                        var item = (from x in items
                                    where x.ItemID == Convert.ToInt32(find_gridview.Rows[e.RowIndex].Cells[1].Value)
                                    select x).FirstOrDefault();
                        dataContext.Items.Remove(item);
                        try
                        {
                            dataContext.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("       Database write permission not available.     ");
                        }
                        search_fill(find_gridview);
                    }
                }
            }
        }

        //refresh button on search results
        private void refresh_search_Click(object sender, EventArgs e)
        {
            find_gridview.DataSource = null;                //Necessary to reset user alteration of grid size 
            Datasrc_SelectedIndexChanged(sender, e);
        }

        //Context Menus
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

        //Move selection away from the button row
        private void find_gridview_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                find_gridview.CurrentCell = find_gridview.Rows[0].Cells[1];
            }
            catch { }
        }

        //Set and reset action of Enter button
        private void searchbar_Enter(object sender, EventArgs e)
        {
            //MessageBox.Show("0");
            this.AcceptButton = search_btn;
        }

        private void searchbar_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }

        //Auto select text upon enter
        private void numBox_Enter(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;
            num.Select(0, num.Text.Length);
        }

        private void numBox_Enter(object sender, MouseEventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;
            num.Select(0, num.Text.Length);
        }
        private void textbox_Enter(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            t.SelectAll();
        }
        private void textbox_Enter(object sender, MouseEventArgs e)
        {
            TextBox t = (TextBox)sender;
            t.SelectAll();
        }

        // Auto Load address 
        private void billname_Leave(object sender, EventArgs e)
        {
            using (var dataContext = new DBConnection())
            {
                var invoice = dataContext.Invoice.ToList();
                var data = invoice.Where(x => x.BillToName == billname.Text.ToString())
                                .Select(x => x.BillToAdd).FirstOrDefault();
                if (data != null)
                {
                    billaddr.Text = data.ToString();
                }
            }
        }

        // Edit check starts

        // Generate and for edit_check

        private ulong andgen(ulong i)
        {
            ulong j;
            ulong ans = 0;
            for (j = 0; j < 64; j++)
            {
                if (j != i)
                {
                    ans = ans + (ulong)Math.Pow(2, j);
                }
            }
            return 0;
        }

        private void textbox_TextChanged(object sender, ulong i)
        {
            TextBox t = new TextBox();
            t = (TextBox)sender;
            if (edit_check != 1)
            {
                if (t.Text.Length == 0)
                {
                    edit_check = edit_check & andgen(i);
                }
                else
                {
                    edit_check = edit_check | (ulong)Math.Pow(2, i);
                }
            }
            if(edit_check < 2)
            {
                search.Enabled = true;
            }
            else
            {
               search.Enabled = false;
            }
        }

        private void numbox_TextChanged(object sender, ulong i)
        {
            if (sender.GetType().ToString() == "System.Windows.Forms.NumericUpDown")                //Necessary to avoid collision with disctype combobox
            {
                NumericUpDown t = new NumericUpDown();
                t = (NumericUpDown)sender;
                if (edit_check != 1)
                {
                    if (t.Value == 0)
                    {
                        edit_check = edit_check & andgen(i);
                    }
                    else
                    {
                        edit_check = edit_check | (ulong)Math.Pow(2, i);
                    }
                }
                if (edit_check < 2)
                {
                    search.Enabled = true;
                }
                else
                {
                    search.Enabled = false;
                }
            }
        }

        private void datetime_TextChanged(object sender, EventArgs e)
        {
            ulong i = 39;

            ddt.MinDate = invdt.Value;                      //Due date can't be in the past

            if (invdt.Value.Date > ddt.Value.Date)          //Due date can't be in the past
            {
                ddt.Value = invdt.Value;
            }
            //check if form has been changed
            else if (edit_check != 1)
            {
                if (invdt.Value.Date == ddt.Value.Date)
                {
                    edit_check = edit_check & andgen(i);
                }
                else
                {
                    edit_check = edit_check | (ulong)Math.Pow(2, i);
                }
            }

            if (edit_check < 2)
            {
                search.Enabled = true;
            }
            else
            {
                search.Enabled = false;
            }
        }

        private void billname_TextChanged(object sender, EventArgs e)
        {
            textbox_TextChanged(sender, 1);
        }

        private void billaddr_TextChanged(object sender, EventArgs e)
        {
            textbox_TextChanged(sender, 2);
        }
        
        private void desc_TextChanged(object sender, EventArgs e)
        {
            TextBox t = new TextBox();
            t = (TextBox)sender;
            textbox_TextChanged(sender, Convert.ToUInt64(t.Tag) + 2);
        }

        //Save items edit
        private void find_gridview_Leave(object sender, EventArgs e)
        {
            DataGridView dgview = new DataGridView();
            dgview = (DataGridView)sender;

            if (datasrc.SelectedIndex == 1)                     
            {                                                                  
                using (var dataContext = new DBConnection())
                {
                    var items = dataContext.Items.ToList();
                    foreach(DataGridViewRow dg in dgview.Rows)
                    {
                        var item = (from x in items
                                    where x.ItemID == Convert.ToInt32(dg.Cells[1].Value)
                                    select x).FirstOrDefault();
                        item.ItemName = dg.Cells[2].Value.ToString();
                        item.ItemPrice = decimal.Parse(dg.Cells[3].Value.ToString());
                    }
                    try
                    {
                        dataContext.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("       Database write permission not available.     ");
                    }
                    search_fill(find_gridview);             //reloads updated results
                }
            }
        }
    }
}
