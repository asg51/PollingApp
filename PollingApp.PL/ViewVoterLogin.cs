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
    public partial class ViewVoterLogin : Form
    {
        BlockChainsData _blockChainsData;
        public ViewVoterLogin()
        {
            InitializeComponent();
            Managers.clientManager.Login += ClientManager_Login;
            Managers.clientManager.NotLogin += ClientManager_NotLogin;
        }

        private void ClientManager_NotLogin(BlockChainsData blockChainsData, int index)
        {
            if (this.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    Block<Voter> block = blockChainsData.BlockChainForVoters.GetBlocks().FirstOrDefault(x => x.Transactions.Index == index);
                    string name = block.Transactions.Name + " " + block.Transactions.Surname;
                    MessageBox.Show($"{name} kişi oy kullanmıştır!");
                    TxtClear();
                });
            }
        }

        private void ClientManager_Login(BlockChainsData blockChainsData, int index)
        {
            if (this.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    ViewMakingChoices makingChoices = ViewForms.makingChoices ?? (ViewForms.makingChoices = new ViewMakingChoices());
                    this.Hide();
                    makingChoices.Show(blockChainsData, index);
                    TxtClear();
                });
            }
        }

        public void Show(BlockChainsData blockChainsData)
        {
            _blockChainsData = blockChainsData;
            this.Show();
        }
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            PollingList.blockChainList.Delete(_blockChainsData);
            Managers.clientManager.ExitSystemBlockChain(_blockChainsData.Urls, _blockChainsData.BlockChainForPollName);

            ViewPollLogin pollLogin = ViewForms.pollLogin ?? (ViewForms.pollLogin = new ViewPollLogin());
            this.Hide();
            pollLogin.Showing();
            TxtClear();
        }

        private void Btn_Ingress_Click(object sender, EventArgs e)
        {
            try
            {
                Managers.voterManager.VoterLogin(txt_Key.Text, txt_Password.Text, _blockChainsData);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void TxtClear()
        {
            txt_Key.Text = "";
            txt_Password.Text = "";
        }
    }
}
