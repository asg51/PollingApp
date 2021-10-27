using PollingApp.BL.Contcat;
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

namespace PollingApp.PL
{
    public partial class ViewVoters : Form
    {
        private Poll _poll;
        public ViewVoters()
        {
            InitializeComponent();
        }
        public void Show(Poll poll)
        {
            _poll = poll;
            Pnl_VotersAdd();

            this.Show();
        }
        private void Pnl_VotersAdd()
        {
            pnl_Voters.Controls.Clear();
            
            foreach (Voter voter in _poll.Voter.GetList())
                pnl_Voters.Controls.Add(ViewsEditing.Voters.AddTableVoter(voter));
            pnl_Voters.Controls.Add(ViewsEditing.Voters.AddTableVoterName());
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

                ViewVoterAdd voterAdd = ViewForms.voterAdd ?? (ViewForms.voterAdd = new ViewVoterAdd());

                this.Hide();
                voterAdd.Show(_poll);
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
        public Poll GetPoll() => _poll;
    }
}
