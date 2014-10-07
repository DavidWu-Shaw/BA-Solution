using System;
using BA.Web.Common;
using CRM.Data.Criterias;

namespace BA.Web.Shop.UserControls
{
    public partial class SupplierSearchCriteria : BaseControl
    {
        public const string ControlUrl = @"/Shop/UserControls/SupplierSearchCriteria.ascx";

        private SupplierCriteria _currentCriteria;
        public SupplierCriteria CurrentCriteria
        {
            get
            {
                if (_currentCriteria == null)
                {
                    _currentCriteria = new SupplierCriteria();
                    _currentCriteria.SupplierName = txtName.Text.Trim();
                    _currentCriteria.ZipCode = txtPostalCode.Text.Trim();
                    _currentCriteria.Address = txtAddress.Text.Trim();
                }

                return _currentCriteria;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            lblName.Text = Localize("Shop.UserControls.SupplierSearchCriteria.lblName", "Name:");
            lblAddr.Text = Localize("Shop.UserControls.SupplierSearchCriteria.lblAddr", "Address:");
            lblPostCode.Text = Localize("Shop.UserControls.SupplierSearchCriteria.lblPostCode", "Postal Code:");
        }
    }
}