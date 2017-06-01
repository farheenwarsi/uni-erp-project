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
    public partial class deliveryChalan : Form
    {
        public deliveryChalan()
        {
            InitializeComponent();
        }

        private void deliveryChalan_Load(object sender, EventArgs e)
        {
            formStyle("Delivery Chalan Generate Form");
            getApprovedOrders();
        }


        public void formStyle(string formTitle)
        {
            this.Text = formTitle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            this.button6.FlatStyle = this.button5.FlatStyle = FlatStyle.Flat;
            this.button5.BackColor = this.button6.BackColor = Color.White;

            //this.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            //this.tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;


            this.comboBox4.Items.Add("Close");
            this.comboBox4.Items.Add("Open");
            this.comboBox4.SelectedIndex = 0;
        }

        private void getApprovedOrders()
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select SOID from SO where Status='Open' and Approve='Approved';", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                this.comboBox1.Items.Add(dr["SOID"]);
            }
            mc.conn.Close();
            //autoGenerateId();
            //this.textBox8.Text = this.textBox2.Text;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            hideForm();
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
            OleDbCommand cmd = new OleDbCommand("select * from SO where SOID='" + this.comboBox1.Text + "';", mc.conn);
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

            OleDbCommand cmd1 = new OleDbCommand("select * from SOProducts where SOID='" + this.comboBox1.Text + "';", mc.conn);
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
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            generateGrn();
            updateStatus();
            hideForm();
        }

        private void generateGrn()
        {

            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd =
            new OleDbCommand("insert into DC(DCHID,BaseDocument,Status,CName,DCDate,Ddate,DCHDate)" +
            "values(@DCHID,@BaseDocument,@Status,@CName,@DCDate,@Ddate,@DCHDate);", mc.conn);

            cmd.Parameters.AddWithValue("@DCHID", "DCH_" + this.comboBox1.Text);
            cmd.Parameters.AddWithValue("@BaseDocument", this.comboBox1.Text);
            cmd.Parameters.AddWithValue("@Status", "Open");
            cmd.Parameters.AddWithValue("@VName", this.textBox14.Text);
            cmd.Parameters.AddWithValue("@DCDate", this.textBox21.Text);
            cmd.Parameters.AddWithValue("@Ddate", this.textBox16.Text);
            DateTime thisDay = DateTime.Today;
            cmd.Parameters.AddWithValue("@DCHDate", thisDay);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Delivery chalan successfully Generated.");
            mc.conn.Close();

        }

        private void updateStatus()
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd = new OleDbCommand("update SO SET Status = '" + this.comboBox4.Text + "' where SOID='" + this.comboBox1.Text + "';", mc.conn);
            cmd.ExecuteReader();
            MessageBox.Show("Status successfully updated");
            mc.conn.Close();
        }
        private void hideForm()
        {
            Form4 f4 = new Form4();
            this.Hide();
            f4.Show();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
