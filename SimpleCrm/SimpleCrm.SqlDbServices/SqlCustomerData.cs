using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;


namespace SimpleCrm.SqlDbServices
{
    public class SqlCustomerData : ICustomerData
    {
        private readonly SimpleCrmDbContext _context;

        public SqlCustomerData(SimpleCrmDbContext simpleCrmDbContext)
        {
            _context = simpleCrmDbContext;
                                                  
        }

        public Customer Get(int id)
        {
            return _context.Customer.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customer.ToList();
        }

        
        public void Add(Customer customer)
        {
            _context.Add(customer);
            
            
        }

        public void Update(Customer customer)
        {
            
           
        }
        public void Delete(int id)
        {
            var cust = new Customer { Id = id };
            
            _context.Remove(cust);
        }

        public void Commit()
        {
            _context.SaveChanges();
           
        }

        public List<Customer> GetByStatus(CustomerStatusType status, int pageIndex, int take, string orderBy)
        {
            

            var splitOrderBy = orderBy.Split(',');
            foreach (string sortExpression in splitOrderBy)
            {
                var nameAndDirection = sortExpression.Split(' ');
                if (nameAndDirection.Length == 2)
                {
                    var direction = nameAndDirection[1].ToUpper();
                    if (direction != "DESC" && direction != "ASC")
                        throw new Exception("invalid direction!");
                }
            }

            /** foreach (string sortExpression in splitOrderBy)
             {
                 var columnName = sortExpression.Split(' ');
                 if (!columnName.Contains("FirstName", "LastName", "EmailAddress") 
                 {
                 
                         throw new Exception("invalid coumn name!");
                 }
             }**/

            var sortableColumns = new string[] { "FirstName", "LastName", "EmailAddress" }.ToList();
            if (!sortableColumns.Contains("a".ToUpper()))
                throw new Exception("invalid coumn name!");

            var query = _context.Customer.Where(x => x.Status == status);
                if (!string.IsNullOrWhiteSpace(orderBy))
            {
                query = query.OrderBy(orderBy); // this "works", but don't do this. don't trust the client code (which can be spoofed)
            }
            return query.Skip(pageIndex * take)
              .Take(take)
              .ToList();

        }

        
    }
}
