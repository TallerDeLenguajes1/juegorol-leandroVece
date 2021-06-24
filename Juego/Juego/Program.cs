using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

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
               string anioNacimiento = "1/1/" + rnd.Next(1500, DateTime.Now.Year).ToString();
               personaje1 = new personaje(nombres[rnd.Next(nombres.Length)],
                                            apodos[rnd.Next(apodos.Length)],
                                            CalculoEdad.CalcularEdad(DateTime.Parse(anioNacimiento)), 100,
                                            DateTime.Parse(anioNacimiento),
                                            representante());
                personaje1.estadisticasJugador();

                listPersonaje.Add(personaje1);

            }
            //muestra los atributos de los personajes
            foreach (personaje obj in listPersonaje)
            {
                listarAtributos(obj);
                Console.WriteLine("#######");
            }


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


                    Console.WriteLine("daño del rival {0} = {1} ", listPersonaje[pj1].Nombre, danio1);
                    Console.WriteLine("daño del rival {0} = {1} ", listPersonaje[pj2].Nombre, danio2);
                    listPersonaje[pj1].Salud -= danio2;
                    listPersonaje[pj2].Salud -= danio1;
                    Console.WriteLine("salud del rival {0} = {1} ", listPersonaje[pj1].Nombre, listPersonaje[pj1].Salud);
                    Console.WriteLine("salud del rival {0} = {1} ", listPersonaje[pj2].Nombre, listPersonaje[pj2].Salud);


                }

                determinarGanador(pj1, pj2, listPersonaje);
                if (listPersonaje.Count != 1)
                {
                    Console.WriteLine("siguiente pelea");
                }
            }

            Console.WriteLine("ganador del toneo es {0}", listPersonaje[0].Nombre);
            guardarGanador("Registro de batasha", ".csv", listPersonaje[0]);



        }

        public static string representante()
        {
            Random rnd = new Random();
            var url = $" https://restcountries.eu/rest/v2 ";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {

                using(WebResponse response = request.GetResponse())
                {
                    using(Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader((strReader)))
                        {
                            string responseBody = objReader.ReadToEnd();
                            List<Currency> ListPaisesMundo = JsonSerializer.Deserialize<List<Currency>>(responseBody);//porque era una matriz era necesario hacer una list
                            int indice = rnd.Next(ListPaisesMundo.Count);
                            return ListPaisesMundo[indice].name;
                            /*foreach (paises pais in ListPaisesMundo)
                            {
                                Console.WriteLine(pais.name + '\n');
                            }*/
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }
        }



        public static void guardarGanador(string nombreArchivo, string formato, personaje ganador)
        {
            FileStream Archivo = new FileStream(nombreArchivo + formato, FileMode.Create);
            using (StreamWriter strWrite = new StreamWriter(Archivo))
            {
                strWrite.WriteLine("{0};{1};{2}", ganador.Nombre, ganador.Tipo, ganador.Tipo);
                strWrite.Close();
            }
        }


        //funciones 
        static string inicioPelea()
        {
            Console.WriteLine("¿iniciar pelea? Y/N"); //tiempo para verificar datos
            return Console.ReadLine().ToLower();
        }

        static void listarAtributos(personaje obj)
        {
            PropertyInfo[] lst = typeof(personaje).GetProperties();

            foreach (PropertyInfo dato in lst)
            {

                string Valor = dato.GetValue(obj).ToString();
                Console.WriteLine(dato.Name + " = " + Valor);

            }

            /*referencia 
            docs.microsoft.com/es-es/dotnet/api/system.reflection.propertyinfo.getvalue?view=net-5.0#System_Reflection_PropertyInfo_GetValue_System_Object*/
        }
        static void determinarGanador(int indicePjt1, int indicePjt2, List<personaje> listPersonaje)
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

        //clases de Json
        public class Currency
        {
            public string code { get; set; }
            public string name { get; set; }
            public string symbol { get; set; }
        }

        public class Language
        {
            public string iso639_1 { get; set; }
            public string iso639_2 { get; set; }
            public string name { get; set; }
            public string nativeName { get; set; }
        }

        public class Translations
        {
            public string de { get; set; }
            public string es { get; set; }
            public string fr { get; set; }
            public string ja { get; set; }
            public string it { get; set; }
            public string br { get; set; }
            public string pt { get; set; }
            public string nl { get; set; }
            public string hr { get; set; }
            public string fa { get; set; }
        }

        public class RegionalBloc
        {
            public string acronym { get; set; }
            public string name { get; set; }
            public List<object> otherAcronyms { get; set; }
            public List<string> otherNames { get; set; }
        }

        public class paises
        {
            public string name { get; set; }
            public List<string> topLevelDomain { get; set; }
            public string alpha2Code { get; set; }
            public string alpha3Code { get; set; }
            public List<string> callingCodes { get; set; }
            public string capital { get; set; }
            public List<string> altSpellings { get; set; }
            public string region { get; set; }
            public string subregion { get; set; }
            public int population { get; set; }
            public List<double> latlng { get; set; }
            public string demonym { get; set; }
            public double? area { get; set; }
            public double? gini { get; set; }
            public List<string> timezones { get; set; }
            public List<string> borders { get; set; }
            public string nativeName { get; set; }
            public string numericCode { get; set; }
            public List<Currency> currencies { get; set; }
            public List<Language> languages { get; set; }
            public Translations translations { get; set; }
            public string flag { get; set; }
            public List<RegionalBloc> regionalBlocs { get; set; }
            public string cioc { get; set; }
        }



    }
}
