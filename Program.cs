//Este es un ejemplo en donde las entidades NO son inyectadas sino sumamente dependientes
namespace InyeccionDeDependencias1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sender1 = new SMSService();
            var sender2 = new EmailService();
            var customerService = new CustomerService();
            var communicationService = new CommunicationService(sender1);

            var customers = customerService.GetCustomers();

            var message = "Mensaje para transmitir a todos los clientes";

            foreach (var customer in customers)
            {
                communicationService.SendMessage(customer, message);
            }
        }
    }
}
