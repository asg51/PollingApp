using PollingApp.BL.Contcat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PollingApp.PL
{
    public partial class ViewAdmins : Form
    {
        private Entities.Poll _poll;
        public ViewAdmins()
        {
            InitializeComponent();
        }

        public void Show(Entities.Poll poll)
        {
            _poll = poll;
            Pnl_AdminsAdd();

            this.Show();
        }
        private void Pnl_AdminsAdd()
        {
            pnl_Admins.Controls.Clear();

            foreach (Entities.Admin admin in _poll.Admins.GetList())
                pnl_Admins.Controls.Add(ViewsEditing.Admins.AddTableVoter(admin));
            pnl_Admins.Controls.Add(ViewsEditing.Admins.AddTableVoterName());
        }
        private void Btn_Back_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);
                ViewVoterAndAdmin voterAndAdmin = ViewForms.voterAndAdmin ?? (ViewForms.voterAndAdmin = new ViewVoterAndAdmin());

                this.Hide();
                voterAndAdmin.Show(_poll);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);
                ViewAdminAdd adminAdd = ViewForms.adminAdd ?? (ViewForms.adminAdd = new ViewAdminAdd());

                this.Hide();
                adminAdd.Show(_poll);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }
        private void BlockChainControlState(string message)
        {
            if (message == "Seçim başlatılmış durumdadır artık değişiklik yapamazsınız!" || message == "Seçim Silinmiştir!")
            {
                this.Hide();
                ViewHomePageForAdmin homePageForAdmin = ViewForms.homePageForAdmin ?? (ViewForms.homePageForAdmin = new ViewHomePageForAdmin());
                homePageForAdmin.Show();
            }
        }
    }
}
