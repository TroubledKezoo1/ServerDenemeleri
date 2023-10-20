namespace KOMAKRO
{
    partial class KOMacro
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KOMacro));
            this.btnCreateStop = new System.Windows.Forms.Button();
            this.btnCreateStart = new System.Windows.Forms.Button();
            this.txtCreateUserName = new System.Windows.Forms.TextBox();
            this.txtCreateLobyName = new System.Windows.Forms.TextBox();
            this.btnJoinConnect = new System.Windows.Forms.Button();
            this.txtJoinUserName = new System.Windows.Forms.TextBox();
            this.txtJoinLobyName = new System.Windows.Forms.TextBox();
            this.btnJoinStop = new System.Windows.Forms.Button();
            this.X = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreateStop
            // 
            this.btnCreateStop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCreateStop.BackgroundImage")));
            this.btnCreateStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCreateStop.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCreateStop.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCreateStop.Location = new System.Drawing.Point(518, 478);
            this.btnCreateStop.Margin = new System.Windows.Forms.Padding(6);
            this.btnCreateStop.Name = "btnCreateStop";
            this.btnCreateStop.Size = new System.Drawing.Size(233, 32);
            this.btnCreateStop.TabIndex = 8;
            this.btnCreateStop.Text = "Baglantiyi kes";
            this.btnCreateStop.UseVisualStyleBackColor = true;
            this.btnCreateStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnCreateStart
            // 
            this.btnCreateStart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCreateStart.BackgroundImage")));
            this.btnCreateStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCreateStart.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCreateStart.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCreateStart.Location = new System.Drawing.Point(518, 436);
            this.btnCreateStart.Margin = new System.Windows.Forms.Padding(6);
            this.btnCreateStart.Name = "btnCreateStart";
            this.btnCreateStart.Size = new System.Drawing.Size(233, 30);
            this.btnCreateStart.TabIndex = 7;
            this.btnCreateStart.Text = "Lobi olustur";
            this.btnCreateStart.UseVisualStyleBackColor = true;
            this.btnCreateStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtCreateUserName
            // 
            this.txtCreateUserName.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtCreateUserName.Location = new System.Drawing.Point(477, 394);
            this.txtCreateUserName.Margin = new System.Windows.Forms.Padding(6);
            this.txtCreateUserName.Name = "txtCreateUserName";
            this.txtCreateUserName.Size = new System.Drawing.Size(331, 26);
            this.txtCreateUserName.TabIndex = 5;
            this.txtCreateUserName.Text = "Port";
            this.txtCreateUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCreateUserName.Click += new System.EventHandler(this.txtServerPort_Click);
            // 
            // txtCreateLobyName
            // 
            this.txtCreateLobyName.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtCreateLobyName.Location = new System.Drawing.Point(477, 350);
            this.txtCreateLobyName.Margin = new System.Windows.Forms.Padding(6);
            this.txtCreateLobyName.Name = "txtCreateLobyName";
            this.txtCreateLobyName.Size = new System.Drawing.Size(331, 26);
            this.txtCreateLobyName.TabIndex = 6;
            this.txtCreateLobyName.Text = "IP Adresi";
            this.txtCreateLobyName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCreateLobyName.Click += new System.EventHandler(this.txtServerHost_Click);
            // 
            // btnJoinConnect
            // 
            this.btnJoinConnect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnJoinConnect.BackgroundImage")));
            this.btnJoinConnect.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.btnJoinConnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnJoinConnect.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnJoinConnect.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnJoinConnect.Location = new System.Drawing.Point(78, 436);
            this.btnJoinConnect.Margin = new System.Windows.Forms.Padding(6);
            this.btnJoinConnect.Name = "btnJoinConnect";
            this.btnJoinConnect.Size = new System.Drawing.Size(233, 30);
            this.btnJoinConnect.TabIndex = 15;
            this.btnJoinConnect.Text = "Kullaniciya baglan";
            this.btnJoinConnect.UseVisualStyleBackColor = true;
            this.btnJoinConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtJoinUserName
            // 
            this.txtJoinUserName.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtJoinUserName.Location = new System.Drawing.Point(27, 394);
            this.txtJoinUserName.Margin = new System.Windows.Forms.Padding(6);
            this.txtJoinUserName.Name = "txtJoinUserName";
            this.txtJoinUserName.Size = new System.Drawing.Size(331, 26);
            this.txtJoinUserName.TabIndex = 13;
            this.txtJoinUserName.Text = "Port";
            this.txtJoinUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtJoinUserName.Click += new System.EventHandler(this.txtClientPort_Click);
            // 
            // txtJoinLobyName
            // 
            this.txtJoinLobyName.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtJoinLobyName.Location = new System.Drawing.Point(27, 350);
            this.txtJoinLobyName.Margin = new System.Windows.Forms.Padding(6);
            this.txtJoinLobyName.Name = "txtJoinLobyName";
            this.txtJoinLobyName.Size = new System.Drawing.Size(331, 26);
            this.txtJoinLobyName.TabIndex = 14;
            this.txtJoinLobyName.Text = "IP Adresi";
            this.txtJoinLobyName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtJoinLobyName.Click += new System.EventHandler(this.txtClientHost_Click);
            // 
            // btnJoinStop
            // 
            this.btnJoinStop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnJoinStop.BackgroundImage")));
            this.btnJoinStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnJoinStop.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnJoinStop.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnJoinStop.Location = new System.Drawing.Point(78, 478);
            this.btnJoinStop.Name = "btnJoinStop";
            this.btnJoinStop.Size = new System.Drawing.Size(233, 32);
            this.btnJoinStop.TabIndex = 16;
            this.btnJoinStop.Text = "Baglantiyi kes";
            this.btnJoinStop.UseVisualStyleBackColor = true;
            this.btnJoinStop.Click += new System.EventHandler(this.btnClientStop_Click);
            // 
            // X
            // 
            this.X.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("X.BackgroundImage")));
            this.X.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.X.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.X.Location = new System.Drawing.Point(12, 12);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(33, 34);
            this.X.TabIndex = 17;
            this.X.Text = "X";
            this.X.UseVisualStyleBackColor = true;
            this.X.Click += new System.EventHandler(this.X_Click);
            // 
            // KOMacro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(844, 562);
            this.Controls.Add(this.X);
            this.Controls.Add(this.btnJoinStop);
            this.Controls.Add(this.btnJoinConnect);
            this.Controls.Add(this.btnCreateStart);
            this.Controls.Add(this.txtJoinUserName);
            this.Controls.Add(this.btnCreateStop);
            this.Controls.Add(this.txtJoinLobyName);
            this.Controls.Add(this.txtCreateLobyName);
            this.Controls.Add(this.txtCreateUserName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "KOMacro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KOMacro";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KOMacro_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.KOMacro_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KOMacro_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCreateStop;
        private System.Windows.Forms.Button btnCreateStart;
        private System.Windows.Forms.TextBox txtCreateUserName;
        private System.Windows.Forms.TextBox txtCreateLobyName;
        private System.Windows.Forms.Button btnJoinConnect;
        private System.Windows.Forms.TextBox txtJoinUserName;
        private System.Windows.Forms.TextBox txtJoinLobyName;
        private System.Windows.Forms.Button btnJoinStop;
        private System.Windows.Forms.Button X;
    }
}

