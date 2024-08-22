namespace InyeccionDeDependencias1
{
    /// <summary>
    /// Versión 1:  Sin inyección de dependencias
    /// La clase CommunicationService debería tratar con objetos abstractos, no con objetos específicos como el EmailService. Esta clase debería encargarse de hacer su trabajo de comunicación sin importar el tipo de plataforma o servicio de comunicación: email, SMS, notificaciones push, etc. Pero esto no pasa, está armada de tal forma que sólo puede hacer su trabajo sí o sí con el servicio de email: es totalmente dependiente del EmailService. Claro, está bien que esta clase sea dependiente de un objeto de comunicación (depende de que se le pase una plataforma como email, sms, etc); lo que no está bien es que dependa de un objeto específico, de una plataforma específica, como en este caso el email: Si el día de mañana se necesita que la comunicación sea vía por SMS tendríamos que cambiar todo el código.
    /// </summary>
    //public class CommunicationService
    //{
    //    private EmailService _emailService;
    //    public CommunicationService()
    //    {
    //        _emailService = new EmailService();
    //    }

    //    public void SendMessage(Customer customer, string message)
    //    {
    //        _emailService.Send(customer, message);
    //    }
    //}


    /// <summary>
    /// Versión 2:  Con inyección de dependencias
    /// Al igual que la versión 1, para funcionar, la clase CommunicationService sigue dependiendo de un objeto/dependencia, pero éste es abstracto (el sender), que unas veces puede ser email, otras sms, etc. De esta forma, la clase CommunicationService ya NO crea el objeto del que depende, no crea la instancia del servicio de email o sms, la recibe inyectada como parámetro (el sender).
    /// Para que esto funcione, obviamente cualquiera que sea la dependencia que se inyecte/envíe como parámetro debe tener un método Send(), para que así el método SendMessage pueda leer la dependencia inyectada y enviar la comunicación. Justo para esto es que se creó la interface ISender que contiene un método Send, y todos los servicios de comunicación (email, sms, etc) implementan esta interface.
    /// En resumen, en la versión SIN inyección de dependencias la clase estaba casada con la comunicación vía email. Pero en la versión CON inyección de dependencias la comunicación puede ser mediante cualquier tipo de comunicación, y para que esto funcione, todos los servicios de comunicación deben implementar el mismo método Send.
    /// </summary>
    public class CommunicationService
    {
        private ISender _sender;
        public CommunicationService(ISender sender)
        {
            _sender = sender;
        }

        public void SendMessage(Customer customer, string message)
        {
            _sender.Send(customer, message);
        }
    }

}
