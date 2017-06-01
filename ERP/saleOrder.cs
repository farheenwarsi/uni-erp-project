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
    public partial class saleOrder : Form
    {
        public saleOrder()
        {
            InitializeComponent();
        }

        //initialize global veriables
        public static int counter = 0;

        private void saleOrder_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = Form4.soTabIndex;
            this.tabPage1.Text = "Sale Order";
            this.tabPage2.Text = "Approve Order";
            this.groupBox1.Text = "Sale Order Info";
            this.groupBox2.Text = "Product Info";
            this.groupBox3.Text = "Customer Info";
            this.groupBox4.Text = "Selected Order";
            this.textBox9.ReadOnly = true;
            this.button1.Text = "Add Order";
            this.button2.Text = "Cancel";
            formStyle("Sale Order");
            vendorInfo();
            productIfo();
            countQuery();
            getUnApproveOrders();
            this.comboBox4.Items.Add("Approved");
            this.comboBox4.Items.Add("Not Approved");
            this.comboBox4.SelectedIndex = 0;
        }

        public void formStyle(string formTitle)
        {
            this.Text = formTitle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            //this.button1.FlatStyle = this.button2.FlatStyle = this.button3.FlatStyle =
            //this.button5.FlatStyle = this.button6.FlatStyle = FlatStyle.Flat;
            //this.button1.BackColor = this.button2.BackColor = this.button3.BackColor =
            //this.button5.BackColor = this.button6.BackColor = Color.White;

            //this.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            //this.tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            this.dateTimePicker1.MinDate = DateTime.Now.AddDays(3);
        }

        private void vendorInfo()
        {
            this.textBox4.ReadOnly = this.textBox5.ReadOnly =
            this.textBox6.ReadOnly = this.textBox7.ReadOnly = true;

            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select * from Customer;", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                this.comboBox3.Items.Add(dr["CID"]);

            }
            mc.conn.Close();


        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select * from Customer where CID='" + this.comboBox3.Text + "';", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                textBox4.Text = dr["CName"].ToString();
                textBox5.Text = dr["CGroup"].ToString();
                textBox6.Text = dr["ContectPerson"].ToString();
                textBox7.Text = dr["CPPH"].ToString();

            }
            mc.conn.Close();
            autoGenerateId();
            //this.textBox9.Text = textBox5.Text + "-" + this.comboBox3.Text + "/" + this.comboBox2.Text + "/" + counter;
        }

        private void productIfo()
        {
            this.textBox1.ReadOnly = this.textBox2.ReadOnly = true;

            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select * from Products;", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                this.comboBox2.Items.Add(dr["PModel"]);

            }
            mc.conn.Close();
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select * from Products where PModel='" + this.comboBox2.Text + "';", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                this.textBox1.Text = dr["PName"].ToString();
                this.textBox2.Text = dr["BasePrice"].ToString();
                this.textBox3.Text = "1";
            }
            mc.conn.Close();
            autoGenerateId();
            this.textBox8.Text = this.textBox2.Text;
            //this.textBox9.Text = textBox5.Text + "-" + this.comboBox3.Text + "/" + this.comboBox2.Text + "/" + counter;
        }

        private void autoGenerateId()
        {
            if (textBox5.Text != "")
            {
                string tempStr = (textBox5.Text.Length > 3) ? textBox5.Text.Substring(0, 3) : textBox5.Text.Substring(0, 2);
                this.textBox9.Text = tempStr + "_" + counter + "_" + DateTime.Now.Year.ToString();
            }
            else
            {
                this.textBox9.Text = textBox5.Text + "_" + this.comboBox3.Text + "_" + counter;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox3.Text != "")
            {
                this.textBox8.Text = (Convert.ToInt64(this.textBox2.Text) * Convert.ToInt64(this.textBox3.Text)).ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addSaleOrder();
            this.dataGridView1.Rows.Add(this.textBox9.Text, this.textBox1.Text, this.textBox3.Text,
                                        this.textBox8.Text, this.dateTimePicker1.Value.Date.ToString("d"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addSaleOrderDetail();
            getUnApproveOrders();
            Form4.soTabIndex = 1;
            saleOrder so = new saleOrder();
            this.Hide();
            so.Show();
        }

        private void addSaleOrderDetail()
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd =
            new OleDbCommand("insert into SO(Soid,Ddate,Status,Approve,CDept,CName,Cid,CContectPerson,CCPPH,TotalAmount,GoodRecieved, DCDate)" +
            "values(@Soid,@Ddate,@Status,@Approve,@CDept,@CName,@Cid,@CContectPerson,@CCPPH,@TotalAmount,@GoodRecieved,@DCDate);", mc.conn);

            cmd.Parameters.AddWithValue("@Soid", this.textBox9.Text);
            cmd.Parameters.AddWithValue("@DDate", this.dateTimePicker1.Value.Date);
            cmd.Parameters.AddWithValue("@Status", "Open");
            cmd.Parameters.AddWithValue("@Approve", "Not Approved");
            cmd.Parameters.AddWithValue("@CDept", this.textBox5.Text);
            cmd.Parameters.AddWithValue("@CName", this.textBox4.Text);
            cmd.Parameters.AddWithValue("@Cid", this.comboBox3.Text);
            cmd.Parameters.AddWithValue("@CContectPerson", this.textBox6.Text);
            cmd.Parameters.AddWithValue("@CCPPH", this.textBox7.Text);
            cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
            cmd.Parameters.AddWithValue("@GoodRecieved", "No");
            DateTime thisDay = DateTime.Today;
            cmd.Parameters.AddWithValue("@DCDate", thisDay);
            //cmd.Parameters.AddWithValue("@DCDate", thisDay.ToString("d"));

            cmd.ExecuteNonQuery();
            MessageBox.Show("Order successfilly added.");
            mc.conn.Close();
        }

        int totalAmount = 0;
        private void addSaleOrder()
        {

            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd = new OleDbCommand("insert into SOProducts(Soid,PModel,PQty)values(@SOID,@PModel,@PQty);", mc.conn);
            cmd.Parameters.AddWithValue("@SOID", this.textBox9.Text);
            cmd.Parameters.AddWithValue("@PModel", this.comboBox2.Text);
            cmd.Parameters.AddWithValue("@PQty", this.textBox3.Text);
            cmd.ExecuteNonQuery();
            mc.conn.Close();

            totalAmount += Convert.ToInt16(this.textBox8.Text);
            this.textBox20.Text = totalAmount.ToString();

        }

        private void countQuery()
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select count(SOID) from SOProducts;", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                counter = Convert.ToInt32(dr[0]);
                counter++;

            }
            mc.conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4.soTabIndex = 0;
            removeAddProductFromdb();
            hideForm();
        }

        private void removeAddProductFromdb()
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("delete from SOProducts where SOID ='" + this.textBox9.Text + "'", mc.conn);
            cmd1.ExecuteNonQuery();
            mc.conn.Close();
        }

        private void getUnApproveOrders()
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select SOID from SO where Approve='Not Approved';", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                this.comboBox1.Items.Add(dr["SOID"]);
            }
            mc.conn.Close();
            autoGenerateId();
            this.textBox8.Text = this.textBox2.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4.soTabIndex = 0;
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

        private void button6_Click(object sender, EventArgs e)
        {
            Form4.soTabIndex = 0;
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd = new OleDbCommand("update SO SET Approve = '" + this.comboBox4.Text + "' where SOID='" + this.comboBox1.Text + "';", mc.conn);
            cmd.ExecuteReader();
            MessageBox.Show("Status successfully updated");
            mc.conn.Close();
            hideForm();
        }

        private void hideForm()
        {
            Form4 f4 = new Form4();
            this.Hide();
            f4.Show();
        }
        
    }
}
