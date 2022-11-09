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

        public List<Customer> GetAll(int pageIndex, int take, string orderBy)
        {

            var sortableColumns = new string[] { "FIRSTNAME", "LASTNAME", "EMAILADDRESS" }.ToList();
            var splitOrderBy = orderBy.Split(',');
            foreach (string sortExpression in splitOrderBy)
            {
                var nameAndDirection = sortExpression.Split(' ');
                if (nameAndDirection.Length > 2)
                {
                    throw new Exception("Too many values!");
                }
                if (nameAndDirection.Length == 2)
                {
                    var direction = nameAndDirection[1].ToUpper();
                    if (direction != "DESC" && direction != "ASC")
                        throw new Exception("invalid direction!");
                }
                var columnName = nameAndDirection[0].ToUpper();
                if (!sortableColumns.Contains(columnName))
                    throw new Exception("invalid column name!");
            }

             

            
            

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
