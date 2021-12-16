using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildBehaviour.UI.Tabs.Interfaces
{
    public interface IPupilsTab
    {
        Task AddorUpdatePupils(DataGridView dgrid);
        Task LoadPupils(DataGridView dgrid);
    }
}