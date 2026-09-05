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
    public unsafe void DemostrarPunteros()
    {
        int totalRegistros = Clientes.Count + Repartidores.Count + Vehiculos.Count + Paquetes.Count;
        int* pTotal = &totalRegistros;

        Console.WriteLine("\n--- DEMOSTRACIÓN DE USO DE PUNTEROS ---");
        Console.WriteLine($"Dirección de memoria: {(long)pTotal:X}");
        Console.WriteLine($"Valor apuntado (Total de objetos en sistema): {*pTotal}");
        Console.WriteLine("---------------------------------------\n");
    }

    public int ContarPaquetesRecursivo(List<Paquete> lista, int indice)
    {
        if (indice >= lista.Count) return 0;
        return 1 + ContarPaquetesRecursivo(lista, indice + 1);
    }

    public double CalcularIngresosRecursivo(List<Entrega> lista, int indice)
    {
        if (indice >= lista.Count) return 0.0;
        double monto = (lista[indice].Estado == "ENTREGADA") ? lista[indice].Total : 0.0;
        return monto + CalcularIngresosRecursivo(lista, indice + 1);
    }


    public bool ExisteCodigoCliente(string codigo) => Clientes.Exists(c => c.Codigo == codigo);
    public bool ExisteCodigoRepartidor(string codigo) => Repartidores.Exists(r => r.Codigo == codigo);
    public bool ExistePlacaVehiculo(string placa) => Vehiculos.Exists(v => v.Placa != "N/A" && v.Placa == placa);
}


class Program
{
    static SistemaGoXela sistema = new SistemaGoXela();

