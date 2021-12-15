using ChildBehaviour.BLL.DTOs;
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
    public class ConfigTab : IConfigTab
    {
        private readonly HttpClient _client;
        public ConfigTab()
        {
            _client = new ApiClient().InitClient();
        }

        private async void FillRecommendations(DataGridView dgrid)
        {
            try
            {
                dgrid.Rows.Clear();

                var response = await _client.GetAsync($"Recommendations/GetRecommendations");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(response.StatusCode.ToString());
                    return;
                }
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<Response<IEnumerable<RecommendationDto>>>(content);
                if (items?.Success ?? false)
                {
                    foreach (var item in items.Data)
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

        private async void FillSymptoms(DataGridView dgrid)
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
                        dgrid.Rows.Add(item.Id, item.Name, item.IsActive);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }
        private async void FillBehaviours(DataGridView dgrid)
        {
            try
            {
                dgrid.Rows.Clear();

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
                        dgrid.Rows.Add(item.Id, item.Name, item.IsActive);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }

        private async void AddOrUpdateBehaviours(DataGridView dgrid)
        {
            try
            {
                if (dgrid.Rows.Count - 1 > 0)
                {
                    var items = new List<BehaviourDto>();
                    for (int i = 0; i < dgrid.Rows.Count - 1; i++)
                    {
                        var item = new BehaviourDto
                        {
                            Id = string.IsNullOrWhiteSpace(dgrid.Rows[i].Cells[0].Value?.ToString()) ? 0 : int.Parse(dgrid.Rows[i].Cells[0].Value.ToString()),
                            Name = dgrid.Rows[i].Cells[1].Value?.ToString(),
                            IsActive = bool.Parse(dgrid.Rows[i].Cells[2].Value?.ToString()),
                        };
                        items.Add(item);
                    }

                    var response = await _client.PostAsync($"Behaviour/AddOrUpdateBehaviour", new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json"));
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

        private async void AddOrUpdateRecommendations(DataGridView dgrid)
        {
            try
            {
                if (dgrid.Rows.Count - 1 > 0)
                {
                    var items = new List<RecommendationDto>();
                    for (int i = 0; i < dgrid.Rows.Count - 1; i++)
                    {
                        var item = new RecommendationDto
                        {
                            Id = string.IsNullOrWhiteSpace(dgrid.Rows[i].Cells[0].Value?.ToString()) ? 0 : int.Parse(dgrid.Rows[i].Cells[0].Value.ToString()),
                            Name = dgrid.Rows[i].Cells[1].Value?.ToString(),
                            IsActive = bool.Parse(dgrid.Rows[i].Cells[2].Value?.ToString()),
                        };
                        items.Add(item);
                    }

                    var response = await _client.PostAsync($"Recommendations/AddOrUpdate", new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json"));
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

        private async void AddOrUpdateSymptoms(DataGridView dgrid)
        {
            try
            {
                if (dgrid.Rows.Count - 1 > 0)
                {
                    var items = new List<SymptomDto>();
                    for (int i = 0; i < dgrid.Rows.Count - 1; i++)
                    {
                        var item = new SymptomDto
                        {
                            Id = string.IsNullOrWhiteSpace(dgrid.Rows[i].Cells[0].Value?.ToString()) ? 0 : int.Parse(dgrid.Rows[i].Cells[0].Value.ToString()),
                            Name = dgrid.Rows[i].Cells[1].Value?.ToString(),
                            IsActive = bool.Parse(dgrid.Rows[i].Cells[2].Value?.ToString()),
                        };
                        items.Add(item);
                    }

                    var response = await _client.PostAsync($"Symptoms/AddOrUpdate", new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json"));
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

        public void FillGridView(string selectedItem, DataGridView dgrid)
        {
            switch (selectedItem)
            {
                case "Behaviours":
                    {
                        FillBehaviours(dgrid);
                        break;
                    }
                case "Recommendations":
                    {
                        FillRecommendations(dgrid);
                        break;
                    }
                case "Symptoms":
                    {
                        FillSymptoms(dgrid);
                        break;
                    }
                default:
                    break;
            }
        }

        public void SaveFromGridView(string selectedItem, DataGridView dgrid)
        {
            switch (selectedItem)
            {
                case "Behaviours":
                    {
                        AddOrUpdateBehaviours(dgrid);
                        break;
                    }
                case "Recommendations":
                    {
                        AddOrUpdateRecommendations(dgrid);
                        break;
                    }
                case "Symptoms":
                    {
                        AddOrUpdateSymptoms(dgrid);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
