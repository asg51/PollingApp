using MetroFramework.Controls;
using PollingApp.BL;
using PollingApp.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PollingApp.Entities.BlockChainList;

namespace PollingApp.PL.ViewsEditing
{
    public static class MakingChoices
    {
        internal static FlowLayoutPanel GetChosenTable(BlockChainsData blockChainsData, int width)
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Width = width;
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.AutoScroll = true;

            for (int i = 1; i < blockChainsData.BlockChainForChosens.Chain.Count+1; i += 3)
            {
                if (blockChainsData.BlockChainForChosens.Chain.Count+1 < 4 ||
                    i + 3 > blockChainsData.BlockChainForChosens.Chain.Count+1)
                {
                    IList<Block<Chosen>> blocks = new List<Block<Chosen>>();
                    for (int x = i; x < blockChainsData.BlockChainForChosens.Chain.Count; x++)
                    {
                        blocks.Add(blockChainsData.BlockChainForChosens.Chain[x]);
                    }
                    blocks.Add(new Block<Chosen>(DateTime.Now, string.Empty, new Chosen("Boş", -1)));
                    flowLayoutPanel.Controls.Add(GetChosenTableRow(blocks, i / 3, blockChainsData.BlockChainForPollName, width));
                }
                else
                {
                    flowLayoutPanel.Controls.Add(GetChosenTableRow(
                        new List<Block<Chosen>>{
                        blockChainsData.BlockChainForChosens.Chain[i],
                        blockChainsData.BlockChainForChosens.Chain[i + 1],
                        blockChainsData.BlockChainForChosens.Chain[i + 2]}, i / 3, blockChainsData.BlockChainForPollName, width));
                }
            }
            return flowLayoutPanel;

        }
        private static TableLayoutPanel GetChosenTableRow(IList<Block<Chosen>> blocks, int column, string pollName, int width)
        {
            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.RowCount = 1;

            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));

            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Height = 150;
            tableLayoutPanel1.Width = width;
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.Dock = DockStyle.Top;

            for (int i = 0; i < blocks.Count; i++)
            {
                tableLayoutPanel1.Controls.Add(GetChosenTablePanel(blocks[i], pollName), i % 3, column);
            }
            return tableLayoutPanel1;
        }
        private static TableLayoutPanel GetChosenTablePanel(Block<Chosen> block, string pollName)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewHomePageForAdmin));
            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_Polls.BackgroundImage")));
            tableLayoutPanel1.BackgroundImageLayout = ImageLayout.Stretch;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Width = 150;

            tableLayoutPanel1.Controls.Add(GetLabel(block.Transactions.ChosenName), 0, 0);
            tableLayoutPanel1.Controls.Add(GetButton("Seç", pollName, block.Transactions.Index.ToString()), 0, 1);

            return tableLayoutPanel1;
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
        private static MetroButton GetButton(string name, string pollName, string index)
        {
            MetroButton metroButton = new MetroButton
            {
                Anchor = AnchorStyles.None,
                Text = name,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                UseSelectable = false,
                Name = "btnApprove_" + pollName + "_" + index
            };
            metroButton.Click += MetroButton_Click;
            return metroButton;
        }

        private static void MetroButton_Click(object sender, EventArgs e)
        {
            int voterIndex = ViewForms.makingChoices._index;
            int index = GetIndex(sender as MetroButton);
            string pollName = GetPollName(sender as MetroButton);

            string chosenName = GetChosenName(pollName, index);

            if (index == -1 && chosenName == string.Empty)
            {
                DialogResult dialogResult = MessageBox.Show("Boş oy kullanıyorsunuz onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show("Oy kullanılıyor lütfen bekleyiniz!", "Bekleme");
                    Managers.blockChainManager.Voting(pollName, voterIndex, index);
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show($"{chosenName} oy kullanıyorsunuz onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    MessageBox.Show("Oy kullanılıyor lütfen bekleyiniz!", "Bekleme");
                    Managers.blockChainManager.Voting(pollName, voterIndex, index);
                }
            }
        }
        private static int GetIndex(MetroButton metroButton) => int.Parse(metroButton.Name.Split('_')[2]);
        private static string GetPollName(MetroButton metroButton) => metroButton.Name.Split('_')[1];
        private static string GetChosenName(string pollName, int index)
        {
            IList<Block<Chosen>> blocks = PollingList.blockChainList.GetBlockChains().
                FirstOrDefault(x => x.BlockChainForPollName == pollName).BlockChainForChosens.GetBlocks();
            for (int i = 1; i < blocks.Count; i++)
            {
                if (blocks[i].Transactions.Index == index)
                    return blocks[i].Transactions.ChosenName;
            }
            return string.Empty;
        }
    }
}
