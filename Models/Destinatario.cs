using System;

namespace sms_project.Models
{
    public class Destinatario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string numero { get; set; }      
        public string Nivel {get;set;}
        public string Grado {get;set;}
        public string Seccion {get;set;}
    }
}