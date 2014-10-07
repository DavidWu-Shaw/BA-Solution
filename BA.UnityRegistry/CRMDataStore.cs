using System.IO;
using Framework.Data.NHibernate;
using Framework.Sql;
using Framework.Core;
using System.Reflection;

namespace BA.UnityRegistry
{
    public class CRMDataStore : NHDataStore
    {
        public CRMDataStore(IConnectionString connection, bool isWebApplication)
            : base(connection, isWebApplication, BuildConfigFilePath(isWebApplication))
        {
            DllFolderPath = ApplicationHelper.GetDllFolderPath(IsWebApplication);
        }

        /// <summary>
        /// Gets or sets the DLL folder path.
        /// </summary>
        /// <value>The DLL folder path.</value>
        private string DllFolderPath
        {
            get;
            set;
        }

        private static string BuildConfigFilePath(bool isWebApplication)
        {
            string path = ApplicationHelper.GetDllFolderPath(isWebApplication);
            return Path.Combine(path, "ConfigFiles\\CRMDataStore.cfg.xml");
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            //string path = ApplicationHelper.GetDllFolderPath(IsWebApplication);
            //string path1 = Path.Combine(path, "SubjectEngineMapping");
            //RegisterMappingDirectory(path1);
            //string path2 = Path.Combine(path, "PAMapping");
            //RegisterMappingDirectory(path2);
            RegisterModuleMappingAssembly("CRM");
            RegisterModuleMappingAssembly("SubjectEngine");
            RegisterModuleMappingAssembly("Setup");
        }

        /// <summary>
        /// Registers a module mapping assembly.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        private void RegisterModuleMappingAssembly(string moduleName)
        {
            string assemblyName = string.Format(@"{0}.Repository.dll", moduleName);

            string assemblyPath = Path.Combine(DllFolderPath,assemblyName);

            Assembly assembly = Assembly.LoadFrom(assemblyPath);

            RegisterMappingAssembly(assembly);
        }        
    }
}
