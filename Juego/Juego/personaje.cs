using System;
using System.Collections.Generic;
using System.Text;

namespace Juego
{
    class personaje
    {
        private string tipo;
        private string nombre, apodo;
        private int edad, salud;
        private DateTime FechaNacimiento;
        private string representante;


        int velociodad, fuerza, destreza, nivel, armadura;
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apodo { get => apodo; set => apodo = value; }
        public int Edad { get => edad; set => edad = value; }
        public int Salud { get => salud; set => salud = value; }
        public DateTime FechaNacimiento1 { get => FechaNacimiento; set => FechaNacimiento = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public int Velociodad { get => velociodad; set => velociodad = value; }
        public int Fuerza { get => fuerza; set => fuerza = value; }
        public int Destreza { get => destreza; set => destreza = value; }
        public int Nivel { get => nivel; set => nivel = value; }
        public int Armadura { get => armadura; set => armadura = value; }
        public string Representante { get => representante; set => representante = value; }

        public personaje(string nombre, string apodo, int edad, int salud, DateTime fecha, string representante)
        {
            Random rnd = new Random();
            this.Tipo =  ((TipoPersonaje)rnd.Next(Enum.GetNames(typeof(TipoPersonaje)).Length)).ToString();
            this.Apodo = apodo;
            this.edad = edad;
            this.FechaNacimiento = DateTime.Parse(fecha.ToString("MM/dd/yyyy"));
            this.Nombre = nombre;
            this.Salud = salud;
            this.Representante = representante;

        }
        enum TipoPersonaje
        {
            ada,
            humano,
            elfo,
            enano,
            ogro
        }

        public void estadisticasJugador()
        {
            Random rnd = new Random();
            this.Velociodad = rnd.Next(1,10);
            this.Destreza = rnd.Next(1, 10);
            this.Fuerza = rnd.Next(1, 10);
            this.Nivel = rnd.Next(1, 10);
            this.Armadura = rnd.Next(1, 10);
        }


        public float PoderDisparo()
        {
            return this.Fuerza * this.Nivel * this.Destreza;
        }

        public double efectividadDispoaro(int rnd)
        {
            return rnd;
        }

        public double valorAtaque(float PD, double ED)
        {
            return (PD * ED);
        }

        public float poderDefensa()
        {
            return this.Armadura * this.Velociodad;
        }

        public double danioProvocado()
        {
            Random rnd = new Random();
            double ED = efectividadDispoaro(rnd.Next(1,10));
            
            double VA = valorAtaque(PoderDisparo(), ED);
            float PDEF = poderDefensa();
            double power = (((VA * ED) - PDEF) / 5000) * 100;
            if (power > 30) power = 30;
            return power;
        }
    }
}
