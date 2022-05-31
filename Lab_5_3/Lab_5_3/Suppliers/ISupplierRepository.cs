using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    interface ISupplierRepository  
    {
        public IEnumerable<Supplier> GetSuppliers();
        public Supplier GetSupplier(int id);
        public void UpdateSupplier(Supplier supplier);
        public void CreateSupplier(Supplier supplier);
        public void DeleteSupplier(int id);
    }
}
