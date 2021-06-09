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
        public FrmBackup()
        {
            InitializeComponent();
        }

        private void btnSource_Click(object sender, EventArgs e)
        {

            using (var DiaFolder = new FolderBrowserDialog())
            {
                DialogResult Result = DiaFolder.ShowDialog();

                if (Result == DialogResult.OK && !string.IsNullOrEmpty(DiaFolder.SelectedPath))
                {
                    txtSource.Text = DiaFolder.SelectedPath;
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
    }
}
