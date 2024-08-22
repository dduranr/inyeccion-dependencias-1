using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InyeccionDeDependencias1
{
    public class SMSService : ISender
    {
        public void Send(Customer customer, string message)
        {
            Trace.WriteLine($"Se envió por SMS el mensaje a {customer.Name}");
        }
    }
}
