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
            
            
        }

        public void Update(Customer customer)
        {
            
           
        }

        public void Commit()
        {
            _simpleCrmDbContext.SaveChanges();
           
        }

        public List<Customer> GetByStatus(CustomerStatusType status, int pageIndex, int take, string OrderBy)
        {
            throw new NotImplementedException();
        }

        public void Delete(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
