using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildBehaviour.UI.Tabs.Interfaces
{
    public interface ICheckListTab
    {
        Task Execute(DataGridView dgrid);
        Task LoadPupils(ComboBox comboBox);
        void FillSymptoms(DataGridView dgrid);
    }
}