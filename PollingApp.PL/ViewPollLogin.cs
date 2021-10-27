using PollingApp.BL;
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
    public partial class ViewPollLogin : Form
    {
        Form _form;
        public ViewPollLogin()
        {
            InitializeComponent();
        }

        public void Show(Form form)
        {
            P2PContext.client.ClientPostPollEvent += Client_ClientPostPollEvent;
            P2PContext.client.ClientPostPollNotNullEvent += Client_ClientPostPollNotNullEvent;
            P2PContext.client.ErrorSendingData += Client_ErrorSendingData;

            _form = form;
            TxtClear();
            this.Show();
        }
        public void Showing()
        {
            P2PContext.client.ClientPostPollEvent += Client_ClientPostPollEvent;
            P2PContext.client.ClientPostPollNotNullEvent += Client_ClientPostPollNotNullEvent;
            P2PContext.client.ErrorSendingData += Client_ErrorSendingData;

            TxtClear();
            this.Show();
        }

        private void Client_ErrorSendingData()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    ViewMessageBox messageBox = ViewForms.messageBox ?? (ViewForms.messageBox = new ViewMessageBox());
                    messageBox.Hide();
                    this.Show();

                    MessageBox.Show("Bilgiler hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
            else
            {
                ViewMessageBox messageBox = ViewForms.messageBox ?? (ViewForms.messageBox = new ViewMessageBox());
                messageBox.Hide();
                this.Show();

                MessageBox.Show("Bilgiler hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Client_ClientPostPollNotNullEvent(BlockChainsData blockChainsData)
        {
            if (this.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    ViewMessageBox messageBox = ViewForms.messageBox ?? (ViewForms.messageBox = new ViewMessageBox());
                    messageBox.Hide();
                    this.Show();

                    MessageBox.Show("Hata böyle bir seçim bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtClear();
                });
            }
            else
            {
                ViewMessageBox messageBox = ViewForms.messageBox ?? (ViewForms.messageBox = new ViewMessageBox());
                messageBox.Hide();
                this.Show();

                MessageBox.Show("Hata böyle bir seçim bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtClear();
            }
        }

        private void Client_ClientPostPollEvent(BlockChainsData blockChainsData)
        {
            if (this.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    this.Hide();
                    P2PContext.client.ClientPostPollEvent -= Client_ClientPostPollEvent;
                    P2PContext.client.ClientPostPollNotNullEvent -= Client_ClientPostPollNotNullEvent;
                    P2PContext.client.ErrorSendingData -= Client_ErrorSendingData;


                    ViewMessageBox messageBox = ViewForms.messageBox ?? (ViewForms.messageBox = new ViewMessageBox());
                    messageBox.Hide();
                    if (_form.Name == "ViewMonitoringScreen")
                    {
                        (_form as ViewMonitoringScreen).Show(blockChainsData);
                    }
                    else if (_form.Name == "ViewVoterLogin")
                    {
                        (_form as ViewVoterLogin).Show(blockChainsData);
                    }
                });
            }
            else
            {
                this.Hide();
                P2PContext.client.ClientPostPollEvent -= Client_ClientPostPollEvent;
                P2PContext.client.ClientPostPollNotNullEvent -= Client_ClientPostPollNotNullEvent;
                P2PContext.client.ErrorSendingData -= Client_ErrorSendingData;

                ViewMessageBox messageBox = ViewForms.messageBox ?? (ViewForms.messageBox = new ViewMessageBox());
                messageBox.Hide();

                if (_form.Name == "ViewMonitoringScreen")
                {
                    (_form as ViewMonitoringScreen).Show(blockChainsData);
                }
                else if (_form.Name == "ViewVoterLogin")
                {
                    (_form as ViewVoterLogin).Show(blockChainsData);
                }
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            ViewLoginPage loginPage = ViewForms.loginPage ?? (ViewForms.loginPage = new ViewLoginPage());

            this.Hide();
            loginPage.Show();
        }

        private void Btn_Aprrove_Click(object sender, EventArgs e)
        {            
            ViewMessageBox messageBox = ViewForms.messageBox ?? (ViewForms.messageBox = new ViewMessageBox());
            this.Hide();
            messageBox.Show("Seçime giriliyor lütfen bekleyiniz.");

            Managers.pollManager.LoginPoll(txt_PollName.Text, txt_Ip.Text);
        }

        private void TxtClear()
        {
            txt_Ip.Text = "";
            txt_PollName.Text = "";
        }
    }
}
