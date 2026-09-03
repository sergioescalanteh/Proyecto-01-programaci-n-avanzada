using System;
using System.Collections.Generic;

namespace GoXelaDelivery
{
    
    public struct Coordenada
    {
        private double latitud;
        public double Latitud
        {
            get { return latitud; }
            set { latitud = value; }
        }

        private double longitud;
        public double Longitud
        {
            get { return longitud; }
            set { longitud = value; }
        }

        public Coordenada(double lat, double lon)
        {
            latitud = lat;
            longitud = lon;
        }
    }

  
    public abstract class Persona
    {
        private string codigo;
        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string nombreCompleto;
        public string NombreCompleto
        {
            get { return nombreCompleto; }
            set { nombreCompleto = value; }
        }

        private string telefono;
        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public Persona(string codigo, string nombre, string telefono)
        {
            this.codigo = codigo;
            this.nombreCompleto = nombre;
            this.telefono = telefono;
        }

        public virtual void MostrarInformacion() 
        {
            Console.WriteLine($"Código: {Codigo} | Nombre: {NombreCompleto} | Teléfono: {Telefono}");
        }
    }

    public class Cliente : Persona
    {
        private string correo;
        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        private string direccion;
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        private int cantidadSolicitudes;
        public int CantidadSolicitudes
        {
            get { return cantidadSolicitudes; }
            set { cantidadSolicitudes = value; }
        }

        public Cliente(string codigo, string nombre, string telefono, string correo, string direccion)
            : base(codigo, nombre, telefono)
        {
            this.correo = correo;
            this.direccion = direccion;
            this.cantidadSolicitudes = 0;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Correo: {Correo} | Dirección: {Direccion} | Solicitudes: {CantidadSolicitudes}");
        }
    }

    public class Repartidor : Persona
    {
        private string licencia;
        public string Licencia
        {
            get { return licencia; }
            set { licencia = value; }
        }

        private string tipoLicencia;
        public string TipoLicencia
        {
            get { return tipoLicencia; }
            set { tipoLicencia = value; }
        }

        private string estado; 
        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private int entregasRealizadas;
        public int EntregasRealizadas
        {
            get { return entregasRealizadas; }
            set { entregasRealizadas = value; }
        }

        private double calificacionPromedio;
        public double CalificacionPromedio
        {
            get { return calificacionPromedio; }
            set { calificacionPromedio = value; }
        }

        public Repartidor(string codigo, string nombre, string telefono, string licencia, string tipoLicencia)
            : base(codigo, nombre, telefono)
        {
            this.licencia = licencia;
            this.tipoLicencia = tipoLicencia;
            this.estado = "Disponible";
            this.entregasRealizadas = 0;
            this.calificacionPromedio = 5.0;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Licencia: {Licencia} ({TipoLicencia}) | Estado: {Estado} | Entregas: {EntregasRealizadas} | Calificación: {CalificacionPromedio:F1}");
        }
    }

   
    public abstract class Vehiculo
    {
        private string codigo;
        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string placa;
        public string Placa
        {
            get { return placa; }
            set { placa = value; }
        }

        private string marca;
        public string Marca
        {
            get { return marca; }
            set { marca = value; }
        }

        private string modelo;
        public string Modelo
        {
            get { return modelo; }
            set { modelo = value; }
        }

        private double capacidadMaxima;
        public double CapacidadMaxima
        {
            get { return capacidadMaxima; }
            set { capacidadMaxima = value; }
        }

        private string estado;
        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private double costoOperativo;
        public double CostoOperativo
        {
            get { return costoOperativo; }
            set { costoOperativo = value; }
        }

        public Vehiculo(string codigo, string placa, string marca, string modelo, double capacidad, double costoOperativo)
        {
            this.codigo = codigo;
            this.placa = placa;
            this.marca = marca;
            this.modelo = modelo;
            this.capacidadMaxima = capacidad;
            this.estado = "Disponible";
            this.costoOperativo = costoOperativo;
        }

        public abstract bool PuedeTransportar(Paquete paquete);
        public abstract double CalcularCostoOperativo();

        public virtual void MostrarInformacion()
        {
            Console.WriteLine($"[{GetType().Name}] Cod: {Codigo} | Placa: {Placa} | Capacidad: {CapacidadMaxima}kg | Estado: {Estado}");
        }
    }

    public class Bicicleta : Vehiculo
    {
        public Bicicleta(string codigo, string marca, string modelo)
            : base(codigo, "N/A", marca, modelo, 10.0, 5.0) { }

        public override bool PuedeTransportar(Paquete paquete)
        {
            return paquete.Peso <= CapacidadMaxima && (paquete is Documento || paquete is PaqueteEstandar);
        }

        public override double CalcularCostoOperativo()
        {
            return CostoOperativo;
        }
    }

    public class Motocicleta : Vehiculo
    {
        public Motocicleta(string codigo, string placa, string marca, string modelo)
            : base(codigo, placa, marca, modelo, 30.0, 15.0) { }

        public override bool PuedeTransportar(Paquete paquete)
        {
            return paquete.Peso <= CapacidadMaxima && !(paquete is ProductoRefrigerado);
        }

        public override double CalcularCostoOperativo()
        {
            return CostoOperativo * 1.2;
        }
    }

    public class Automovil : Vehiculo
    {
        public Automovil(string codigo, string placa, string marca, string modelo)
            : base(codigo, placa, marca, modelo, 200.0, 35.0) { }

        public override bool PuedeTransportar(Paquete paquete)
        {
            return paquete.Peso <= CapacidadMaxima;
        }

        public override double CalcularCostoOperativo()
        {
            return CostoOperativo * 1.5;
        }
    }