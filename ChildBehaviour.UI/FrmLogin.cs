
using ChildBehaviour.BLL.DTOs;
using ChildBehaviour.UI.Models;
using ChildBehaviour.UI.Tabs.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ChildBehaviour.UI
{
    public partial class FrmLogin : Form
    {
        private readonly HttpClient _client;
        private readonly CurrentUser _currentUser;
        private readonly IConfigTab _configTab;
        private readonly ITabCommons _tabCommons;
        private readonly IDecisionTableTab _decisionTableTab;
        private readonly IRecommendationsTab _recommendationsTab;
        private readonly IPupilsTab _pupilsTab;
        private readonly ICheckListTab _checkListTab;
        public FrmLogin(IConfigTab configTab, ITabCommons tabCommons, IDecisionTableTab decisionTableTab,
            IRecommendationsTab recommendationsTab, IPupilsTab pupilsTab,ICheckListTab checkListTab, CurrentUser currentUser)
        {
            InitializeComponent();
            _client = new ApiClient().InitClient();
            _currentUser = currentUser;
            _configTab = configTab;
            _tabCommons = tabCommons;
            _decisionTableTab = decisionTableTab;
            _recommendationsTab = recommendationsTab;
            _pupilsTab = pupilsTab;
            _checkListTab = checkListTab;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Please fill all details");
                return;
            }
            Login(_currentUser);
        }

        private async Task Login(CurrentUser currentUser)
        {
            try
            {

                var command = new LoginDto
                {
                    UserName = txtUserName.Text,
                    Password = txtPassword.Text
                };

                var response = await _client.PostAsync($"Users/Login", new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(response.StatusCode.ToString());
                    return;
                }
                var content = await response.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<Response<UserDto>>(content);

                if (!user.Success)
                {
                    MessageBox.Show(user.Message);
                    return;
                }
                currentUser.Set(user.Data);
                this.Hide();
                var frmMain = new FrmMain(_configTab, _tabCommons, _decisionTableTab,
                                          _recommendationsTab,_pupilsTab,_checkListTab, currentUser);
                frmMain.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }

        }
    }
}
