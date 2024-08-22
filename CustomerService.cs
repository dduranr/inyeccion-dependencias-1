namespace InyeccionDeDependencias1
{
    public class CustomerService
    {
        private CustomerRepository _repository;
        public CustomerService()
        {
            _repository = new CustomerRepository();
        }

        public List<Customer> GetCustomers()
        {
            return _repository.GetCustomers();
        }
    }
}
