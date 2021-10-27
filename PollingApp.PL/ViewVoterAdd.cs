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
using PollingApp.BL;
using PollingApp.BL.Contcat;
using PollingApp.Entities;

namespace PollingApp.PL
{
    public partial class ViewVoterAdd : Form
    {
        private Poll _poll;
        private Voter _voter;
        public ViewVoterAdd()
        {
            InitializeComponent();
        }
        public void Show(Poll poll)
        {
            lbl_VoterName.Text = "Seçmen Ekle";
            btn_Add.Text = "Ekle";

            _voter = null;
            _poll = poll;
            TxtClear();
            this.Show();
        }
        public void ShowEdit(Poll poll, Voter voter)
        {
            lbl_VoterName.Text = "Seçmen Düzenle";
            btn_Add.Text = "Güncelle";

            _voter = voter;
            _poll = poll;
            FillingInTxt();
            this.Show();
        }
        private void FillingInTxt()
        {
            txt_Key.Text = _voter.Key;
            txt_Name.Text = _voter.Name;
            txt_Password.Text = _voter.Password;
            txt_Surname.Text = _voter.Surname;
        }
        private void TxtClear()
        {
            txt_Key.Text = "";
            txt_Name.Text = "";
            txt_Password.Text = "";
            txt_Surname.Text = "";
        }
        private void VoterAdd()=>
            Managers.voterManager.Add(new Voter(txt_Key.Text, txt_Password.Text, txt_Name.Text, txt_Surname.Text,
                    CurrentAdmin.AdminKey, Managers.voterManager.LastIndex(_poll) + 1), _poll);
        private void VoterEdit() =>
            Managers.voterManager.Edit(_poll,_voter, new Voter(txt_Key.Text, txt_Password.Text, txt_Name.Text, txt_Surname.Text,
                    CurrentAdmin.AdminKey, Managers.voterManager.LastIndex(_poll) + 1));
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);
                if (_voter == null)
                    VoterAdd();
                else
                    VoterEdit();

                TxtClear();
                MessageBox.Show("Başarlı bir şekilde gerçekleşti.");
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
                ViewVoters voters = ViewForms.voters ?? (ViewForms.voters = new ViewVoters());

                this.Hide();
                TxtClear();
                voters.Show(_poll);
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
