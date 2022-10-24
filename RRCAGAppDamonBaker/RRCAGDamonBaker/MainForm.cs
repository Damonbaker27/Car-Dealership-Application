using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RRCAGAppDamonBaker;
using RRCAG.Data;

namespace RRCAGDamonBaker
{
    public partial class MainForm : Form
    {
        
        
        public MainForm()
        {
            InitializeComponent();

            this.mnuFileOpenSalesQuote.Click += MnuFileOpenSalesQuote_Click;
            this.mnuFileExit.Click += MnuFileExit_Click;
            this.mnuHelpAbout.Click += MnuHelpAbout_Click;
            this.mnuFileOpenCarWash.Click += MnuFileOpenCarWash_Click;
            this.mnuDataVehicle.Click += MnuDataVehicle_Click;

            
        }
     

        private void MnuDataVehicle_Click(object sender, EventArgs e)
        {
           // VehicleInformationForm form = new VehicleInformationForm(BindingList<Vehicle>));

           // form.ShowDialog();
        }

        private void MnuFileOpenCarWash_Click(object sender, EventArgs e)
        {
            CarWashForm form = new CarWashForm();

            form.Show();
        }

        /// <summary>
        /// Handles the click event of the about menu item.
        /// </summary>
        private void MnuHelpAbout_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();

            form.ShowDialog();
        }

        /// <summary>
        /// Handles the click event of the Exit menu item.
        /// </summary>
        private void MnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the click event of the Salesquote menu item.
        /// </summary>
        private void MnuFileOpenSalesQuote_Click(object sender, EventArgs e)
        {
            SalesQuoteForm form= new SalesQuoteForm();

            form.ShowDialog();          
        }

        /// <summary>
        /// Handles the click event of the vehicle menu item.
        /// </summary>
        private void mnuDataVehicle_Click(object sender, EventArgs e)
        {

        }
    }
}
