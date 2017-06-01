using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        public void formStyle(string formTitle)
        {
            this.Text = formTitle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            foreach (Control c in this.Controls)
            {
                Button b = c as Button;
                if (b != null)
                {
                    //b.FlatStyle = FlatStyle.Flat;
                    //b.BackColor = Color.White;
                }
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.CancelButton = this.button5;
            this.formStyle("Main screen");
            this.button3.Enabled = false;
            this.button3.Visible = false;
            this.button4.Text = "Go to Purchase";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Vendor v = new Vendor();
            this.Hide();
            v.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Customer c = new Customer();
            this.Hide();
            c.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        public static int poTabIndex = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            purchaseOrder po = new purchaseOrder();
            po.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            //Application.Exit();
            Form3 f3 = new Form3();
            this.Hide();
            f3.Show();
        }

        private void invoice_button_Click(object sender, EventArgs e)
        {
            Invoice inv = new Invoice();
            inv.Show();
            this.Hide();
        }

        private void grn_button_Click(object sender, EventArgs e)
        {
            Grn grn = new Grn();
            grn.Show();
            this.Hide();
        }
    }
}
