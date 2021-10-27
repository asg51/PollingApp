using PollingApp.BL;
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
    public partial class ViewPollEdit : Form
    {
        Poll _poll;
        public ViewPollEdit()
        {
            InitializeComponent();
        }
        public void Show(Poll poll)
        {
            _poll = poll;
            txt_Name.Text = poll.PollingName;

            this.Show();
        }

        private void Btn_Back_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);
                this.Hide();
                TxtClear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }

        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);
                Managers.pollManager.Edit(_poll, new Poll(txt_Name.Text, -1));
                MessageBox.Show("Başarılı bir şekilde güncellenmiştir.");
                TxtClear();
                this.Hide();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }
        private void TxtClear()
        {
            txt_Name.Text = "";
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
