
namespace Lab_5_3
{   public interface IDBObjectDataProcessor
    {
        public string MenuTitle { get;  }
        public void ShowAllObjects();
        public void CreateObject();
        public void ReadObject();
        public void EditObject();
        public void DeleteObject();
    }
}
