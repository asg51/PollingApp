
namespace PollingApp.PL
{
    partial class ViewMakingChoices
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Exit = new MetroFramework.Controls.MetroButton();
            this.lbl_VoterName = new System.Windows.Forms.Label();
            this.pnl_Chosens = new MetroFramework.Controls.MetroPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnl_Chosens, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1336, 768);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel2.Controls.Add(this.btn_Exit, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_VoterName, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1330, 70);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btn_Exit
            // 
            this.btn_Exit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Exit.Location = new System.Drawing.Point(21, 9);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(91, 52);
            this.btn_Exit.TabIndex = 2;
            this.btn_Exit.Text = "Çıkış";
            this.btn_Exit.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btn_Exit.UseSelectable = true;
            this.btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // lbl_VoterName
            // 
            this.lbl_VoterName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_VoterName.AutoSize = true;
            this.lbl_VoterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_VoterName.ForeColor = System.Drawing.Color.White;
            this.lbl_VoterName.Location = new System.Drawing.Point(731, 22);
            this.lbl_VoterName.Name = "lbl_VoterName";
            this.lbl_VoterName.Size = new System.Drawing.Size(0, 25);
            this.lbl_VoterName.TabIndex = 3;
            // 
            // pnl_Chosens
            // 
            this.pnl_Chosens.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_Chosens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Chosens.HorizontalScrollbarBarColor = true;
            this.pnl_Chosens.HorizontalScrollbarHighlightOnWheel = false;
            this.pnl_Chosens.HorizontalScrollbarSize = 10;
            this.pnl_Chosens.Location = new System.Drawing.Point(3, 79);
            this.pnl_Chosens.Name = "pnl_Chosens";
            this.pnl_Chosens.Size = new System.Drawing.Size(1330, 686);
            this.pnl_Chosens.TabIndex = 1;
            this.pnl_Chosens.VerticalScrollbarBarColor = true;
            this.pnl_Chosens.VerticalScrollbarHighlightOnWheel = false;
            this.pnl_Chosens.VerticalScrollbarSize = 10;
            // 
            // ViewMakingChoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1336, 768);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewMakingChoices";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewMakingChoices";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private MetroFramework.Controls.MetroButton btn_Exit;
        private System.Windows.Forms.Label lbl_VoterName;
        private MetroFramework.Controls.MetroPanel pnl_Chosens;
    }
}