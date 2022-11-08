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
        List<Customer> GetByStatus(CustomerStatusType status, int pageIndex, int take, string OrderBy);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete (int customerId);
        void Commit();
    }
}
