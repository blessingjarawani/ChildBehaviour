
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
        public FrmLogin(IConfigTab configTab, CurrentUser currentUser)
        {
            InitializeComponent();
            _client = new ApiClient().InitClient();
            _currentUser = currentUser;
            _configTab = configTab;

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
                var frmMain = new FrmMain(_configTab,currentUser);
                frmMain.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }

        }
    }
}
