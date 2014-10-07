using System.ServiceModel;
using System.Windows;
using BA.WebService.Silverlight.ProductService;
using BA.WebService.Silverlight.TestService;
using BA.WebService.Silverlight.ProductWebService;

namespace BA.WebService.Silverlight
{
    public static class WebServiceClientFactory
    {
        private static readonly string ServerUrl = BuildServerUrl();

        public static ProductServiceClient GetProductService()
        {
            ProductServiceClient client = new ProductServiceClient();

            client.Endpoint.Address = new EndpointAddress(ServerUrl + "WebServices/ProductService.svc");

            return client;
        }

        public static ProductWebServiceSoapClient GetProductWebService()
        {
            ProductWebServiceSoapClient client = new ProductWebServiceSoapClient();

            client.Endpoint.Address = new EndpointAddress(ServerUrl + "WebServices/ProductWebService.asmx");

            return client;
        }

        public static TestServiceClient GetTestService()
        {
            TestServiceClient client = new TestServiceClient();

            client.Endpoint.Address = new EndpointAddress(ServerUrl + "WebServices/TestService.svc");

            return client;
        }

        private static string BuildServerUrl()
        {
            int clientBinIndex = Application.Current.Host.Source.AbsoluteUri.IndexOf("/ClientBin");

            string url = Application.Current.Host.Source.AbsoluteUri.Remove(clientBinIndex);

            if (!url.EndsWith("/"))
            {
                url += "/";
            }

            return url;
        }
    }
}
