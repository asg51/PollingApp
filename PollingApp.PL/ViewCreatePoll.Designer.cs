
namespace PollingApp.PL
{
    partial class ViewCreatePoll
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewCreatePoll));
            this.label1 = new System.Windows.Forms.Label();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.btn_Approve = new MetroFramework.Controls.MetroButton();
            this.btn_Back = new MetroFramework.Controls.MetroButton();
            this.txt_AdminSurname = new MetroFramework.Controls.MetroTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_AdminName = new MetroFramework.Controls.MetroTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_PollingName = new MetroFramework.Controls.MetroTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_AdminPassword = new MetroFramework.Controls.MetroTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_AdminKey = new MetroFramework.Controls.MetroTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.metroPanel1.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(28, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Yeni Seçim Ekleme";
            // 
            // metroPanel1
            // 
            this.metroPanel1.BackColor = System.Drawing.Color.Honeydew;
            this.metroPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("metroPanel1.BackgroundImage")));
            this.metroPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.metroPanel1.Controls.Add(this.label1);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(38, 37);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(334, 49);
            this.metroPanel1.TabIndex = 1;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // metroPanel2
            // 
            this.metroPanel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("metroPanel2.BackgroundImage")));
            this.metroPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.metroPanel2.Controls.Add(this.txt_AdminPassword);
            this.metroPanel2.Controls.Add(this.label5);
            this.metroPanel2.Controls.Add(this.txt_AdminKey);
            this.metroPanel2.Controls.Add(this.label6);
            this.metroPanel2.Controls.Add(this.btn_Approve);
            this.metroPanel2.Controls.Add(this.btn_Back);
            this.metroPanel2.Controls.Add(this.txt_AdminSurname);
            this.metroPanel2.Controls.Add(this.label4);
            this.metroPanel2.Controls.Add(this.txt_AdminName);
            this.metroPanel2.Controls.Add(this.label3);
            this.metroPanel2.Controls.Add(this.txt_PollingName);
            this.metroPanel2.Controls.Add(this.label2);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(38, 108);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(334, 261);
            this.metroPanel2.TabIndex = 2;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // btn_Approve
            // 
            this.btn_Approve.Location = new System.Drawing.Point(170, 215);
            this.btn_Approve.Name = "btn_Approve";
            this.btn_Approve.Size = new System.Drawing.Size(75, 23);
            this.btn_Approve.TabIndex = 11;
            this.btn_Approve.Text = "Onayla";
            this.btn_Approve.UseSelectable = true;
            this.btn_Approve.Click += new System.EventHandler(this.Btn_Approve_Click);
            // 
            // btn_Back
            // 
            this.btn_Back.Location = new System.Drawing.Point(73, 215);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(75, 23);
            this.btn_Back.TabIndex = 10;
            this.btn_Back.Text = "Geri";
            this.btn_Back.UseSelectable = true;
            this.btn_Back.Click += new System.EventHandler(this.Btn_Back_Click);
            // 
            // txt_AdminSurname
            // 
            // 
            // 
            // 
            this.txt_AdminSurname.CustomButton.Image = null;
            this.txt_AdminSurname.CustomButton.Location = new System.Drawing.Point(97, 1);
            this.txt_AdminSurname.CustomButton.Name = "";
            this.txt_AdminSurname.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_AdminSurname.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_AdminSurname.CustomButton.TabIndex = 1;
            this.txt_AdminSurname.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_AdminSurname.CustomButton.UseSelectable = true;
            this.txt_AdminSurname.CustomButton.Visible = false;
            this.txt_AdminSurname.Lines = new string[0];
            this.txt_AdminSurname.Location = new System.Drawing.Point(140, 97);
            this.txt_AdminSurname.MaxLength = 32767;
            this.txt_AdminSurname.Name = "txt_AdminSurname";
            this.txt_AdminSurname.PasswordChar = '\0';
            this.txt_AdminSurname.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_AdminSurname.SelectedText = "";
            this.txt_AdminSurname.SelectionLength = 0;
            this.txt_AdminSurname.SelectionStart = 0;
            this.txt_AdminSurname.ShortcutsEnabled = true;
            this.txt_AdminSurname.Size = new System.Drawing.Size(119, 23);
            this.txt_AdminSurname.TabIndex = 7;
            this.txt_AdminSurname.UseSelectable = true;
            this.txt_AdminSurname.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_AdminSurname.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(56, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Soyad:";
            // 
            // txt_AdminName
            // 
            // 
            // 
            // 
            this.txt_AdminName.CustomButton.Image = null;
            this.txt_AdminName.CustomButton.Location = new System.Drawing.Point(97, 1);
            this.txt_AdminName.CustomButton.Name = "";
            this.txt_AdminName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_AdminName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_AdminName.CustomButton.TabIndex = 1;
            this.txt_AdminName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_AdminName.CustomButton.UseSelectable = true;
            this.txt_AdminName.CustomButton.Visible = false;
            this.txt_AdminName.Lines = new string[0];
            this.txt_AdminName.Location = new System.Drawing.Point(140, 60);
            this.txt_AdminName.MaxLength = 32767;
            this.txt_AdminName.Name = "txt_AdminName";
            this.txt_AdminName.PasswordChar = '\0';
            this.txt_AdminName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_AdminName.SelectedText = "";
            this.txt_AdminName.SelectionLength = 0;
            this.txt_AdminName.SelectionStart = 0;
            this.txt_AdminName.ShortcutsEnabled = true;
            this.txt_AdminName.Size = new System.Drawing.Size(119, 23);
            this.txt_AdminName.TabIndex = 5;
            this.txt_AdminName.UseSelectable = true;
            this.txt_AdminName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_AdminName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(56, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ad:";
            // 
            // txt_PollingName
            // 
            // 
            // 
            // 
            this.txt_PollingName.CustomButton.Image = null;
            this.txt_PollingName.CustomButton.Location = new System.Drawing.Point(97, 1);
            this.txt_PollingName.CustomButton.Name = "";
            this.txt_PollingName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_PollingName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_PollingName.CustomButton.TabIndex = 1;
            this.txt_PollingName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_PollingName.CustomButton.UseSelectable = true;
            this.txt_PollingName.CustomButton.Visible = false;
            this.txt_PollingName.Lines = new string[0];
            this.txt_PollingName.Location = new System.Drawing.Point(140, 23);
            this.txt_PollingName.MaxLength = 32767;
            this.txt_PollingName.Name = "txt_PollingName";
            this.txt_PollingName.PasswordChar = '\0';
            this.txt_PollingName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_PollingName.SelectedText = "";
            this.txt_PollingName.SelectionLength = 0;
            this.txt_PollingName.SelectionStart = 0;
            this.txt_PollingName.ShortcutsEnabled = true;
            this.txt_PollingName.Size = new System.Drawing.Size(119, 23);
            this.txt_PollingName.TabIndex = 3;
            this.txt_PollingName.UseSelectable = true;
            this.txt_PollingName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_PollingName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(56, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Seçim Adı:";
            // 
            // txt_AdminPassword
            // 
            // 
            // 
            // 
            this.txt_AdminPassword.CustomButton.Image = null;
            this.txt_AdminPassword.CustomButton.Location = new System.Drawing.Point(97, 1);
            this.txt_AdminPassword.CustomButton.Name = "";
            this.txt_AdminPassword.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_AdminPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_AdminPassword.CustomButton.TabIndex = 1;
            this.txt_AdminPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_AdminPassword.CustomButton.UseSelectable = true;
            this.txt_AdminPassword.CustomButton.Visible = false;
            this.txt_AdminPassword.Lines = new string[0];
            this.txt_AdminPassword.Location = new System.Drawing.Point(140, 171);
            this.txt_AdminPassword.MaxLength = 32767;
            this.txt_AdminPassword.Name = "txt_AdminPassword";
            this.txt_AdminPassword.PasswordChar = '\0';
            this.txt_AdminPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_AdminPassword.SelectedText = "";
            this.txt_AdminPassword.SelectionLength = 0;
            this.txt_AdminPassword.SelectionStart = 0;
            this.txt_AdminPassword.ShortcutsEnabled = true;
            this.txt_AdminPassword.Size = new System.Drawing.Size(119, 23);
            this.txt_AdminPassword.TabIndex = 9;
            this.txt_AdminPassword.UseSelectable = true;
            this.txt_AdminPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_AdminPassword.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(56, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 18);
            this.label5.TabIndex = 12;
            this.label5.Text = "Şifre:";
            // 
            // txt_AdminKey
            // 
            // 
            // 
            // 
            this.txt_AdminKey.CustomButton.Image = null;
            this.txt_AdminKey.CustomButton.Location = new System.Drawing.Point(97, 1);
            this.txt_AdminKey.CustomButton.Name = "";
            this.txt_AdminKey.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_AdminKey.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_AdminKey.CustomButton.TabIndex = 1;
            this.txt_AdminKey.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_AdminKey.CustomButton.UseSelectable = true;
            this.txt_AdminKey.CustomButton.Visible = false;
            this.txt_AdminKey.Lines = new string[0];
            this.txt_AdminKey.Location = new System.Drawing.Point(140, 134);
            this.txt_AdminKey.MaxLength = 32767;
            this.txt_AdminKey.Name = "txt_AdminKey";
            this.txt_AdminKey.PasswordChar = '\0';
            this.txt_AdminKey.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_AdminKey.SelectedText = "";
            this.txt_AdminKey.SelectionLength = 0;
            this.txt_AdminKey.SelectionStart = 0;
            this.txt_AdminKey.ShortcutsEnabled = true;
            this.txt_AdminKey.Size = new System.Drawing.Size(119, 23);
            this.txt_AdminKey.TabIndex = 8;
            this.txt_AdminKey.UseSelectable = true;
            this.txt_AdminKey.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_AdminKey.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(56, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 18);
            this.label6.TabIndex = 10;
            this.label6.Text = "Anahtar:";
            // 
            // ViewCreatePoll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.metroPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewCreatePoll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreatePoll";
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroTextBox txt_PollingName;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroTextBox txt_AdminSurname;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroTextBox txt_AdminName;
        private System.Windows.Forms.Label label3;
        private MetroFramework.Controls.MetroButton btn_Approve;
        private MetroFramework.Controls.MetroButton btn_Back;
        private MetroFramework.Controls.MetroTextBox txt_AdminPassword;
        private System.Windows.Forms.Label label5;
        private MetroFramework.Controls.MetroTextBox txt_AdminKey;
        private System.Windows.Forms.Label label6;
    }
}