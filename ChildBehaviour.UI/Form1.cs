using ChildBehaviour.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildBehaviour.UI
{
    public partial class FrmMain : Form
    {
        private readonly CurrentUser _currentUser;
        public FrmMain(CurrentUser user = null)
        {
            InitializeComponent();
            _currentUser = user;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
