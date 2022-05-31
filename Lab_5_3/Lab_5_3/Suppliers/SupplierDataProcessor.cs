using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    internal class SupplierDataProcessor : IDBObjectDataProcessor
    {
        private static ISupplierRepository supplierRepository = default;
        public string MenuTitle { get => "SUPPLIER MENU"; }

        public SupplierDataProcessor(ISupplierRepository repository)
        {
            if (supplierRepository == null)
                supplierRepository = repository;
        }

        public static Supplier SelectSupplier()
        {
            var suppliers = (List<Supplier>)supplierRepository.GetSuppliers();
            var supplierID = UserInteraction.ChooseElementFromList<Supplier>(suppliers, "Choose supplier");
            return suppliers[supplierID];
        }
        public void CreateObject()
        {
            var newSupplier = new Supplier
            {
                Name = UserInteraction.GetString("Enter supplier name"),
                Phone = UserInteraction.GetString("Enter supplier phone"),
                EMail = UserInteraction.GetString("Enter supplier Email")
            };

            supplierRepository.CreateSupplier(newSupplier);
        }

        public void DeleteObject()
        {
            var id = UserInteraction.GetInt("Enter supplier ID");
            supplierRepository.DeleteSupplier(id);
        }

        public void EditObject()
        {
            var supplier = GetSupplierByID();

            if (supplier == null)
                return;

            var fields = VariableFields.GetChangingFields(typeof(Supplier));

            while (true)
            {
                var index = UserInteraction.ChooseElementFromList<VariableFields>(fields, "Choose field to edit");

                if (index == fields.Count - 1)
                {
                    if (supplier.IsModified)
                    {
                        supplierRepository.UpdateSupplier(supplier);
                    }
                    return;
                }

                var newValue = UserInteraction.GetString($"Enter new {fields[index]} for {supplier}");

                switch (fields[index].FieldName)
                {
                    case "Name":
                        supplier.Name = newValue;
                        break;

                    case "Phone":
                        supplier.Phone = newValue;
                        break;

                    case "EMail":
                        supplier.EMail = newValue;
                        break;

                }

                supplier.SetModifiedOn();
            }
        }

        public void ReadObject()
        {
            var supplier = GetSupplierByID();
            Console.WriteLine(supplier);
        }

        public void ShowAllObjects()
        {
            var suppliers = supplierRepository.GetSuppliers();

            DatabaseTable.ShowObjectsList(suppliers, "Suppliers:");           
        }

        public static Supplier GetSupplierByID()
        {
            var id = UserInteraction.GetInt("Enter supplier ID");

            return GetSupplierByID(id);
        }

        public static Supplier GetSupplierByID(int id)
        {
            return supplierRepository.GetSupplier(id);
        }
    }
}
