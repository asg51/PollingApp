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
    public static class Voters
    {
        public static TableLayoutPanel AddTableVoterName()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 7;
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Height = 50;
            tableLayoutPanel.BackColor = Color.Transparent;
            tableLayoutPanel.Dock = DockStyle.Top;


            tableLayoutPanel.Controls.Add(GetLabel("Adı"), 0, 0);
            tableLayoutPanel.Controls.Add(GetLabel("Soyadı"), 1, 0);
            tableLayoutPanel.Controls.Add(GetLabel("Anahtar"), 2, 0);
            tableLayoutPanel.Controls.Add(GetLabel("Şifre"), 3, 0);
            tableLayoutPanel.Controls.Add(GetLabel("Ekleyen Admin"), 4, 0);

            return tableLayoutPanel;
        }
        public static TableLayoutPanel AddTableVoter(Voter voter)
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 7;
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
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
                Name = "btnEdit_" + voter.Index.ToString()
            };
            buttonEdit.Click += ButtonEdit_Click;

            MetroButton buttonDelete = new MetroButton
            {
                Anchor = AnchorStyles.None,
                Text = "Sil",
                Theme = MetroFramework.MetroThemeStyle.Dark,
                UseSelectable = false,
                Name = "btnDelete_" + voter.Index.ToString()
            };
            buttonDelete.Click += ButtonDelete_Click;

            tableLayoutPanel.Controls.Add(GetLabel(voter.Name), 0, 0);
            tableLayoutPanel.Controls.Add(GetLabel(voter.Surname), 1, 0);
            tableLayoutPanel.Controls.Add(GetLabel(voter.Key), 2, 0);
            tableLayoutPanel.Controls.Add(GetLabel(voter.Password), 3, 0);
            tableLayoutPanel.Controls.Add(GetLabel(voter.AddedAdminKey), 4, 0);
            tableLayoutPanel.Controls.Add(buttonEdit, 5, 0);
            tableLayoutPanel.Controls.Add(buttonDelete, 6, 0);

            return tableLayoutPanel;
        }

        private static void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(ViewForms.voters.GetPoll());

                ViewForms.voters.pnl_Voters.Controls.Remove(FindButtonInTable((sender as MetroButton)));
                Managers.voterManager.Delete(FindVoter(GetVoterIndex(sender as MetroButton)), ViewForms.toBeChosen.GetPoll());
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
                PollStateManager.PollIsControl(ViewForms.voters.GetPoll());

                ViewVoterAdd voterAdd = ViewForms.voterAdd ?? (ViewForms.voterAdd = new ViewVoterAdd());
                voterAdd.ShowEdit(ViewForms.toBeChosen.GetPoll(), FindVoter(GetVoterIndex(sender as MetroButton)));
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
                ForeColor = Color.Black,
            };
        }
        private static Voter FindVoter(int index)
        {
            Poll poll = ViewForms.toBeChosen.GetPoll();
            return poll.Voter[index];
        }
        private static TableLayoutPanel FindButtonInTable(MetroButton button)
        {
            foreach (Control votersControl in ViewForms.voters.pnl_Voters.Controls)
            {
                foreach (Control control in votersControl.Controls)
                {
                    if (button == control)
                    {
                        return votersControl as TableLayoutPanel;
                    }
                }
            }
            return null;
        }
        private static int GetVoterIndex(MetroButton metroButton) => int.Parse((metroButton as MetroButton).Name.Split('_')[1]);
        private static void BlockChainControlState(string message)
        {
            if (message == "Seçim başlatılmış durumdadır artık değişiklik yapamazsınız!" || message == "Seçim Silinmiştir!")
            {
                ViewVoters voters = ViewForms.voters ?? (ViewForms.voters = new ViewVoters());
                voters.Hide();
                ViewHomePageForAdmin homePageForAdmin = ViewForms.homePageForAdmin ?? (ViewForms.homePageForAdmin = new ViewHomePageForAdmin());
                homePageForAdmin.Show();
            }
        }
    }
}
