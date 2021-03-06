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
    public class DecisionTableTab : IDecisionTableTab
    {
        private readonly HttpClient _client;
        public DecisionTableTab()
        {
            _client = new ApiClient().InitClient();
        }



        public async Task LoadBehaviourSymptoms(int selectedValue, DataGridView dgrid)
        {
            try
            {
                dgrid.Rows.Clear();
                var response = await _client.GetAsync($"Behaviour/GetBehaviourSymptoms?id={selectedValue}");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(response.StatusCode.ToString());
                    return;
                }
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<Response<IEnumerable<BehaviourDto>>>(content);
                if (items?.Success ?? false)
                {
                    var symptoms = items.Data.SelectMany(t => t.Symptoms);
                    foreach (var item in symptoms)
                    {
                        dgrid.Rows.Add(item.Id, item.Name, item.IsActive);

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }
        public async Task AddOrUpdateDecisionTable(int behaviourId, DataGridView dgrid)
        {
            try
            {
                if (dgrid.Rows.Count > 0)
                {

                    var behaviour = new BehaviourDto
                    {
                        Id = behaviourId,
                        Symptoms = new List<SymptomDto>()
                    };
                    for (int i = 0; i < dgrid.Rows.Count; i++)
                    {
                        var symptom = new SymptomDto
                        {
                            Id = string.IsNullOrWhiteSpace(dgrid.Rows[i].Cells[0].Value?.ToString()) ? 0 : int.Parse(dgrid.Rows[i].Cells[0].Value.ToString()),
                            Name = dgrid.Rows[i].Cells[1].Value?.ToString(),
                            IsActive = bool.Parse(dgrid.Rows[i].Cells[2].Value?.ToString()),
                        };
                        behaviour.Symptoms.Add(symptom);
                    }

                    var response = await _client.PostAsync($"Behaviour/AddBehaviourSymptoms", new StringContent(JsonConvert.SerializeObject(behaviour), Encoding.UTF8, "application/json"));
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


    }
}
