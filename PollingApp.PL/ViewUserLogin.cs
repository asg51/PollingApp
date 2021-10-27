using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PollingApp.BL;
using PollingApp.Entities.P2PModel;

namespace PollingApp.PL
{
    public partial class ViewUserLogin : Form
    {
        Form _form;
        public ViewUserLogin()
        {
            InitializeComponent();
        }
        private void Client_ErrorSendingData()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    ViewMessageBox messageBox = ViewForms.messageBox ?? (ViewForms.messageBox = new ViewMessageBox());
                    messageBox.Hide();
                    this.Show();

                    MessageBox.Show("Hata bilgiler yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
            else
            {
                ViewMessageBox messageBox = ViewForms.messageBox ?? (ViewForms.messageBox = new ViewMessageBox());
                messageBox.Hide();
                this.Show();

                MessageBox.Show("Hata böyle bir seçim bulunamadı!", "hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Client_CompletedSendingData()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    ViewMessageBox messageBox = ViewForms.messageBox ?? (ViewForms.messageBox = new ViewMessageBox());
                    messageBox.Hide();

                    ViewHomePageForAdmin homePageForAdmin = ViewForms.homePageForAdmin ?? (ViewForms.homePageForAdmin = new ViewHomePageForAdmin());
                    homePageForAdmin.Show();
                });
            }
            else
            {
                ViewMessageBox messageBox = ViewForms.messageBox ?? (ViewForms.messageBox = new ViewMessageBox());
                messageBox.Hide();

                ViewHomePageForAdmin homePageForAdmin = ViewForms.homePageForAdmin ?? (ViewForms.homePageForAdmin = new ViewHomePageForAdmin());
                homePageForAdmin.Show();
            }
        }

        private void Client_NotCompletedSendingData()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    P2PContext.client.NotCompletedSendingData -= Client_NotCompletedSendingData;
                    P2PContext.client.CompletedSendingData -= Client_CompletedSendingData;
                    P2PContext.client.ErrorSendingData -= Client_ErrorSendingData;

                    ViewMessageBox messageBox = ViewForms.messageBox ?? (ViewForms.messageBox = new ViewMessageBox());
                    messageBox.Hide();
                    this.Show();

                    MessageBox.Show("Hata böyle bir seçim bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
            else
            {
                P2PContext.client.NotCompletedSendingData -= Client_NotCompletedSendingData;
                P2PContext.client.CompletedSendingData -= Client_CompletedSendingData;
                P2PContext.client.ErrorSendingData -= Client_ErrorSendingData;

                ViewMessageBox messageBox = ViewForms.messageBox ?? (ViewForms.messageBox = new ViewMessageBox());
                messageBox.Hide();
                this.Show();

                MessageBox.Show("Hata böyle bir seçim bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                ViewMessageBox messageBox = ViewForms.messageBox ?? (ViewForms.messageBox = new ViewMessageBox());
                messageBox.Show("Seçime giriliyor");
                this.Hide();

                Managers.clientManager.Connect(new ConnectAsAdmin(txt_Ip.Text, txt_User.Text, txt_Password.Text, txt_PollName.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            P2PContext.client.NotCompletedSendingData -= Client_NotCompletedSendingData;
            P2PContext.client.CompletedSendingData -= Client_CompletedSendingData;
            P2PContext.client.ErrorSendingData -= Client_ErrorSendingData;

            _form.Show();
            this.Hide();
        }
        public void Show(Form form)
        {
            _form = form;

            P2PContext.client.NotCompletedSendingData += Client_NotCompletedSendingData;
            P2PContext.client.CompletedSendingData += Client_CompletedSendingData;
            P2PContext.client.ErrorSendingData += Client_ErrorSendingData;

            this.Show();
        }
    }
}
