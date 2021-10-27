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

namespace PollingApp.PL
{
    public partial class ViewMessageBox : Form
    {
        public ViewMessageBox()
        {
            InitializeComponent();
        }

        public void Show(string message)
        {
            lbl_Message.Text = message;
            this.Show();
        }
    }
}
