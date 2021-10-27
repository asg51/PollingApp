using PollingApp.BL;
using PollingApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PollingApp.Entities.BlockChainList;

namespace PollingApp.PL
{
    public partial class ViewHomePageForAdmin : Form
    {
        private object _lockObject = new object();
        public ViewHomePageForAdmin()
        {
            InitializeComponent();
            PollingList.blockChainList.BlockChainControlEvent += BlockChainList_BlockChainControlEvent;
            PollingList.dbPoll.ControlEvent += DbPoll_ControlEvent;
            Managers.blockChainManager.ViewerScreenUpdate += BlockChainManager_ViewerScreenUpdate;
        }

        private void BlockChainManager_ViewerScreenUpdate(BlockChainsData blockChainsData)
        {
            ViewsEditing.HomePageForAdmin.UpdateCircleProgressBar(blockChainsData);
        }

        private async void DbPoll_ControlEvent()
        {
            await Pnl_AddPollAsync();
        }

        private async void BlockChainList_BlockChainControlEvent()
        {
            await Pnl_AddPollAsync();
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bütün seçimler silinecektir onaylıyormusunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                Managers.pollManager.FillPoll();

                ViewLoginPage loginPage = ViewForms.loginPage ?? (ViewForms.loginPage = new ViewLoginPage());
                loginPage.Show();
                this.Hide();
            }
        }

        private void Btn_NewPoll_Click(object sender, EventArgs e)
        {
            ViewCreatePoll createPoll = ViewForms.createPoll ?? (ViewForms.createPoll = new ViewCreatePoll());
            createPoll.Show(true);
            this.Hide();
        }
        internal void Pnl_AddPoll()
        {

            pnl_Polls.Controls.Clear();

            foreach (BlockChainsData blockChainsData in PollingList.blockChainList.GetBlockChains())
            {

                pnl_Polls.Controls.Add(ViewsEditing.HomePageForAdmin.AddTablePoll(blockChainsData));

            }
            foreach (Poll poll in PollingList.dbPoll.GetList())
            {
                pnl_Polls.Controls.Add(ViewsEditing.HomePageForAdmin.AddTablePoll(poll));

            }

        }
        internal async Task Pnl_AddPollAsync()
        {
            Task task = Task.Factory.StartNew(() =>
            {
                lock (_lockObject)
                {
                    if (this.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            pnl_Polls.Controls.Clear();
                        });
                    }
                    else
                    {
                        pnl_Polls.Controls.Clear();
                    }
                    foreach (BlockChainsData blockChainsData in PollingList.blockChainList.GetBlockChains())
                    {
                        if (this.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
                        {
                            //Eğer farklı bir iş parçacığından talep gelmişse aşağıdaki Invoke metoduyla işlem gerçekleştiriliyor.
                            this.Invoke((MethodInvoker)delegate ()
                        {
                            pnl_Polls.Controls.Add(ViewsEditing.HomePageForAdmin.AddTablePoll(blockChainsData));
                        });
                        }
                        else
                        {
                            pnl_Polls.Controls.Add(ViewsEditing.HomePageForAdmin.AddTablePoll(blockChainsData));
                        }
                    }
                    foreach (Poll poll in PollingList.dbPoll.GetList())
                    {
                        if (this.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
                        {
                            //Eğer farklı bir iş parçacığından talep gelmişse aşağıdaki Invoke metoduyla işlem gerçekleştiriliyor.
                            this.Invoke((MethodInvoker)delegate ()
                        {
                            pnl_Polls.Controls.Add(ViewsEditing.HomePageForAdmin.AddTablePoll(poll));
                        });
                        }
                        else
                        {
                            pnl_Polls.Controls.Add(ViewsEditing.HomePageForAdmin.AddTablePoll(poll));
                        }
                    }
                }
            });

            await task;
        }

        private void ViewHomePageForAdmin_Shown(object sender, EventArgs e)
        {
            lbl_IpAddress.Text = Managers.serverManager.GetIpAddress();
        }

        private void Btn_ManageElections_Click(object sender, EventArgs e)
        {
            ViewUserLogin userLogin = ViewForms.userLogin ?? (ViewForms.userLogin = new ViewUserLogin());

            ViewHomePageForAdmin homePageForAdmin = ViewForms.homePageForAdmin ?? (ViewForms.homePageForAdmin = new ViewHomePageForAdmin());

            this.Hide();
            userLogin.Show(homePageForAdmin);
        }
    }
}
