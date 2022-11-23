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
            var splitOrderBy = orderBy.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string sortExpression in splitOrderBy)
            {
                if (String.IsNullOrWhiteSpace(sortExpression))
                {
                    continue;
                }
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
                {
                    throw new Exception("invalid column name!");
                }
                    
            }
             if (String.IsNullOrWhiteSpace(orderBy))
            {
                orderBy = "Lastname Asc";
            }

            return _context.Customer
                .OrderBy(orderBy)
                .Skip(pageIndex * take)
                .Take(take)
                .ToList();

             

            
            

            

        }

        
    }
}
