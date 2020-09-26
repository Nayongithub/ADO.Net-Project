using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace ADO.NET
{
    public partial class frmRegistration : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=HotelManagement;Integrated Security=True");
        public frmRegistration()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation()==true)
            {
                Image img = Image.FromFile(txtPicture.Text);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);

                SqlCommand cmd = new SqlCommand("INSERT INTO tblUser (Username,Phone,Email,Password,ReTypePassword,Picture) VALUES(@Username,@Phone,@Email,@Password,@ReTypePassword,@Picture)", con);
                cmd.Parameters.AddWithValue("@Username", txtName.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@ReTypePassword", txtReTypePassword.Text);
                cmd.Parameters.Add(new SqlParameter("@picture", SqlDbType.VarBinary) { Value = ms.ToArray() });

                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Data Inserted successfully!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                con.Close();
                frmLogin fl = new frmLogin();
                fl.Show();
                this.Hide();
            }
            
        }

        private void btnPicture_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPicture.Text = openFileDialog1.FileName;
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                txtPicture.Text = openFileDialog1.FileName;
            }
            
        }
        private bool Validation()
        {
            if (txtName.Text == "")
            {
                txtName.Focus();
                MessageBox.Show("Name is required!!!");
                return false;
            }
            else if (txtPhone.Text == "")
            {
                MessageBox.Show("Phone Number is required!!!");
                return false;
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Email is required!!!");
                return false;
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Password is required!!!");
                return false;
            }
            else if (txtReTypePassword.Text == "")
            {
                MessageBox.Show("Re-Type Password is required!!!");
                return false;
            }

            else if (txtPassword.Text!=txtReTypePassword.Text)
            {
                txtPassword.Clear();
                txtReTypePassword.Clear();
                txtPassword.Focus();
                MessageBox.Show("Password does not match!!!");
                return false;
            }
            else if (txtPicture.Text == "")
            {
                MessageBox.Show("Picture is required!!!");
                return false;
            }

            return true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                txtReTypePassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtReTypePassword.UseSystemPasswordChar = false;
            }
        }
    }
}
