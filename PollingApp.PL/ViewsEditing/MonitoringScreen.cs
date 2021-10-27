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
    public static class MonitoringScreen
    {
        public static void UpdateCircleProgressBar(BlockChainsData blockChainsData, List<CircularProgressBar.CircularProgressBar> circularProgressBars)
        {
            int totalVotesCast = blockChainsData.UsedVote.GetBlocks().Count - 1;
            foreach (CircularProgressBar.CircularProgressBar circularProgressBar in circularProgressBars)
            {
                int index = GetIndex(circularProgressBar.Name);
                int sum = blockChainsData.UsedVote.GetBlocks().Where(x => x.Transactions == index).Count();
                double value = Convert.ToDouble(sum) / Convert.ToDouble(totalVotesCast) * 100.0;

                if (circularProgressBar.InvokeRequired)
                {
                    circularProgressBar.Invoke((MethodInvoker)delegate ()
                    {
                        circularProgressBar.Value = Convert.ToInt32(value);
                        circularProgressBar.Text = "%" + value.ToString().Split(',')[0];
                        try
                        {
                            circularProgressBar.SubscriptText = "." + value.ToString().Split(',')[1][0]+ value.ToString().Split(',')[1][1];
                        }
                        catch
                        {
                            circularProgressBar.SubscriptText = "." + 0;
                        }
                    });
                }
            }
        }
        public static FlowLayoutPanel AddChosenCircleProgressBar(BlockChainsData blockChainsData, int width, out List<CircularProgressBar.CircularProgressBar> circularProgressBars)
        {
            circularProgressBars = new List<CircularProgressBar.CircularProgressBar>();
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Width = width;
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.AutoScroll = true;
            for (int i = 1; i < blockChainsData.BlockChainForChosens.Chain.Count + 1; i += 3)
            {
                if (blockChainsData.BlockChainForChosens.Chain.Count + 1 < 4 ||
                    i + 3 > blockChainsData.BlockChainForChosens.Chain.Count + 1)
                {
                    IList<CircularProgressBar.CircularProgressBar> circularProgressBar;
                    IList<Block<Chosen>> blocks = new List<Block<Chosen>>();
                    for (int x = i; x < blockChainsData.BlockChainForChosens.Chain.Count; x++)
                    {
                        blocks.Add(blockChainsData.BlockChainForChosens.Chain[x]);
                    }
                    blocks.Add(new Block<Chosen>(DateTime.Now, string.Empty, new Chosen("Boş", -1)));
                    flowLayoutPanel.Controls.Add(GetChosenTableRow(blocks, i / 3, blockChainsData.BlockChainForPollName, width, out circularProgressBar));
                    circularProgressBars.AddRange(circularProgressBar);
                }
                else
                {
                    IList<CircularProgressBar.CircularProgressBar> circularProgressBar;
                    flowLayoutPanel.Controls.Add(GetChosenTableRow(
                        new List<Block<Chosen>>{
                        blockChainsData.BlockChainForChosens.Chain[i],
                        blockChainsData.BlockChainForChosens.Chain[i + 1],
                        blockChainsData.BlockChainForChosens.Chain[i + 2]}, i / 3, blockChainsData.BlockChainForPollName, width, out circularProgressBar));
                    circularProgressBars.AddRange(circularProgressBar);
                }
            }

            UpdateCircleProgressBar(blockChainsData, circularProgressBars);
            return flowLayoutPanel;
        }
        private static Panel GetChosenTableRow(IList<Block<Chosen>> blocks, int column, string pollName, int width, out IList<CircularProgressBar.CircularProgressBar> circularProgressBars)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Height = 400;
            panel.Width = width;

            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.RowCount = 1;

            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));

            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Height = 400;
            tableLayoutPanel1.Width = width;
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.Dock = DockStyle.Fill;

            circularProgressBars = new List<CircularProgressBar.CircularProgressBar>();
            for (int i = 0; i < blocks.Count; i++)
            {
                CircularProgressBar.CircularProgressBar circularProgressBar;
                tableLayoutPanel1.Controls.Add(GetChosenCircularProgressBar(blocks[i], pollName, out circularProgressBar), i % 3, column);
                circularProgressBars.Add(circularProgressBar);
            }
            panel.Controls.Add(tableLayoutPanel1);
            return panel;
        }
        private static TableLayoutPanel GetChosenCircularProgressBar(Block<Chosen> block, string pollName, out CircularProgressBar.CircularProgressBar circularProgressBar)
        {
            circularProgressBar = new CircularProgressBar.CircularProgressBar();
            circularProgressBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            circularProgressBar.AnimationSpeed = 500;
            circularProgressBar.BackColor = System.Drawing.Color.Transparent;
            circularProgressBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            circularProgressBar.ForeColor = System.Drawing.Color.White;
            circularProgressBar.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            circularProgressBar.InnerMargin = 2;
            circularProgressBar.InnerWidth = -1;
            circularProgressBar.Location = new System.Drawing.Point(223, 74);
            circularProgressBar.MarqueeAnimationSpeed = 2000;
            circularProgressBar.Name = "circularProgressBar_" + block.Transactions.Index;
            circularProgressBar.OuterColor = System.Drawing.Color.Transparent;
            circularProgressBar.OuterMargin = 0;
            circularProgressBar.OuterWidth = 0;
            circularProgressBar.ProgressColor = System.Drawing.Color.White;
            circularProgressBar.ProgressWidth = 25;
            circularProgressBar.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            circularProgressBar.Size = new System.Drawing.Size(320, 320);
            circularProgressBar.StartAngle = 90;
            circularProgressBar.SubscriptColor = System.Drawing.Color.White;
            circularProgressBar.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            circularProgressBar.SubscriptText = ".0";
            circularProgressBar.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            circularProgressBar.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            circularProgressBar.SuperscriptText = "";
            circularProgressBar.TabIndex = 0;
            circularProgressBar.Text = "%0";
            circularProgressBar.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            circularProgressBar.Value = 0;
            circularProgressBar.Dock = DockStyle.Fill;

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewHomePageForAdmin));
            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_Polls.BackgroundImage")));
            tableLayoutPanel1.BackgroundImageLayout = ImageLayout.Stretch;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Height = 390;

            tableLayoutPanel1.Controls.Add(circularProgressBar, 0, 0);
            tableLayoutPanel1.Controls.Add(GetLabel(block.Transactions.ChosenName), 0, 1);

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
        private static int GetIndex(string name) => int.Parse(name.Split('_')[1]);
    }
}
