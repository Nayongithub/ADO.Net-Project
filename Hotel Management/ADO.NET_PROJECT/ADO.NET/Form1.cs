using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO.NET
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomer fc = new frmCustomer();
            fc.MdiParent = this;
            fc.Show();
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployee fe = new frmEmployee();
            fe.MdiParent = this;
            fe.Show();
        }

        private void reservationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReservation fr = new frmReservation();
            fr.MdiParent = this;
            fr.Show();
        }

        private void registrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistration freg = new frmRegistration();
            freg.MdiParent = this;
            freg.Show();
        }

        private void customerInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomerInfoReport cr = new frmCustomerInfoReport();
            cr.MdiParent = this;
            cr.Show();
        }
    }
}
