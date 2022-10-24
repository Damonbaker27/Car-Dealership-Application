using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Baker.Damon.Business;
using RRCAGAppDamonBaker;
using RRCAG.Data;


namespace RRCAGDamonBaker
{
    public partial class SalesQuoteForm : Form
    {
        private BindingSource SalesQuoteSource;
        decimal chosenVehicle;
        SalesQuote quote;
        
        private BindingList<Vehicle> vehicles;
        private BindingSource vehiclesSource;

        public SalesQuoteForm()
        {
            InitializeComponent();

            this.Load += SalesQuoteForm_Load;
            this.btnReset.Click += BtnReset_Click;
            this.btnCalculateQuote.Click += BtnCalculateQuote_Click;           
            this.cboVehicle.SelectedIndexChanged += CboVehicle_SelectedIndexChanged;
            this.mnuVehicleSalesViewVehicleInformation.Click += MnuVehicleSalesViewVehicleInformation_Click;
            this.mnuVehicleSalesFileClose.Click += MnuVehicleSalesFileClose_Click;          

            this.SalesQuoteSource = new BindingSource();
            this.SalesQuoteSource.DataSource = typeof(SalesQuote);
         
            this.vehicles = new BindingList<Vehicle>(DataRetriever.GetVehicles());           
            this.vehiclesSource = new BindingSource();
            this.vehiclesSource.DataSource = this.vehicles;

            this.cboVehicle.DataSource = this.vehicles;
            this.cboVehicle.DisplayMember = "StockId";
            cboVehicle.SelectedIndex = -1;
        } 

        /// <summary>
        /// Handles the click event of the File close button.
        /// </summary>
        private void MnuVehicleSalesFileClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the click event of the vehicle information mnu button.
        /// </summary>
        private void MnuVehicleSalesViewVehicleInformation_Click(object sender, EventArgs e)
        {
            VehicleInformationForm form = new VehicleInformationForm(vehiclesSource);

            form.ShowDialog();
        }

        /// <summary>
        /// Handles the click event of the index of combo box changing. 
        /// </summary>
        private void CboVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            this.lblSummarySalesTax.DataBindings.Clear();
            this.lblSummaryVehicleSalesPrice.DataBindings.Clear();
            this.lblSummarySubtotal.DataBindings.Clear();
            this.lblSummaryTotal.DataBindings.Clear();
            this.lblSummaryTradein.DataBindings.Clear();
            this.lblSummaryAmountDue.DataBindings.Clear();

            this.lblSummaryVehicleSalesPrice.Text = string.Empty;
            this.lblSummarySalesTax.Text = string.Empty;
            this.lblSummarySubtotal.Text = string.Empty;
            this.lblSummaryTotal.Text = string.Empty;
            this.lblSummaryTradein.Text = string.Empty;
            this.lblSummaryAmountDue.Text = string.Empty;
            this.nudNumberOfYears.Value = 1;
            this.nudAnnualInterestRate.Value = 5.00m;
            this.lblSummaryOptions.Text = string.Empty;
            this.lblMonthlyPayment.Text = string.Empty;
        }


