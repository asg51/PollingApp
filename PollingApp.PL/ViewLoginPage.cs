using PollingApp.BL;
using PollingApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PollingApp.PL
{
    public partial class ViewLoginPage : Form
    {
        public ViewLoginPage()
        {
            ViewForms.loginPage = this;
            P2PContext.server.Start();
            InitializeComponent();
        }

        private void Btn_Admin_Click(object sender, EventArgs e)
        {
            if (PollingList.blockChainList.GetBlockChains().Count + PollingList.dbPoll.GetList().Count > 0)
            {
                ViewHomePageForAdmin homePageForAdmin = ViewForms.homePageForAdmin ?? (ViewForms.homePageForAdmin = new ViewHomePageForAdmin());

                this.Hide();
                homePageForAdmin.Pnl_AddPoll();
                homePageForAdmin.Show(this);
            }
            else
            {
                ViewUserLogin userLogin = ViewForms.userLogin ?? (ViewForms.userLogin = new ViewUserLogin());

                this.Hide();
                userLogin.Show(this);
            }
        }

        private void Btn_NewPoll_Click(object sender, EventArgs e)
        {
            ViewCreatePoll createPoll = ViewForms.createPoll ?? (ViewForms.createPoll = new ViewCreatePoll());

            this.Hide();
            createPoll.Show(false);
        }

        private void Btn_UseVote_Click(object sender, EventArgs e)
        {
            ViewPollLogin pollLogin = ViewForms.pollLogin ?? (ViewForms.pollLogin = new ViewPollLogin());
            ViewVoterLogin voterLogin = ViewForms.voterLogin ?? (ViewForms.voterLogin = new ViewVoterLogin());
            this.Hide();
            pollLogin.Show(voterLogin);
        }

        private void Btn_MonitoringScreen_Click(object sender, EventArgs e)
        {
            ViewPollLogin pollLogin = ViewForms.pollLogin ?? (ViewForms.pollLogin = new ViewPollLogin());
            ViewMonitoringScreen monitoringScreen = ViewForms.monitoringScreen ?? (ViewForms.monitoringScreen = new ViewMonitoringScreen());
            this.Hide();
            pollLogin.Show(monitoringScreen);
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
