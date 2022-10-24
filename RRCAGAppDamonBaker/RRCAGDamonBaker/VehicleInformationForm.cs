using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RRCAG.Data;

namespace RRCAGDamonBaker
{   
    public partial class VehicleInformationForm : Form
    {
        private BindingSource vehiclesSource;
        Vehicle honda;

        public VehicleInformationForm(BindingSource source)
        {
            InitializeComponent();

            this.btnVehicleInformationClose.Click += BtnVehicleInformationClose_Click;

            this.vehiclesSource = source;

            this.Text =vehiclesSource.Current.ToString();

            BindControls();

            honda = (Vehicle)vehiclesSource.Current;

            
        }


        private void BindControls()
        {

            Binding stockIdBinding = new Binding("Text", this.vehiclesSource, "StockID");
            this.lblVehicleInfoStockId.DataBindings.Add(stockIdBinding);

            Binding yearBinding = new Binding("Text", this.vehiclesSource, "ManufacturedYear");
            this.lblVehicleInfoYear.DataBindings.Add(yearBinding);

            Binding manufactureBinding = new Binding("Text", this.vehiclesSource, "Manufacturer");
            this.lblVehicleInfoManufacturer.DataBindings.Add(manufactureBinding);

            Binding ModelBinding = new Binding("Text", this.vehiclesSource, "Model");
            this.lblVehicleInfoModel.DataBindings.Add(ModelBinding);

            Binding transmissionBinding = new Binding("Text", this.vehiclesSource, "IsAutomatic");
           

            this.lblVehicleInfoTransmission.DataBindings.Add(transmissionBinding);

            Binding mileageBinding = new Binding("Text", this.vehiclesSource, "Mileage");
            this.lblVehicleInfoMileage.DataBindings.Add(mileageBinding);
            mileageBinding.FormattingEnabled = true;
            mileageBinding.FormatString = "N0";

            Binding colorBinding = new Binding("Text", this.vehiclesSource, "Colour");
            this.lblColor.DataBindings.Add(colorBinding);

            Binding basePriceBinding = new Binding("Text", this.vehiclesSource, "BasePrice");
            this.lblVehicleInfoBasePrice.DataBindings.Add(basePriceBinding);
            basePriceBinding.FormattingEnabled = true;
            basePriceBinding.FormatString = "C";








        }






















        private void BtnVehicleInformationClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