    static void Main(string[] args)
    {
        CargarDatosDePrueba();

        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("            GOXELA DELIVERY            ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Gestión de Clientes");
            Console.WriteLine("2. Gestión de Repartidores");
            Console.WriteLine("3. Gestión de Vehículos");
            Console.WriteLine("4. Gestión de Paquetes");
            Console.WriteLine("5. Gestión de Entregas");
            Console.WriteLine("6. Gestión de Incidencias");
            Console.WriteLine("7. Reportes");
            Console.WriteLine("8. Demostración Técnica (Punteros)");
            Console.WriteLine("9. Salir");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();
            try
            {
                switch (opcion)
                {
                    case "1": MenuClientes(); break;
                    case "2": MenuRepartidores(); break;
                    case "3": MenuVehiculos(); break;
                    case "4": MenuPaquetes(); break;
                    case "5": MenuEntregas(); break;
                    case "6": MenuIncidencias(); break;
                    case "7": MenuReportes(); break;
                    case "8": sistema.DemostrarPunteros(); PresioneContinuar(); break;
                    case "9": salir = true; break;
                    default: Console.WriteLine("Opción no válida."); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERROR]: {ex.Message}");
                PresioneContinuar();
            }
        }
    }
    static void MenuClientes()
    {
        Console.Clear();
        Console.WriteLine("--- GESTIÓN DE CLIENTES ---");
        Console.WriteLine("1. Registrar Cliente\n2. Consultar Clientes");
        Console.Write("Seleccione: ");
        string op = Console.ReadLine();

        if (op == "1")
        {
            Console.Write("Código: "); string cod = Console.ReadLine();
            if (sistema.ExisteCodigoCliente(cod)) throw new Exception("Código de cliente duplicado.");

            Console.Write("Nombre Completo: "); string nom = Console.ReadLine();
            Console.Write("Teléfono: "); string tel = Console.ReadLine();
            Console.Write("Correo: "); string cor = Console.ReadLine();
            Console.Write("Dirección: "); string dir = Console.ReadLine();

            if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(cod)) throw new Exception("Campos obligatorios vacíos.");

            sistema.Clientes.Add(new Cliente(cod, nom, tel, cor, dir));
            Console.WriteLine("¡Cliente registrado exitosamente!");
        }
        else if (op == "2")
        {
            foreach (var c in sistema.Clientes) c.MostrarInformacion();
        }
        PresioneContinuar();
    }

    static void MenuRepartidores()
    {
        Console.Clear();
        Console.WriteLine("--- GESTIÓN DE REPARTIDORES ---");
        Console.WriteLine("1. Registrar Repartidor\n2. Consultar Repartidores");
        Console.Write("Seleccione: ");
        string op = Console.ReadLine();

        if (op == "1")
        {
            Console.Write("Código: "); string cod = Console.ReadLine();
            if (sistema.ExisteCodigoRepartidor(cod)) throw new Exception("Código de repartidor duplicado.");

            Console.Write("Nombre: "); string nom = Console.ReadLine();
            Console.Write("Teléfono: "); string tel = Console.ReadLine();
            Console.Write("Número de Licencia: "); string lic = Console.ReadLine();
            Console.Write("Tipo de Licencia (A/B/M): "); string tipo = Console.ReadLine();

            sistema.Repartidores.Add(new Repartidor(cod, nom, tel, lic, tipo));
            Console.WriteLine("Repartidor registrado.");
        }
        else if (op == "2")
        {
            foreach (var r in sistema.Repartidores) r.MostrarInformacion();
        }
        PresioneContinuar();
    }

    static void MenuVehiculos()
    {
        Console.Clear();
        Console.WriteLine("--- GESTIÓN DE VEHÍCULOS ---");
        Console.WriteLine("1. Registrar Bicicleta\n2. Registrar Motocicleta\n3. Registrar Automóvil\n4. Consultar Vehículos");
        Console.Write("Seleccione: ");
        string op = Console.ReadLine();

        if (op == "1" || op == "2" || op == "3")
        {
            Console.Write("Código: "); string cod = Console.ReadLine();
            Console.Write("Marca: "); string marca = Console.ReadLine();
            Console.Write("Modelo: "); string mod = Console.ReadLine();
            string placa = "N/A";

            if (op != "1")
            {
                Console.Write("Placa: "); placa = Console.ReadLine();
                if (sistema.ExistePlacaVehiculo(placa)) throw new Exception("Placa duplicada.");
            }

            if (op == "1") sistema.Vehiculos.Add(new Bicicleta(cod, marca, mod));
            else if (op == "2") sistema.Vehiculos.Add(new Motocicleta(cod, placa, marca, mod));
            else if (op == "3") sistema.Vehiculos.Add(new Automovil(cod, placa, marca, mod));

            Console.WriteLine("Vehículo agregado correctamente.");
        }
        else if (op == "4")
        {
            foreach (var v in sistema.Vehiculos) v.MostrarInformacion();
        }
        PresioneContinuar();
    }

    static void MenuPaquetes()
    {
        Console.Clear();
        Console.WriteLine("--- GESTIÓN DE PAQUETES ---");
        Console.WriteLine("1. Registrar Documento\n2. Registrar Paquete Estándar\n3. Registrar Paquete Frágil\n4. Registrar Producto Refrigerado");
        Console.Write("Seleccione: ");
        string op = Console.ReadLine();

        Console.Write("Código: "); string cod = Console.ReadLine();
        Console.Write("Descripción: "); string desc = Console.ReadLine();
        Console.Write("Peso (kg): "); double peso = double.Parse(Console.ReadLine());
        Console.Write("Valor Declarado (Q): "); double val = double.Parse(Console.ReadLine());
        Console.Write("Dirección Origen: "); string ori = Console.ReadLine();
        Console.Write("Dirección Destino: "); string des = Console.ReadLine();

        if (op == "1") sistema.Paquetes.Add(new Documento(cod, desc, peso, val, ori, des));
        else if (op == "2") sistema.Paquetes.Add(new PaqueteEstandar(cod, desc, peso, val, ori, des));
        else if (op == "3") sistema.Paquetes.Add(new PaqueteFragil(cod, desc, peso, val, ori, des));
        else if (op == "4")
        {
            Console.Write("Temperatura Requerida (°C): "); double temp = double.Parse(Console.ReadLine());
            sistema.Paquetes.Add(new ProductoRefrigerado(cod, desc, peso, val, ori, des, temp));
        }
        Console.WriteLine("Paquete registrado exitosamente.");
        PresioneContinuar();
    }

    static void MenuEntregas()
    {
        Console.Clear();
        Console.WriteLine("--- GESTIÓN DE ENTREGAS ---");
        Console.WriteLine("1. Crear Solicitud de Entrega\n2. Asignar Repartidor y Vehículo\n3. Avanzar Estado de Entrega\n4. Consultar Entregas");
        Console.Write("Seleccione: ");
        string op = Console.ReadLine();

        if (op == "1")
        {
            Console.Write("Código Entrega: "); string cod = Console.ReadLine();
            Console.Write("Código Cliente: "); string codC = Console.ReadLine();
            Cliente cli = sistema.Clientes.Find(c => c.Codigo == codC);
            if (cli == null) throw new Exception("Cliente no encontrado.");

            Console.Write("Código Paquete: "); string codP = Console.ReadLine();
            Paquete paq = sistema.Paquetes.Find(p => p.Codigo == codP);
            if (paq == null) throw new Exception("Paquete no encontrado.");

            Console.Write("Distancia Estimada (KM): "); double dist = double.Parse(Console.ReadLine());
            Console.Write("Tipo Servicio (Normal/Prioritario/Urgente): "); string serv = Console.ReadLine();

            Entrega nueva = new Entrega(cod, cli, paq, dist, serv);
            cli.CantidadSolicitudes++;
            sistema.Entregas.Add(nueva);
            Console.WriteLine($"Entrega registrada. Total calculado: Q{nueva.Total:F2}");
        }
        else if (op == "2")
        {
            Console.Write("Código Entrega: "); string codE = Console.ReadLine();
            Entrega ent = sistema.Entregas.Find(e => e.Codigo == codE);
            if (ent == null) throw new Exception("Entrega no existe.");

            Console.Write("Código Repartidor: "); string codR = Console.ReadLine();
            Repartidor rep = sistema.Repartidores.Find(r => r.Codigo == codR);
            if (rep == null || rep.Estado != "Disponible") throw new Exception("Repartidor no disponible u ocupado.");

            Console.Write("Código Vehículo: "); string codV = Console.ReadLine();
            Vehiculo veh = sistema.Vehiculos.Find(v => v.Codigo == codV);
            if (veh == null || veh.Estado != "Disponible") throw new Exception("Vehículo no disponible.");

            if (!veh.PuedeTransportar(ent.PaqueteAsociado))
                throw new Exception("El vehículo NO es compatible con el peso o tipo de paquete.");

            ent.RepartidorAsignado = rep;
            ent.VehiculoAsignado = veh;
            rep.Estado = "Asignado";
            veh.Estado = "Asignado";

            ent.CambiarEstado("ASIGNADA");
            Console.WriteLine("Repartidor y Vehículo asignados correctamente.");
        }
        else if (op == "3")
        {
            Console.Write("Código Entrega: "); string codE = Console.ReadLine();
            Entrega ent = sistema.Entregas.Find(e => e.Codigo == codE);
            if (ent == null) throw new Exception("Entrega no encontrada.");

            Console.WriteLine($"Estado actual: {ent.Estado}");
            Console.Write("Ingrese nuevo estado (RECOGIDA/EN RUTA/ENTREGADA/CANCELADA): ");
            string nEstado = Console.ReadLine();

            if (ent.CambiarEstado(nEstado))
                Console.WriteLine("Estado actualizado.");
        }
        else if (op == "4")
        {
            foreach (var e in sistema.Entregas)
            {
                Console.WriteLine($"[{e.Codigo}] Cliente: {e.ClienteSolicitante.NombreCompleto} | Paquete: {e.PaqueteAsociado.Descripcion} | Estado: {e.Estado} | Total: Q{e.Total:F2}");
            }
        }
        PresioneContinuar();
    }