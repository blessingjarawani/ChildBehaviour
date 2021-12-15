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
        private readonly ITabCommons _tabCommons;
        private readonly IDecisionTableTab _decisionTableTab;
        public FrmMain(IConfigTab configTab , ITabCommons tabCommons, IDecisionTableTab decisionTableTab, CurrentUser user = null)
        {
            InitializeComponent();
            _currentUser = user;
            _configTab = configTab;
            _tabCommons = tabCommons;
            _decisionTableTab = decisionTableTab;
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {

                case 1:
                    {
                        //Your Changes
                        break;
                    }
                case 2:
                    {
                        //Your Changes 
                        break;
                    }
                case 3:
                    {
                        _tabCommons.LoadBehaviours(cboDecisionBehaviorSelect);
                        break;
                    }
                    default:
                    break;
            }
        }

        private void cboDecisionBehaviorSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedIndex = (cboDecisionBehaviorSelect.SelectedItem as ComboboxItem)?.Value ?? 0;
            if (selectedIndex > 0)
            {
                _decisionTableTab.LoadBehaviourSymptoms(selectedIndex, dgridDecisionTable);
            }
        }

        private void btnDecisionTabSave_Click(object sender, EventArgs e)
        {
            var selectedIndex = (cboDecisionBehaviorSelect.SelectedItem as ComboboxItem)?.Value ?? 0;
            if (selectedIndex > 0)
            {
                _decisionTableTab.AddOrUpdateDecisionTable(selectedIndex, dgridDecisionTable);
            }
        }
    }
}
