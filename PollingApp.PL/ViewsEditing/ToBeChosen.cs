using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
using PollingApp.BL;
using PollingApp.BL.Contcat;
using PollingApp.Entities;

namespace PollingApp.PL.ViewsEditing
{
    public static class ToBeChosen
    {
        public static TableLayoutPanel AddTableChosen(string name, int index)
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Height = 50;
            tableLayoutPanel.BackColor = Color.Transparent;
            tableLayoutPanel.Dock = DockStyle.Top;

            Label label = new Label
            {
                Text = name,
                Anchor = AnchorStyles.None,
                ForeColor = Color.Black,
            };

            MetroButton buttonEdit = new MetroButton
            {
                Anchor = AnchorStyles.None,
                Text = "Düzenle",
                Theme = MetroFramework.MetroThemeStyle.Dark,
                UseSelectable = false,
                Name = "btnEdit_" + index.ToString()
            };
            buttonEdit.Click += ButtonEdit_Click;

            MetroButton buttonDelete = new MetroButton
            {
                Anchor = AnchorStyles.None,
                Text = "Sil",
                Theme = MetroFramework.MetroThemeStyle.Dark,
                UseSelectable = false,
                Name = "btnDelete_" + index.ToString()
            };
            buttonDelete.Click += ButtonDelete_Click;

            tableLayoutPanel.Controls.Add(label, 0, 0);
            tableLayoutPanel.Controls.Add(buttonEdit, 1, 0);
            tableLayoutPanel.Controls.Add(buttonDelete, 2, 0);

            return tableLayoutPanel;
        }

        private static void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(ViewForms.toBeChosen.GetPoll());

                ViewForms.toBeChosen.pnl_Chosens.Controls.Remove(FindButtonInTable((sender as MetroButton)));
                Managers.chosenManager.Delete(FindChosen(GetChosenIndex(sender as MetroButton)), ViewForms.toBeChosen.GetPoll());
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

                ViewChosenEdit chosenEdit = ViewForms.chosenEdit ?? (ViewForms.chosenEdit = new ViewChosenEdit());
                chosenEdit.Show(ViewForms.toBeChosen.GetPoll(), FindChosen(GetChosenIndex(sender as MetroButton)), sender as MetroButton);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }
        public static void Pnl_ChosnesEdit(MetroButton metroButton, string name)
        {
            foreach (Control control in FindButtonInTable(metroButton).Controls)
            {
                if (control.GetType() == typeof(Label))
                {
                    (control as Label).Text = name;
                    break;
                }
            }
        }
        private static Chosen FindChosen(int index)
        {
            Poll poll = ViewForms.toBeChosen.GetPoll();
            return poll.Chosen.Get(index - 1);
        }
        private static TableLayoutPanel FindButtonInTable(MetroButton button)
        {
            foreach (Control toBeChosencontrol in ViewForms.toBeChosen.pnl_Chosens.Controls)
            {
                foreach (Control control in toBeChosencontrol.Controls)
                {
                    if (button == control)
                    {
                        return toBeChosencontrol as TableLayoutPanel;
                    }
                }
            }
            return null;
        }
        private static int GetChosenIndex(MetroButton metroButton) => int.Parse(metroButton.Name.Split('_')[1]);
        private static void BlockChainControlState(string message)
        {
            if (message == "Seçim başlatılmış durumdadır artık değişiklik yapamazsınız!" || message == "Seçim Silinmiştir!")
            {
                ViewToBeChosen toBeChosen = ViewForms.toBeChosen ?? (ViewForms.toBeChosen = new ViewToBeChosen());
                toBeChosen.Hide();
                ViewHomePageForAdmin homePageForAdmin = ViewForms.homePageForAdmin ?? (ViewForms.homePageForAdmin = new ViewHomePageForAdmin());
                homePageForAdmin.Show();
            }
        }
    }
}
