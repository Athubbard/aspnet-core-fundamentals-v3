using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrm.SqlDbServices
{
    public class SqlCustomerData : ICustomerData
    {
        private readonly SimpleCrmDbContext _simpleCrmDbContext;

        public SqlCustomerData(SimpleCrmDbContext simpleCrmDbContext)
        {
            _simpleCrmDbContext = simpleCrmDbContext;
        }

        public Customer Get(int id)
        {
            return _simpleCrmDbContext.Customer.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _simpleCrmDbContext.Customer.ToList();
        }

        public void Add(Customer customer)
        {
            _simpleCrmDbContext.Add(customer);
            
            _simpleCrmDbContext.SaveChanges();
        }

        public void Update(Customer customer)
        {
            
            _simpleCrmDbContext.SaveChanges();
        }
    }
}
