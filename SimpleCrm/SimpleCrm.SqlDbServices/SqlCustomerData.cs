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

        public List<Customer> GetAll(CustomerListParameters listParameters)
        {

            var sortableColumns = new string[] { "FIRSTNAME", "LASTNAME", "EMAILADDRESS" }.ToList();
            var splitOrderBy = (listParameters.Orderby ?? "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
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
             if (String.IsNullOrWhiteSpace(listParameters.Orderby))
            {
                listParameters.Orderby = "Lastname Asc";
                
                
                
            }

            // once an IQueryable is converted into an IList/List, the SQL query is finalized and sent to the database
            IQueryable<Customer> sortedResults = _context.Customer
               .OrderBy(listParameters.Orderby); //validated above to nothing unexpected, this is OK now
                                                 //  calls can be chained onto sortedResults

            if (!string.IsNullOrWhiteSpace(listParameters.LastName))
            {
                sortedResults = sortedResults
                    .Where(x => x.LastName == listParameters.LastName.Trim());

                
            } // the query still is not sent to the database after this line.


            // TOOD: add more optional where clauses for other filter parameters.
            if (!string.IsNullOrWhiteSpace(listParameters.Term))

            sortedResults = sortedResults.Where(x => (x.FirstName + " " + x.LastName).Contains(listParameters.Term)); 
               

            
            return sortedResults
                .Skip((listParameters.Page - 1) * listParameters.Take)
                .Take(listParameters.Take)
                .ToList();










        }

        
    }
}
