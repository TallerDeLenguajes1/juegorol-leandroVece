using System;
using System.Collections.Generic;
using System.Reflection;

namespace Juego
{
    class Program
    {
        static void Main(string[] args)
        {
            personaje personaje1;
            List<personaje> listPersonaje = new List<personaje>();
            Random rnd = new Random();
            string aux;
            int danio1, danio2;
            int pj1, pj2;

            Console.WriteLine("Creacion de personajes.");
            Console.WriteLine("¿cuantos personajes quiere crear?"); //pregunta la cantidad de personajes a crear
            int camtidadPers = Convert.ToInt32(Console.ReadLine());

            string[] nombres = { "pepe", "Juana", "carlos", "el tipaso" };
            string[] apodos = { "Elsapo", "tipaseca", "renegado", "huesitos" };

            for (int i = 0; i < camtidadPers; i++)
            {
                personaje1 = new personaje(nombres[rnd.Next(nombres.Length)],
                                            apodos[rnd.Next(apodos.Length)], 
                                            CalculoEdad.CalcularEdad(DateTime.Parse("1/12/2000")),100,
                                            DateTime.Parse("1/12/2000"));
                personaje1.estadisticasJugador();

                listPersonaje.Add(personaje1);
                
            }
            //muestra los atributos de los personajes
            foreach (personaje obj in listPersonaje)
            {
                listarAtributos(obj);

            }

            Console.WriteLine("#######");

            aux = inicioPelea();
            while (aux != "y")
            {
               aux = inicioPelea();
            }

            Console.Clear();//limpia la pantalla 
            Console.WriteLine("inicio de la batasha \n");
            //pelea hasta que queda el ganador
            while (listPersonaje.Count != 1)
            {
                pj1 = rnd.Next(listPersonaje.Count);
                pj2 = rnd.Next(listPersonaje.Count);

                for (int i = 0; i < 3; i++)
                {
                    while (pj1 == pj2)
                    {
                        pj2 = rnd.Next(listPersonaje.Count);
                    }
                    danio1 = Convert.ToInt32(listPersonaje[pj1].danioProvocado());
                    danio2 = Convert.ToInt32(listPersonaje[pj2].danioProvocado());

                    
                    Console.WriteLine("daño del rival 1 = {0} ", danio1);
                    Console.WriteLine("daño del rival 2 = {0} ",  danio2);
                    listPersonaje[pj1].Salud -= danio2;
                    listPersonaje[pj2].Salud -= danio1;
                    Console.WriteLine("salud del rival 1 = {0}", listPersonaje[pj1].Salud);
                    Console.WriteLine("salud del rival 2 = {0} \n", listPersonaje[pj2].Salud );


                }

                determinarGanador(pj1, pj2);
                if (listPersonaje.Count != 1)
                {
                    Console.WriteLine("siguiente pelea");
                }
            }

            Console.WriteLine("ganador del toneo es {0}", listPersonaje[0].Nombre);

                //funciones 
            string inicioPelea()
            {
                Console.WriteLine("¿iniciar pelea? Y/N"); //tiempo para verificar datos
                return Console.ReadLine().ToLower();
            }

            void determinarGanador(int indicePjt1,int indicePjt2)
            {
                if (listPersonaje[indicePjt1].Salud > listPersonaje[indicePjt2].Salud)
                {
                    listPersonaje[indicePjt1].Salud = 100;
                    listPersonaje.RemoveAt(indicePjt2);
                }
                else if (listPersonaje[indicePjt2].Salud > listPersonaje[indicePjt1].Salud)
                {
                    listPersonaje[indicePjt2].Salud = 100;
                    listPersonaje.RemoveAt(indicePjt1);
                }
                else
                {
                    Console.WriteLine("Empate");
                }
            
            }

            void listarAtributos(personaje obj)
            {
                PropertyInfo[] lst = typeof(personaje).GetProperties();

                foreach (PropertyInfo dato in lst)
                {
                    string Valor = dato.GetValue(obj).ToString();
                    Console.WriteLine(dato.Name + " = " + Valor);
                }
            }
        }
        

    }
}
