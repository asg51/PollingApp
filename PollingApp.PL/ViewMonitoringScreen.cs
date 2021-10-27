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
    public partial class ViewMonitoringScreen : Form
    {
        BlockChainsData _blockChainsData;
        List<CircularProgressBar.CircularProgressBar> circularProgressBars;
        public ViewMonitoringScreen()
        {
            InitializeComponent();
            Managers.blockChainManager.ViewerScreenUpdate += BlockChainManager_ViewerScreenUpdate;
        }

        private void BlockChainManager_ViewerScreenUpdate(BlockChainsData blockChainsData)
        {
            ViewsEditing.MonitoringScreen.UpdateCircleProgressBar(blockChainsData, circularProgressBars);
        }

        public void Show(BlockChainsData blockchaindata)
        {
            _blockChainsData = blockchaindata;
            PnlChonesFill();
            this.Show();
        }
        private void PnlChonesFill()
        {
            pnl_Chosens.Controls.Clear();
            pnl_Chosens.Controls.Add(ViewsEditing.MonitoringScreen.AddChosenCircleProgressBar(_blockChainsData, pnl_Chosens.Width,out circularProgressBars));
        }
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            PollingList.blockChainList.Delete(_blockChainsData);
            Managers.clientManager.ExitSystemBlockChain(_blockChainsData.Urls, _blockChainsData.BlockChainForPollName);

            ViewLoginPage loginPage = ViewForms.loginPage ?? (ViewForms.loginPage = new ViewLoginPage());
            this.Hide();
            loginPage.Show();
        }
    }
}
