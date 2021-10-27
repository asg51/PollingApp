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
    public partial class ViewAdminAdd : Form
    {
        private Poll _poll;
        private Admin _admin;
        public ViewAdminAdd()
        {
            InitializeComponent();
        }

        public void Show(Poll poll)
        {
            lbl_AdminName.Text = "Admin Ekle";
            btn_Add.Text = "Ekle";

            _admin = null;
            _poll = poll;
            TxtClear();
            this.Show();
        }
        public void ShowEdit(Poll poll, Admin admin)
        {
            lbl_AdminName.Text = "Admin Düzenle";
            btn_Add.Text = "Güncelle";

            _admin = admin;
            _poll = poll;
            FillingInTxt();
            this.Show();
        }
        private void AdminAdd() =>
            Managers.adminManager.Add(new Admin(txt_Key.Text, txt_Password.Text, txt_Name.Text, txt_Surname.Text,
                    Managers.voterManager.LastIndex(_poll) + 1), _poll);
        private void AdminEdit() =>
            Managers.adminManager.Edit(_poll,_admin, new Admin(txt_Key.Text, txt_Password.Text, txt_Name.Text, txt_Surname.Text,
                     Managers.voterManager.LastIndex(_poll) + 1));
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);
                if (_admin == null)
                    AdminAdd();
                else
                    AdminEdit();

                TxtClear();
                MessageBox.Show("Başarlı bir şekilde gerçekleşti.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }

        private void TxtClear()
        {
            txt_Key.Text = "";
            txt_Name.Text = "";
            txt_Password.Text = "";
            txt_Surname.Text = "";
        }
        private void FillingInTxt()
        {
            txt_Key.Text = _admin.Key;
            txt_Name.Text = _admin.Name;
            txt_Password.Text = _admin.Password;
            txt_Surname.Text = _admin.Surname;
        }
        private void Btn_Back_Click(object sender, EventArgs e)
        {
            PollStateManager.PollIsControl(_poll);
            TxtClear();
            ViewAdmins admins = ViewForms.admins ?? (ViewForms.admins = new ViewAdmins());

            this.Hide();
            admins.Show(_poll);
        }
        private void BlockChainControlState(string message)
        {
            if(message== "Seçim başlatılmış durumdadır artık değişiklik yapamazsınız!"||message== "Seçim Silinmiştir!")
            {
                this.Hide();
                ViewHomePageForAdmin homePageForAdmin=ViewForms.homePageForAdmin ?? (ViewForms.homePageForAdmin = new ViewHomePageForAdmin());
                homePageForAdmin.Show();
            }
        }
    }
}
