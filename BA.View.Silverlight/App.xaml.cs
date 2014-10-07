using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BA.View.Silverlight
{
    public partial class App : Application
    {
        private const string PackageRelativePath = "ClientBin/BA.View.Silverlight.xap";
        private const string SketchPageRelativePath = "Sketch/ShowSketch.aspx?ProductId=";

        public string SketchPageUrl { get; private set; }

        public App()
        {
            this.Startup += this.Application_Startup;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        public new static App Current
        {
            get
            {
                return Application.Current.Cast<App>();
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SetSketchPageUrl();
            this.RootVisual = new MainPage();
        }

        private void SetSketchPageUrl()
        {
            int relativePathLength = PackageRelativePath.Length;
            int startIndex = Host.Source.AbsoluteUri.Length - relativePathLength;
            string hostUrl = Host.Source.AbsoluteUri.Remove(startIndex, relativePathLength);

            SketchPageUrl = hostUrl + SketchPageRelativePath;
        }


        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // a ChildWindow control.
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                ChildWindow errorWin = new ErrorWindow(e.ExceptionObject);
                errorWin.Show();
            }
        }
    }
}