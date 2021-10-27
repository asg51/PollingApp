using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using PollingApp.Entities;
using PollingApp.BL;
using PollingApp.BL.Contcat;

namespace PollingApp.PL
{
    public partial class ViewToBeChosen : Form
    {
        Poll _poll;
        public ViewToBeChosen()
        {
            InitializeComponent();
        }
        public void Show(Entities.Poll poll)
        {
            _poll = poll;
            lbl_PoolName.Text = poll.PollingName;
            Pnl_ChosenAdd();
            TxtClear();
            this.Show();
        }
        private void TxtClear()
        {
            txt_Name.Text = "";
        }
        private void Pnl_ChosenAdd()
        {
            pnl_Chosens.Controls.Clear();
            foreach (Chosen chosen in _poll.Chosen.GetList())
            {
                pnl_Chosens.Controls.Add(ViewsEditing.ToBeChosen.AddTableChosen(chosen.ChosenName, Managers.chosenManager.LastIndex(_poll)));
            }
        }
        private void Pnl_ChosenAdd(string name)
        {
            pnl_Chosens.Controls.Add(ViewsEditing.ToBeChosen.AddTableChosen(name, Managers.chosenManager.LastIndex(_poll)));
        }
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);
                Managers.chosenManager.Add(new Chosen(txt_Name.Text, Managers.chosenManager.LastIndex(_poll) + 1), _poll);
                Pnl_ChosenAdd(txt_Name.Text);
                TxtClear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }

        private void Btn_Back_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);
                DialogResult dialogResult = MessageBox.Show("Geri dönerseniz yaptığınız tüm işlemler silincektir, onaylıyor musunuz?",
                    "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    ViewCreatePoll createPoll = ViewForms.createPoll ?? (ViewForms.createPoll = new ViewCreatePoll());
                    this.Hide();
                    createPoll.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
            TxtClear();
        }

        private void Btn_Approve_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);
                if (Managers.chosenManager.EnoughVoters(_poll))
                {
                    ViewVoterAndAdmin voterAndAdmin = ViewForms.voterAndAdmin ?? (ViewForms.voterAndAdmin = new ViewVoterAndAdmin());

                    this.Hide();
                    voterAndAdmin.Show(_poll);
                }
                else
                    MessageBox.Show("En az 2 seçmen olmak zorunda!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
            TxtClear();
        }

        public Poll GetPoll()
        {
            return _poll;
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
