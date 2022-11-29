using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrm
{
    public interface ICustomerData
    {
        IEnumerable<Customer> GetAll();
        Customer Get(int id);
        List<Customer> GetAll(CustomerListParameters customerListParameters);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete (int customerId);
        void Commit();
    }
}
