namespace InyeccionDeDependencias1
{
    public class CustomerRepository
    {
        public CustomerRepository()
        {
        }
        public List<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer() { Id=1, Name="Sócrates", Email="socrates@gmail.com", Phone="12345678" },
                new Customer() { Id=2, Name="Platón", Email="platon@gmail.com", Phone="87654321" },
                new Customer() { Id=3, Name="Aristóteles", Email="aristoteles@gmail.com", Phone="12344321" }
            };
        }
    }
}
