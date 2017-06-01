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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        public static int soTabIndex = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            saleOrder so = new saleOrder();
            this.Hide();
            so.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.Text = "Sale Order Option";
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = false;
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

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            this.Hide();
            f3.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            deliveryChalan dc = new deliveryChalan();
            this.Hide();
            dc.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            receivedChallan rc = new receivedChallan();
            this.Hide();
            rc.Show();
        }
    }
}
