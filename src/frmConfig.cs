using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NekoForWindows
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Neko.MyDir + @"\neko\config");
        }

        private void YamlConfig()
        {
            try
            {
                string directoryPath = Neko.MyDir + @"\neko\config";

                // Get all files in the directory with a .yaml or .yml extension
                var yamlFiles = Directory.GetFiles(directoryPath, "*.yaml")
                                          .Union(Directory.GetFiles(directoryPath, "*.yml"));

                // Bind the list of full paths to the ComboBox
                cbConfig.DataSource = yamlFiles.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add an event handler for ComboBox selection change if needed
        private void cbConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Access the selected YAML file's full path
            
            string selectedYAMLFilePath = cbConfig.SelectedItem.ToString();

            //if (btnChange.PerformClick())
            // Do something with the selected YAML file's full path...
        }
        private void tmrList_Tick(object sender, EventArgs e)
        {

        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                Module.ReadConfig(cbConfig.SelectedItem.ToString());
                txtPort.Text = Properties.Settings.Default.ConfigPort;
                txtRedir.Text = Properties.Settings.Default.ConfigRedir;
                txtSocks.Text = Properties.Settings.Default.ConfigSocks;
                txtMixed.Text = Properties.Settings.Default.ConfigMixed;
                txtTproxy.Text = Properties.Settings.Default.ConfigTProxy;
                txtMode.Text = Properties.Settings.Default.ConfigMode;
                txtEnhanced.Text = Properties.Settings.Default.ConfigEnhanced;
                txtSecret.Text = Properties.Settings.Default.ConfigSecret;
                txtController.Text = Properties.Settings.Default.ConfigController;
                Properties.Settings.Default.ConfigSelect = cbConfig.SelectedItem.ToString();
            }
            catch {
                
            }
            
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            try
            {
                YamlConfig();
                txtPort.Text = Properties.Settings.Default.ConfigPort;
                txtRedir.Text = Properties.Settings.Default.ConfigRedir;
                txtSocks.Text = Properties.Settings.Default.ConfigSocks;
                txtMixed.Text = Properties.Settings.Default.ConfigMixed;
                txtTproxy.Text = Properties.Settings.Default.ConfigTProxy;
                txtMode.Text = Properties.Settings.Default.ConfigMode;
                txtEnhanced.Text = Properties.Settings.Default.ConfigEnhanced;
                txtSecret.Text = Properties.Settings.Default.ConfigSecret;
                txtController.Text = Properties.Settings.Default.ConfigController;
                cbConfig.SelectedItem = Properties.Settings.Default.ConfigSelect;
            }
            catch
            {

            }
            
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ConfigPort = txtPort.Text;
            Properties.Settings.Default.ConfigRedir = txtRedir.Text;
            Properties.Settings.Default.ConfigSocks = txtSocks.Text;
            Properties.Settings.Default.ConfigMixed = txtMixed.Text;
            Properties.Settings.Default.ConfigTProxy = txtTproxy.Text;
            Properties.Settings.Default.ConfigMode = txtMode.Text;
            Properties.Settings.Default.ConfigEnhanced = txtEnhanced.Text;
            Properties.Settings.Default.ConfigSecret = txtSecret.Text;
            Properties.Settings.Default.ConfigController = txtController.Text;
            Properties.Settings.Default.ConfigSelect = cbConfig.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        private void cbConfig_Click(object sender, EventArgs e)
        {
            YamlConfig();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
