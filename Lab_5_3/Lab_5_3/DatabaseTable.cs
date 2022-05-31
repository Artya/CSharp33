using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    public class DatabaseTable
    {
        public int ID { get; set; }
        public bool IsModified { get; private set; }

        public void SetModifiedOn()
        {
            IsModified = true;
        }

        public static void WorkWithDBObject<T>(T menuHandler) where T: IDBObjectDataProcessor
        {
            while (true)
            {
                var operation = (DBObjectMenu)UserInteraction.ChooseEnumValue(menuHandler.MenuTitle, typeof(DBObjectMenu));

                switch (operation)
                {
                    case DBObjectMenu.ReturnToMainMenu:
                        return;

                    case DBObjectMenu.ShowAllElements:
                        menuHandler.ShowAllObjects();
                        break;

                    case DBObjectMenu.GetElement:
                        menuHandler.ReadObject();
                        break;

                    case DBObjectMenu.CreateNewElement:
                        menuHandler.CreateObject();
                        break;

                    case DBObjectMenu.UpdateElement:
                        menuHandler.EditObject();
                        break;

                    case DBObjectMenu.DeleteElement:
                        menuHandler.DeleteObject();
                        break;

                    default:
                        Console.WriteLine($"Operation {operation} not implemented");
                        break;
                }
            }
        }

        public static void ShowObjectsList(IEnumerable<DatabaseTable> objects, string title)
        {
            Console.WriteLine(title);

            foreach (var obj in objects)
            {
                Console.WriteLine($"{obj} ({obj.ID})");
            }

            Console.WriteLine();
        }

        public virtual Dictionary<string, string> GetFields() 
        {
            return new Dictionary<string, string>();
        }
    }
}
