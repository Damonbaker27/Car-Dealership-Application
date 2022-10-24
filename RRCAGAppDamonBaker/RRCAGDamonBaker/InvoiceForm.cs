using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RRCAGDamonBaker
{
    public partial class InvoiceForm : Form
    {
        public InvoiceForm()
        {
            InitializeComponent();

            this.lblDate.Text = DateTime.Now.ToString("M/d/yyyy");
            this.lblInvoiceTitle.Text = "Car Wash Invoice";
        }

        private void lblCity_Click(object sender, EventArgs e)
        {

        }
    }
}
