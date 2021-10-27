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
    public partial class ViewVoterAndAdmin : Form
    {
        private Poll _poll;
        public ViewVoterAndAdmin()
        {
            InitializeComponent();
        }
        public void Show(Poll poll)
        {
            _poll = poll;

            NumberOfSettings();
            Pnl_ChosensAdd();
            SetDate();

            this.Show();
        }
        private void SetDate()
        {
            if (_poll.PollTime != null)
            {
                datetime_Start.Value = _poll.PollTime.StartTime;
                datetime_Finish.Value = _poll.PollTime.FinishTime;
            }
        }
        private void NumberOfSettings()
        {
            lbl_NumberOfAdmin.Text = _poll.Admins.Count.ToString();
            lbl_NumberOfVoter.Text = _poll.Voter.Count.ToString();
        }
        private void Pnl_ChosensAdd()
        {
            pnl_Chosens.Controls.Clear();
            foreach (Chosen chosen in _poll.Chosen.GetList())
            {
                Label label = new Label
                {
                    Text = chosen.ChosenName,
                    ForeColor = Color.White,
                    BackColor = Color.Transparent,
                    Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(162))),
                    Anchor = AnchorStyles.None
                };
                Panel panel = new Panel()
                {
                    Dock = DockStyle.Top,
                    Height = 25,
                    Width = pnl_Chosens.Width,
                    BackColor = Color.Transparent
                };
                panel.Controls.Add(label);
                pnl_Chosens.Controls.Add(panel);

            }
        }
        private void Btn_Admins_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);
                ViewAdmins admins = ViewForms.admins ?? (ViewForms.admins = new ViewAdmins());

                this.Hide();
                admins.Show(_poll);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }

        private void Btn_Voters_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);
                ViewVoters voters = ViewForms.voters ?? (ViewForms.voters = new ViewVoters());

                this.Hide();
                voters.Show(_poll);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }

        private void Btn_AddVoter_Click(object sender, EventArgs e)
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

        private void Btn_AddAdmin_Click(object sender, EventArgs e)
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

        private void Btn_Back_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);
                ViewToBeChosen toBeChosen = ViewForms.toBeChosen ?? (ViewForms.toBeChosen = new ViewToBeChosen());

                this.Hide();
                toBeChosen.Show(_poll);
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

                ViewHomePageForAdmin homePageForAdmin = ViewForms.homePageForAdmin ?? 
                    (ViewForms.homePageForAdmin = new ViewHomePageForAdmin());
                Managers.pollManager.AddPollTime(_poll, new PollTime(datetime_Start.Value, datetime_Finish.Value));

                homePageForAdmin.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }

        private void Btn_Chosen_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);

                ViewToBeChosen toBeChosen = ViewForms.toBeChosen ?? (ViewForms.toBeChosen = new ViewToBeChosen());

                this.Hide();
                toBeChosen.Show(_poll);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }

        private void Btn_PollEdit_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);

                ViewPollEdit pollEdit = ViewForms.pollEdit ?? (ViewForms.pollEdit = new ViewPollEdit());

                pollEdit.Show(_poll);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            try
            {
                PollStateManager.PollIsControl(_poll);

                ViewHomePageForAdmin homePageForAdmin = ViewForms.homePageForAdmin ??
                    (ViewForms.homePageForAdmin = new ViewHomePageForAdmin());

                Managers.pollManager.AddPollTime(_poll, new PollTime(datetime_Start.Value, datetime_Finish.Value));

                DialogResult dialogResult = MessageBox.Show("Bir daha bu bilgileri değiştiremiyeksiniz ve zamanı gelince seçim otamatik başlıyacaktır onaylıyor musunuz?"
                , "Dikkat!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    StartPoll(_poll);
                    this.Hide();
                    homePageForAdmin.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                BlockChainControlState(ex.Message);
            }
        }
        private void StartPoll(Poll poll)
        {
            Managers.clientManager.BlockChainStart(poll.Urls, poll.PollingName);
            Task.Run(async () =>
            {
                CheckForIllegalCrossThreadCalls = false;
                await Managers.blockChainManager.AddAsync(poll);
            });
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
