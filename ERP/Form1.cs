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
    public partial class Form1 : Form
    {
        public Form1()
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
                    //b.BackColor = Color.WhiteSmoke;
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.formStyle("Login Form");
            this.AcceptButton = this.button1;
            this.textBox2.PasswordChar = '*';
            this.label1.Text = "Username";
            this.label2.Text = "Password";
            this.button1.Text = "Login";
            this.button2.Text = "Cancel";

            this.textBox1.Text = "BS3A";
            this.textBox2.Text = "BS3A";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if( (this.textBox1.Text == "BS3A" || this.textBox1.Text == "bs3a") && 
                (this.textBox2.Text == "BS3A" || this.textBox1.Text == "bs3a"))
            {
                //Form2 f2 = new Form2();
                Form3 f3 = new Form3();
                this.Hide();
                //f2.Show();
                f3.Show();
            }
            else
            {
                MessageBox.Show("Username or Password is invalid !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
