using System.Diagnostics;

namespace InyeccionDeDependencias1
{
    public class EmailService : ISender
    {
        public void Send(Customer customer, string message)
        {
            Trace.WriteLine($"Se envió por EMAIL el mensaje a {customer.Name}");
        }
    }
}
