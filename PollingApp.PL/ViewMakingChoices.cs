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
    public partial class ViewMakingChoices : Form
    {
        internal BlockChainsData _blockChainsData;
        internal int _index;
        public ViewMakingChoices()
        {
            InitializeComponent();
            Managers.clientManager.Voted += ClientManager_Voted;
            Managers.clientManager.NotVoted += ClientManager_NotVoted;
        }

        private void ClientManager_NotVoted(BlockChainsData blockChainsData, int index)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    MessageBox.Show("Hata oy kullanamadı!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);

                    this.Hide();

                    ViewVoterLogin voterLogin = ViewForms.voterLogin ?? (ViewForms.voterLogin = new ViewVoterLogin());

                    voterLogin.Show(blockChainsData);
                });
            }
        }

        private void ClientManager_Voted(BlockChainsData blockChainsData, int index)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    MessageBox.Show("Başaralı bir şekilde oy kullanıldı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();

                    ViewVoterLogin voterLogin = ViewForms.voterLogin ?? (ViewForms.voterLogin = new ViewVoterLogin());

                    voterLogin.Show(blockChainsData);
                });
            }
        }

        public void Show(BlockChainsData blockchaindata, int index)
        {
            _blockChainsData = blockchaindata;
            _index = index;
            PnlChonesFill();
            this.Show();
        }
        private void PnlChonesFill()
        {
            pnl_Chosens.Controls.Clear();
            pnl_Chosens.Controls.Add(ViewsEditing.MakingChoices.GetChosenTable(_blockChainsData, pnl_Chosens.Width));
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Oy kullanmadan çıkarsanız otomatik olarak boş olarak atanacaktır!" +
                  "\nÇıkmak istiyormusunuz", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Managers.blockChainManager.Voting(_blockChainsData.BlockChainForPollName, _index, -1);
                MessageBox.Show("Oy kullanılıyor lütfen bekleyiniz!", "Bekleme");
            }
        }
    }
}
