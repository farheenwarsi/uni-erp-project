using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ERP
{
    public partial class receivedChallan : Form
    {
        public receivedChallan()
        {
            InitializeComponent();
        }

        private void receivedChallan_Load(object sender, EventArgs e)
        {
            formStyle("Received Challan Form");
            getGrnOpenedOrder();
            countQuery();
        }

        public void formStyle(string formTitle)
        {
            this.Text = formTitle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            //this.button6.FlatStyle = this.button5.FlatStyle = FlatStyle.Flat;
            //this.button5.BackColor = this.button6.BackColor = Color.White;

            //this.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            //this.tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;


            this.comboBox4.Items.Add("Close");
            this.comboBox4.Items.Add("Open");
            this.comboBox4.SelectedIndex = 0;
        }

        private void getGrnOpenedOrder()
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select DCHID from DC where Status='Open';", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                this.comboBox1.Items.Add(dr["DCHID"]);
            }
            mc.conn.Close();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.dataGridView2.Rows.Count >= 1)
            {
                this.dataGridView2.Rows.Clear();
            }
            getProductInfo();
        }

        private void getProductInfo()
        {

            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd = new OleDbCommand("select * from SO where SOID='" + this.comboBox1.Text.Substring(4) + "';", mc.conn);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox15.Text = dr["TotalAmount"].ToString();
                DateTime dDate = Convert.ToDateTime(dr["Ddate"].ToString());
                textBox21.Text = dDate.ToString("d");
                DateTime dCDate = Convert.ToDateTime(dr["DCDate"].ToString());
                textBox16.Text = dCDate.ToString("d");
                textBox10.Text = dr["CID"].ToString();
                textBox14.Text = dr["CName"].ToString();
                textBox13.Text = dr["CDept"].ToString();
                textBox11.Text = dr["CCPPH"].ToString();
                textBox12.Text = dr["CContectPerson"].ToString();
            }

            OleDbCommand cmd1 = new OleDbCommand("select * from SOProducts where SOID='" + this.comboBox1.Text.Substring(4) + "';", mc.conn);
            OleDbDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                OleDbCommand cmd2 = new OleDbCommand("select * from Products where PModel='" + dr1["PModel"].ToString() + "';", mc.conn);
                OleDbDataReader dr2 = cmd2.ExecuteReader();

                while (dr2.Read())
                {
                    this.dataGridView2.Rows.Add(dr1["PModel"].ToString(), dr2["PName"].ToString(), dr1["PQty"].ToString(),
                                                dr2["BasePrice"].ToString(), (Convert.ToInt16(dr1["PQty"].ToString()) * Convert.ToInt16(dr2["BasePrice"].ToString())));
                }

            }

            mc.conn.Close();
            getGRNDetail();
        }

        private void getGRNDetail()
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd = new OleDbCommand("select * from DC where DCHID='" + this.comboBox1.Text + "';", mc.conn);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DateTime dDate = Convert.ToDateTime(dr["DCHDate"].ToString());
                textBox1.Text = dDate.ToString("d");
            }
            mc.conn.Close();

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            generateInvoice();
            updateGRN();
            hideForm();
        }

        private void generateInvoice()
        {

            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd =
            new OleDbCommand("insert into RI(InvoiceNO,RDate,DCHID,feedback)values(@InvoiceNO,@RDate,@DCHID,@feedback);", mc.conn);

            cmd.Parameters.AddWithValue("@InvoiceNO", index);
            DateTime thisDay = DateTime.Today;
            cmd.Parameters.AddWithValue("@RDate", thisDay);
            cmd.Parameters.AddWithValue("@DCHID", this.comboBox1.Text);
            cmd.Parameters.AddWithValue("@feedback", this.textBox2.Text);

            cmd.ExecuteNonQuery();
            mc.conn.Close();

        }

        private void updateGRN()
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd = new OleDbCommand("update DC SET Status = '" + this.comboBox4.Text + "' where DCHID='" + this.comboBox1.Text + "';", mc.conn);
            cmd.ExecuteReader();
            MessageBox.Show("Received Invoice successfully Generated and" + Environment.NewLine + "Status successfully updated");
            mc.conn.Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            hideForm();
        }

        private void hideForm()
        {
            Form4 f4 = new Form4();
            this.Hide();
            f4.Show();
        }

        private string index = "";
        private void countQuery()
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select count(InvoiceNO) from RI;", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                int count = Convert.ToInt32(dr[0]);
                count++;
                index = count.ToString();
            }
            mc.conn.Close();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
