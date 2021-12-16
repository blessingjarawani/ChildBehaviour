using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildBehaviour.UI.Tabs.Interfaces
{
    public interface IRecommendationsTab
    {
        Task AddOrUpdateRecommendations(int behaviourId, DataGridView dgrid);
        Task LoadBehaviourRecommondations(int selectedValue, DataGridView dgrid);
    }
}