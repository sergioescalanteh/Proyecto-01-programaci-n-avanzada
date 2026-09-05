using GoXelaDelivery;
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
    public abstract class Paquete
    {
        private string codigo;
        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string descripcion;
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private double peso;
        public double Peso
        {
            get { return peso; }
            set { peso = value; }
        }

        private double valorDeclarado;
        public double ValorDeclarado
        {
            get { return valorDeclarado; }
            set { valorDeclarado = value; }
        }

        private string direccionOrigen;
        public string DireccionOrigen
        {
            get { return direccionOrigen; }
            set { direccionOrigen = value; }
        }

        private string direccionDestino;
        public string DireccionDestino
        {
            get { return direccionDestino; }
            set { direccionDestino = value; }
        }

        private string estado;
        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public Paquete(string codigo, string descripcion, double peso, double valorDeclarado, string origen, string destino)
        {
            if (peso <= 0) throw new ArgumentException("El peso del paquete debe ser mayor a 0.");

            this.codigo = codigo;
            this.descripcion = descripcion;
            this.peso = peso;
            this.valorDeclarado = valorDeclarado;
            this.direccionOrigen = origen;
            this.direccionDestino = destino;
            this.estado = "En Almacén";
        }

        public abstract double CalcularTarifaBase(double distancia);
    }

    public class Documento : Paquete
    {
        public Documento(string codigo, string descripcion, double peso, double valor, string origen, string destino)
            : base(codigo, descripcion, peso, valor, origen, destino) { }

        public override double CalcularTarifaBase(double distancia)
        {
            return 15.0 + (distancia * 1.5);
        }
    }

    public class PaqueteEstandar : Paquete
    {
        public PaqueteEstandar(string codigo, string descripcion, double peso, double valor, string origen, string destino)
            : base(codigo, descripcion, peso, valor, origen, destino) { }

        public override double CalcularTarifaBase(double distancia)
        {
            return 25.0 + (Peso * 2.0) + (distancia * 2.0);
        }
    }

    public class PaqueteFragil : Paquete
    {
        public PaqueteFragil(string codigo, string descripcion, double peso, double valor, string origen, string destino)
            : base(codigo, descripcion, peso, valor, origen, destino) { }

        public override double CalcularTarifaBase(double distancia)
        {
            double baseTarifa = 30.0 + (Peso * 2.5) + (distancia * 2.5);
            return baseTarifa + (ValorDeclarado * 0.05);
        }
    }

    public class ProductoRefrigerado : Paquete
    {
        private double temperaturaRequerida;
        public double TemperaturaRequerida
        {
            get { return temperaturaRequerida; }
            set { temperaturaRequerida = value; }
        }

        public ProductoRefrigerado(string codigo, string descripcion, double peso, double valor, string origen, string destino, double temp)
            : base(codigo, descripcion, peso, valor, origen, destino)
        {
            this.temperaturaRequerida = temp;
        }

        public override double CalcularTarifaBase(double distancia)
        {
            return 40.0 + (Peso * 3.0) + (distancia * 3.0);
        }
    }

    public class Incidencia
    {
        private string codigo;
        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string tipo;
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private string descripcion;
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private DateTime fecha;
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        private string estado;
        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private string accionTomada;
        public string AccionTomada
        {
            get { return accionTomada; }
            set { accionTomada = value; }
        }

        public Incidencia(string codigo, string tipo, string descripcion)
        {
            this.codigo = codigo;
            this.tipo = tipo;
            this.descripcion = descripcion;
            this.fecha = DateTime.Now;
            this.estado = "Abierta";
            this.accionTomada = "En revisión";
        }
    }

    public class Entrega
    {
        private string codigo;
        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Cliente clienteSolicitante;
        public Cliente ClienteSolicitante
        {
            get { return clienteSolicitante; }
            set { clienteSolicitante = value; }
        }

        private Paquete paqueteAsociado;
        public Paquete PaqueteAsociado
        {
            get { return paqueteAsociado; }
            set { paqueteAsociado = value; }
        }

        private Repartidor repartidorAsignado;
        public Repartidor RepartidorAsignado
        {
            get { return repartidorAsignado; }
            set { repartidorAsignado = value; }
        }

        private Vehiculo vehiculoAsignado;
        public Vehiculo VehiculoAsignado
        {
            get { return vehiculoAsignado; }
            set { vehiculoAsignado = value; }
        }

        private DateTime fechaSolicitud;
        public DateTime FechaSolicitud
        {
            get { return fechaSolicitud; }
            set { fechaSolicitud = value; }
        }

        private double distanciaKM;
        public double DistanciaKM
        {
            get { return distanciaKM; }
            set { distanciaKM = value; }
        }

        private string tipoServicio;
        public string TipoServicio
        {
            get { return tipoServicio; }
            set { tipoServicio = value; }
        }

        private string estado;
        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private double tarifaBase;
        public double TarifaBase
        {
            get { return tarifaBase; }
            set { tarifaBase = value; }
        }

        private double recargos;
        public double Recargos
        {
            get { return recargos; }
            set { recargos = value; }
        }

        private double descuentos;
        public double Descuentos
        {
            get { return descuentos; }
            set { descuentos = value; }
        }

        private double total;
        public double Total
        {
            get { return total; }
            set { total = value; }
        }

        private List<Incidencia> listaIncidencias;
        public List<Incidencia> ListaIncidencias
        {
            get { return listaIncidencias; }
            set { listaIncidencias = value; }
        }


        public Entrega(string codigo, Cliente cliente, Paquete paquete, double distancia, string tipoServicio)
        {
            if (distancia <= 0) throw new ArgumentException("La distancia debe ser mayor a 0.");

            this.codigo = codigo;
            this.clienteSolicitante = cliente;
            this.paqueteAsociado = paquete;
            this.distanciaKM = distancia;
            this.tipoServicio = tipoServicio;
            this.fechaSolicitud = DateTime.Now;
            this.estado = "SOLICITADA";
            this.listaIncidencias = new List<Incidencia>();

            CalcularTarifa();
        }

        public Entrega(string codigo, Cliente cliente, Paquete paquete, double distancia, string tipoServicio, double descuentoEspecial)
            : this(codigo, cliente, paquete, distancia, tipoServicio)
        {
            this.descuentos = descuentoEspecial;
            this.total -= this.descuentos;
        }
    }
}
public void CalcularTarifa()
{
    TarifaBase = PaqueteAsociado.CalcularTarifaBase(DistanciaKM);
    Recargos = 0;

    if (TipoServicio == "Prioritario") Recargos += 15.0;
    else if (TipoServicio == "Urgente") Recargos += 30.0;

    Total = TarifaBase + Recargos - Descuentos;
}

