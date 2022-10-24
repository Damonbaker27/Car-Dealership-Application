using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Baker.Damon.Business;
using RRCAGDamonBaker;

namespace RRCAGDamonBaker
{
    public partial class CarWashForm : Form
    {
        BindingList<String> fragrances = new BindingList<string>();
        private BindingSource fragranceSource;

        BindingList<String> packages = new BindingList<string>();
        private BindingSource packageSource;

        BindingList<String> exteriors = new BindingList<string>();
        private BindingSource exteriorSource;

        BindingList<String> interiors = new BindingList<string>();
        private BindingSource interiorSource;

        private CarWashInvoice invoice;        
        private BindingSource CarWashSource;

        decimal packageCost;
        decimal fragranceCost;

        bool fragranceChosen;
        bool packageChosen;



        public CarWashForm()
        {
            InitializeComponent();

            this.fragrances = new BindingList<string>();
            this.fragranceSource = new BindingSource();
            this.fragranceSource.DataSource = this.fragrances;

            this.packages = new BindingList<string>();
            this.packageSource = new BindingSource();
            this.packageSource.DataSource = this.packages;

            this.exteriors = new BindingList<string>();
            this.exteriorSource = new BindingSource();
            this.exteriorSource.DataSource = this.exteriors;

            this.interiors = new BindingList<string>();
            this.interiorSource = new BindingSource();
            this.interiorSource.DataSource = this.interiors;

            this.invoice = null;
                      
            this.CarWashSource = new BindingSource();
            this.CarWashSource.DataSource = typeof(CarWashInvoice);

            this.Load += CarWashForm_Load;
            this.cboPackage.SelectedIndexChanged += CboPackage_SelectedIndexChanged;
            this.mnuFileGenerateInvoice.Click += MnuFileGenerateInvoice_Click;
            this.cboFragrance.SelectedIndexChanged += CboFragrance_SelectedIndexChanged;
            this.mnuFileExit.Click += MnuFileExit_Click;

            this.mnuFileGenerateInvoice.Enabled = false;
         
            BindControls();

        }

        private void MnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CboFragrance_SelectedIndexChanged(object sender, EventArgs e)
        {
            fragranceChosen = true;
            Calculate();
        }

        /// <summary>
        /// Hnadles the click event of the Generate invoice menu item.
        /// </summary>
        private void MnuFileGenerateInvoice_Click(object sender, EventArgs e)
        {
            CarWashInvoiceForm form = new CarWashInvoiceForm(this.CarWashSource);

            form.ShowDialog();
        }

        /// <summary>
        /// Handles the combo box index changed event.
        /// </summary>
        private void CboPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            packageChosen = true;
            Calculate();
        }


        /// <summary>
        /// handles the load event.
        /// </summary>
        private void CarWashForm_Load(object sender, EventArgs e)
        {
            try
            {
                FileStream fragranceStream = new FileStream("fragrances.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(fragranceStream);

                while (reader.Peek() != -1)
                {
                    string input = reader.ReadLine();
                    input.TrimEnd().Substring(0, (input.Length - 1));
                    fragrances.Add(input);
                }

                reader.Close();
                fragranceStream.Dispose();


                FileStream packageStream = new FileStream("packages.txt", FileMode.Open, FileAccess.Read);
                StreamReader packageReader = new StreamReader(packageStream);

                while (packageReader.Peek() != -1)
                {
                    string input = packageReader.ReadLine();
                    input.Substring(0, (input.Length - 1));
                    packages.Add(input);
                }

                packageReader.Close();
                packageStream.Dispose();
            }
            catch(FileNotFoundException)
            {
                DialogResult removeChoice = MessageBox.Show("Fragrances data file not found.", "Data File Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

                this.Close();
            }

            packageChosen = true;
            fragranceChosen = true;
            Calculate();
        }

        private void BindControls()
        {
            
            this.cboFragrance.DataSource = this.fragranceSource;
            this.cboPackage.DataSource = this.packageSource;
            this.lstExterior.DataSource = this.exteriorSource;
            this.lstInterior.DataSource = this.interiorSource;

            Binding subtotalBind = new Binding("Text", this.CarWashSource, "SubTotal");
            this.lblSubTotal.DataBindings.Add(subtotalBind);
            subtotalBind.FormattingEnabled = true;
            subtotalBind.FormatString = "C";

            Binding gstBind = new Binding("Text", this.CarWashSource, "GoodsAndServicesTaxCharged");
            this.lblGst.DataBindings.Add(gstBind);
            gstBind.FormattingEnabled = true;
            gstBind.FormatString = "N";

            Binding pstBind = new Binding("Text", this.CarWashSource, "ProvincialSalesTaxCharged");
            this.lblPst.DataBindings.Add(pstBind);
            pstBind.FormattingEnabled = true;
            pstBind.FormatString = "N";

            Binding totalBind = new Binding("Text", this.CarWashSource, "Total");
            this.lblTotal.DataBindings.Add(totalBind);
            totalBind.FormattingEnabled = true;
            totalBind.FormatString = "C";
            

        }

        private void Calculate()
        {
            
            
            if (fragranceChosen && packageChosen == true)
            {
                interiors.Clear();
                exteriors.Clear();

                if (cboPackage.SelectedIndex == 0)
                {
                    this.interiors.Add(cboFragrance.SelectedItem.ToString());
                    this.exteriors.Add("Handwash");
                    this.packageCost = 7.50m;
                }

                if (cboPackage.SelectedIndex == 1)
                {
                    this.interiors.Add(cboFragrance.SelectedItem.ToString());
                    this.interiors.Add("Shampoo Carpets");
                    this.exteriors.Add("Handwax");
                    this.packageCost = 15.00m;
                }

                if (cboPackage.SelectedIndex == 2)
                {
                    this.interiors.Add(cboFragrance.SelectedItem.ToString());
                    this.interiors.Add("Shampoo Carpets");
                    this.interiors.Add("Shampoo Upholstery");
                    this.exteriors.Add("Handwax");
                    this.exteriors.Add("Wheel Polish");
                    this.packageCost = 35.00m;
                }

                if (cboPackage.SelectedIndex == 3)
                {
                    this.interiors.Add(cboFragrance.SelectedItem.ToString());
                    this.interiors.Add("Shampoo Carpets");
                    this.interiors.Add("Shampoo Upholstery");
                    this.interiors.Add("Detail Engine Compartment");
                    this.exteriors.Add("Handwax");
                    this.exteriors.Add("Wheel Polish");
                    this.exteriors.Add("Interior Protection Coat");
                    this.packageCost = 55.00m;
                }

                //Pine
                if (cboFragrance.SelectedIndex == 0)
                {
                    fragranceCost = 0.00m;
                }
                //Hawaiian Mist
                if (cboFragrance.SelectedIndex == 1)
                {
                    fragranceCost = 2.75m;
                }
                //Baby Powder
                if (cboFragrance.SelectedIndex == 2)
                {
                    fragranceCost = 1.50m;
                }
                //Country Floral
                if (cboFragrance.SelectedIndex == 3)
                {
                    fragranceCost = 2.25m;
                }
                //Pina Colada
                if (cboFragrance.SelectedIndex == 4)
                {
                    fragranceCost = 0.75m;
                }
                //Vanilla
                if (cboFragrance.SelectedIndex == 5)
                {
                    fragranceCost = 2.00m;
                }

                CarWashInvoice invoice = new CarWashInvoice(0.07m, 0.05m, packageCost, fragranceCost);
                this.CarWashSource.DataSource = invoice;

                this.mnuFileGenerateInvoice.Enabled = true;
            }


        }


    }
}
