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
    public partial class Grn : Form
    {
        public Grn()
        {
            InitializeComponent();
        }

        private void Grn_Load(object sender, EventArgs e)
        {
            formStyle("GRN Generate Form");
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

            //this.button6.FlatStyle = this.button5.FlatStyle = FlatStyle.Flat;
            //this.button5.BackColor = this.button6.BackColor = Color.White;

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
            OleDbCommand cmd1 = new OleDbCommand("select POID from PO where Status='Open' and Approve='Approved';", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                this.comboBox1.Items.Add(dr["POID"]);
            }
            mc.conn.Close();
            //autoGenerateId();
            //this.textBox8.Text = this.textBox2.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            hideForm();
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
            OleDbCommand cmd = new OleDbCommand("select * from PO where POID='" + this.comboBox1.Text + "';", mc.conn);
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

            OleDbCommand cmd1 = new OleDbCommand("select * from POProducts where POID='" + this.comboBox1.Text + "';", mc.conn);
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

        private void button6_Click(object sender, EventArgs e)
        {
            generateGrn();
            updateStatus();
            hideForm();
        }

        private void generateGrn() {

            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd =
            new OleDbCommand("insert into GRN(GRNID,BaseDocument,Status,VName,DCDate,Ddate,GRDate)" +
            "values(@GRNID,@BaseDocument,@Status,@VName,@DCDate,@Ddate,@GRDate);", mc.conn);

            cmd.Parameters.AddWithValue("@GRNID", "GRN_" + this.comboBox1.Text);
            cmd.Parameters.AddWithValue("@BaseDocument", this.comboBox1.Text);
            cmd.Parameters.AddWithValue("@Status", "Open");
            cmd.Parameters.AddWithValue("@VName", this.textBox14.Text);
            cmd.Parameters.AddWithValue("@DCDate", this.textBox21.Text);
            cmd.Parameters.AddWithValue("@Ddate", this.textBox16.Text);
            DateTime thisDay = DateTime.Today;
            cmd.Parameters.AddWithValue("@GRDate", thisDay);

            cmd.ExecuteNonQuery();
            MessageBox.Show("GRN successfully Generated.");
            mc.conn.Close();

        }

        private void updateStatus() {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd = new OleDbCommand("update PO SET Status = '" + this.comboBox4.Text + "' where POID='" + this.comboBox1.Text + "';", mc.conn);
            cmd.ExecuteReader();
            MessageBox.Show("Status successfully updated");
            mc.conn.Close();
        }
        private void hideForm()
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.Show();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
