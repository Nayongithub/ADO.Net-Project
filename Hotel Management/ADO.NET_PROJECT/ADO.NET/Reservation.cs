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
    public partial class frmReservation : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=HotelManagement;Integrated Security=True");
        public frmReservation()
        {
            InitializeComponent();
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
            else if (txtTotalCost.Text == "")
            {
                MessageBox.Show("Total Cost is required!!!");
                return false;
            }
            else if (txtTotaldays.Text == "")
            {
                MessageBox.Show("Total days is required!!!");
                return false;
            }

            return true;
        }
        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblReservation", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void AllClear()
        {
            txtId.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtTotaldays.Clear();
            txtTotalCost.Clear();
            cmbRoomType.SelectedIndex = -1;
            dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            dateTimePicker2.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (Validation() == true)
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tblReservation (Name,Phone,CheckIn,CheckOut,TotalDays,TotalCost,RoomType_ID) VALUES(@Name,@Phone,@CheckIn,@CheckOut,@TotalDays,@TotalCost,@RoomType_ID)", con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@CheckIn", dateTimePicker1.Value.ToString());
                cmd.Parameters.AddWithValue("@CheckOut", dateTimePicker2.Value.ToString());
                cmd.Parameters.AddWithValue("@TotalDays", txtTotaldays.Text);
                cmd.Parameters.AddWithValue("@TotalCost", txtTotalCost.Text);
                cmd.Parameters.AddWithValue("@RoomType_ID", cmbRoomType.SelectedValue);

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

        private void frmReservation_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hotelManagementDataSet2.tblRoomType' table. You can move, or remove it, as needed.
            this.tblRoomTypeTableAdapter.Fill(this.hotelManagementDataSet2.tblRoomType);
            LoadGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE tblReservation WHERE Reservation_ID='" + txtId.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data Deleted successfully.");
            LoadGrid();
            AllClear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblReservation WHERE Reservation_ID='" + txtId.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                txtName.Text = dt.Rows[0][1].ToString();
                txtPhone.Text = dt.Rows[0][2].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0][3].ToString());
                dateTimePicker2.Value = Convert.ToDateTime(dt.Rows[0][4].ToString());
                txtTotaldays.Text = dt.Rows[0][5].ToString();
                txtTotalCost.Text = dt.Rows[0][6].ToString();
                cmbRoomType.SelectedValue = dt.Rows[0][7].ToString();
            }
            else
            {
                MessageBox.Show("No Data Found !!!");
            }
        }

        private void cmbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE tblReservation SET Name = '" + txtName.Text + "',Phone = '" + txtPhone.Text + "',CheckIn='" + dateTimePicker1.Value + "',CheckOut='" + dateTimePicker2.Value + "',TotalDays='" + txtTotaldays.Text + "',TotalCost='"+txtTotalCost.Text+"',RoomType_ID='" + cmbRoomType.SelectedValue.ToString() + "' where Reservation_ID = '" + txtId.Text + "'";
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
