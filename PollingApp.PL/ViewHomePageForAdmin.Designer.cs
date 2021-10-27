
namespace PollingApp.PL
{
    partial class ViewHomePageForAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewHomePageForAdmin));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_ManageElections = new MetroFramework.Controls.MetroButton();
            this.btn_NewPoll = new MetroFramework.Controls.MetroButton();
            this.btn_Exit = new MetroFramework.Controls.MetroButton();
            this.pnl_Polls = new MetroFramework.Controls.MetroPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_IpAddress = new System.Windows.Forms.Label();
            this.pnl_Chosens = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnl_Chosens, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1336, 768);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pnl_Polls, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(938, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(395, 762);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel3.Controls.Add(this.btn_ManageElections, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_NewPoll, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_Exit, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(389, 70);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // btn_ManageElections
            // 
            this.btn_ManageElections.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_ManageElections.Location = new System.Drawing.Point(18, 9);
            this.btn_ManageElections.Name = "btn_ManageElections";
            this.btn_ManageElections.Size = new System.Drawing.Size(91, 52);
            this.btn_ManageElections.TabIndex = 2;
            this.btn_ManageElections.Text = "Seçim Yönet";
            this.btn_ManageElections.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btn_ManageElections.UseSelectable = true;
            this.btn_ManageElections.Click += new System.EventHandler(this.Btn_ManageElections_Click);
            // 
            // btn_NewPoll
            // 
            this.btn_NewPoll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_NewPoll.Location = new System.Drawing.Point(148, 9);
            this.btn_NewPoll.Name = "btn_NewPoll";
            this.btn_NewPoll.Size = new System.Drawing.Size(91, 52);
            this.btn_NewPoll.TabIndex = 0;
            this.btn_NewPoll.Text = "Yeni Seçim";
            this.btn_NewPoll.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btn_NewPoll.UseSelectable = true;
            this.btn_NewPoll.Click += new System.EventHandler(this.Btn_NewPoll_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Exit.Location = new System.Drawing.Point(279, 9);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(91, 52);
            this.btn_Exit.TabIndex = 1;
            this.btn_Exit.Text = "Çıkış";
            this.btn_Exit.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btn_Exit.UseSelectable = true;
            this.btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // pnl_Polls
            // 
            this.pnl_Polls.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_Polls.BackgroundImage")));
            this.pnl_Polls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_Polls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Polls.HorizontalScrollbarBarColor = true;
            this.pnl_Polls.HorizontalScrollbarHighlightOnWheel = false;
            this.pnl_Polls.HorizontalScrollbarSize = 10;
            this.pnl_Polls.Location = new System.Drawing.Point(3, 79);
            this.pnl_Polls.Name = "pnl_Polls";
            this.pnl_Polls.Size = new System.Drawing.Size(389, 451);
            this.pnl_Polls.TabIndex = 1;
            this.pnl_Polls.VerticalScrollbarBarColor = true;
            this.pnl_Polls.VerticalScrollbarHighlightOnWheel = false;
            this.pnl_Polls.VerticalScrollbarSize = 10;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.lbl_IpAddress, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 536);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(389, 223);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // lbl_IpAddress
            // 
            this.lbl_IpAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_IpAddress.AutoSize = true;
            this.lbl_IpAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_IpAddress.ForeColor = System.Drawing.Color.White;
            this.lbl_IpAddress.Location = new System.Drawing.Point(3, 10);
            this.lbl_IpAddress.Name = "lbl_IpAddress";
            this.lbl_IpAddress.Size = new System.Drawing.Size(0, 24);
            this.lbl_IpAddress.TabIndex = 0;
            // 
            // pnl_Chosens
            // 
            this.pnl_Chosens.AutoScroll = true;
            this.pnl_Chosens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Chosens.Location = new System.Drawing.Point(3, 3);
            this.pnl_Chosens.Name = "pnl_Chosens";
            this.pnl_Chosens.Size = new System.Drawing.Size(929, 762);
            this.pnl_Chosens.TabIndex = 1;
            // 
            // ViewHomePageForAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1336, 768);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewHomePageForAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HomePage";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.ViewHomePageForAdmin_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private MetroFramework.Controls.MetroButton btn_NewPoll;
        private MetroFramework.Controls.MetroButton btn_Exit;
        private MetroFramework.Controls.MetroPanel pnl_Polls;
        private MetroFramework.Controls.MetroButton btn_ManageElections;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lbl_IpAddress;
        internal System.Windows.Forms.Panel pnl_Chosens;
    }
}