        /// <summary>
        /// Handles the click event of the calculate quote button.
        /// </summary>
        private void BtnCalculateQuote_Click(object sender, EventArgs e)
        { 
   
            this.errorProvider.SetError(this.cboVehicle, string.Empty);
            this.errorProvider.SetError(this.txtTradeInValue, string.Empty);
            bool errorflag = false;

            if (cboVehicle.SelectedItem == null)
            {
                this.errorProvider.SetError(cboVehicle, "A vehicle must be selected.");
                errorflag = true;
            }

            try
            {
                SalesQuote quote1 = new SalesQuote(1m, 1m, 0.14m);
                quote1.TradeInAmount = Int32.Parse(txtTradeInValue.Text);
            }
            catch(ArgumentOutOfRangeException)
            {
                this.errorProvider.SetError(txtTradeInValue, "Trade-in value cannot be less than 0.");
                errorflag = true;

            }
            catch (FormatException)
            {
                if (txtTradeInValue.Text.Equals(string.Empty))
                {
                    this.errorProvider.SetError(txtTradeInValue, "Trade-in value is a required field.");
                }
                else
                {
                    this.errorProvider.SetError(txtTradeInValue, "Trade-in value cannot contain letters or special characters.");
                }
                
                errorflag = true;
            }


            Accessories chosenAccessory = Accessories.none;
            ExteriorFinish finishChosen = ExteriorFinish.Standard;

            //Validates the checkboxes.
            if (chkLeatherInterior.Checked && chkComputerNavigation.Checked && chkStereoSystem.Checked)
            {
                chosenAccessory = Accessories.All;
            }
        
            else if (chkStereoSystem.Checked && chkLeatherInterior.Checked)
            {
                chosenAccessory = Accessories.StereoAndLeather;
            }
            
            else if (chkStereoSystem.Checked && chkComputerNavigation.Checked)
            {
                chosenAccessory = Accessories.StereoAndNavigation;
            }
            
            else if (chkLeatherInterior.Checked && chkComputerNavigation.Checked)
            {
                chosenAccessory = Accessories.LeatherAndNavigation;
            }
           
            else if (chkComputerNavigation.Checked)
            {
                chosenAccessory = Accessories.ComputerNavigation;
            }

            else if (chkStereoSystem.Checked)
            {
                chosenAccessory = Accessories.StereoSystem;
            }

            else if (chkLeatherInterior.Checked)
            {
                chosenAccessory = Accessories.LeatherInterior;
            }

            else
            {
                chosenAccessory = Accessories.none;
            }

            
            //Validates the radio button input 
            if (radStandard.Checked)
            {
                finishChosen = ExteriorFinish.Standard;
            }

            if (radPearlized.Checked)
            {
                finishChosen = ExteriorFinish.Pearlized;
            }

            if (radCustom.Checked)
            {
                finishChosen = ExteriorFinish.Custom;
            }

           
            Vehicle vehicle = (Vehicle)vehiclesSource.Current;
            decimal vehiclePrice = vehicle.BasePrice;

            try
            {
                if ( vehiclePrice< Int32.Parse(txtTradeInValue.Text))
                {
                    this.errorProvider.SetError(txtTradeInValue, "Trade-in value cannot exceed the vehicle sale price.");
                    errorflag = true;
                }
            }
            catch (FormatException)
            {
                errorflag = true;
            }


            //If everything is valid and the error flag has not been raised.
            if (errorflag == false)
            {

                SalesQuote quote = new SalesQuote(vehiclePrice,Int32.Parse(txtTradeInValue.Text), 0.13m, chosenAccessory, finishChosen);
                this.SalesQuoteSource.DataSource = quote;
                
                BindControls();

                this.lblSummaryOptions.Text = Math.Round(quote.AccessoryCost + quote.FinishCost, 2).ToString("$#.##");

                decimal monthlyPayment = Financial.GetPayment((Decimal.Parse(this.nudAnnualInterestRate.Text) / 100),
                Int32.Parse(this.nudNumberOfYears.Text), quote.SubTotal);

                this.lblMonthlyPayment.Text = monthlyPayment.ToString("$#.##");

                this.mnuVehicleSalesViewVehicleInformation.Enabled = true;
              
            }

        }

        /// <summary>
        /// Handles the click event of the reset button.
        /// </summary>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            string messageBoxText = "Do you want to reset the form?";
            string caption = "Reset Form";
            DialogResult d;
            d = MessageBox.Show(messageBoxText, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if(d == DialogResult.Yes)
            {  
                this.errorProvider.SetError(this.cboVehicle, string.Empty);
                this.errorProvider.SetError(this.txtTradeInValue, string.Empty);

                cboVehicle.SelectedIndex = -1;
                
                this.txtTradeInValue.Text = "0";
                this.lblSummarySalesTax.DataBindings.Clear();
                this.lblSummaryVehicleSalesPrice.DataBindings.Clear();
                this.lblSummarySubtotal.DataBindings.Clear();
                this.lblSummaryTotal.DataBindings.Clear();
                this.lblSummaryTradein.DataBindings.Clear();
                this.lblSummaryAmountDue.DataBindings.Clear();

                this.lblSummaryVehicleSalesPrice.Text = string.Empty;
                this.lblSummarySalesTax.Text = string.Empty;
                this.lblSummarySubtotal.Text = string.Empty;
                this.lblSummaryTotal.Text = string.Empty;
                this.lblSummaryTradein.Text = string.Empty;
                this.lblSummaryAmountDue.Text = string.Empty;

                this.lblSummaryOptions.Text = string.Empty;
                this.lblMonthlyPayment.Text = string.Empty;
                this.radStandard.PerformClick();
                this.chkComputerNavigation.Checked = false;
                this.chkLeatherInterior.Checked = false;
                this.chkStereoSystem.Checked = false;
                this.nudNumberOfYears.Value = 1;
                this.nudAnnualInterestRate.Value = 5.00m;
            }                   

        }

