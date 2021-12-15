using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildBehaviour.UI.Tabs.Interfaces
{
    public interface ITabCommons
    {
        Task LoadBehaviours(ComboBox comboBox);
    }
}