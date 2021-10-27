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
using static PollingApp.Entities.BlockChainList;

namespace PollingApp.PL.ViewsEditing
{
    public static class HomePageForAdmin
    {
        private static string pollName;
        private static List<CircularProgressBar.CircularProgressBar> circularProgresses;
        public static TableLayoutPanel AddTablePoll(Poll poll)
        {
            return AddTable("Düzenle", "btnEdit_" + poll.Index.ToString(),
                "btnPollDelete_" + poll.Index.ToString(), poll.PollingName, poll.PollTime.StartTime, poll.PollTime.FinishTime);
        }
        public static TableLayoutPanel AddTablePoll(BlockChainsData blockChainsData)
        {
            return AddTable("İncele", "btnReview_" + blockChainsData.Index.ToString(), "btnBlockDelete_" + blockChainsData.Index.ToString()
                   , blockChainsData.BlockChainForPollName, blockChainsData.PollTime.StartTime, blockChainsData.PollTime.FinishTime);
        }
        private static TableLayoutPanel AddTable(string btnEditText, string btnEditName,
            string btnDeleteName, string pollName, DateTime start, DateTime finish)
        {
            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Height = 100;
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.Dock = DockStyle.Top;

            TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Height = 50;
            tableLayoutPanel2.BackColor = Color.Transparent;
            tableLayoutPanel2.Dock = DockStyle.Fill;

            TableLayoutPanel tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Height = 50;
            tableLayoutPanel3.BackColor = Color.Transparent;
            tableLayoutPanel3.Dock = DockStyle.Fill;


            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 1);



            MetroButton buttonEdit = new MetroButton
            {
                Anchor = AnchorStyles.None,
                Text = btnEditText,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                UseSelectable = false,
                Name = btnEditName
            };
            buttonEdit.Click += ButtonEdit_Click;

            MetroButton buttonDelete = new MetroButton
            {
                Anchor = AnchorStyles.None,
                Text = "Sil",
                Theme = MetroFramework.MetroThemeStyle.Dark,
                UseSelectable = false,
                Name = btnDeleteName
            };
            buttonDelete.Click += ButtonDelete_Click;

            tableLayoutPanel2.Controls.Add(GetLabel(pollName), 0, 0);
            tableLayoutPanel2.Controls.Add(buttonEdit, 1, 0);
            tableLayoutPanel2.Controls.Add(buttonDelete, 2, 0);

            tableLayoutPanel3.Controls.Add(GetLabel(start.ToString("dd.MM.yyyy HH:mm")), 0, 0);
            tableLayoutPanel3.Controls.Add(GetHyphenLabel(), 1, 0);
            tableLayoutPanel3.Controls.Add(GetLabel(finish.ToString("dd.MM.yyyy HH:mm")), 2, 0);


