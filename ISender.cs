using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InyeccionDeDependencias1
{
    public interface ISender
    {
        void Send(Customer customer, string message);
    }
}
