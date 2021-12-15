using System.Windows.Forms;

namespace ChildBehaviour.UI.Tabs.Interfaces
{
    public interface IConfigTab
    {
        void FillGridView(string selectedItem, DataGridView dgrid);
        void SaveFromGridView(string selectedItem, DataGridView dgrid);
    }
}