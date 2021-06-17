using System;
using System.Collections.Generic;
using System.Text;

namespace Juego
{
    class CalculoEdad
    {
        public static int CalcularEdad(DateTime fechaNacimiento)
        {
            DateTime fechaActual = DateTime.Today;

            if (fechaNacimiento > fechaActual)
            {
                Console.WriteLine("La fecha de nacimiento es mayor que la actual.");
                return -1;
            }
            else
            {
                int edad = fechaActual.Year - fechaNacimiento.Year;

                if (fechaNacimiento.Month > fechaActual.Month || (fechaNacimiento.Month == fechaActual.Month && fechaNacimiento.Day > fechaActual.Day))
                {
                    --edad;
                }

                return edad;
            }
        }
    }
}
