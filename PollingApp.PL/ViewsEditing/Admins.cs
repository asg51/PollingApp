using MetroFramework.Controls;
using PollingApp.BL;
using PollingApp.BL.Contcat;
using PollingApp.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PollingApp.PL.ViewsEditing
{
    public static class Admins
    {
        public static TableLayoutPanel AddTableVoterName()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 6;
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Height = 50;
            tableLayoutPanel.BackColor = Color.Transparent;
            tableLayoutPanel.Dock = DockStyle.Top;


            tableLayoutPanel.Controls.Add(GetLabel("Ad"), 0, 0);
            tableLayoutPanel.Controls.Add(GetLabel("Soyad"), 1, 0);
            tableLayoutPanel.Controls.Add(GetLabel("Anahtar"), 2, 0);
            tableLayoutPanel.Controls.Add(GetLabel("Şifre"), 3, 0);

            return tableLayoutPanel;
        }
        public static TableLayoutPanel AddTableVoter(Admin Admin)
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 6;
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Height = 50;
            tableLayoutPanel.BackColor = Color.Transparent;
            tableLayoutPanel.Dock = DockStyle.Top;


            MetroButton buttonEdit = new MetroButton
            {
                Anchor = AnchorStyles.None,
                Text = "Düzenle",
                Theme = MetroFramework.MetroThemeStyle.Dark,
                UseSelectable = false,
                Name = "btnEdit_" + Admin.Index.ToString()
            };
            buttonEdit.Click += ButtonEdit_Click;

            MetroButton buttonDelete = new MetroButton
            {
                Anchor = AnchorStyles.None,
                Text = "Sil",
                Theme = MetroFramework.MetroThemeStyle.Dark,
                UseSelectable = false,
                Name = "btnDelete_" + Admin.Index.ToString()
            };
            buttonDelete.Click += ButtonDelete_Click;

            tableLayoutPanel.Controls.Add(GetLabel(Admin.Name), 0, 0);
            tableLayoutPanel.Controls.Add(GetLabel(Admin.Surname), 1, 0);
            tableLayoutPanel.Controls.Add(GetLabel(Admin.Key), 2, 0);
            tableLayoutPanel.Controls.Add(GetLabel(Admin.Password), 3, 0);
            tableLayoutPanel.Controls.Add(buttonEdit, 4, 0);
            tableLayoutPanel.Controls.Add(buttonDelete, 5, 0);

            return tableLayoutPanel;
        }

        private static void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(ViewForms.toBeChosen.GetPoll());

                Managers.adminManager.Delete(FindAdmin(GetAdminIndex(sender as MetroButton)), ViewForms.toBeChosen.GetPoll());
                ViewForms.admins.pnl_Admins.Controls.Remove(FindButtonInTable((sender as MetroButton)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }

        private static void ButtonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(ViewForms.toBeChosen.GetPoll());

                ViewAdminAdd adminAdd = ViewForms.adminAdd ?? (ViewForms.adminAdd = new ViewAdminAdd());
                adminAdd.ShowEdit(ViewForms.toBeChosen.GetPoll(), FindAdmin(GetAdminIndex(sender as MetroButton)));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }
        private static Label GetLabel(string name)
        {
            return new Label
            {
                Text = name,
                Anchor = AnchorStyles.None,
                ForeColor = Color.Black
            };
        }
        private static Admin FindAdmin(int index)
        {
            Poll poll = ViewForms.toBeChosen.GetPoll();
            return poll.Admins[index];
        }
        private static TableLayoutPanel FindButtonInTable(MetroButton button)
        {
            foreach (Control adminConrol in ViewForms.admins.pnl_Admins.Controls)
            {
                foreach (Control control in adminConrol.Controls)
                {
                    if (button == control)
                    {
                        return adminConrol as TableLayoutPanel;
                    }
                }
            }
            return null;
        }
        private static void BlockChainControlState(string message)
        {
            if (message == "Seçim başlatılmış durumdadır artık değişiklik yapamazsınız!" || message == "Seçim Silinmiştir!")
            {
                ViewAdmins admins = ViewForms.admins ?? (ViewForms.admins = new ViewAdmins());
                admins.Hide();
                ViewHomePageForAdmin homePageForAdmin = ViewForms.homePageForAdmin ?? (ViewForms.homePageForAdmin = new ViewHomePageForAdmin());
                homePageForAdmin.Show();
            }
        }
        private static int GetAdminIndex(MetroButton metroButton) => int.Parse((metroButton as MetroButton).Name.Split('_')[1]);
    }
}
