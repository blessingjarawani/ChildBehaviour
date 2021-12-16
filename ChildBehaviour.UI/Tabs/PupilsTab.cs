using ChildBehaviour.BLL.DTOs;
using ChildBehaviour.UI.Models;
using ChildBehaviour.UI.Tabs.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildBehaviour.UI.Tabs
{
    public class PupilsTab : IPupilsTab
    {
        private readonly HttpClient _client;
        private readonly CurrentUser _currentUser;
        public PupilsTab(CurrentUser currentUser)
        {
            _client = new ApiClient().InitClient();
            _currentUser = currentUser;
        }

        public async Task LoadPupils(DataGridView dgrid)
        {
            try
            {
                dgrid.Rows.Clear();
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
                        dgrid.Rows.Add(item.Id, item.FirstName, item.Surname, item.DOB, item.IsActive);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message);
            }
        }

        public async Task AddorUpdatePupils(DataGridView dgrid)
        {
            try
            {
                if (dgrid.Rows.Count - 1 > 0)
                {

                    var pupils = new List<PupilDto>();

                    for (int i = 0; i < dgrid.Rows.Count - 1; i++)
                    {
                        var isDate = DateTime.TryParse(dgrid.Rows[i].Cells[3]?.Value?.ToString(), out var newDate);
                        if (!isDate)
                        {
                            dgrid.Rows[i].Cells[3].Style.BackColor = Color.Red;
                            MessageBox.Show($"Invalid Date on row {i + 1}");
                            return;
                        }
                        var symptom = new PupilDto
                        {
                            ParentId = _currentUser.User.Id,
                            Id = string.IsNullOrWhiteSpace(dgrid.Rows[i].Cells[0].Value?.ToString()) ? 0 : int.Parse(dgrid.Rows[i].Cells[0].Value.ToString()),
                            FirstName = dgrid.Rows[i].Cells[1].Value?.ToString(),
                            Surname = dgrid.Rows[i].Cells[2].Value?.ToString(),
                            DOB = newDate,
                            IsActive = bool.Parse(dgrid.Rows[i].Cells[4].Value?.ToString()),
                        };
                        pupils.Add(symptom);
                    }

                    var response = await _client.PostAsync($"Users/AddOrUpdatePupils", new StringContent(JsonConvert.SerializeObject(pupils), Encoding.UTF8, "application/json"));
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
