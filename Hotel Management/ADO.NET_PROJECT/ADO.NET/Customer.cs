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
    public partial class frmCustomer : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=HotelManagement;Integrated Security=True");

        
        public frmCustomer()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (Validation()==true)
            {
                string Gender = "";
                if (radioBtnMale.Checked == true)
                {
                    Gender = "Male";
                }
                else if (radioBtnFemale.Checked == true)
                {
                    Gender = "Female";
                }
                else if (radioBtnOther.Checked == true)
                {
                    Gender = "Other";
                }
                
                Image img = Image.FromFile(txtPicture.Text);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);

                SqlCommand cmd = new SqlCommand("INSERT INTO tblCustomer (Customer_Name,Phone_Number,Email,Gender,Customer_Address,CheckIn,CheckOut,Room_No,RoomType_ID,Picture) VALUES(@Customer_Name,@Phone_Number,@Email,@Gender,@Customer_Address,@CheckIn,@CheckOut,@Room_No,@RoomType_ID,@Picture)", con);
                cmd.Parameters.AddWithValue("@Customer_Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Phone_Number", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@Customer_Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@CheckIn", dateTimePicker1.Value.ToString());
                cmd.Parameters.AddWithValue("@CheckOut", dateTimePicker2.Value.ToString());
                cmd.Parameters.AddWithValue("@Room_No", txtRoomNo.Text);
                cmd.Parameters.AddWithValue("@RoomType_ID", cmbRoomType.SelectedValue);
                cmd.Parameters.Add(new SqlParameter("@Picture", SqlDbType.VarBinary) { Value = ms.ToArray() });

                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Data Inserted successfully!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                con.Close();
                LoadGrid();
                AllClear();

            }
            
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hotelManagementDataSet.tblRoomType' table. You can move, or remove it, as needed.
            this.tblRoomTypeTableAdapter.Fill(this.hotelManagementDataSet.tblRoomType);
            LoadGrid();

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblCustomer WHERE Customer_ID='"+txtId.Text+"'",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count==1)
            {
                txtName.Text = dt.Rows[0][1].ToString();
                txtPhone.Text = dt.Rows[0][2].ToString();
                txtEmail.Text = dt.Rows[0][3].ToString();
                 
                if (dt.Rows[0][4].ToString()=="Male")
                {
                    radioBtnMale.Checked = true;
                }
                else if (dt.Rows[0][4].ToString()=="Female")
                {
                    radioBtnFemale.Checked = true;
                }
                else
                {
                    radioBtnOther.Checked = true;
                }
                txtAddress.Text = dt.Rows[0][5].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0][6].ToString());
                dateTimePicker2.Value = Convert.ToDateTime(dt.Rows[0][7].ToString());
                txtRoomNo.Text = dt.Rows[0][8].ToString();
                cmbRoomType.SelectedValue = dt.Rows[0][9].ToString();
                txtPicture.Text = dt.Rows[0][10].ToString();
                if (dt.Rows[0][10] !=null)
                {
                    byte[] img = (byte[])dt.Rows[0][10];
                    if (img == null)
                    {
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    MessageBox.Show("Photo not found!");
                }
                
            }
            else
            {
                MessageBox.Show("No Data Found !!!");
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
            else if ((radioBtnMale.Checked == false) && (radioBtnFemale.Checked == false) && (radioBtnOther.Checked == false))
            {
                MessageBox.Show("Please Select Gender!!!");
                return false;
            }
            else if (txtAddress.Text == "")
            {
                MessageBox.Show("Address is required!!!");
                return false;
            }
            else if (txtPicture.Text == "")
            {
                MessageBox.Show("Please Insert A Picture!!!");
                return false;
            }
            

            return true;
        }

        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblCustomer", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void AllClear()
        {
            txtId.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtPicture.Clear();
            txtRoomNo.Clear();
            cmbRoomType.SelectedIndex = 0;
            dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            dateTimePicker2.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            if (radioBtnMale.Checked)
            {
                radioBtnMale.Checked = false;
            }
            else if (radioBtnFemale.Checked)
            {
                radioBtnFemale.Checked = false;
            }
            else if (radioBtnOther.Checked)
            {
                radioBtnOther.Checked = false;
            }
            pictureBox1.Image = null;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE tblCustomer WHERE Customer_ID='"+txtId.Text+"'";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data Deleted successfully.");
            LoadGrid();
            AllClear();
        }

        

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            string Gender = "";
            if (radioBtnMale.Checked == true)
            {
                Gender = "Male";
            }
            else if (radioBtnFemale.Checked == true)
            {
                Gender = "Female";
            }
            else if (radioBtnOther.Checked == true)
            {
                Gender = "Other";
            }
            else
            {
                MessageBox.Show("Please Select Gender", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE tblCustomer SET Customer_Name = '" + txtName.Text + "',Phone_Number = '" + txtPhone.Text + "',Email = '" + txtEmail.Text + "',Gender='" + Gender + "',Customer_Address = '" + txtAddress.Text + "',CheckIn='" + dateTimePicker1.Value + "',CheckOut='" + dateTimePicker2.Value + "',Room_No='" + txtRoomNo.Text + "',RoomType_ID='" + cmbRoomType.SelectedValue.ToString() + "' where Customer_ID = '" + txtId.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data Updated successfully!!!");
            LoadGrid();
            AllClear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            AllClear();
        }
    }
}