            return tableLayoutPanel1;
        }
        private static void ButtonDelete_Click(object sender, EventArgs e)
        {
            MetroButton metroButton = sender as MetroButton;
            string name = GetName(metroButton);
            try
            {
                if (name == "btnPollDelete")
                    PollStateManager.PollIsControl(FindPoll(GetIndex(metroButton)));

                Delete(sender as MetroButton);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }

        private static void ButtonEdit_Click(object sender, EventArgs e)
        {
            MetroButton metroButton = sender as MetroButton;
            string name = GetName(metroButton);
            try
            {
                if (name == "btnEdit")
                {
                    PollStateManager.PollIsControl(FindPoll(GetIndex(sender as MetroButton)));
                }

                Edit(sender as MetroButton);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        private static Poll FindPoll(int index) => PollingList.dbPoll[index];
        private static BlockChainsData FindBlockChain(int index) => PollingList.blockChainList[index];

        private static void Delete(MetroButton metroButton)
        {
            string name = GetName(metroButton);
            DialogResult dialogResult = MessageBox.Show("Seçimi herkesten silmek istiyorsanız Evet'e" +
                "\nSeçimi sadece uygulamadan kaldırmak istiyorsanız Hayır'a" +
                "\nHiç birşey yapmak istemiyorsanız Iptal'e basınız.", "Silme İşlemi", MessageBoxButtons.YesNoCancel);
            if (dialogResult != DialogResult.Cancel)
            {
                bool state = dialogResult == DialogResult.Yes ? true : false;
                if (name == "btnPollDelete")
                    Managers.pollManager.Delete(FindPoll(GetIndex(metroButton)), state);
                else if (name == "btnBlockDelete")
                    Managers.blockChainManager.Delete(FindBlockChain(GetIndex(metroButton)), state);
            }
        }
        private static void Edit(MetroButton metroButton)
        {
            string name = GetName(metroButton);
            int index = GetIndex(metroButton);

            if (name == "btnEdit")
            {
                ViewToBeChosen toBeChosen = ViewForms.toBeChosen ?? (ViewForms.toBeChosen = new ViewToBeChosen());
                ViewHomePageForAdmin homePageForAdmin = ViewForms.homePageForAdmin ??
                    (ViewForms.homePageForAdmin = new ViewHomePageForAdmin());

                homePageForAdmin.Hide();
                toBeChosen.Show(Managers.pollManager.GetPoll(GetIndex(metroButton)));
            }
            else if (name == "btnReview")
            {
                var blockChainList = PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.Index == index);
                pollName = blockChainList.BlockChainForPollName;
                ViewForms.homePageForAdmin.pnl_Chosens.Controls.Add(AddChosenCircleProgressBar(blockChainList, ViewForms.homePageForAdmin.pnl_Chosens.Width, out circularProgresses));
            }
        }
        private static int GetIndex(MetroButton metroButton) => int.Parse(metroButton.Name.Split('_')[1]);
        private static string GetName(MetroButton metroButton) => (metroButton.Name.Split('_')[0]);
        private static Label GetHyphenLabel() => new Label
        {
            Text = "-",
            Anchor = AnchorStyles.None,
            ForeColor = Color.Black,
        };
        private static void BlockChainControlState(string message)
        {
            if (message == "Seçim başlatılmış durumdadır artık değişiklik yapamazsınız!" || message == "Seçim Silinmiştir!")
            {
                ViewHomePageForAdmin homePageForAdmin = ViewForms.homePageForAdmin ?? (ViewForms.homePageForAdmin = new ViewHomePageForAdmin());
                Task.Run(async () => await homePageForAdmin.Pnl_AddPollAsync());
            }
        }


        public static void UpdateCircleProgressBar(BlockChainsData blockChainsData)
        {
            if (blockChainsData.BlockChainForPollName == pollName)
            {
                int totalVotesCast = blockChainsData.UsedVote.GetBlocks().Count - 1;
                foreach (CircularProgressBar.CircularProgressBar circularProgressBar in circularProgresses)
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
                                circularProgressBar.SubscriptText = "." + value.ToString().Split(',')[1][0] + value.ToString().Split(',')[1][1];
                            }
                            catch
                            {
                                circularProgressBar.SubscriptText = "." + 0;
                            }
                        });
                    }
                }
            }
        }
        public static FlowLayoutPanel AddChosenCircleProgressBar(BlockChainsData blockChainsData, int width, out List<CircularProgressBar.CircularProgressBar> circularProgressBars)
        {
            circularProgressBars = new List<CircularProgressBar.CircularProgressBar>();
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Width = width-20;
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.AutoScroll = true;
            for (int i = 1; i < blockChainsData.BlockChainForChosens.Chain.Count + 1; i += 2)
            {
                if (blockChainsData.BlockChainForChosens.Chain.Count + 1 < 3 ||
                    i + 2 > blockChainsData.BlockChainForChosens.Chain.Count + 1)
                {
                    IList<CircularProgressBar.CircularProgressBar> circularProgressBar;
                    IList<Block<Chosen>> blocks = new List<Block<Chosen>>();
                    for (int x = i; x < blockChainsData.BlockChainForChosens.Chain.Count; x++)
                    {
                        blocks.Add(blockChainsData.BlockChainForChosens.Chain[x]);
                    }
                    blocks.Add(new Block<Chosen>(DateTime.Now, string.Empty, new Chosen("Boş", -1)));
                    flowLayoutPanel.Controls.Add(GetChosenTableRow(blocks, i / 2, blockChainsData.BlockChainForPollName, width-20, out circularProgressBar));
                    circularProgressBars.AddRange(circularProgressBar);
                }
                else
                {
                    IList<CircularProgressBar.CircularProgressBar> circularProgressBar;
                    flowLayoutPanel.Controls.Add(GetChosenTableRow(
                        new List<Block<Chosen>>{
                        blockChainsData.BlockChainForChosens.Chain[i],
                        blockChainsData.BlockChainForChosens.Chain[i + 1]},
                        i / 2, blockChainsData.BlockChainForPollName, width-20, out circularProgressBar));
                    circularProgressBars.AddRange(circularProgressBar);
                }
            }

            UpdateCircleProgressBar(blockChainsData);
            return flowLayoutPanel;
        }
        private static Panel GetChosenTableRow(IList<Block<Chosen>> blocks, int column, string pollName, int width, out IList<CircularProgressBar.CircularProgressBar> circularProgressBars)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Height = 410;
            panel.Width = width;

            TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.RowCount = 1;

            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Height = 400;
            tableLayoutPanel1.Width = width;
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.Dock = DockStyle.Fill;

            circularProgressBars = new List<CircularProgressBar.CircularProgressBar>();
            for (int i = 0; i < blocks.Count; i++)
            {
                CircularProgressBar.CircularProgressBar circularProgressBar;
                tableLayoutPanel1.Controls.Add(GetChosenCircularProgressBar(blocks[i], pollName, out circularProgressBar), i % 2, column);
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
            tableLayoutPanel1.Height = 400;

            tableLayoutPanel1.Controls.Add(circularProgressBar, 0, 0);
            tableLayoutPanel1.Controls.Add(GetLabel(block.Transactions.ChosenName), 0, 1);

            return tableLayoutPanel1;
        }
        private static int GetIndex(string name) => int.Parse(name.Split('_')[1]);
    }
}

