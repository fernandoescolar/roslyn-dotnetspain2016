using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetConference.MarranadaConRoslyn
{
    public class Alumno {
        public string Nombre { get; set; }
    }
    class Program {
        static void Main() {
            var a = new List<Alumno>() {
                new Alumno() { Nombre = "Juan Bacardit" },
                new Alumno() { Nombre = "Fernando Escolar" } };
            a.ForEach(i => Console.WriteLine(string.Format("Hello, {0}", i.Nombre)));
        }
    }
}