public bool CambiarEstado(string nuevoEstado)
{
    if (Estado == "FINALIZADA" || Estado == "ENTREGADA" || Estado == "CANCELADA")
    {
        Console.WriteLine("Error: No se puede modificar una entrega finalizada o cancelada.");
        return false;
    }

    if (nuevoEstado == "ASIGNADA" && Estado == "SOLICITADA") Estado = "ASIGNADA";
    else if (nuevoEstado == "RECOGIDA" && Estado == "ASIGNADA") Estado = "RECOGIDA";
    else if (nuevoEstado == "EN RUTA" && Estado == "RECOGIDA") Estado = "EN RUTA";
    else if (nuevoEstado == "ENTREGADA" && Estado == "EN RUTA")
    {
        Estado = "ENTREGADA";
        RepartidorAsignado.Estado = "Disponible";
        VehiculoAsignado.Estado = "Disponible";
        RepartidorAsignado.EntregasRealizadas++;
    }
    else if (nuevoEstado == "CANCELADA")
    {
        Estado = "CANCELADA";
        if (RepartidorAsignado != null) RepartidorAsignado.Estado = "Disponible";
        if (VehiculoAsignado != null) VehiculoAsignado.Estado = "Disponible";
    }
    else if (nuevoEstado == "CON INCIDENCIA") Estado = "CON INCIDENCIA";
    else
    {
        Console.WriteLine($"Transición de estado inválida de [{Estado}] a [{nuevoEstado}].");
        return false;
    }

    return true;
}
}

public class SistemaGoXela
{
    private List<Cliente> clientes = new List<Cliente>();
    public List<Cliente> Clientes
    {
        get { return clientes; }
        set { clientes = value; }
    }

    private List<Repartidor> repartidores = new List<Repartidor>();
    public List<Repartidor> Repartidores
    {
        get { return repartidores; }
        set { repartidores = value; }
    }

    private List<Vehiculo> vehiculos = new List<Vehiculo>();
    public List<Vehiculo> Vehiculos
    {
        get { return vehiculos; }
        set { vehiculos = value; }
    }

    private List<Paquete> paquetes = new List<Paquete>();
    public List<Paquete> Paquetes
    {
        get { return paquetes; }
        set { paquetes = value; }
    }

    private List<Entrega> entregas = new List<Entrega>();
    public List<Entrega> Entregas
    {
        get { return entregas; }
        set { entregas = value; }
    }
