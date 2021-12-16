using ChildBehaviour.BLL.DTOs;
using ChildBehaviour.UI.Models;
using ChildBehaviour.UI.Tabs.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildBehaviour.UI.Tabs
{
    public class TabCommons : ITabCommons
    {
        private readonly HttpClient _client;
        public TabCommons()
        {
            _client = new ApiClient().InitClient();
        }

        public async Task LoadBehaviours(ComboBox comboBox)
        {
            try
            {
                comboBox.Items.Clear();
                var response = await _client.GetAsync($"Behaviour/GetBehaviours");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(response.StatusCode.ToString());
                    return;
                }
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<Response<IEnumerable<BehaviourDto>>>(content);
                if (items?.Success ?? false)
                {
                    foreach (var item in items.Data)
                    {
                        var comboboxItem = new ComboboxItem
                        {
                            Value = item.Id,
                            Text = item.Name
                        };
                        comboBox.Items.Add(comboboxItem);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }

       
    }
}
