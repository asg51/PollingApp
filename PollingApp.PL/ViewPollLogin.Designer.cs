
namespace PollingApp.PL
{
    partial class ViewPollLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewPollLogin));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Aprrove = new MetroFramework.Controls.MetroButton();
            this.btn_Cancel = new MetroFramework.Controls.MetroButton();
            this.txt_Ip = new MetroFramework.Controls.MetroTextBox();
            this.txt_PollName = new MetroFramework.Controls.MetroTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(450, 400);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btn_Aprrove, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.btn_Cancel, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.txt_Ip, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.txt_PollName, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(48, 163);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(354, 234);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btn_Aprrove
            // 
            this.btn_Aprrove.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Aprrove.Location = new System.Drawing.Point(215, 161);
            this.btn_Aprrove.Name = "btn_Aprrove";
            this.btn_Aprrove.Size = new System.Drawing.Size(100, 38);
            this.btn_Aprrove.TabIndex = 13;
            this.btn_Aprrove.Text = "Onayla";
            this.btn_Aprrove.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btn_Aprrove.UseSelectable = true;
            this.btn_Aprrove.Click += new System.EventHandler(this.Btn_Aprrove_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Cancel.Location = new System.Drawing.Point(38, 161);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(100, 38);
            this.btn_Cancel.TabIndex = 12;
            this.btn_Cancel.Text = "İptal";
            this.btn_Cancel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btn_Cancel.UseSelectable = true;
            this.btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // txt_Ip
            // 
            this.txt_Ip.Anchor = System.Windows.Forms.AnchorStyles.None;
            // 
            // 
            // 
            this.txt_Ip.CustomButton.Image = null;
            this.txt_Ip.CustomButton.Location = new System.Drawing.Point(122, 1);
            this.txt_Ip.CustomButton.Name = "";
            this.txt_Ip.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_Ip.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_Ip.CustomButton.TabIndex = 1;
            this.txt_Ip.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_Ip.CustomButton.UseSelectable = true;
            this.txt_Ip.CustomButton.Visible = false;
            this.txt_Ip.Lines = new string[0];
            this.txt_Ip.Location = new System.Drawing.Point(193, 110);
            this.txt_Ip.MaxLength = 32767;
            this.txt_Ip.Name = "txt_Ip";
            this.txt_Ip.PasswordChar = '\0';
            this.txt_Ip.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_Ip.SelectedText = "";
            this.txt_Ip.SelectionLength = 0;
            this.txt_Ip.SelectionStart = 0;
            this.txt_Ip.ShortcutsEnabled = true;
            this.txt_Ip.Size = new System.Drawing.Size(144, 23);
            this.txt_Ip.TabIndex = 4;
            this.txt_Ip.UseSelectable = true;
            this.txt_Ip.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_Ip.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txt_PollName
            // 
            this.txt_PollName.Anchor = System.Windows.Forms.AnchorStyles.None;
            // 
            // 
            // 
            this.txt_PollName.CustomButton.Image = null;
            this.txt_PollName.CustomButton.Location = new System.Drawing.Point(122, 1);
            this.txt_PollName.CustomButton.Name = "";
            this.txt_PollName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_PollName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_PollName.CustomButton.TabIndex = 1;
            this.txt_PollName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_PollName.CustomButton.UseSelectable = true;
            this.txt_PollName.CustomButton.Visible = false;
            this.txt_PollName.Lines = new string[0];
            this.txt_PollName.Location = new System.Drawing.Point(193, 52);
            this.txt_PollName.MaxLength = 32767;
            this.txt_PollName.Name = "txt_PollName";
            this.txt_PollName.PasswordChar = '\0';
            this.txt_PollName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_PollName.SelectedText = "";
            this.txt_PollName.SelectionLength = 0;
            this.txt_PollName.SelectionStart = 0;
            this.txt_PollName.ShortcutsEnabled = true;
            this.txt_PollName.Size = new System.Drawing.Size(144, 23);
            this.txt_PollName.TabIndex = 3;
            this.txt_PollName.UseSelectable = true;
            this.txt_PollName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_PollName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seçim Adı:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ip:";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(48, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(354, 154);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(91, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(171, 148);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // ViewChosenLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(450, 400);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewChosenLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewChosenLogin";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroTextBox txt_Ip;
        private MetroFramework.Controls.MetroTextBox txt_PollName;
        private MetroFramework.Controls.MetroButton btn_Aprrove;
        private MetroFramework.Controls.MetroButton btn_Cancel;
    }
}