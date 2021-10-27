using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PollingApp.Entities;
using PollingApp.BL;
using MetroFramework.Controls;
using PollingApp.BL.Contcat;

namespace PollingApp.PL
{
    public partial class ViewChosenEdit : Form
    {
        Poll _poll;
        Chosen _chosen;
        MetroButton _metroButton;
        public ViewChosenEdit()
        {
            InitializeComponent();
        }
        public void Show(Poll poll,Chosen chosen,MetroButton metroButton)
        {
            _poll = poll;
            _chosen = chosen;
            _metroButton = metroButton;
            txt_Name.Text = chosen.ChosenName;
            this.Show();
        }

        private void Btn_Back_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);
                TxtClear();
                this.Hide();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }

        private void Btn_Approve_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);
                Managers.chosenManager.Edit(_poll,_chosen, new Chosen(txt_Name.Text, _chosen.Index));
                ViewsEditing.ToBeChosen.Pnl_ChosnesEdit(_metroButton, txt_Name.Text);
                TxtClear();
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
        void TxtClear()
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
