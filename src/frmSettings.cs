using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NekoForWindows
{
    public partial class frmSettings : Form
    {

        readonly string _latestVersionLinkClient = "https://raw.githubusercontent.com/rtaserver/neko/neko-windows/version.txt";
        string _ChangelogCore = string.Empty;
        string _changelogClient = string.Empty;

        string _LatestVersionClient = string.Empty;
        string _LatestVersionCore = string.Empty;

        string _noNewVersionMessage = "You already have the latest version!";
        string _betaVersionMessage = "You are using an experimental version!";
        string _newVersionMessage = "There is a new version available! Do you want to download it now?\nApp will restart in a few seconds.";

        private string NewDownloadLinkClient(string latestVersion)
        {
            return string.Format("https://github.com/rtaserver/neko/releases/download/{0}/NekoForWindows-{0}.exe", latestVersion);
        }

        private string NewDownloadLinkCore(string latestVersion, string Arch)
        {
            return string.Format("https://github.com/MetaCubeX/mihomo/releases/download/{0}/mihomo-windows-{1}-{0}.zip", latestVersion, Arch);
        }

        UpdateForm _updateForm;

        public void CheckForUpdate(bool silentCheck = false)
        {

            WebClient client = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            try
            {
                _LatestVersionClient = client.DownloadString(_latestVersionLinkClient).Trim();
                _changelogClient = client.DownloadString("https://raw.githubusercontent.com/rtaserver/neko/neko-windows/CHANGELOG.md").Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Neko For Windows", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(_LatestVersionClient))
            {
                bool conversionSuccess = float.TryParse(_LatestVersionClient, out float latestVersionFloat);
                if (!conversionSuccess)
                {
                    return;
                }

                if (latestVersionFloat > Program.GetCurrentVersionToFloat())
                {
                    _updateForm = new UpdateForm(_newVersionMessage, true, "client", Module.ParseChangelog(_changelogClient), _LatestVersionClient);
                    if (_updateForm.ShowDialog() == DialogResult.Yes)
                    {
                        frmMain main = new frmMain();
                        main.GoSettings();
                        try
                        {
                            btnCheckClient.Enabled = false;
                            btnCheckCore.Enabled = false;

                            Assembly currentAssembly = Assembly.GetEntryAssembly();

                            if (currentAssembly == null)
                            {
                                currentAssembly = Assembly.GetCallingAssembly();
                            }

                            string appFolder = Path.GetDirectoryName(currentAssembly.Location);
                            string appName = Path.GetFileNameWithoutExtension(currentAssembly.Location);
                            string appExtension = Path.GetExtension(currentAssembly.Location);

                            string archiveFile = Path.Combine(appFolder, "NekoForWindows_old" + appExtension);
                            string appFile = Path.Combine(appFolder, appName + appExtension);
                            string tempFile = Path.Combine(appFolder, "NekoForWindows_tmp" + appExtension);

                            // DOWNLOAD NEW VERSION
                            DownloadFile(NewDownloadLinkClient(_LatestVersionClient), tempFile);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            btnCheckClient.Enabled = true;
                            btnCheckCore.Enabled = true;
                        }
                    }
                }
                else if (latestVersionFloat == Program.GetCurrentVersionToFloat())
                {
                    if (!silentCheck)
                    {
                        _updateForm = new UpdateForm(_noNewVersionMessage, false, "client", string.Empty, _LatestVersionClient);
                        _updateForm.ShowDialog();
                    }
                    
                }
                else
                {
                    if (!silentCheck)
                    {
                        _updateForm = new UpdateForm(_betaVersionMessage, false, "client", string.Empty, _LatestVersionClient);
                        _updateForm.ShowDialog();
                    }
                }
            }
        }
        
        public frmSettings()
        {
            InitializeComponent();
        }

        private void btnCheckClient_Click(object sender, EventArgs e)
        {
            CheckForUpdate();
            
        }


        private async void CheckCoreUpdate()
        {
            WebClient client = new WebClient
            {
                Encoding = Encoding.UTF8
            };

            string latestVersion = string.Empty;
            try
            {
                latestVersion =  await Module.GetLatestTagBodyGithub("MetaCubeX","mihomo");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Neko For Windows", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(latestVersion))
            {
                if (latestVersion != Properties.Settings.Default.CoreVersion)
                {
                    string changelog = await Module.GetLatestTagBodyGithub("MetaCubeX", "mihomo");
                    _updateForm = new UpdateForm(_newVersionMessage, true,"core", Module.ParseChangelog(changelog), latestVersion);
                    if (_updateForm.ShowDialog() == DialogResult.Yes)
                    {
                        try
                        {
                            btnCheckClient.Enabled = false;
                            btnCheckCore.Enabled = false;

                            // STOP NEKO WHEN STARTED
                            frmHome home = new frmHome();
                            if (home.Status() == "ENABLE")
                            {
                                home.StopNeko();
                            }

                            string Arch = string.Empty;
                            if (Module.CheckArchitecture() == Architecture.Arm64.ToString())
                            {
                                Arch = "arm64";
                            }
                            else if (Module.CheckArchitecture() == Architecture.X64.ToString())
                            {
                                Arch = "amd64";
                            }
                            else if (Module.CheckArchitecture() == Architecture.X86.ToString())
                            {
                                Arch = "amd64";
                            }
                            string savepath = Path.Combine(Neko.CorePath, "mihomo.zip");
                            DownloadFile(NewDownloadLinkCore(latestVersion, Arch), savepath);
                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            btnCheckClient.Enabled = true;
                            btnCheckCore.Enabled = true;
                        }
                    }
                }
                else if (latestVersion == Properties.Settings.Default.CoreVersion)
                {
                    _updateForm = new UpdateForm(_noNewVersionMessage, false,"core", string.Empty, latestVersion);
                    _updateForm.ShowDialog();
                }
                else
                {
                    _updateForm = new UpdateForm(_betaVersionMessage, false, "core" ,string.Empty, latestVersion);
                    _updateForm.ShowDialog();
                }
            }
        }

        

        private void btnCheckCore_Click(object sender, EventArgs e)
        {
            CheckCoreUpdate();
        }

        

        private readonly WebClient webClient = new WebClient();

        private void DownloadFile(string url,string savePath)
        {
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

            // Start the asynchronous download
            webClient.DownloadFileAsync(new Uri(url), savePath);
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            PGBar.Visible = true;
            // Update the progress bar
            PGBar.Value = e.ProgressPercentage;
            // Optionally update a label with progress information
            PGBar.Text = $"{e.BytesReceived / 1024} KB / {e.TotalBytesToReceive / 1024} KB";
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // Check if the download was completed successfully
            if (e.Error == null)
            {
                try
                {
                    string targetFilePath = Path.Combine(Neko.CorePath, "mihomo.zip");
                    if (File.Exists(targetFilePath))
                    {
                        MessageBox.Show("Update Core complete!");
                        if (File.Exists(Path.Combine(Neko.CorePath, "mihomo.exe")))
                        {
                            File.Delete(Path.Combine(Neko.CorePath, "mihomo.exe"));
                        }
                        Module.Unzip(targetFilePath, Neko.CorePath);
                        Module.RenameFileEx(Neko.CorePath, "mihomo-windows-*.exe", "mihomo.exe");
                        File.Delete(targetFilePath);
                        Properties.Settings.Default.CoreVersion = _LatestVersionCore;
                        Properties.Settings.Default.Save();
                    }

                    {
                        Assembly currentAssembly = Assembly.GetEntryAssembly();

                        if (currentAssembly == null)
                        {
                            currentAssembly = Assembly.GetCallingAssembly();
                        }
                        string appFolder = Path.GetDirectoryName(currentAssembly.Location);
                        string appName = Path.GetFileNameWithoutExtension(currentAssembly.Location);
                        string appExtension = Path.GetExtension(currentAssembly.Location);

                        string archiveFile = Path.Combine(appFolder, "NekoForWindows_old" + appExtension);
                        string appFile = Path.Combine(appFolder, appName + appExtension);
                        string tempFile = Path.Combine(appFolder, "NekoForWindows_tmp" + appExtension);

                        if (File.Exists(tempFile))
                        {
                            MessageBox.Show("Update Client complete!");
                            // STOP NEKO WHEN STARTED
                            frmHome home = new frmHome();
                            if (home.Status() == "ENABLE")
                            {
                                home.StopNeko();
                            }

                            // DELETE PREVIOUS BACK-UP
                            if (File.Exists(archiveFile))
                            {
                                File.Delete(archiveFile);
                            }

                            // MAKE BACK-UP
                            File.Move(appFile, archiveFile);

                            // PATCH
                            File.Move(tempFile, appFile);

                            // BYPASS SINGLE-INSTANCE MECHANISM
                            Application.Restart();
                        }
                    }
                    
                }
                catch 
                {

                }
            }
            else
            {
                MessageBox.Show($"Error: {e.Error.Message}");
            }

            // Re-enable the download button after downloading
            btnCheckClient.Enabled = true;
            btnCheckCore.Enabled = true;
            PGBar.Visible = false;
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
   
        }
    }
}
