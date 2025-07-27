using DataAccess;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class CustomerService : BaseService<Customer>
    {
        public CustomerService(IGenericRepository<Customer> repository) : base(repository) { }

        // Add any customer-specific business logic here
        public IEnumerable<Customer> SearchByName(string name)
        {
            return repository.Find(c => c.CustomerFullName.Contains(name));
        }

        public Customer Authenticate(string email, string password)
        {
            return repository.Find(c => c.EmailAddress == email && c.Password == password).FirstOrDefault();
        }
    }
} 