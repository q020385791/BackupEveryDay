using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackupEveryDay
{
    public partial class FrmBackup : Form
    {
        private string _Path;
        private string ConfigName="FileConfig";
        public FrmBackup()
        {
            _Path = string.Empty;
            InitializeComponent();
            InitConfig();
        }
        private void FrmBackup_Shown(object sender, EventArgs e)
        {
            txtSource.Text = GetConfig("SourcePath");
            txtTarget.Text = GetConfig("TargetPath");
            Zoom();
            AutoStart(bool.Parse(GetConfig("AutoStart")));
        }
        private void btnSource_Click(object sender, EventArgs e)
        {

            using (var DiaFolder = new FolderBrowserDialog())
            {
                DialogResult Result = DiaFolder.ShowDialog();

                if (Result == DialogResult.OK && !string.IsNullOrEmpty(DiaFolder.SelectedPath))
                {
                    txtSource.Text = DiaFolder.SelectedPath;
                    SetConfig("SourcePath", DiaFolder.SelectedPath);
                }
            }
        }
        private void btnTarget_Click(object sender, EventArgs e)
        {
            using (var DiaFolder = new FolderBrowserDialog())
            {
                DialogResult Result = DiaFolder.ShowDialog();

                if (Result == DialogResult.OK && !string.IsNullOrEmpty(DiaFolder.SelectedPath))
                {
                    txtTarget.Text = DiaFolder.SelectedPath;
                    SetConfig("TargetPath", DiaFolder.SelectedPath);
                }
            }
        }
        private void btnBackupNow_Click(object sender, EventArgs e)
        {
            string SourceDirPath = txtSource.Text;
            string TargetDirPath = Path.Combine(txtTarget.Text, DateTime.Now.ToString("yyyyMMdd"));

            if (!Directory.Exists(TargetDirPath))
            {
                Directory.CreateDirectory(TargetDirPath);
            }

            string[] SourceFiles = Directory.GetFiles(SourceDirPath, "*", SearchOption.AllDirectories);
            string[] SourceFolder = Directory.GetDirectories(SourceDirPath, "*", SearchOption.AllDirectories);

            #region 檢查目標資料夾是否存在
            foreach (var sFolder in SourceFolder)
            {
                string tFolder = sFolder.Replace(SourceDirPath, TargetDirPath);
                if (!Directory.Exists(tFolder))
                {
                    Directory.CreateDirectory(tFolder);
                }
            }
            #endregion

            #region  檢查檔案存在
            for (int i = 0; i < SourceFiles.Count(); i++)
            {
                var SourceFilepath = SourceFiles[i];
                var TargetFilePath = SourceFiles[i].Replace(SourceDirPath, TargetDirPath);

                //檢查檔案
                if (File.Exists(TargetFilePath))
                {
                    File.Delete(TargetFilePath);
                }
                File.Copy(SourceFilepath, TargetFilePath);
            }
            #endregion

        }

        #region 記事本參數
        public void InitConfig()
        {
            _Path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (!File.Exists(Path.Combine(_Path, ConfigName)))
            {
                using (StreamWriter w = File.AppendText(Path.Combine(_Path, ConfigName)))
                {

                    string Defaultstring = '{' + Environment.NewLine
                         + "\"SourcePath\":\"\"," + Environment.NewLine
                         + "\"TargetPath\":\"\"," + Environment.NewLine
                          + "\"AutoStart\":\"true\"" + Environment.NewLine
                         + "}" + Environment.NewLine;
                    w.WriteAsync(Defaultstring);
                    //AutoStart
                }
            }

        }

        public bool SetConfig(string Key, string NewValue)
        {
            var jobject = JObject.Parse(File.ReadAllText(Path.Combine(_Path, ConfigName)));
            jobject[Key] = NewValue;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jobject, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(ConfigName, output);
            return true;
        }

        public string GetConfig(string Key)
        {
            var jobject = JObject.Parse(File.ReadAllText(Path.Combine(_Path, ConfigName)));
            return jobject[Key].ToString();
        }
        #endregion

        private void Zoom()
        {
            NotifyIcon MyNotifyIcon = new NotifyIcon();

            MyNotifyIcon.BalloonTipText = "滑鼠移動到Icon上要顯示的文字";
            MyNotifyIcon.Text = "縮小視窗的標題";

            MyNotifyIcon.Icon = new Icon(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),"logo.ico"));
            MyNotifyIcon.Click += (Mysender, Mye) =>
            {
                //取消在通知欄顯示Icon
                MyNotifyIcon.Visible = false;
                //顯示在工具列
                this.ShowInTaskbar = true;
                //顯示程式的視窗
                this.Show();
            };
            ContextMenu contextMenu = new ContextMenu();
            MenuItem notifyIconMenuItem1 = new MenuItem();
            //可以設定是否可勾選
            notifyIconMenuItem1.Checked = false;
            notifyIconMenuItem1.Index = 1;
            notifyIconMenuItem1.Text = "復原";
            notifyIconMenuItem1.Click += (ItemSender, Iteme) =>
            {
                //取消在通知欄顯示Icon
                MyNotifyIcon.Visible = false;
                //顯示在工具列
                this.ShowInTaskbar = true;
                //顯示程式的視窗
                this.Show();
            };
            contextMenu.MenuItems.Add(notifyIconMenuItem1);
            MyNotifyIcon.ContextMenu = contextMenu;
            //讓程式在工具列中隱藏
            this.ShowInTaskbar = false;
            //隱藏程式本身的視窗
            this.Hide();
            //通知欄顯示Icon
            MyNotifyIcon.Visible = true;

            //通知欄提示 (顯示時間毫秒，標題，內文，類型)
            MyNotifyIcon.ShowBalloonTip(500, "Program still Running", "Your program now listening......", ToolTipIcon.Info);
        }

        public static void AutoStart(bool isAuto)
        {
            try
            {
                if (isAuto == true)
                {
                    RegistryKey R_local = Registry.LocalMachine;
                    //RegistryKey R_local = Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    R_run.SetValue("開機自動執行", Application.ExecutablePath.ToString());
                    R_run.Close();
                    R_local.Close();
                }
                else
                {
                    RegistryKey R_local = Registry.LocalMachine;
                    //RegistryKey R_local = Registry.CurrentUser;
                    RegistryKey R_run = R_local.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    R_run.DeleteValue("開機自動執行", false);
                    R_run.Close();
                    R_local.Close();
                }

     
            }
            catch (Exception)
            {
                MessageBox.Show("您需要管理員許可權修改", "提示");
            }
        }


    }
}
