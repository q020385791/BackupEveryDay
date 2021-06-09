
namespace BackupEveryDay
{
    partial class FrmBackup
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labSource = new System.Windows.Forms.Label();
            this.labTarget = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.btnSource = new System.Windows.Forms.Button();
            this.btnTarget = new System.Windows.Forms.Button();
            this.btnBackupNow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labSource
            // 
            this.labSource.AutoSize = true;
            this.labSource.Location = new System.Drawing.Point(58, 52);
            this.labSource.Name = "labSource";
            this.labSource.Size = new System.Drawing.Size(62, 18);
            this.labSource.TabIndex = 0;
            this.labSource.Text = "來源：";
            // 
            // labTarget
            // 
            this.labTarget.AutoSize = true;
            this.labTarget.Location = new System.Drawing.Point(4, 84);
            this.labTarget.Name = "labTarget";
            this.labTarget.Size = new System.Drawing.Size(116, 18);
            this.labTarget.TabIndex = 1;
            this.labTarget.Text = "目標資料夾：";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(125, 43);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(449, 29);
            this.txtSource.TabIndex = 2;
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(125, 78);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(449, 29);
            this.txtTarget.TabIndex = 3;
            // 
            // btnSource
            // 
            this.btnSource.Location = new System.Drawing.Point(586, 44);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(123, 28);
            this.btnSource.TabIndex = 4;
            this.btnSource.Text = "選擇來源";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // btnTarget
            // 
            this.btnTarget.Location = new System.Drawing.Point(586, 75);
            this.btnTarget.Name = "btnTarget";
            this.btnTarget.Size = new System.Drawing.Size(123, 28);
            this.btnTarget.TabIndex = 5;
            this.btnTarget.Text = "備份資料夾";
            this.btnTarget.UseVisualStyleBackColor = true;
            this.btnTarget.Click += new System.EventHandler(this.btnTarget_Click);
            // 
            // btnBackupNow
            // 
            this.btnBackupNow.Location = new System.Drawing.Point(586, 109);
            this.btnBackupNow.Name = "btnBackupNow";
            this.btnBackupNow.Size = new System.Drawing.Size(123, 33);
            this.btnBackupNow.TabIndex = 6;
            this.btnBackupNow.Text = "手動備份";
            this.btnBackupNow.UseVisualStyleBackColor = true;
            this.btnBackupNow.Click += new System.EventHandler(this.btnBackupNow_Click);
            // 
            // FrmBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 157);
            this.Controls.Add(this.btnBackupNow);
            this.Controls.Add(this.btnTarget);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.txtTarget);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.labTarget);
            this.Controls.Add(this.labSource);
            this.Name = "FrmBackup";
            this.Text = "資料備份";
            this.Shown += new System.EventHandler(this.FrmBackup_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labSource;
        private System.Windows.Forms.Label labTarget;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Button btnTarget;
        private System.Windows.Forms.Button btnBackupNow;
    }
}

