using System;

namespace RegistroPacientes
{
    // Datos de un paciente en espera.
    public class Paciente
    {
        public string Nombre { get; private set; }

        // 1 = P1 (alta), 2 = P2 (media), 3 = P3 (baja)
        public int Prioridad { get; private set; }

        // Numero consecutivo de llegada (sirve para desempatar).
        public int NumeroLlegada { get; private set; }

        public DateTime HoraLlegada { get; private set; }

        public Paciente(string nombre, int prioridad, int numeroLlegada)
        {
            Nombre = nombre;
            Prioridad = prioridad;
            NumeroLlegada = numeroLlegada;
            HoraLlegada = DateTime.Now;
        }

        public string PrioridadTexto
        {
            get { return "P" + Prioridad; }
        }

        public override string ToString()
        {
            return Nombre + " - Prioridad: " + PrioridadTexto;
        }
    }
}
