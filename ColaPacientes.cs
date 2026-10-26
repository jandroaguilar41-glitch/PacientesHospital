using System;
using System.Collections.Generic;

namespace RegistroPacientes
{
    // Nodo de la lista enlazada.
    internal class NodoPaciente
    {
        public Paciente Dato;
        public NodoPaciente Siguiente;

        public NodoPaciente(Paciente dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }

    // Cola de prioridad implementada con una lista enlazada propia.
    // La lista se mantiene SIEMPRE ordenada:
    //   1) por prioridad: P1 primero, luego P2 y al final P3.
    //   2) a igual prioridad, por orden de llegada (el que llego antes va antes).
    // El frente de la lista es el siguiente paciente a atender.
    public class ColaPacientes
    {
        public const int PrioridadAlta = 1;   // P1
        public const int PrioridadMedia = 2;  // P2
        public const int PrioridadBaja = 3;   // P3

        private NodoPaciente frente;
        private int cantidad;
        private int contadorLlegada;

        public int Cantidad
        {
            get { return cantidad; }
        }

        public bool EstaVacia
        {
            get { return frente == null; }
        }

        // Convierte "P1", "P2" o "P3" al numero 1, 2 o 3. Devuelve 0 si no es valida.
        public static int ConvertirPrioridad(string texto)
        {
            if (texto == null) return 0;
            switch (texto.Trim().ToUpper())
            {
                case "P1": return PrioridadAlta;
                case "P2": return PrioridadMedia;
                case "P3": return PrioridadBaja;
                default: return 0;
            }
        }

        // Agrega un paciente en la posicion que le corresponde segun su prioridad.
        public Paciente Encolar(string nombre, int prioridad)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del paciente es requerido.");
            if (prioridad < PrioridadAlta || prioridad > PrioridadBaja)
                throw new ArgumentException("La prioridad debe ser P1, P2 o P3.");

            contadorLlegada++;
            Paciente paciente = new Paciente(nombre.Trim(), prioridad, contadorLlegada);
            NodoPaciente nuevo = new NodoPaciente(paciente);

            // Caso 1: lista vacia, o el nuevo tiene mas prioridad (numero menor) que el frente.
            if (frente == null || prioridad < frente.Dato.Prioridad)
            {
                nuevo.Siguiente = frente;
                frente = nuevo;
            }
            else
            {
                // Caso 2: avanzar mientras el siguiente tenga prioridad igual o mayor.
                // Usar "<=" hace que, a igual prioridad, el nuevo quede DESPUES de los
                // que ya estaban (se respeta el orden de llegada).
                NodoPaciente actual = frente;
                while (actual.Siguiente != null && actual.Siguiente.Dato.Prioridad <= prioridad)
                {
                    actual = actual.Siguiente;
                }
                nuevo.Siguiente = actual.Siguiente;
                actual.Siguiente = nuevo;
            }

            cantidad++;
            return paciente;
        }

        // Sobrecarga que recibe la prioridad como texto ("P1", "P2", "P3").
        public Paciente Encolar(string nombre, string prioridadTexto)
        {
            return Encolar(nombre, ConvertirPrioridad(prioridadTexto));
        }

        // Ve el siguiente paciente a atender sin sacarlo (null si no hay).
        public Paciente VerSiguiente()
        {
            return frente == null ? null : frente.Dato;
        }

        // Saca y devuelve el siguiente paciente a atender (null si no hay).
        public Paciente Desencolar()
        {
            if (frente == null) return null;

            Paciente atendido = frente.Dato;
            frente = frente.Siguiente;
            cantidad--;
            return atendido;
        }

        // Lista de pacientes pendientes, ya en orden de atencion.
        public List<Paciente> ObtenerPendientes()
        {
            List<Paciente> lista = new List<Paciente>();
            NodoPaciente actual = frente;
            while (actual != null)
            {
                lista.Add(actual.Dato);
                actual = actual.Siguiente;
            }
            return lista;
        }
    }
}
