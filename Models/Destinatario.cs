using System;
using System.Collections.Generic;
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
    public class Seleccionable
    {
        public IEnumerable<string> Niveles {get; set;}
        public IEnumerable<string> Grados { get; set; }
        public IEnumerable<string> Secciones { get; set; }

    }
    public class Mensaje_Destinatarios
    {
        public List<Destinatario> Lista {get;set;}
        public string Mensaje {get; set;}
        public string NombreQ { get; set;}
        public string NivelQ { get; set; }
        public string GradoQ { get; set; }
        public string SeccionQ { get; set; }
        public Seleccionable Select { get; set; }
    }
    
}