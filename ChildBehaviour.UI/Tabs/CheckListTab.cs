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
    public class CheckListTab : ICheckListTab
    {
        private readonly HttpClient _client;
        private readonly CurrentUser _currentUser;
        public CheckListTab(CurrentUser currentUser)
        {
            _client = new ApiClient().InitClient();
            _currentUser = currentUser;
        }

        public async Task LoadPupils(ComboBox comboBox)
        {
            try
            {
                comboBox.Items.Clear();
                var response = await _client.GetAsync($"Users/GetParentPupils?id={_currentUser.User.Id}");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(response.StatusCode.ToString());
                    return;
                }
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<Response<IEnumerable<PupilDto>>>(content);
                if (items?.Success ?? false)
                {
                    foreach (var item in items.Data)
                    {
                        var comboboxItem = new ComboboxItem
                        {
                            Value = item.Id,
                            Text = item.FullName
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

        public async Task Execute(DataGridView dgrid)
        {
            try
            {
                if (dgrid.Rows.Count > 0)
                {

                    var symptoms = new List<SymptomDto>();
                    for (int i = 0; i < dgrid.Rows.Count; i++)
                    {
                        var symptom = new SymptomDto
                        {
                            Id = string.IsNullOrWhiteSpace(dgrid.Rows[i].Cells[0].Value?.ToString()) ? 0 : int.Parse(dgrid.Rows[i].Cells[0].Value.ToString()),
                            Name = dgrid.Rows[i].Cells[1].Value?.ToString(),
                            IsActive = bool.Parse(dgrid.Rows[i].Cells[2].Value?.ToString()),
                        };
                        symptoms.Add(symptom);
                    }
                    var response = await _client.PostAsync($"Diagnosis/Execute", new StringContent(JsonConvert.SerializeObject(symptoms), Encoding.UTF8, "application/json"));
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(response.StatusCode.ToString());
                        return;
                    }
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BaseResponse>(content);
                    MessageBox.Show(result.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }

        public async void FillSymptoms(DataGridView dgrid)
        {
            try
            {
                dgrid.Rows.Clear();

                var response = await _client.GetAsync($"Symptoms/GetSymptoms");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(response.StatusCode.ToString());
                    return;
                }
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<Response<IEnumerable<SymptomDto>>>(content);
                if (items?.Success ?? false)
                {
                    foreach (var item in items.Data)
                    {
                        dgrid.Rows.Add(item.Id, item.Name, false);
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
