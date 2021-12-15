using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildBehaviour.UI.Tabs.Interfaces
{
    public interface IDecisionTableTab
    {
        Task LoadBehaviourSymptoms(int selectedValue, DataGridView dgrid);
        Task AddOrUpdateDecisionTable(int behaviourId, DataGridView dgrid);
    }
}