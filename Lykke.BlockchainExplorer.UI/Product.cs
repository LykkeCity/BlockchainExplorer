using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Lykke.BlockchainExplorer.UI
{
    public static class Product
    {
        private static readonly Lazy<string> _version = new Lazy<string>(GetVersion);
        public static string Version => _version.Value;

        private static string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion;

            return version;
        }
    }
}