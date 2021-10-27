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
using PollingApp.Entities;

namespace PollingApp.PL
{
    public partial class ViewCreatePoll : Form
    {
        private static bool _state = false;
        public ViewCreatePoll()
        {
            InitializeComponent();
        }
        public void Show(bool state)
        {
            _state = state;
            this.Show();
        }
        private void Btn_Approve_Click(object sender, EventArgs e)
        {
            try
            {
                Poll poll = new Poll(txt_PollingName.Text, PollingList.dbPoll.GetLastIndex() + 1);
                Admin admin = new Admin(txt_AdminKey.Text, txt_AdminPassword.Text, txt_AdminName.Text, txt_AdminSurname.Text, 1);

                Managers.pollManager.Add(poll, admin);
                TxtClear();

                ViewToBeChosen toBeChosen = ViewForms.toBeChosen ?? (ViewForms.toBeChosen = new ViewToBeChosen());

                this.Hide();
                TxtClear();
                toBeChosen.Show(poll);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Btn_Back_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (_state)
                ViewForms.homePageForAdmin.Show();
            else
                ViewForms.loginPage.Show();

            TxtClear();
        }
        private void TxtClear()
        {
            txt_AdminKey.Text = "";
            txt_AdminName.Text = "";
            txt_AdminPassword.Text = "";
            txt_AdminSurname.Text = "";
            txt_PollingName.Text = "";
        }
    }
}
