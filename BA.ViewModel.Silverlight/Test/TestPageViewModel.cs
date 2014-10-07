using System;
using System.Collections.ObjectModel;
using BA.WebService.Silverlight.ProductWebService;
using BA.WebService.Silverlight;

namespace BA.ViewModel.Silverlight.Test
{
    public class TestPageViewModel : Framework.UI.ViewModel
    {
        public ProductWebServiceSoapClient ProductService { get; set; }

        public ObservableCollection<SupplierInfoDto> Suppliers
        {
            get
            {
                return GetValue(() => Suppliers);
            }
            set
            {
                SetValue(() => Suppliers, value);
            }
        }

        public SupplierInfoDto CurrentSupplier
        {
            get
            {
                return GetValue(() => CurrentSupplier);
            }
            set
            {
                SetValue(() => CurrentSupplier, value);
                GetProductsByCurrentSupplier();
            }
        }

        public ObservableCollection<ProductInfoDto> Products
        {
            get
            {
                return GetValue(() => Products);
            }
            set
            {
                SetValue(() => Products, value);
            }
        }

        public TestPageViewModel()
        {
            InitServices();
            InitServiceEvents();
            InitModel();
        }

        private void InitServices()
        {
            ProductService = WebServiceClientFactory.GetProductWebService();
        }

        private void InitServiceEvents()
        {
            ProductService.RetrieveAllSupplierCompleted += new EventHandler<RetrieveAllSupplierCompletedEventArgs>(ProductService_RetrieveAllSupplierCompleted);
            ProductService.RetrieveProductsBySupplierCompleted += new EventHandler<RetrieveProductsBySupplierCompletedEventArgs>(ProductService_RetrieveProductsBySupplierCompleted);
        }

        private void InitModel()
        {
            ProductService.RetrieveAllSupplierAsync();
        }

        private void GetProductsByCurrentSupplier()
        {
            if (CurrentSupplier != null)
            {
                ProductService.RetrieveProductsBySupplierAsync(Convert.ToInt32(CurrentSupplier.SupplierId));
            }
        }

        protected void ProductService_RetrieveProductsBySupplierCompleted(object sender, RetrieveProductsBySupplierCompletedEventArgs e)
        {
            Products = e.Result;
        }

        protected void ProductService_RetrieveAllSupplierCompleted(object sender, RetrieveAllSupplierCompletedEventArgs e)
        {
            Suppliers = e.Result;
        }
    }
}
