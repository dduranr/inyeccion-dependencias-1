# Inyección de dependencias

La inyección de dependencias es una forma de implementar el principio SOLID: *inversión de dependencia*, que consiste en que las entidades (clases, objetos, etc) deben depender de abstracciones y no de implementaciones concretas.

Este proyecto es de **C#**, y está basado en el código de [The Coder Cave esp](https://www.youtube.com/@TheCoderCave): https://www.youtube.com/watch?v=0HYcf5ku3-w

El objetivo de este proyecto es que se pueda enviar un mensaje a una lista de usuarios.

## Versión 1: Sin inyección de dependencias

La clase *CommunicationService* se encarga de establecer el medio de comunicación y llamar al método para enviar el mensaje. Debería encargarse de hacer su trabajo de comunicación sin importar el tipo de plataforma o servicio de comunicación: email, SMS, notificaciones push, etc. Pero esto no pasa. La clase está armada de tal forma que sólo puede hacer su trabajo sí o sí con el servicio de email: es totalmente dependiente del *EmailService*. Claro, está bien que esta clase sea dependiente de un objeto de comunicación (depende de que se le pase una plataforma como email, sms, etc); lo que no está bien es que dependa de un objeto específico, de una plataforma concreta, como en este caso el email: Si el día de mañana se necesita que la comunicación sea vía SMS tendríamos que cambiar todo el código de esa clase.

**Clase Program (punto de entrada de la aplicación de consola)**

	namespace InyeccionDeDependencias1 {
	    class Program {
	        static void Main(string[] args) {
	            var customerService = new CustomerService();
	            var communicationService = new CommunicationService();
	            var customers = customerService.GetCustomers();
	            var message = "Este es el mensaje";

	            foreach (var customer in customers) {
	                communicationService.SendMessage(customer, message);
	            }
	        }
	    }
	}

**Clase CommunicationService**

	namespace InyeccionDeDependencias1 {
	    public class CommunicationService {
	       private EmailService _emailService;
	       public CommunicationService() {
	           _emailService = new EmailService();
	       }
	       public void SendMessage(Customer customer, string message) {
	           _emailService.Send(customer, message);
	       }
	    }
	}

## Versión 2: Con inyección de dependencias

Al igual que la versión 1, para funcionar, la clase *CommunicationService* sigue dependiendo de un objeto/dependencia, pero éste ahora es abstracto (el sender), que puede ser el que sea (email, sms, etc). De esta forma, la clase *CommunicationService* ya NO crea el objeto del que depende, no crea la dependencia, no crea la instancia del servicio de email o sms, la recibe inyectada como parámetro (el sender).

Para que esto funcione, obviamente cualquiera que sea la dependencia que se inyecte como parámetro debe tener un método Send(), para que así el método *SendMessage* pueda leer la dependencia inyectada y usar el método Send() de la dependencia y así enviar la comunicación. Justo para esto es que se creó la interface *ISender* que contiene un método Send(), y todos los servicios de comunicación (email, sms, etc) la implementan.

En resumen, en la versión **SIN** inyección de dependencias la clase estaba casada con la comunicación vía email. Pero en la versión **CON** inyección de dependencias la comunicación puede ser mediante cualquier tipo de medio, y para que esto funcione, todos los servicios de comunicación deben implementar el mismo método Send(). Al final, si se requiere alternar entre comunicación SMS o email, sólo hay que cambiar una sola línea en la clase *Program*.

**Clase Program**

	namespace InyeccionDeDependencias1 {
	    class Program {
	        static void Main(string[] args) {
	            var sender1 = new SMSService();
	            var sender2 = new EmailService();
	            var customerService = new CustomerService();
	            var communicationService = new CommunicationService(sender2);
	            var customers = customerService.GetCustomers();
	            var message = "Mensaje para transmitir a todos los clientes";

	            foreach (var customer in customers) {
	                communicationService.SendMessage(customer, message);
	            }
	        }
	    }
	}

**Iterface ISender**

	namespace InyeccionDeDependencias1 {
	    public interface ISender {
	        void Send(Customer customer, string message);
	    }
	}

**Clase EmailService**

	namespace InyeccionDeDependencias1 {
	    public class EmailService : ISender {
	        public void Send(Customer customer, string message) {
	            Trace.WriteLine($"Se envió por EMAIL el mensaje a {customer.Name}");
	        }
	    }
	}

**Clase CommunicationService**

	namespace InyeccionDeDependencias1 {
	    public class CommunicationService {
	        private ISender _sender;
	        public CommunicationService(ISender sender) {
	            _sender = sender;
	        }
	        public void SendMessage(Customer customer, string message) {
	            _sender.Send(customer, message);
	        }
	    }
	}
