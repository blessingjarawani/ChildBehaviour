using ChildBehaviour.UI.Models;
using ChildBehaviour.UI.Tabs.Interfaces;
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
        private readonly IConfigTab _configTab;
        public FrmMain(IConfigTab configTab ,CurrentUser user = null)
        {
            InitializeComponent();
            _currentUser = user;
            _configTab = configTab;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void cboConfigItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            _configTab.FillGridView(cboConfigItems.Text,dgridConfigs) ;
        }

        

        private void btnConFigsSave_Click(object sender, EventArgs e)
        {
            _configTab.SaveFromGridView(cboConfigItems.Text, dgridConfigs);
        }

        private void dgridConfigs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
