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
    public partial class Invoice : Form
    {
        public Invoice()
        {
            InitializeComponent();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            formStyle("Invoice Form");
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
            OleDbCommand cmd1 = new OleDbCommand("select GRNID from GRN where Status='Open';", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                this.comboBox1.Items.Add(dr["GRNID"]);
            }
            mc.conn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
            OleDbCommand cmd = new OleDbCommand("select * from PO where POID='" + this.comboBox1.Text.Substring(4) + "';", mc.conn);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox15.Text = dr["TotalAmount"].ToString();
                DateTime dDate = Convert.ToDateTime(dr["Ddate"].ToString());
                textBox21.Text = dDate.ToString("d");
                DateTime dCDate = Convert.ToDateTime(dr["DCDate"].ToString());
                textBox16.Text = dCDate.ToString("d");
                textBox10.Text = dr["VID"].ToString();
                textBox14.Text = dr["VName"].ToString();
                textBox13.Text = dr["VDept"].ToString();
                textBox11.Text = dr["VCPPH"].ToString();
                textBox12.Text = dr["VContectPerson"].ToString();
            }

            OleDbCommand cmd1 = new OleDbCommand("select * from POProducts where POID='" + this.comboBox1.Text.Substring(4) + "';", mc.conn);
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
            OleDbCommand cmd = new OleDbCommand("select * from GRN where GRNID='" + this.comboBox1.Text + "';", mc.conn);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DateTime dDate = Convert.ToDateTime(dr["GRDate"].ToString());
                textBox1.Text = dDate.ToString("d");
            }
            mc.conn.Close();

        }

        private void button6_Click(object sender, EventArgs e)
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
            new OleDbCommand("insert into Invoice(InvoiceNO,RDate,GRNID,feedback)values(@InvoiceNO,@RDate,@GRNID,@feedback);", mc.conn);

            cmd.Parameters.AddWithValue("@InvoiceNO", index);
            DateTime thisDay = DateTime.Today;
            cmd.Parameters.AddWithValue("@RDate", thisDay);
            cmd.Parameters.AddWithValue("@GRNID", this.comboBox1.Text);
            cmd.Parameters.AddWithValue("@feedback", this.textBox2.Text);

            cmd.ExecuteNonQuery();
            mc.conn.Close();

        }

        private void updateGRN() {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd = new OleDbCommand("update GRN SET Status = '" + this.comboBox4.Text + "' where GRNID='" + this.comboBox1.Text + "';", mc.conn);
            cmd.ExecuteReader();
            MessageBox.Show("Invoice successfully Generated and" +  Environment.NewLine + "Status successfully updated");
            mc.conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            hideForm();
        }

        private void hideForm()
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.Show();
        }

        private string index = ""; 
        private void countQuery()
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select count(InvoiceNO) from Invoice;", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                int count = Convert.ToInt32(dr[0]);
                count++;
                index = count.ToString();
            }
            mc.conn.Close();
        }


    }
}