        private void BindControls()
        {
            this.lblSummarySalesTax.DataBindings.Clear();
            this.lblSummaryVehicleSalesPrice.DataBindings.Clear();
            this.lblSummarySubtotal.DataBindings.Clear();
            this.lblSummaryTotal.DataBindings.Clear();
            this.lblSummaryTradein.DataBindings.Clear();
            this.lblSummaryAmountDue.DataBindings.Clear();

            this.lblSummaryVehicleSalesPrice.Text = string.Empty;
            this.lblSummarySalesTax.Text = string.Empty;
            this.lblSummarySubtotal.Text = string.Empty;
            this.lblSummaryTotal.Text = string.Empty;
            this.lblSummaryTradein.Text = string.Empty;
            this.lblSummaryAmountDue.Text = string.Empty;



            Binding vehicleSalePriceBind = new Binding("Text", this.SalesQuoteSource, "VehicleSalePrice");
            this.lblSummaryVehicleSalesPrice.DataBindings.Add(vehicleSalePriceBind);
            vehicleSalePriceBind.FormattingEnabled = true;
            vehicleSalePriceBind.FormatString = "C";

            Binding subtotalBind = new Binding("Text", this.SalesQuoteSource, "SubTotal");
            this.lblSummarySubtotal.DataBindings.Add(subtotalBind);
            subtotalBind.FormattingEnabled = true;
            subtotalBind.FormatString = "C";

            Binding salesTaxBind = new Binding("Text", this.SalesQuoteSource, "SalesTax");
            this.lblSummarySalesTax.DataBindings.Add(salesTaxBind);
            salesTaxBind.FormattingEnabled = true;
            salesTaxBind.FormatString = "C";

            Binding totalBind = new Binding("Text", this.SalesQuoteSource, "Total");
            totalBind.FormattingEnabled = true;
            totalBind.FormatString = "C";
            this.lblSummaryTotal.DataBindings.Add(totalBind);
            

            Binding tradeInBind = new Binding("Text", this.SalesQuoteSource, "TradeInAmount");
            this.lblSummaryTradein.DataBindings.Add(tradeInBind);
            tradeInBind.FormattingEnabled = true;
            tradeInBind.FormatString = "C";

            Binding amountDueBind = new Binding("Text", this.SalesQuoteSource, "AmountDue");
            this.lblSummaryAmountDue.DataBindings.Add(amountDueBind);
            amountDueBind.FormattingEnabled = true;
            amountDueBind.FormatString = "C";

            this.lblSummarySubtotal.DataBindings.Clear();
            this.lblSummarySalesTax.DataBindings.Clear();
            this.lblSummaryTotal.DataBindings.Clear();
            this.lblSummaryTradein.DataBindings.Clear();
            this.lblSummaryAmountDue.DataBindings.Clear();



        }

        
        
        
        
        
        
        
        
        
        
        
        
        



        
        /// <summary>
        /// Handles the load event.
        /// </summary>
        private void SalesQuoteForm_Load(object sender, EventArgs e)
        {          
           //no further implementation.
        }      
                    
        //accidentally made these two at some point and getting rid of them causes issues, so here they be.
        private void label11_Click(object sender, EventArgs e)
        {
        }
        private void label12_Click(object sender, EventArgs e)
        {
        }
    }
}
