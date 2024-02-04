using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace NekoForWindows
{
    internal static class Program
    {
        /// <summary>
        /// Version properties. Do NOT leave them empty
        /// </summary>
        internal readonly static float Major = 1;
        internal readonly static float Minor = 0;
        //internal readonly static bool EXPERIMENTAL_BUILD = false;

        internal static string GetCurrentVersionTostring()
        {
            return $"{Major.ToString()}.{Minor.ToString()}";
        }

        internal static float GetCurrentVersionToFloat()
        {
            return float.Parse(GetCurrentVersionTostring());
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            const string _json = @"NekoForWindows.Newtonsoft.Json.dll";
            const string _Siticone = @"NekoForWindows.Siticone.Desktop.UI.dll";
            const string _Buffers = @"NekoForWindows.System.Buffers.dll";
            const string _Vectors = @"NekoForWindows.System.Numerics.Vectors.dll";
            const string _Yaml = @"NekoForWindows.YamlDotNet.dll";
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            EmbeddedAssembly.Load(_json, _json.Replace("NekoForWindows.", string.Empty));
            EmbeddedAssembly.Load(_Siticone, _Siticone.Replace("NekoForWindows.", string.Empty));
            EmbeddedAssembly.Load(_Buffers, _Buffers.Replace("NekoForWindows.", string.Empty));
            EmbeddedAssembly.Load(_Vectors, _Vectors.Replace("NekoForWindows.", string.Empty));
            EmbeddedAssembly.Load(_Yaml, _Yaml.Replace("NekoForWindows.", string.Empty));

            const string _NewFile = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<configuration>\r\n\t<configSections>\r\n  <sectionGroup name=\"userSettings\" type=\"System.Configuration.UserSettingsGroup, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\" >\r\n   <section name=\"NekoForWindows.Properties.Settings\" type=\"System.Configuration.ClientSettingsSection, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\" allowExeDefinition=\"MachineToLocalUser\" requirePermission=\"false\" />\r\n  </sectionGroup>\r\n </configSections>\r\n <startup>\r\n\r\n\t\t<supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.8.1\"/>\r\n\t</startup>\r\n <userSettings>\r\n  <NekoForWindows.Properties.Settings>\r\n   <setting name=\"CoreVersion\" serializeAs=\"String\">\r\n    <value>v1.17.0</value>\r\n   </setting>\r\n   <setting name=\"ClientVersion\" serializeAs=\"String\">\r\n    <value />\r\n   </setting>\r\n   <setting name=\"ConfigPort\" serializeAs=\"String\">\r\n    <value />\r\n   </setting>\r\n   <setting name=\"ConfigRedir\" serializeAs=\"String\">\r\n    <value />\r\n   </setting>\r\n   <setting name=\"ConfigSocks\" serializeAs=\"String\">\r\n    <value />\r\n   </setting>\r\n   <setting name=\"ConfigMixed\" serializeAs=\"String\">\r\n    <value />\r\n   </setting>\r\n   <setting name=\"ConfigEnhanced\" serializeAs=\"String\">\r\n    <value />\r\n   </setting>\r\n   <setting name=\"ConfigTProxy\" serializeAs=\"String\">\r\n    <value />\r\n   </setting>\r\n   <setting name=\"ConfigSecret\" serializeAs=\"String\">\r\n    <value />\r\n   </setting>\r\n   <setting name=\"ConfigMode\" serializeAs=\"String\">\r\n    <value />\r\n   </setting>\r\n   <setting name=\"ConfigController\" serializeAs=\"String\">\r\n    <value />\r\n   </setting>\r\n   <setting name=\"ConfigSelect\" serializeAs=\"String\">\r\n    <value />\r\n   </setting>\r\n   <setting name=\"FirstRun\" serializeAs=\"String\">\r\n    <value>True</value>\r\n   </setting>\r\n  </NekoForWindows.Properties.Settings>\r\n </userSettings>\r\n</configuration>";
            string _FileName = $"{Application.ExecutablePath.Replace(Application.StartupPath + "\\", "")}.config";
            if (File.Exists(Application.StartupPath + _FileName))
            {
                
                File.Delete(_FileName);
                string startupPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(startupPath, _FileName);

                try
                {
                    File.WriteAllText(filePath, _NewFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            else
            {
                string startupPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(startupPath, _FileName);
                try
                {
                    File.WriteAllText(filePath, _NewFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());


        }
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = (Exception)e.ExceptionObject;
            MessageBox.Show($"Program.Main-UnhandledException, {error.Message}, {error.StackTrace}");
        }


    }
}
