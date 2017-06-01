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
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            this.tabPage1.Text = "Add Customer";
            this.tabPage2.Text = "Approve Customer";
            addForm();
            countQuery();
            this.formStyle("Customer");
            tab2();
        }

        public void formStyle(string formTitle)
        {
            this.Text = formTitle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
        }

        private void addForm()
        {
            this.title.Text = " Add Customer ";
            this.tab2Title.Text = " Approve Customer ";
            this.label1.Text = this.tab2ID.Text = "ID";
            this.label2.Text = this.tab2Name.Text = "Name";
            this.label3.Text = this.tab2CreditLimit.Text = "Credit limit";
            this.label4.Text = this.tab2Ph1.Text = "Contact No 1";
            this.label5.Text = this.tab2Ph2.Text = "Contact No 2";
            this.label6.Text = this.tab2Address.Text = "Address";
            this.label7.Text = this.tab2Email.Text = "E-mail";
            this.label8.Text = this.tab2Cpph.Text = "Contact Person No";
            this.label9.Text = this.tab2CPName.Text = "Contact Person Name";
            this.label10.Text = this.tab2Group.Text = "Group Name";
            this.label11.Text = this.tab2Status.Text = "Status";
            this.label12.Text = this.tab2City.Text = "City";
            //this.label13.Text = this.tab2CPName.Text = "Contact Person Name";
            this.button1.Text = "Add Customer";
            this.button2.Text = this.button4.Text = "Cancel";
            this.button3.Text = "Approve Customer";
            this.button1.FlatStyle = this.button2.FlatStyle = this.button3.FlatStyle = this.button4.FlatStyle = FlatStyle.Flat;
            this.button1.BackColor = this.button2.BackColor = this.button3.BackColor = this.button4.BackColor = Color.White;
            this.tableLayoutPanel1.CellBorderStyle = this.tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            this.comboBox3.Items.Add("Active");
            this.comboBox3.Items.Add("Not-Active");
            this.comboBox3.SelectedIndex = 1;
            this.comboBox6.Items.Add("Active");
            this.comboBox6.Items.Add("Not-Active");

            setGrpupValues();

            defaultValue();
        }

        private void defaultValue()
        {
            this.textBox1.Text = "Haider";
            this.textBox2.Text = "1500";
            this.textBox3.Text = "123456789";
            this.textBox4.Text = "123456789";
            this.textBox5.Text = "Abc Street xyz road";
            this.textBox6.Text = "haider@gmail.com";
            this.textBox7.Text = "123456789";
            this.textBox8.Text = "Abdul Jabbar";
            this.textBox9.Text = "Karachi";
            //this.textBox10.Text = "Abdul Jabbar";

            this.textName.ReadOnly = this.textCreditLimit.ReadOnly = this.textEmail.ReadOnly =
            this.textPH1.ReadOnly = this.textPH2.ReadOnly = this.textAddress.ReadOnly = 
            this.textCity.ReadOnly = this.textCPName.ReadOnly = this.textCPPH.ReadOnly =  true;
        }

        private void countQuery()
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select count(CID) from Customer;", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                int count = Convert.ToInt32(dr[0]);
                count++;
                if (count < 10)
                {
                    this.comboBox1.Items.Add("0" + count);
                }
                else
                {
                    this.comboBox1.Items.Add(count.ToString());
                }
                this.comboBox1.SelectedIndex = 0;

            }
            mc.conn.Close();
        }

        private void setGrpupValues()
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select grpname from cusgroup;", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                this.comboBox2.Items.Add(dr["grpname"]);

            }
            this.comboBox2.SelectedIndex = 0;
            mc.conn.Close();
        }
        private void hideForm()
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.Show();
        }

        private void Clear(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                TextBox textBox = child as TextBox;
                if (textBox == null) Clear(child);
                else textBox.Text = string.Empty;

                ComboBox combox = child as ComboBox;
                if (combox == null) Clear(child);
                else
                {
                    combox.Items.Clear();
                    combox.Text = string.Empty;

                }
            }
        }

        private void tab2()
        {

            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select CID from Customer where CStatus = 'Not-Active';", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr["CID"]);

            }
            mc.conn.Close();

        }

        private void comboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select * from Customer where CID='" + this.comboBox4.Text + "';", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                this.textName.Text = dr["CNAME"].ToString();
                this.textCreditLimit.Text = dr["creditlimit"].ToString();
                this.textPH1.Text = dr["PH1"].ToString();
                this.textPH2.Text = dr["PH2"].ToString();
                this.textAddress.Text = dr["CAddress"].ToString();
                this.textEmail.Text = dr["CEmail"].ToString();
                this.textCPPH.Text = dr["CPPH"].ToString();
                this.textCity.Text = dr["CITY"].ToString();
                this.comboBox5.Text = dr["CGroup"].ToString();
                this.comboBox5.Items.Add(dr["CGroup"]);
                this.comboBox5.SelectedIndex = 0;
                //this.comboBox6.Text = dr["CStatus"].ToString();
                this.comboBox6.SelectedIndex = 0;
                //this.textCity.Text = dr["VCity"].ToString();
                this.textCPName.Text = dr["ContectPerson"].ToString();

            }
            mc.conn.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd = new OleDbCommand("insert into customer(cid,cname,caddress,city,ph1,ph2,contectperson,cpph,cemail,creditlimit,cstatus,cgroup)" + 
            "values(@cid,@cname,@caddress,@city,@ph1,@ph2,@contectperson,@cpph,@cemail,@creditlimit,@cstatus,@cgroup);", mc.conn);
            
            cmd.Parameters.AddWithValue("@cid", this.comboBox1.Text);
            cmd.Parameters.AddWithValue("@cname", this.textBox1.Text);
            cmd.Parameters.AddWithValue("@caddress", this.textBox5.Text);
            cmd.Parameters.AddWithValue("@city", this.textBox9.Text);
            cmd.Parameters.AddWithValue("@ph1", this.textBox3.Text);
            cmd.Parameters.AddWithValue("@ph2", this.textBox4.Text);
            cmd.Parameters.AddWithValue("@contectperson", this.textBox8.Text);
            cmd.Parameters.AddWithValue("@cpph", this.textBox7.Text);
            cmd.Parameters.AddWithValue("@cemail", this.textBox6.Text);
            cmd.Parameters.AddWithValue("@creditlimit", this.textBox2.Text);
            cmd.Parameters.AddWithValue("@cstatus", this.comboBox3.Text);
            cmd.Parameters.AddWithValue("@cgroup", this.comboBox2.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("New Customer successfilly added.");
            Clear(this.tabControl1.SelectedTab);
            mc.conn.Close();
            tab2();
            this.tabControl1.SelectedIndex = 1;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            hideForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd = new OleDbCommand("update Customer SET cstatus = '" + this.comboBox6.Text + "' where CID='" + this.comboBox4.Text + "';", mc.conn);
            cmd.ExecuteReader();
            MessageBox.Show("Status successfully updated");
            mc.conn.Close();
            hideForm();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hideForm();
        }
    }
}
