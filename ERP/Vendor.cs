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
    public partial class Vendor : Form
    {
        public Vendor()
        {
            InitializeComponent();
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

        private void Vendor_Load(object sender, EventArgs e)
        {
            this.tabPage1.Text = "Add Vendor";
            this.tabPage2.Text = "Approve Vendor";
            addForm();
            countQuery();
            this.formStyle("Vendor");
            tab2();
        }

        private void addForm() {
            this.title.Text = " Add Vendor ";
            this.tab2Title.Text = " Approve Vendor ";
            this.label1.Text = this.tab2ID.Text = "ID";
            this.label2.Text = this.tab2Name.Text = "Name";
            this.label3.Text = this.tab2Code.Text = "Code";
            this.label4.Text = this.tab2Ph1.Text = "Contact No 1";
            this.label5.Text = this.tab2Ph2.Text =  "Contact No 2";
            this.label6.Text = this.tab2Address.Text = "Address";
            this.label7.Text = this.tab2Email.Text = "E-mail";
            this.label8.Text = this.tab2Cpph.Text = "Contact Person No";
            this.label9.Text = this.tab2Fax.Text = "Fax No";
            this.label10.Text = this.tab2Group.Text = "Group Name";
            this.label11.Text = this.tab2Status.Text = "Status";
            this.label12.Text = this.tab2City.Text = "City";
            this.label13.Text = this.tab2CPName.Text = "Contact Person Name";
            this.button1.Text = "Add Vendor";
            this.button2.Text = this.button4.Text = "Cancel";
            this.button3.Text = "Approve Vendor";
            //this.button1.FlatStyle = this.button2.FlatStyle = this.button3.FlatStyle = this.button4.FlatStyle = FlatStyle.Flat;
            //this.button1.BackColor = this.button2.BackColor = this.button3.BackColor = this.button4.BackColor = Color.White;
           // this.tableLayoutPanel1.CellBorderStyle = this.tableLayoutPanel2.CellBorderStyle  = TableLayoutPanelCellBorderStyle.Inset;

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
                this.textBox2.Text = "ABCD1234";
                this.textBox3.Text = "123456789";
                this.textBox4.Text = "123456789";
                this.textBox5.Text = "Abc Street xyz road";
                this.textBox6.Text = "haider@gmail.com";
                this.textBox7.Text = "123456789";
                this.textBox8.Text = "FAXabc123";
                this.textBox9.Text = "Karachi";
                this.textBox10.Text = "Abdul Jabbar";

                this.textName.ReadOnly = this.textCode.ReadOnly = this.textEmail.ReadOnly = 
                this.textPH1.ReadOnly = this.textCode.ReadOnly = this.textPH2.ReadOnly = 
                this.textAddress.ReadOnly = this.textName.ReadOnly = this.textCity.ReadOnly = 
                this.textCPName.ReadOnly = this.textCPPH.ReadOnly = this.textFax.ReadOnly = true;
            
        }

        private void countQuery() {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select count(VID) from Vendor;", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                int count = Convert.ToInt32(dr[0]);
                count++;
                if (count < 10) {
                    this.comboBox1.Items.Add( "0" + count );
                }
                else
                {
                    this.comboBox1.Items.Add (count.ToString());
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

        private void button2_Click(object sender, EventArgs e)
        {
            hideForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd = 
            new OleDbCommand("insert into Vendor(VID,VName,VCode,PH1,PH2,VAddress,VEmail,CPPH,VFax,VCity,CPName,VGroup,VStatus)" +
            "values(@VID,@VName,@VCode,@PH1,@PH2,@VAddress,@VEmail,@CPPH,@VFax,@VCity,@CPName,@VGroup,@VStatus);", mc.conn);

            cmd.Parameters.AddWithValue("@VID", this.comboBox1.Text);
            cmd.Parameters.AddWithValue("@VName", this.textBox1.Text);
            cmd.Parameters.AddWithValue("@VCode", this.textBox2.Text);
            cmd.Parameters.AddWithValue("@PH1", this.textBox3.Text);
            cmd.Parameters.AddWithValue("@PH2", this.textBox4.Text);
            cmd.Parameters.AddWithValue("@VAddress", this.textBox5.Text);
            cmd.Parameters.AddWithValue("@VEmail", this.textBox6.Text);
            cmd.Parameters.AddWithValue("@CPPH", this.textBox7.Text);
            cmd.Parameters.AddWithValue("@VFax", this.textBox8.Text);
            cmd.Parameters.AddWithValue("@VCity", this.textBox9.Text);
            cmd.Parameters.AddWithValue("@CPName", this.textBox10.Text);
            cmd.Parameters.AddWithValue("@VGroup", this.comboBox2.Text);
            cmd.Parameters.AddWithValue("@VStatus", this.comboBox3.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("New Vendor successfilly added.");
            Clear(this.tabControl1.SelectedTab);
            mc.conn.Close();
            tab2();
            this.tabControl1.SelectedIndex = 1;
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

        private void tab2() {

            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select VID from Vendor where VStatus = 'Not-Active';", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr["VID"]);

            }
            mc.conn.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd = new OleDbCommand("update Vendor SET Vstatus = '" + this.comboBox6.Text + "' where VID='" + this.comboBox4.Text + "';", mc.conn);
            cmd.ExecuteReader();
            MessageBox.Show("Status successfully updated");
            mc.conn.Close();
            hideForm();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hideForm();
        }

        private void hideForm()
        {
            Form2 f2 = new Form2();
            this.Hide();
            f2.Show();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            myConnection mc = new myConnection();
            mc.conn.Open();
            OleDbCommand cmd1 = new OleDbCommand("select * from Vendor where VID='" + this.comboBox4.Text + "';", mc.conn);
            OleDbDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                this.textName.Text = dr["VNAME"].ToString();
                this.textCode.Text = dr["VCODE"].ToString();
                this.textPH1.Text = dr["PH1"].ToString();
                this.textPH2.Text = dr["PH2"].ToString();
                this.textAddress.Text = dr["VAddress"].ToString();
                this.textEmail.Text = dr["VEmail"].ToString();
                this.textCPPH.Text = dr["CPPH"].ToString();
                this.textFax.Text = dr["VFax"].ToString();
                this.comboBox5.Text = dr["VGroup"].ToString();
                this.comboBox5.Items.Add(dr["VGroup"]);
                this.comboBox5.SelectedIndex = 0;
                this.comboBox6.Text = dr["VStatus"].ToString();
                this.comboBox6.SelectedIndex = 0;
                this.textCity.Text = dr["VCity"].ToString();
                this.textCPName.Text = dr["CPName"].ToString();

            }
            mc.conn.Close();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox10.Text != "" && this.textBox10.Text.Length <= 15)
            {
                this.textBox10.Text = CustomValidation.checkAlphabetInput(this.textBox10.Text);
                this.textBox10.SelectionStart = this.textBox10.Text.Length;
            }
            else if(this.textBox10.Text != "" && this.textBox10.Text.Length > 15) {
                MessageBox.Show("Text lenght should be less then fifteen Characters.");
                textBox10.Text = textBox10.Text.Remove(textBox10.Text.Length - 1);
                this.textBox10.SelectionStart = this.textBox10.Text.Length;
            }
        }
    }
}
