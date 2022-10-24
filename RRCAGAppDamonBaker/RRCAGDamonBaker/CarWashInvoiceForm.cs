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

namespace RRCAGDamonBaker
{
    public partial class CarWashInvoiceForm : InvoiceForm
    {
        private BindingSource carWashSource;
              
        public CarWashInvoiceForm(BindingSource source)
        {
            InitializeComponent();

            this.carWashSource = source;                             
            
            BindControls();

        }



        private void BindControls()
        {

            Binding packageBind = new Binding("Text", this.carWashSource, "PackageCost");
            this.lblpackagePrice.DataBindings.Add(packageBind);
            packageBind.FormattingEnabled = true;
            packageBind.FormatString = "C";

            Binding fragranceBind = new Binding("Text", this.carWashSource, "FragranceCost");
            this.lblfragrancePrice.DataBindings.Add(fragranceBind);
            fragranceBind.FormattingEnabled = true;
            fragranceBind.FormatString = "N";

            Binding taxesBind = new Binding("Text", this.carWashSource, "GoodsAndServicesTaxCharged");
            this.lblTaxes.DataBindings.Add(taxesBind);
            taxesBind.FormattingEnabled = true;
            taxesBind.FormatString = "N";

            Binding totalBind = new Binding("Text", this.carWashSource, "Total");
            this.lbltotal.DataBindings.Add(totalBind);
            totalBind.FormattingEnabled = true;
            totalBind.FormatString = "C";

            Binding subtotalBind = new Binding("Text", this.carWashSource, "SubTotal");
            this.lblSubtotal.DataBindings.Add(subtotalBind);
            subtotalBind.FormattingEnabled = true;
            subtotalBind.FormatString = "C";

        }
    }
}
