using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.DTOs.DTOsRespuestas;
using Aguila.Core.Entities;
using Aguila.Core.Enumeraciones;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class condicionActivosService : IcondicionActivosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IactivoMovimientosService _activoMovimientosService;
        private readonly IactivoMovimientosActualService _activoMovimientosActualService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public condicionActivosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,
                                        IactivoMovimientosService activoMovimientosService,
                                        IactivoMovimientosActualService activoMovimientosActualService,
                                        IImagenesRecursosService imagenesRecursosService 

                                       )
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _activoMovimientosService = activoMovimientosService;
            _activoMovimientosActualService = activoMovimientosActualService;
            _imagenesRecursosService = imagenesRecursosService;
            
        }

        public PagedList<condicionActivos> GetCondiciones(condicionActivosQueryFilter filter)
        {

            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            //se valida que vengan los filtros de fechas
            if (!filter.ignorarFechas)
            {
                if (filter.fechaInicio == null || filter.fechaFin == null)
                {
                    throw new AguilaException("Debe Especificar una Fecha de Inicio y una Fecha Final para Obtener los datos.", 404);
                }
                else
                {
                    var dias = (filter.fechaFin - filter.fechaInicio).Value.Days;
                    if (dias > 31) throw new AguilaException("Debe Especificar un Rango de Fechas No Mayor de 31 dias.", 404);
                }
            }

            //Personalizacion de Query Salida de informacion, solo se seleccionan ciertos campos de la tabla en la base de datos
            //Se cargan en el entity solo los campos que vengan, los demas se quedan con valores iniciales o nulos
            //Coun AutoMapper y dtos personalizados se pueden dar respuesta solo con los datos requeridos

            //var condiciones = _unitOfWork.condicionActivosRepository.GetAllIncludes();

            var condiciones = _unitOfWork.condicionActivosRepository
                              .GetAllIncludes().Select(p =>
                new condicionActivos
                {
                    id = p.id,
                    tipoCondicion = p.tipoCondicion,
                    idActivo = p.idActivo,
                    idEstacionTrabajo = p.idEstacionTrabajo,
                    idEmpleado = p.idEmpleado,
                    idReparacion = p.idReparacion,
                    idEstado = p.idEstado,
                    idImagenRecursoFirma = p.idImagenRecursoFirma,
                    idImagenRecursoFotos = p.idImagenRecursoFotos,
                    ubicacionIdEntrega = p.ubicacionIdEntrega,
                    idUsuario = p.idUsuario,
                    movimiento = p.movimiento,
                    disponible = p.disponible,
                    cargado = p.cargado,
                    serie = p.serie,
                    numero = p.numero,
                    inspecVeriOrden = p.inspecVeriOrden,
                    observaciones = p.observaciones,
                    daniosObserv = p.daniosObserv,
                    fecha = p.fecha,
                    fechaCreacion = p.fechaCreacion,
                    usuario = new Usuarios
                    {
                        Id = p.usuario.Id,
                        Nombre = p.usuario.Nombre,
                        Username = p.usuario.Username
                    },
                    activoOperacion = new activoOperaciones
                    {
                        codigo = p.activoOperacion.codigo
                       
                    },
                    estacionTrabajo = new EstacionesTrabajo { Codigo = p.estacionTrabajo.Codigo, Nombre = p.estacionTrabajo.Nombre, Tipo = p.estacionTrabajo.Tipo },
                    empleado = new empleados { codigo = p.empleado.codigo, nombres = p.empleado.nombres },
                    //ubicacionEntrega = new listas { id = p.ubicacionEntrega.id,valor = p.ubicacionEntrega.valor, descripcion = p.ubicacionEntrega.descripcion}
                    ubicacionEntrega = p.ubicacionEntrega
                });

            //
            string x = condiciones.Select(e => e).ToString();

            //filtrado por fechas
            if(!filter.ignorarFechas)
            condiciones = condiciones.Where(e=>e.fecha >= filter.fechaInicio && e.fecha < filter.fechaFin.Value.AddDays(1));

            if (filter.tipoCondicion != null)
            {
                condiciones = condiciones.Where(e => e.tipoCondicion.ToLower().Contains(filter.tipoCondicion.ToLower()));
            }

            if(filter.idActivo != null)
            {
                condiciones = condiciones.Where(e => e.idActivo == filter.idActivo);
            }

            if (filter.idEstacionTrabajo != null)
            {
                condiciones = condiciones.Where(e => e.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            if (filter.idEmpleado != null)
            {
                condiciones = condiciones.Where(e => e.idEmpleado == filter.idEmpleado);
            }

            if (filter.idReparacion != null)
            {
                condiciones = condiciones.Where(e => e.idReparacion == filter.idReparacion);
            }

            if (filter.idImagenRecursoFirma != null)
            {
                condiciones = condiciones.Where(e => e.idImagenRecursoFirma == filter.idImagenRecursoFirma);
            }

            if (filter.idImagenRecursoFotos != null)
            {
                condiciones = condiciones.Where(e => e.idImagenRecursoFotos == filter.idImagenRecursoFotos);
            }

            if (filter.ubicacionIdEntrega != null)
            {
                condiciones = condiciones.Where(e => e.ubicacionIdEntrega==filter.ubicacionIdEntrega);
            }

            if (filter.idUsuario != null)
            {
                condiciones = condiciones.Where(e => e.idUsuario == filter.idUsuario);
            }

            if (filter.movimiento != null)
            {
                condiciones = condiciones.Where(e => e.movimiento.ToLower().Contains(filter.movimiento.ToLower()));
            }

            if (filter.disponible != null)
            {
                condiciones = condiciones.Where(e => e.disponible == filter.disponible);
            }

            if (filter.cargado != null)
            {
                condiciones = condiciones.Where(e => e.cargado == filter.cargado);
            }

            if (filter.serie != null)
            {
                condiciones = condiciones.Where(e => e.serie.ToLower().Equals(filter.serie.ToLower()));
            }

            if (filter.numero != null)
            {
                condiciones = condiciones.Where(e => e.numero == filter.numero);
            }

            if (filter.inspecVeriOrden != null)
            {
                condiciones = condiciones.Where(e => e.inspecVeriOrden == filter.inspecVeriOrden);
            }

            if (filter.anio != null)
            {
                condiciones = condiciones.Where(e => e.fecha.Year == filter.anio);
            }

            if(filter.codigo != null)
            {
                condiciones = condiciones.Where(e => e.activoOperacion.codigo.ToLower().Equals(filter.codigo.ToLower()));
            }
            //TODO: FIltro por fechas, maximo 30 dias, en reportes si puede ser maximo 90 dias.

            condiciones = condiciones.OrderByDescending(e => e.fecha);

            var pagedCondiciones = PagedList<condicionActivos>.create(condiciones, filter.PageNumber, filter.PageSize);
            return pagedCondiciones;
        }

        //public async Task<condicionActivos> GetCodicion(int id)
        public async Task<JObject> GetCodicion(string id)
        {
            long idCondicion;
            condicionActivos condicion = new condicionActivos();

            //si es un ID numerico se busca por id la condicion 
            if(long.TryParse(id,out idCondicion))
            {
                condicion = await _unitOfWork.condicionActivosRepository.GetByID(idCondicion);
                if (condicion == null)
                {
                    throw new AguilaException("Condicion NO Existente", 404);
                }
            }// si es un codigo se busca la ultima condicion del codigo de activo enviado en "id"
            else
            {
                condicion = getUltimaCondicion(id);
                if (condicion == null)
                {
                    return null;
                }
            }        
            

            //var cabezal = await _unitOfWork.condicionCabezalRepository.GetByID(condicion.id);
            //var equipo = await _unitOfWork.condicionEquipoRepository.GetByID(condicion.id);
            //var furgon = await _unitOfWork.condicionFurgonRepository.GetByID(condicion.id);
            JObject response = new JObject();
            

           switch (condicion.tipoCondicion.ToLower())
           {
                case "cabezal":
                    var cabezal = await _unitOfWork.condicionCabezalRepository.GetByID(condicion.id);
                    response = await setResponseCabezal(cabezal, condicion);
                    break;

                case "equipo":
                    var equipo = await _unitOfWork.condicionEquipoRepository.GetByID(condicion.id);
                    response = await setResponseEquipo(equipo, condicion);
                    break;

                case "furgon":
                    var furgon = await _unitOfWork.condicionFurgonRepository.GetByID(condicion.id);
                    response = await setResponseFurgon(furgon, condicion);
                    break;
                case "generador":
                    var generador = await _unitOfWork.condicionGenSetRepository.GetByID(condicion.id);
                    response = await setResponseGenerador(generador, condicion);
                    break;
                case "tecnica":
                    var tecnica = await _unitOfWork.condicionTecnicaGenSetRepository.GetByID(condicion.id);
                    response = await setResponseGeneradorTecnica(tecnica, condicion);
                    break;
            }    

            

            return response;        
        }

        public async Task InsertCondicion(condicionActivosDto condicionDto, string user)
        {
            //VALIDA QUE EL ACTIVO SE ENCUENTRA EN EL INVENTARIO DE LA ESTACION DE TRABAJO
            var movActual = await _unitOfWork.activoMovimientosActualRepository.GetByID(condicionDto.idActivo);
            if(movActual.idEstacionTrabajo!=condicionDto.idEstacionTrabajo)
                throw new AguilaException("No es posible Utilizar este equipo/vehiculo, no ha ingresado a su localidad.", 404);

            //VALIDA QUE NO SE PUEDA CREAR CONDICIONES CON FECHAS MAYORES AL DIA ACTUAL
            if (condicionDto.fecha > DateTime.Now)
            {
                if ((Convert.ToDateTime(condicionDto.fecha).Date - Convert.ToDateTime(DateTime.Now).Date).Days >= 1)
                {
                    throw new AguilaException("No es posible registrar el documento con fecha mayor al dia de hoy", 404);
                }
                if ((Convert.ToDateTime(condicionDto.fecha).Date - Convert.ToDateTime(DateTime.Now).Date).Days == 0)
                {
                    condicionDto.fecha = DateTime.Now;
                }
            }

            condicionDto.id = 0;
            condicionDto.fechaCreacion = DateTime.Now;
            condicionCabezal cabezal = new condicionCabezal();
            condicionEquipo equipo = new condicionEquipo();
            condicionFurgon furgon = new condicionFurgon();
            condicionGenSet generador = new condicionGenSet();
            condicionTecnicaGenSet generadorTecnica = new condicionTecnicaGenSet();
            
            //leemos el objeto del detalle de la condicion 
            JObject json = JObject.Parse(condicionDto.condicionDetalle.ToString());

            //se prepara la condicion en base al tipo de condicion enviado
            switch (condicionDto.tipoCondicion.ToLower())
            {
                case "cabezal":
                        cabezal = setCondicionCabezal(json);
                    break;

                case "equipo":
                        equipo = setCondicionEquipo(json);
                    break;

                case "furgon":
                        furgon = setCondicionFurgon(json);
                    break;

                case "generador":
                    generador = setCondicionGenerador(json);
                    break;

                case "tecnica":
                    generadorTecnica = setCondicionGeneradorTecnica(json);
                    break;
            }
            //TODO: convencion de serie siempre A
            condicionDto.serie = "A";

            //VALIDACION DE ESTADOS DE CONDICION
            string xMsg = "";
            switch (condicionDto.movimiento.ToUpper())
            {
                case "INGRESO":
                    xMsg = await _activoMovimientosService.validarMovimiento(condicionDto.idActivo, ControlActivosEventos.CondicionIngreso);
                    break;

                case "SALIDA":
                    xMsg = await _activoMovimientosService.validarMovimiento(condicionDto.idActivo, ControlActivosEventos.CondicionSalida);
                    break;
            }

          if(!string.IsNullOrEmpty(xMsg))
                throw new AguilaException(xMsg, 404);


            try
            {
                var condicion = new condicionActivos
                {
                    tipoCondicion = condicionDto.tipoCondicion,
                    idActivo = condicionDto.idActivo,
                    idEstacionTrabajo = condicionDto.idEstacionTrabajo,
                    idEmpleado = condicionDto.idEmpleado,
                    idReparacion = condicionDto.idReparacion,
                    idImagenRecursoFirma = condicionDto.idImagenRecursoFirma,
                    idImagenRecursoFotos = condicionDto.idImagenRecursoFotos,
                    ubicacionIdEntrega = condicionDto.ubicacionIdEntrega,
                    idUsuario = condicionDto.idUsuario,
                    movimiento = condicionDto.movimiento,
                    disponible = condicionDto.disponible,
                    cargado = condicionDto.cargado,
                    //serie = condicionDto.serie,
                    serie = "A",
                    numero = obtenerCorrelativo(condicionDto),
                    inspecVeriOrden = condicionDto.inspecVeriOrden,
                    observaciones = condicionDto.observaciones,
                    daniosObserv = condicionDto.daniosObserv,
                    fecha = condicionDto.fecha,
                    fechaCreacion = condicionDto.fechaCreacion,
                    //TODO: Definir logisca de asignacion de estado
                    idEstado = condicionDto.idEstado
                };

                //TODO: implementar transaccion

                //inserta condicion activos
                await _unitOfWork.condicionActivosRepository.Add(condicion);
                await _unitOfWork.SaveChangeAsync();
                cabezal.idCondicionActivo = condicion.id;
                equipo.idCondicionActivo = condicion.id;
                furgon.idCondicionActivo = condicion.id;
                generador.idCondicionActivo = condicion.id;
                generadorTecnica.idCondicionActivo = condicion.id;
                condicionDto.id = condicion.id;

                //inserta condicion detalle segun sea el tipo de condicion
                var xEvento = condicion.movimiento.ToUpper().Trim() == "INGRESO" ? ControlActivosEventos.CondicionIngreso : ControlActivosEventos.CondicionSalida;
                switch (condicionDto.tipoCondicion.ToLower())
                {
                    case "cabezal":
                        await _unitOfWork.condicionCabezalRepository.Add(cabezal);
                        await _unitOfWork.SaveChangeAsync();
                        break;
                    case "equipo":
                        await _unitOfWork.condicionEquipoRepository.Add(equipo);
                        await _unitOfWork.SaveChangeAsync();
                        break;
                    case "furgon":
                        await _unitOfWork.condicionFurgonRepository.Add(furgon);
                        await _unitOfWork.SaveChangeAsync();
                        break;
                    case "generador":
                        await _unitOfWork.condicionGenSetRepository.Add(generador);
                        await _unitOfWork.SaveChangeAsync();
                        xEvento = ControlActivosEventos.CondicionGeneradorEstructura;
                        break;
                    case "tecnica":                        
                        await _unitOfWork.condicionTecnicaGenSetRepository.Add(generadorTecnica);
                        await _unitOfWork.SaveChangeAsync();
                        xEvento = ControlActivosEventos.CondicionGeneradorTecnica;
                        break;
                }

                var movimiento = await crearActivoMovimento(condicion);
                
                await _activoMovimientosService.InsertActivoMovimiento(movimiento, xEvento);
            }
            catch (Exception ex)
            {
                throw new AguilaException("Error al guardar Condicion de Activo:" + ex.InnerException.Message,400);
            }      

            
        }

        public async Task<bool> UpdateCondicion(condicionActivos condicion)
        {
            var currentCondicion = await _unitOfWork.condicionActivosRepository.GetByID(condicion.id);
            if (currentCondicion == null)
            {
                throw new AguilaException("Condicion no existente...");
            }

            currentCondicion.idReparacion = condicion.idReparacion;
            currentCondicion.idEmpleado = condicion.idEmpleado;
            currentCondicion.idImagenRecursoFirma = condicion.idImagenRecursoFirma;
            currentCondicion.ubicacionIdEntrega = condicion.ubicacionIdEntrega;
            currentCondicion.movimiento = condicion.movimiento;
            currentCondicion.disponible = condicion.disponible;
            currentCondicion.cargado = condicion.cargado;
            currentCondicion.serie = currentCondicion.serie;
            currentCondicion.inspecVeriOrden = currentCondicion.inspecVeriOrden;
            currentCondicion.observaciones = currentCondicion.observaciones;
            currentCondicion.fecha = currentCondicion.fecha;
            currentCondicion.irregularidadesObserv = currentCondicion.irregularidadesObserv;
            currentCondicion.daniosObserv = currentCondicion.daniosObserv;


            _unitOfWork.condicionActivosRepository.Update(currentCondicion);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteCondicion(int id)
        {
            var currentCondicion = await _unitOfWork.condicionActivosRepository.GetByID(id);
            if (currentCondicion == null)
            {
                throw new AguilaException("Condicion no existente...");
            }

            await _unitOfWork.condicionActivosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        public long obtenerCorrelativo(condicionActivosDto condicionDto)
        {
            condicionActivosQueryFilter filter = new condicionActivosQueryFilter
            {
                idEstacionTrabajo = condicionDto.idEstacionTrabajo,
                serie = condicionDto.serie,
                tipoCondicion = condicionDto.tipoCondicion,
                anio = DateTime.Now.Year,
                ignorarFechas = true,
            };

            long siguiente = 0;
            var condiciones = GetCondiciones(filter);

            if (condiciones.LongCount() < 1)
            {
                
                return (DateTime.Now.Year-2000)*10000+1;
            }

            siguiente = condiciones.Max(e => e.numero);
            return siguiente + 1;
        }

        public condicionCabezal setCondicionCabezal(JObject json)
        {
            condicionCabezal condicion = new condicionCabezal {
                windShield = (string)json["windShield"],
                plumillas = (string)json["plumillas"],
                viscera = (string)json["viscera"],
                rompeVientos = (string)json["rompeVientos"],
                persiana = (string)json["persiana"],
                bumper = (string)json["bumper"],
                capo = (string)json["capo"],
                retrovisor = (string)json["retrovisor"],
                ojoBuey = (string)json["ojoBuey"],
                pataGallo = (string)json["pataGallo"],
                portaLlanta = (string)json["portaLlanta"],
                spoilers = (string)json["spoilers"],
                salpicadera = (string)json["salpicadera"],
                guardaFango = (string)json["guardaFango"],
                taponCombustible = (string)json["taponCombustible"],
                lucesDelanteras = (string)json["lucesDelanteras"],
                lucesTraseras = (string)json["lucesTraseras"],
                pintura = (string)json["pintura"],
                baterias = (string)json["baterias"]                
            };
                        
            //SET DE LLANTAS DEL CABEZAL (MAXIMO 10 LLANTAS)
            var numeroLlantas = JObject.Parse(json.ToString())["condicionesLlantas"].Count();

            if (numeroLlantas ==1) { condicion.llanta1 = setLlanta(json, 0); }
            if (numeroLlantas ==2) { condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1); }
            if (numeroLlantas ==3) { condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                                     condicion.llanta3 = setLlanta(json, 2); }
            if (numeroLlantas ==4) {condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                                    condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3); }
            if (numeroLlantas ==5) {condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                                    condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3); 
                                    condicion.llanta5 = setLlanta(json, 4); }
            if (numeroLlantas ==6) {condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                                    condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                                    condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5); }
            if (numeroLlantas ==7) {condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                                    condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                                    condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5); 
                                    condicion.llanta7 = setLlanta(json, 6); }
            if (numeroLlantas ==8) {condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                                    condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                                    condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
                                    condicion.llanta7 = setLlanta(json, 6); condicion.llanta8 = setLlanta(json, 7); }
            if (numeroLlantas ==9) {condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                                    condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                                    condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
                                    condicion.llanta7 = setLlanta(json, 6); condicion.llanta8 = setLlanta(json, 7); 
                                    condicion.llanta9 = setLlanta(json, 8); }
            if (numeroLlantas ==10) {condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                                     condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                                     condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
                                     condicion.llanta7 = setLlanta(json, 6); condicion.llanta8 = setLlanta(json, 7);
                                     condicion.llanta9 = setLlanta(json, 8); condicion.llanta10 = setLlanta(json, 9); }

            //LLANTAS DE REPUESTO (MAXIMO 2 LLANTAS)
            var numeroLlantasRepuesto = JObject.Parse(json.ToString())["condicionesLlantasRepuesto"].Count();

            if (numeroLlantasRepuesto == 1) {
                condicion.llantaR = setLlantaRepuesto(json, 0);
            }
            if(numeroLlantasRepuesto == 2) {
                condicion.llantaR = setLlantaRepuesto(json, 0);
                condicion.llantaR2 = setLlantaRepuesto(json, 1);
            }



            return condicion;
        }

        public condicionEquipo setCondicionEquipo(JObject json)
        {
            condicionEquipo condicion = new condicionEquipo
            {
                lucesA = (bool)json["lucesA"],
                lucesB = (bool)json["lucesB"],
                lucesC = (bool)json["lucesC"],
                lucesD = (bool)json["lucesD"],
                lucesE = (bool)json["lucesE"],
                lucesF = (bool)json["lucesF"],
                pi = (bool)json["pi"],
                pd = (bool)json["pd"],
                si = (bool)json["si"],
                sd = (bool)json["sd"],
                guardaFangosG = (string)json["guardaFangosG"],
                guardaFangosI = (string)json["guardaFangosI"],
                cintaReflectivaLat = (string)json["cintaReflectivaLat"],
                cintaReflectivaFront = (string)json["cintaReflectivaFront"],
                cintaReflectivaTra = (string)json["cintaReflectivaTra"],
                manitas1 = (string)json["manitas1"],
                manitas2 = (string)json["manitas2"],
                bumper = (string)json["bumper"],
                fricciones = (string)json["fricciones"],
                friccionesLlantas = (string)json["friccionesLlantas"],
                patas = (string)json["patas"],
                ganchos = (string)json["ganchos"],
                balancines = (string)json["balancines"],
                hojasResortes = (string)json["hojasResortes"]
            };

            //SET DE LLANTAS DEL EQUIPO (MAXIMO 12 LLANTAS)
            var numeroLlantas = JObject.Parse(json.ToString())["condicionesLlantas"].Count();

            if (numeroLlantas == 1) { condicion.llanta1 = setLlanta(json, 0); }
            if (numeroLlantas == 2) { condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1); }
            if (numeroLlantas == 3)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2);
            }
            if (numeroLlantas == 4)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
            }
            if (numeroLlantas == 5)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4);
            }
            if (numeroLlantas == 6)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
            }
            if (numeroLlantas == 7)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
                condicion.llanta7 = setLlanta(json, 6);
            }
            if (numeroLlantas == 8)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
                condicion.llanta7 = setLlanta(json, 6); condicion.llanta8 = setLlanta(json, 7);
            }
            if (numeroLlantas == 9)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
                condicion.llanta7 = setLlanta(json, 6); condicion.llanta8 = setLlanta(json, 7);
                condicion.llanta9 = setLlanta(json, 8);
            }
            if (numeroLlantas == 10)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
                condicion.llanta7 = setLlanta(json, 6); condicion.llanta8 = setLlanta(json, 7);
                condicion.llanta9 = setLlanta(json, 8); condicion.llanta10 = setLlanta(json, 9);
            }
            if (numeroLlantas == 11)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
                condicion.llanta7 = setLlanta(json, 6); condicion.llanta8 = setLlanta(json, 7);
                condicion.llanta9 = setLlanta(json, 8); condicion.llanta10 = setLlanta(json, 9);
                condicion.llanta11 = setLlanta(json, 10);
            }
            if (numeroLlantas == 12)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
                condicion.llanta7 = setLlanta(json, 6); condicion.llanta8 = setLlanta(json, 7);
                condicion.llanta9 = setLlanta(json, 8); condicion.llanta10 = setLlanta(json, 9);
                condicion.llanta11 = setLlanta(json, 10); condicion.llanta12 = setLlanta(json, 11);
            }

            //LLANTAS DE REPUESTO (MAXIMO 2 LLANTAS)
            var numeroLlantasRepuesto = JObject.Parse(json.ToString())["condicionesLlantasRepuesto"].Count();

            if (numeroLlantasRepuesto == 1)
            {
                condicion.llantaR = setLlantaRepuesto(json, 0);
            }
            if (numeroLlantasRepuesto == 2)
            {
                condicion.llantaR = setLlantaRepuesto(json, 0);
                condicion.llantaR2 = setLlantaRepuesto(json, 1);
            }

            return condicion;
        }

        public condicionFurgon setCondicionFurgon (JObject json)
        {
            condicionFurgon condicion = new condicionFurgon
            {
                revExtGolpe = (bool)json["revExtGolpe"],
                revExtSeparacion = (bool)json["revExtSeparacion"],
                revExtRoturas = (bool)json["revExtRoturas"],
                revIntGolpes = (bool)json["revIntGolpes"],
                revIntSeparacion = (bool)json["revIntSeparacion"],
                revIntFiltra = (bool)json["revIntFiltra"],
                revIntRotura = (bool)json["revIntRotura"],
                revIntPisoH = (bool)json["revIntPisoH"],
                revIntManchas = (bool)json["revIntManchas"],
                revIntOlores = (bool)json["revIntOlores"],
                revPuertaCerrado = (string)json["revPuertaCerrado"],
                revPuertaEmpaque = (string)json["revPuertaEmpaque"],
                revPuertaCinta = (string)json["revPuertaCinta"],
                limpPiso = (bool)json["limpPiso"],
                limpTecho = (bool)json["limpTecho"],
                limpLateral = (bool)json["limpLateral"],
                limpExt = (bool)json["limpExt"],
                limpPuerta = (bool)json["limpPuerta"],
                limpMancha = (bool)json["limpMancha"],
                limpOlor = (bool)json["limpOlor"],
                limpRefuerzo = (bool)json["limpRefuerzo"],
                lucesA = (bool)json["lucesA"],
                lucesB = (bool)json["lucesB"],
                lucesC = (bool)json["lucesC"],
                lucesD = (bool)json["lucesD"],
                lucesE = (bool)json["lucesE"],
                lucesF = (bool)json["lucesF"],
                lucesG = (bool)json["lucesG"],
                lucesH = (bool)json["lucesH"],
                lucesI = (bool)json["lucesI"],
                lucesJ = (bool)json["lucesJ"],
                lucesK = (bool)json["lucesK"],
                lucesL = (bool)json["lucesL"],
                lucesM = (bool)json["lucesM"],
                lucesN = (bool)json["lucesN"],
                lucesO = (bool)json["lucesO"],
                guardaFangosI = (bool)json["guardaFangosI"],
                guardaFangosD = (bool)json["guardaFangosD"],
                fricciones = (string)json["fricciones"],
                senalizacion = (string)json["senalizacion"],
            };

            //SET DE LLANTAS DEL EQUIPO (MAXIMO 12 LLANTAS)
            var numeroLlantas = JObject.Parse(json.ToString())["condicionesLlantas"].Count();

            if (numeroLlantas == 1) { condicion.llanta1 = setLlanta(json, 0); }
            if (numeroLlantas == 2) { condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1); }
            if (numeroLlantas == 3)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2);
            }
            if (numeroLlantas == 4)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
            }
            if (numeroLlantas == 5)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4);
            }
            if (numeroLlantas == 6)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
            }
            if (numeroLlantas == 7)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
                condicion.llanta7 = setLlanta(json, 6);
            }
            if (numeroLlantas == 8)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
                condicion.llanta7 = setLlanta(json, 6); condicion.llanta8 = setLlanta(json, 7);
            }
            if (numeroLlantas == 9)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
                condicion.llanta7 = setLlanta(json, 6); condicion.llanta8 = setLlanta(json, 7);
                condicion.llanta9 = setLlanta(json, 8);
            }
            if (numeroLlantas == 10)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
                condicion.llanta7 = setLlanta(json, 6); condicion.llanta8 = setLlanta(json, 7);
                condicion.llanta9 = setLlanta(json, 8); condicion.llanta10 = setLlanta(json, 9);
            }
            if (numeroLlantas == 11)
            {
                condicion.llanta1 = setLlanta(json, 0); condicion.llanta2 = setLlanta(json, 1);
                condicion.llanta3 = setLlanta(json, 2); condicion.llanta4 = setLlanta(json, 3);
                condicion.llanta5 = setLlanta(json, 4); condicion.llanta6 = setLlanta(json, 5);
                condicion.llanta7 = setLlanta(json, 6); condicion.llanta8 = setLlanta(json, 7);
                condicion.llanta9 = setLlanta(json, 8); condicion.llanta10 = setLlanta(json, 9);
                condicion.llanta11 = setLlanta(json, 10);
            }

            //LLANTAS DE REPUESTO (MAXIMO 2 LLANTAS)
            var numeroLlantasRepuesto = JObject.Parse(json.ToString())["condicionesLlantasRepuesto"].Count();

            if (numeroLlantasRepuesto == 1)
            {
                condicion.llantaR = setLlantaRepuesto(json, 0);
            }
            if (numeroLlantasRepuesto == 2)
            {
                condicion.llantaR = setLlantaRepuesto(json, 0);
                condicion.llantaR2 = setLlantaRepuesto(json, 1);
            }

            return condicion;
        }

        public condicionGenSet setCondicionGenerador(JObject json)
        {
            condicionGenSet condicion = new condicionGenSet
            {
                galonesRequeridos = (decimal?)json["galonesRequeridos"],
                galonesGenSet = (decimal?)json["galonesGenSet"],
                galonesCompletar = (decimal?)json["galonesCompletar"],
                horometro = (decimal?)json["horometro"],
                horaEncendida = (DateTime?)json["horaEncendida"],
                horaApagada = (DateTime?)json["horaApagada"],
                dieselEntradaSalida = (decimal?)json["dieselEntradaSalida"],
                dieselConsumido = (decimal?)json["dieselConsumido"],
                horasTrabajadas = (decimal?)json["horasTrabajadas"],
                estExPuertasGolpeadas = (bool)json["estExPuertasGolpeadas"],
                estExPuertasQuebradas = (bool)json["estExPuertasQuebradas"],
                estExPuertasFaltantes = (bool)json["estExPuertasFaltantes"],
                estExPuertasSueltas = (bool)json["estExPuertasSueltas"],
                estExBisagrasQuebradas = (bool)json["estExBisagrasQuebradas"],
                panelGolpes = (bool)json["panelGolpes"],
                panelTornillosFaltantes = (bool)json["panelTornillosFaltantes"],
                panelOtros = (bool)json["panelOtros"],
                soporteGolpes = (bool)json["soporteGolpes"],
                soporteTornillosFaltantes = (bool)json["soporteTornillosFaltantes"],
                soporteMarcoQuebrado = (bool)json["soporteMarcoQuebrado"],
                soporteMarcoFlojo = (bool)json["soporteMarcoFlojo"],
                soporteBisagrasQuebradas = (bool)json["soporteBisagrasQuebradas"],
                soporteSoldaduraEstado = (bool)json["soporteSoldaduraEstado"],
                revIntCablesQuemados = (bool)json["revIntCablesQuemados"],
                revIntCablesSueltos = (bool)json["revIntCablesSueltos"],
                revIntReparacionesImpropias = (bool)json["revIntReparacionesImpropias"],
                tanqueAgujeros = (bool)json["tanqueAgujeros"],
                tanqueSoporteDanado = (bool)json["tanqueSoporteDanado"],
                tanqueMedidorDiesel = (bool)json["tanqueMedidorDiesel"],
                tanqueCodoQuebrado = (bool)json["tanqueCodoQuebrado"],
                tanqueTapon = (bool)json["tanqueTapon"],
                tanqueTuberia = (bool)json["tanqueTuberia"],
                pFaltMedidorAceite = (bool)json["pFaltMedidorAceite"],
                pFaltTapaAceite = (bool)json["pFaltTapaAceite"],
                pFaltTaponRadiador = (bool)json["pFaltTaponRadiador"],
            };

            return condicion;
        }

        public condicionTecnicaGenSet setCondicionGeneradorTecnica(JObject json)
        {
            condicionTecnicaGenSet condicion = new condicionTecnicaGenSet
            {
                bateriaCodigo = (string)json["bateriaCodigo"],
                bateriaNivelAcido = (bool)json["bateriaNivelAcido"],
                bateriaArnes = (bool)json["bateriaArnes"],
                bateriaTerminales = (bool)json["bateriaTerminales"],
                bateriaGolpes = (bool)json["bateriaGolpes"],
                bateriaCarga = (bool)json["bateriaCarga"],
                combustibleDiesel = (bool)json["combustibleDiesel"],
                combustibleAgua = (bool)json["combustibleAgua"],
                combustibleAceite = (bool)json["combustibleAceite"],
                combustibleFugas = (bool)json["combustibleFugas"],
                filtroAceite = (bool)json["filtroAceite"],
                filtroDiesel = (bool)json["filtroDiesel"],
                bombaAguaEstado = (bool)json["bombaAguaEstado"],
                escapeAgujeros = (bool)json["escapeAgujeros"],
                escapeDañado = (bool)json["escapeDañado"],
                cojinetesEstado = (bool)json["cojinetesEstado"],
                arranqueFuncionamiento = (bool)json["arranqueFuncionamiento"],
                fajaAlternador = (bool)json["fajaAlternador"],
                enfriamientoAire = (bool)json["enfriamientoAire"],
                enfriamientoAgua = (bool)json["enfriamientoAgua"],
                cantidadGeneradaVolts = (bool)json["cantidadGeneradaVolts"]                
            };
            return condicion;
        }

        public string setLlanta(JObject json, int index)
        {       
         string llanta = (string)json["condicionesLlantas"][index]["codigo"] + ";"
                     +(string)json["condicionesLlantas"][index]["marca"] + ";"
                     + (string)json["condicionesLlantas"][index]["profundidadIzq"] + ";"
                     + (string)json["condicionesLlantas"][index]["profundidadCto"] + ";"
                     + (string)json["condicionesLlantas"][index]["profundidadDer"] + ";"
                     + (string)json["condicionesLlantas"][index]["psi"] + ";"
                     + (string)json["condicionesLlantas"][index]["estado"] + ";"
                     + (string)json["condicionesLlantas"][index]["observaciones"]
                     ;

            return llanta;
        }

        public string setLlantaRepuesto(JObject json, int index)
        {
            string llanta = (string)json["condicionesLlantasRepuesto"][index]["codigo"] + ";"
                        + (string)json["condicionesLlantasRepuesto"][index]["marca"] + ";"
                        + (string)json["condicionesLlantasRepuesto"][index]["profundidadIzq"] + ";"
                        + (string)json["condicionesLlantasRepuesto"][index]["profundidadCto"] + ";"
                        + (string)json["condicionesLlantasRepuesto"][index]["profundidadDer"] + ";"
                        + (string)json["condicionesLlantasRepuesto"][index]["psi"] + ";"
                        + (string)json["condicionesLlantasRepuesto"][index]["estado"] + ";"
                        + (string)json["condicionesLlantasRepuesto"][index]["observaciones"]
                        ;

            return llanta;
        }

        public async Task<activoMovimientos>  crearActivoMovimento(condicionActivos condicion)
        {
            var xEstacion = await _unitOfWork.EstacionesTrabajoRepository.GetByID(condicion.idEstacionTrabajo);

            activoMovimientos movimiento = new activoMovimientos() { 
                idActivo = condicion.idActivo,
                idEstado = condicion.idEstado,
                idEstacionTrabajo = condicion.idEstacionTrabajo,
                idEmpleado = condicion.idEmpleado,
                ubicacionId = condicion.ubicacionIdEntrega,
                lugar = xEstacion.Codigo,
                idUsuario = condicion.idUsuario,
                documento = condicion.numero,
                observaciones = condicion.observaciones,
                cargado = condicion.cargado,
                //TODO: definir tipos de documento a asignar
                tipoDocumento = "Condicion",
                fecha = condicion.fecha,
                fechaCreacion = DateTime.Now                             
            };

            return movimiento;
        }

        public async Task<JObject> setResponseCabezal(condicionCabezal cabezal, condicionActivos condicion)
        {
            
            var empleado = await _unitOfWork.empleadosRepository.GetByID(condicion.idEmpleado);
            var usuario =await  _unitOfWork.UsuariosRepository.GetByID(condicion.idUsuario);
            var activo = await _unitOfWork.activoOperacionesRepository.GetByID(condicion.idActivo);
            //como es tipo cabezal la placa se va a busscar a la tabla vehicuso, cuadno sea de tipo equipo
            //se debe ir a buscar la placa a equipoRemolque
            var vehiculo = await _unitOfWork.vehiculosRepository.GetByID(condicion.idActivo);

            string placa = null;

            if(vehiculo != null)
            {
                placa = vehiculo.placa;
            }

            JObject response = new JObject
            {
                ["id"] = condicion.id,
                ["tipoCondicion"] = condicion.tipoCondicion,
                ["idActivo"] = condicion.idActivo,
                ["codigo"] = activo.codigo,
                ["activo"] = activo.descripcion,
                ["placa"] = placa,
                ["idEstacionTrabajo"] = condicion.idEstacionTrabajo,
                ["idEmpleado"] = condicion.idEmpleado,
                ["empleado"] = empleado.nombres,
                ["idReparacion"] = condicion.idReparacion,
                ["idEstado"] = condicion.idEstado,
                ["idImagenRecursoFirma"] = condicion.idImagenRecursoFirma,
                ["idImagenRecursoFotos"] = condicion.idImagenRecursoFotos,
                ["ubicacionIdEntrega"] = condicion.ubicacionIdEntrega,
                ["idUsuario"] = condicion.idUsuario,
                ["usuario"] = usuario.Nombre,
                ["movimiento"] = condicion.movimiento,
                ["disponible"] = condicion.disponible,
                ["cargado"] = condicion.cargado,
                ["serie"] = condicion.serie,
                ["numero"] = condicion.numero,
                ["inspecVeriOrden"] = condicion.inspecVeriOrden,
                ["observaciones"] = condicion.observaciones,
                ["fecha"] = condicion.fecha,
                ["fechaCreacion"] = condicion.fechaCreacion
            };

            JObject detalle = new JObject {
                ["idCondicionActivo"] = cabezal.idCondicionActivo,               
                ["windShield"] = cabezal.windShield,
                ["plumillas"] = cabezal.plumillas,
                ["viscera"] = cabezal.viscera,
                ["rompeVientos"] = cabezal.rompeVientos,
                ["persiana"] = cabezal.persiana,
                ["bumper"] = cabezal.bumper,
                ["capo"] = cabezal.capo,
                ["retrovisor"] = cabezal.retrovisor,
                ["ojoBuey"] = cabezal.ojoBuey,
                ["pataGallo"] = cabezal.pataGallo,
                ["portaLlanta"] = cabezal.portaLlanta,
                ["spoilers"] = cabezal.spoilers,
                ["salpicadera"] = cabezal.salpicadera,
                ["guardaFango"] = cabezal.guardaFango,
                ["taponCombustible"] = cabezal.taponCombustible,
                ["baterias"] = cabezal.baterias,
                ["lucesDelanteras"] = cabezal.lucesDelanteras,
                ["lucesTraseras"] = cabezal.lucesTraseras,
                ["pintura"] = cabezal.pintura
            };

            response.Add("condicionDetalle",detalle);
            
            List<condicionLlantaDto> listaLlantas = new List<condicionLlantaDto>();
            List<condicionLlantaDto> listaLlantasRepuesto = new List<condicionLlantaDto>();

            //LLANTAS
            if(cabezal.llanta1 != null) { listaLlantas.Add(getLlantaCabezal(cabezal,1)); }
            if (cabezal.llanta2 != null) { listaLlantas.Add(getLlantaCabezal(cabezal, 2)); }
            if (cabezal.llanta3 != null) { listaLlantas.Add(getLlantaCabezal(cabezal, 3)); }
            if (cabezal.llanta4 != null) { listaLlantas.Add(getLlantaCabezal(cabezal, 4)); }
            if (cabezal.llanta5 != null) { listaLlantas.Add(getLlantaCabezal(cabezal, 5)); }
            if (cabezal.llanta6 != null) { listaLlantas.Add(getLlantaCabezal(cabezal, 6)); }
            if (cabezal.llanta7 != null) { listaLlantas.Add(getLlantaCabezal(cabezal, 7)); }
            if (cabezal.llanta8 != null) { listaLlantas.Add(getLlantaCabezal(cabezal, 8)); }
            if (cabezal.llanta9 != null) { listaLlantas.Add(getLlantaCabezal(cabezal, 9)); }
            if (cabezal.llanta10 != null) { listaLlantas.Add(getLlantaCabezal(cabezal,10)); }
            //LLANTAS DE REPUESTO
            if (cabezal.llantaR != null) { listaLlantasRepuesto.Add(getLlantaCabezal(cabezal, 11)); }
            if (cabezal.llantaR2 != null) { listaLlantasRepuesto.Add(getLlantaCabezal(cabezal, 12)); }



            //se agrega al json el arreglo con la condicion de las llantas
            //JObject json = response["condicionDetalle"] as JObject;
            //json.Add("condicionesLlantas", JArray.FromObject(listaLlantas));

            response.Add("condicionesLlantas", JArray.FromObject(listaLlantas));
            response.Add("condicionesLlantasRepuesto", JArray.FromObject(listaLlantasRepuesto));

            return response;
        }

        public async Task<JObject> setResponseEquipo(condicionEquipo equipo, condicionActivos condicion)
        {
            var empleado = await _unitOfWork.empleadosRepository.GetByID(condicion.idEmpleado);
            var usuario = await _unitOfWork.UsuariosRepository.GetByID(condicion.idUsuario);
            var activo = await _unitOfWork.activoOperacionesRepository.GetByID(condicion.idActivo);
            var equipoRemolque = await _unitOfWork.equipoRemolqueRepository.GetByID(condicion.idActivo);

            string placa = null;

            if (equipoRemolque != null)
            {
                placa = equipoRemolque.placa;
            }

            JObject response = new JObject
            {
                ["id"] = condicion.id,
                ["tipoCondicion"] = condicion.tipoCondicion,
                ["idActivo"] = condicion.idActivo,
                ["codigo"] = activo.codigo,
                ["activo"] = activo.descripcion,
                ["placa"] = placa,
                ["idEstacionTrabajo"] = condicion.idEstacionTrabajo,
                ["idEmpleado"] = condicion.idEmpleado,
                ["empleado"] = empleado.nombres,
                ["idReparacion"] = condicion.idReparacion,
                ["idEstado"] = condicion.idEstado,
                ["idImagenRecursoFirma"] = condicion.idImagenRecursoFirma,
                ["idImagenRecursoFotos"] = condicion.idImagenRecursoFotos,
                ["ubicacionIdEntrega"] = condicion.ubicacionIdEntrega,
                ["idUsuario"] = condicion.idUsuario,
                ["usuario"] = usuario.Nombre,
                ["movimiento"] = condicion.movimiento,
                ["disponible"] = condicion.disponible,
                ["cargado"] = condicion.cargado,
                ["serie"] = condicion.serie,
                ["numero"] = condicion.numero,
                ["inspecVeriOrden"] = condicion.inspecVeriOrden,
                ["observaciones"] = condicion.observaciones,
                ["fecha"] = condicion.fecha,
                ["fechaCreacion"] = condicion.fechaCreacion
            };

            JObject detalle = new JObject
            {
                ["idCondicionActivo"] = equipo.idCondicionActivo,
                ["lucesA"] = equipo.lucesA,
                ["lucesB"] = equipo.lucesB,
                ["lucesC"] = equipo.lucesC,
                ["lucesD"] = equipo.lucesD,
                ["lucesE"] = equipo.lucesE,
                ["lucesF"] = equipo.lucesF,
                ["pi"] = equipo.pi,
                ["pd"] = equipo.pd,
                ["si"] = equipo.si,
                ["sd"] = equipo.sd,
                ["guardaFangosG"] = equipo.guardaFangosG,
                ["guardaFangosI"] = equipo.guardaFangosI,
                ["cintaReflectivaLat"] = equipo.cintaReflectivaLat,
                ["cintaReflectivaFront"] = equipo.cintaReflectivaFront,
                ["cintaReflectivaTra"] = equipo.cintaReflectivaTra,
                ["manitas1"] = equipo.manitas1,
                ["manitas2"] = equipo.manitas2,
                ["bumper"] = equipo.bumper,
                ["fricciones"] = equipo.fricciones,
                ["friccionesLlantas"] = equipo.friccionesLlantas,
                ["patas"] = equipo.patas,
                ["ganchos"] = equipo.ganchos,
                ["balancines"] = equipo.balancines,
                ["hojasResortes"] = equipo.hojasResortes,
            };

            response.Add("condicionDetalle", detalle);

            List<condicionLlantaDto> listaLlantas = new List<condicionLlantaDto>();
            List<condicionLlantaDto> listaLlantasRepuesto = new List<condicionLlantaDto>();

            //LLANTAS
            if (equipo.llanta1 != null) { listaLlantas.Add(getLlantaEquipo(equipo, 1)); }
            if (equipo.llanta2 != null) { listaLlantas.Add(getLlantaEquipo(equipo, 2)); }
            if (equipo.llanta3 != null) { listaLlantas.Add(getLlantaEquipo(equipo, 3)); }
            if (equipo.llanta4 != null) { listaLlantas.Add(getLlantaEquipo(equipo, 4)); }
            if (equipo.llanta5 != null) { listaLlantas.Add(getLlantaEquipo(equipo, 5)); }
            if (equipo.llanta6 != null) { listaLlantas.Add(getLlantaEquipo(equipo, 6)); }
            if (equipo.llanta7 != null) { listaLlantas.Add(getLlantaEquipo(equipo, 7)); }
            if (equipo.llanta8 != null) { listaLlantas.Add(getLlantaEquipo(equipo, 8)); }
            if (equipo.llanta9 != null) { listaLlantas.Add(getLlantaEquipo(equipo, 9)); }
            if (equipo.llanta10 != null) { listaLlantas.Add(getLlantaEquipo(equipo, 10)); }
            if (equipo.llanta11 != null) { listaLlantas.Add(getLlantaEquipo(equipo, 11)); }
            if (equipo.llanta12 != null) { listaLlantas.Add(getLlantaEquipo(equipo, 12)); }
            //LLANTAS DE REPUESTO
            if (equipo.llantaR != null) { listaLlantasRepuesto.Add(getLlantaEquipo(equipo, 13)); }
            if (equipo.llantaR2 != null) { listaLlantasRepuesto.Add(getLlantaEquipo(equipo, 14)); }

            response.Add("condicionesLlantas", JArray.FromObject(listaLlantas));
            response.Add("condicionesLlantasRepuesto", JArray.FromObject(listaLlantasRepuesto));


            return response;
        }

        public async Task<JObject> setResponseFurgon(condicionFurgon furgon, condicionActivos condicion)
        {
            var empleado = await _unitOfWork.empleadosRepository.GetByID(condicion.idEmpleado);
            var usuario = await _unitOfWork.UsuariosRepository.GetByID(condicion.idUsuario);
            var activo = await _unitOfWork.activoOperacionesRepository.GetByID(condicion.idActivo);
            var equipoRemolque = await _unitOfWork.equipoRemolqueRepository.GetByID(condicion.idActivo);

            string placa = null;

            if (equipoRemolque != null)
            {
                placa = equipoRemolque.placa;
            }

            JObject response = new JObject
            {
                ["id"] = condicion.id,
                ["tipoCondicion"] = condicion.tipoCondicion,
                ["idActivo"] = condicion.idActivo,
                ["codigo"] = activo.codigo,
                ["activo"] = activo.descripcion,
                ["placa"] = placa,
                ["idEstacionTrabajo"] = condicion.idEstacionTrabajo,
                ["idEmpleado"] = condicion.idEmpleado,
                ["empleado"] = empleado.nombres,
                ["idReparacion"] = condicion.idReparacion,
                ["idEstado"] = condicion.idEstado,
                ["idImagenRecursoFirma"] = condicion.idImagenRecursoFirma,
                ["idImagenRecursoFotos"] = condicion.idImagenRecursoFotos,
                ["ubicacionIdEntrega"] = condicion.ubicacionIdEntrega,
                ["idUsuario"] = condicion.idUsuario,
                ["usuario"] = usuario.Nombre,
                ["movimiento"] = condicion.movimiento,
                ["disponible"] = condicion.disponible,
                ["cargado"] = condicion.cargado,
                ["serie"] = condicion.serie,
                ["numero"] = condicion.numero,
                ["inspecVeriOrden"] = condicion.inspecVeriOrden,
                ["observaciones"] = condicion.observaciones,
                ["fecha"] = condicion.fecha,
                ["fechaCreacion"] = condicion.fechaCreacion
            };

            JObject detalle = new JObject
            {
                ["idCondicionActivo"] = furgon.idCondicionActivo,
                ["revExtGolpe"] = furgon.revExtGolpe,
                ["revExtSeparacion"] = furgon.revExtSeparacion,
                ["revExtRoturas"] = furgon.revExtRoturas,
                ["revIntGolpes"] = furgon.revIntGolpes,
                ["revIntSeparacion"] = furgon.revIntSeparacion,
                ["revIntFiltra"] = furgon.revIntFiltra,
                ["revIntRotura"] = furgon.revIntRotura,
                ["revIntPisoH"] = furgon.revIntPisoH,
                ["revIntManchas"] = furgon.revIntManchas,
                ["revIntOlores"] = furgon.revIntOlores,
                ["revPuertaCerrado"] = furgon.revPuertaCerrado,
                ["revPuertaEmpaque"] = furgon.revPuertaEmpaque,
                ["revPuertaCinta"] = furgon.revPuertaCinta,
                ["limpPiso"] = furgon.limpPiso,
                ["limpTecho"] = furgon.limpTecho,
                ["limpLateral"] = furgon.limpLateral,
                ["limpExt"] = furgon.limpExt,
                ["limpPuerta"] = furgon.limpPuerta,
                ["limpMancha"] = furgon.limpMancha,
                ["limpOlor"] = furgon.limpOlor,
                ["limpRefuerzo"] = furgon.limpRefuerzo,
                ["lucesA"] = furgon.lucesA,
                ["lucesB"] = furgon.lucesB,
                ["lucesC"] = furgon.lucesC,
                ["lucesD"] = furgon.lucesD,
                ["lucesE"] = furgon.lucesE,
                ["lucesF"] = furgon.lucesF,
                ["lucesG"] = furgon.lucesG,
                ["lucesH"] = furgon.lucesH,
                ["lucesI"] = furgon.lucesI,
                ["lucesJ"] = furgon.lucesJ,
                ["lucesK"] = furgon.lucesK,
                ["lucesL"] = furgon.lucesL,
                ["lucesM"] = furgon.lucesM,
                ["lucesN"] = furgon.lucesN,
                ["lucesO"] = furgon.lucesO,
                ["guardaFangosI"] = furgon.guardaFangosI,
                ["guardaFangosD"] = furgon.guardaFangosD,
                ["fricciones"] = furgon.fricciones,
                ["senalizacion"] = furgon.senalizacion
            };

            response.Add("condicionDetalle", detalle);

            List<condicionLlantaDto> listaLlantas = new List<condicionLlantaDto>();
            List<condicionLlantaDto> listaLlantasRepuesto = new List<condicionLlantaDto>();

            //LLANTAS
            if (furgon.llanta1 != null) { listaLlantas.Add(getLlantaFurgon(furgon, 1)); }
            if (furgon.llanta2 != null) { listaLlantas.Add(getLlantaFurgon(furgon, 2)); }
            if (furgon.llanta3 != null) { listaLlantas.Add(getLlantaFurgon(furgon, 3)); }
            if (furgon.llanta4 != null) { listaLlantas.Add(getLlantaFurgon(furgon, 4)); }
            if (furgon.llanta5 != null) { listaLlantas.Add(getLlantaFurgon(furgon, 5)); }
            if (furgon.llanta6 != null) { listaLlantas.Add(getLlantaFurgon(furgon, 6)); }
            if (furgon.llanta7 != null) { listaLlantas.Add(getLlantaFurgon(furgon, 7)); }
            if (furgon.llanta8 != null) { listaLlantas.Add(getLlantaFurgon(furgon, 8)); }
            if (furgon.llanta9 != null) { listaLlantas.Add(getLlantaFurgon(furgon, 9)); }
            if (furgon.llanta10 != null) { listaLlantas.Add(getLlantaFurgon(furgon, 10)); }
            if (furgon.llanta11 != null) { listaLlantas.Add(getLlantaFurgon(furgon, 11)); }
            //LLANTAS DE REPUESTO
            if (furgon.llantaR != null) { listaLlantasRepuesto.Add(getLlantaFurgon(furgon, 12)); }
            if (furgon.llantaR2 != null) { listaLlantasRepuesto.Add(getLlantaFurgon(furgon, 13)); }

            response.Add("condicionesLlantas", JArray.FromObject(listaLlantas));
            response.Add("condicionesLlantasRepuesto", JArray.FromObject(listaLlantasRepuesto));


            return response;
        }

        public async Task<JObject> setResponseGenerador(condicionGenSet generador, condicionActivos condicion)
        {
            var empleado = await _unitOfWork.empleadosRepository.GetByID(condicion.idEmpleado);
            var usuario = await _unitOfWork.UsuariosRepository.GetByID(condicion.idUsuario);
            var activo = await _unitOfWork.activoOperacionesRepository.GetByID(condicion.idActivo);
            //var equipoRemolque = await _unitOfWork.equipoRemolqueRepository.GetByID(condicion.idActivo);

            //string placa = null;

            //if (equipoRemolque != null)
            //{
            //    placa = equipoRemolque.placa;
            //}

            JObject response = new JObject
            {
                ["id"] = condicion.id,
                ["tipoCondicion"] = condicion.tipoCondicion,
                ["idActivo"] = condicion.idActivo,
                ["codigo"] = activo.codigo,
                ["activo"] = activo.descripcion,
                //["placa"] = placa,
                ["idEstacionTrabajo"] = condicion.idEstacionTrabajo,
                ["idEmpleado"] = condicion.idEmpleado,
                ["empleado"] = empleado.nombres,
                ["idReparacion"] = condicion.idReparacion,
                ["idEstado"] = condicion.idEstado,
                ["idImagenRecursoFirma"] = condicion.idImagenRecursoFirma,
                ["idImagenRecursoFotos"] = condicion.idImagenRecursoFotos,
                ["ubicacionIdEntrega"] = condicion.ubicacionIdEntrega,
                ["idUsuario"] = condicion.idUsuario,
                ["usuario"] = usuario.Nombre,
                ["movimiento"] = condicion.movimiento,
                ["disponible"] = condicion.disponible,
                ["cargado"] = condicion.cargado,
                ["serie"] = condicion.serie,
                ["numero"] = condicion.numero,
                ["inspecVeriOrden"] = condicion.inspecVeriOrden,
                ["observaciones"] = condicion.observaciones,
                ["fecha"] = condicion.fecha,
                ["fechaCreacion"] = condicion.fechaCreacion
            };

            JObject detalle = new JObject
            {
                ["idCondicionActivo"] = generador.idCondicionActivo,
                ["galonesRequeridos"] = generador.galonesRequeridos,
                ["galonesGenSet"] = generador.galonesGenSet,
                ["galonesCompletar"] = generador.galonesCompletar,
                ["horometro"] = generador.horometro,
                ["horaEncendida"] = generador.horaEncendida,
                ["horaApagada"] = generador.horaApagada,
                ["dieselEntradaSalida"] = generador.dieselEntradaSalida,
                ["dieselConsumido"] = generador.dieselConsumido,
                ["horasTrabajadas"] = generador.horasTrabajadas,
                ["estExPuertasGolpeadas"] = generador.estExPuertasGolpeadas,
                ["estExPuertasQuebradas"] = generador.estExPuertasQuebradas,
                ["estExPuertasFaltantes"] = generador.estExPuertasFaltantes,
                ["estExPuertasSueltas"] = generador.estExPuertasSueltas,
                ["estExBisagrasQuebradas"] = generador.estExBisagrasQuebradas,
                ["panelGolpes"] = generador.panelGolpes,
                ["panelTornillosFaltantes"] = generador.panelTornillosFaltantes,
                ["panelOtros"] = generador.panelOtros,
                ["soporteGolpes"] = generador.soporteGolpes,
                ["soporteTornillosFaltantes"] = generador.soporteTornillosFaltantes,
                ["soporteMarcoQuebrado"] = generador.soporteMarcoQuebrado,
                ["soporteMarcoFlojo"] = generador.soporteMarcoFlojo,
                ["soporteBisagrasQuebradas"] = generador.soporteBisagrasQuebradas,
                ["soporteSoldaduraEstado"] = generador.soporteSoldaduraEstado,
                ["revIntCablesQuemados"] = generador.revIntCablesQuemados,
                ["revIntCablesSueltos"] = generador.revIntCablesSueltos,
                ["revIntReparacionesImpropias"] = generador.revIntReparacionesImpropias,
                ["tanqueAgujeros"] = generador.tanqueAgujeros,
                ["tanqueSoporteDanado"] = generador.tanqueSoporteDanado,
                ["tanqueMedidorDiesel"] = generador.tanqueMedidorDiesel,
                ["tanqueCodoQuebrado"] = generador.tanqueCodoQuebrado,
                ["tanqueTapon"] = generador.tanqueTapon,
                ["tanqueTuberia"] = generador.tanqueTuberia,
                ["pFaltMedidorAceite"] = generador.pFaltMedidorAceite,
                ["pFaltTapaAceite"] = generador.pFaltTapaAceite,
                ["pFaltTaponRadiador"] = generador.pFaltTaponRadiador,
            };

            response.Add("condicionDetalle", detalle);
            return response;
        }

        public async Task<JObject> setResponseGeneradorTecnica(condicionTecnicaGenSet generadorTecnica, condicionActivos condicion)
        {
            var empleado = await _unitOfWork.empleadosRepository.GetByID(condicion.idEmpleado);
            var usuario = await _unitOfWork.UsuariosRepository.GetByID(condicion.idUsuario);
            var activo = await _unitOfWork.activoOperacionesRepository.GetByID(condicion.idActivo);
            

            JObject response = new JObject
            {
                ["id"] = condicion.id,
                ["tipoCondicion"] = condicion.tipoCondicion,
                ["idActivo"] = condicion.idActivo,
                ["codigo"] = activo.codigo,
                ["activo"] = activo.descripcion,               
                ["idEstacionTrabajo"] = condicion.idEstacionTrabajo,
                ["idEmpleado"] = condicion.idEmpleado,
                ["empleado"] = empleado.nombres,
                ["idReparacion"] = condicion.idReparacion,
                ["idEstado"] = condicion.idEstado,
                ["idImagenRecursoFirma"] = condicion.idImagenRecursoFirma,
                ["idImagenRecursoFotos"] = condicion.idImagenRecursoFotos,
                ["ubicacionIdEntrega"] = condicion.ubicacionIdEntrega,
                ["idUsuario"] = condicion.idUsuario,
                ["usuario"] = usuario.Nombre,
                ["movimiento"] = condicion.movimiento,
                ["disponible"] = condicion.disponible,
                ["cargado"] = condicion.cargado,
                ["serie"] = condicion.serie,
                ["numero"] = condicion.numero,
                ["inspecVeriOrden"] = condicion.inspecVeriOrden,
                ["observaciones"] = condicion.observaciones,
                ["fecha"] = condicion.fecha,
                ["fechaCreacion"] = condicion.fechaCreacion
            };

            JObject detalle = new JObject
            {
                ["idActivo"] = generadorTecnica.idCondicionActivo,
                ["bateriaNivelAcido"] = generadorTecnica.bateriaNivelAcido,
                ["bateriaArnes"] = generadorTecnica.bateriaArnes,
                ["bateriaTerminales"] = generadorTecnica.bateriaTerminales,
                ["bateriaGolpes"] = generadorTecnica.bateriaGolpes,
                ["bateriaCarga"] = generadorTecnica.bateriaCarga,
                ["combustibleDiesel"] = generadorTecnica.combustibleDiesel,
                ["combustibleAgua"] = generadorTecnica.combustibleAgua,
                ["combustibleAceite"] = generadorTecnica.combustibleAceite,
                ["combustibleFugas"] = generadorTecnica.combustibleFugas,
                ["filtroAceite"] = generadorTecnica.filtroAceite,
                ["filtroDiesel"] = generadorTecnica.filtroDiesel,
                ["bombaAguaEstado"] = generadorTecnica.bombaAguaEstado,
                ["escapeAgujeros"] = generadorTecnica.escapeAgujeros,
                ["escapeDañado"] = generadorTecnica.escapeDañado,
                ["cojinetesEstado"] = generadorTecnica.cojinetesEstado,
                ["fajaAlternador"] = generadorTecnica.fajaAlternador,
                ["enfriamientoAire"] = generadorTecnica.enfriamientoAire,
                ["enfriamientoAgua"] = generadorTecnica.enfriamientoAgua,
                ["cantidadGeneradaVolts"] = generadorTecnica.cantidadGeneradaVolts,
                
            };

            response.Add("condicionDetalle", detalle);
            return response;
        }

        public condicionLlantaDto getLlantaCabezal(condicionCabezal cabezal, int index)
        {
            string[] llantaSplit = new string[] {"","" };

            switch (index)
            {
                case 1:
                    llantaSplit = cabezal.llanta1.Split(';');
                    break;
                case 2:
                    llantaSplit = cabezal.llanta2.Split(';');
                    break;
                case 3:
                    llantaSplit = cabezal.llanta3.Split(';');
                    break;
                case 4:
                    llantaSplit = cabezal.llanta4.Split(';');
                    break;
                case 5:
                    llantaSplit = cabezal.llanta5.Split(';');
                    break;
                case 6:
                    llantaSplit = cabezal.llanta6.Split(';');
                    break;
                case 7:
                    llantaSplit = cabezal.llanta7.Split(';');
                    break;
                case 8:
                    llantaSplit = cabezal.llanta8.Split(';');
                    break;
                case 9:
                    llantaSplit = cabezal.llanta9.Split(';');
                    break;
                case 10:
                    llantaSplit = cabezal.llanta10.Split(';');
                    break;
                case 11:
                    llantaSplit = cabezal.llantaR.Split(';');
                    break;
                case 12:
                    llantaSplit = cabezal.llantaR2.Split(';');
                    break;
            }
            
            condicionLlantaDto llanta = new condicionLlantaDto {
                id = index,
                codigo = llantaSplit[0],
                marca = llantaSplit[1],
                profundidadIzq = llantaSplit[2],
                profundidadCto = llantaSplit[3],
                profundidadDer = llantaSplit[4],
                psi = llantaSplit[5],
                estado = llantaSplit[6],
                observaciones = llantaSplit[7]
            };

            return llanta;
        }

        public condicionLlantaDto getLlantaEquipo(condicionEquipo equipo, int index)
        {
            string[] llantaSplit = new string[] { "", "" };

            switch (index)
            {
                case 1:
                    llantaSplit = equipo.llanta1.Split(';');
                    break;
                case 2:
                    llantaSplit = equipo.llanta2.Split(';');
                    break;
                case 3:
                    llantaSplit = equipo.llanta3.Split(';');
                    break;
                case 4:
                    llantaSplit = equipo.llanta4.Split(';');
                    break;
                case 5:
                    llantaSplit = equipo.llanta5.Split(';');
                    break;
                case 6:
                    llantaSplit = equipo.llanta6.Split(';');
                    break;
                case 7:
                    llantaSplit = equipo.llanta7.Split(';');
                    break;
                case 8:
                    llantaSplit = equipo.llanta8.Split(';');
                    break;
                case 9:
                    llantaSplit = equipo.llanta9.Split(';');
                    break;
                case 10:
                    llantaSplit = equipo.llanta10.Split(';');
                    break;
                case 11:
                    llantaSplit = equipo.llanta11.Split(';');
                    break;
                case 12:
                    llantaSplit = equipo.llanta12.Split(';');
                    break;
                case 13:
                    llantaSplit = equipo.llantaR.Split(';');
                    break;
                case 14:
                    llantaSplit = equipo.llantaR2.Split(';');
                    break;
            }

            condicionLlantaDto llanta = new condicionLlantaDto
            {
                id = index,
                codigo = llantaSplit[0],
                marca = llantaSplit[1],
                profundidadIzq = llantaSplit[2],
                profundidadCto = llantaSplit[3],
                profundidadDer = llantaSplit[4],
                psi = llantaSplit[5],
                estado = llantaSplit[6],
                observaciones = llantaSplit[7]
            };

            return llanta;
        }

        public condicionLlantaDto getLlantaFurgon(condicionFurgon furgon, int index)
        {
            string[] llantaSplit = new string[] { "", "" };

            switch (index)
            {
                case 1:
                    llantaSplit = furgon.llanta1.Split(';');
                    break;
                case 2:
                    llantaSplit = furgon.llanta2.Split(';');
                    break;
                case 3:
                    llantaSplit = furgon.llanta3.Split(';');
                    break;
                case 4:
                    llantaSplit = furgon.llanta4.Split(';');
                    break;
                case 5:
                    llantaSplit = furgon.llanta5.Split(';');
                    break;
                case 6:
                    llantaSplit = furgon.llanta6.Split(';');
                    break;
                case 7:
                    llantaSplit = furgon.llanta7.Split(';');
                    break;
                case 8:
                    llantaSplit = furgon.llanta8.Split(';');
                    break;
                case 9:
                    llantaSplit = furgon.llanta9.Split(';');
                    break;
                case 10:
                    llantaSplit = furgon.llanta10.Split(';');
                    break;
                case 11:
                    llantaSplit = furgon.llanta11.Split(';');
                    break;
                case 12:
                    llantaSplit = furgon.llantaR.Split(';');
                    break;
                case 13:
                    llantaSplit = furgon.llantaR2.Split(';');
                    break;
            }

            condicionLlantaDto llanta = new condicionLlantaDto
            {
                id = index,
                codigo = llantaSplit[0],
                marca = llantaSplit[1],
                profundidadIzq = llantaSplit[2],
                profundidadCto = llantaSplit[3],
                profundidadDer = llantaSplit[4],
                psi = llantaSplit[5],
                estado = llantaSplit[6],
                observaciones = llantaSplit[7]
            };

            return llanta;
        }

        public condicionActivos getUltimaCondicion(string codigo)
        {            
            var condiciones = _unitOfWork.condicionActivosRepository.GetAllIncludes();
            condiciones = condiciones.Where(e=>e.activoOperacion.codigo.ToLower().Equals(codigo.ToLower()));
            condiciones = condiciones.OrderByDescending(e => e.fechaCreacion).Take(1);
            return condiciones.FirstOrDefault();
        }

        public async Task<condicionActivos> insert(condicionActivos condicionActivo)
        {
            //VALIDA QUE EL ACTIVO SE ENCUENTRA EN EL INVENTARIO DE LA ESTACION DE TRABAJO
            var movActual = await _unitOfWork.activoMovimientosActualRepository.GetByID(condicionActivo.idActivo);
            if (movActual.idEstacionTrabajo != condicionActivo.idEstacionTrabajo)
                throw new AguilaException("No es posible Utilizar este equipo/vehiculo, no ha ingresado a su localidad.", 404);

            //VALIDA QUE NO SE PUEDA CREAR CONDICIONES CON FECHAS MAYORES AL DIA ACTUAL
            if (condicionActivo.fecha > DateTime.Now)
            {
                if ((Convert.ToDateTime(condicionActivo.fecha).Date - Convert.ToDateTime(DateTime.Now).Date).Days >= 1)
                {
                    throw new AguilaException("No es posible registrar el documento con fecha mayor al dia de hoy", 404);
                }
                if ((Convert.ToDateTime(condicionActivo.fecha).Date - Convert.ToDateTime(DateTime.Now).Date).Days == 0)
                {
                    condicionActivo.fecha = DateTime.Now;
                }
            }

            //VALIDACION DE ESTADOS DE CONDICION
            string xMsg = "";
            switch (condicionActivo.movimiento.ToUpper())
            {
                case "INGRESO":
                    xMsg = await _activoMovimientosService.validarMovimiento(condicionActivo.idActivo, ControlActivosEventos.CondicionIngreso);
                    break;

                case "SALIDA":
                    xMsg = await _activoMovimientosService.validarMovimiento(condicionActivo.idActivo, ControlActivosEventos.CondicionSalida);
                    break;
            }

            if (!string.IsNullOrEmpty(xMsg))
                throw new AguilaException(xMsg, 404);           

            var xNumero = siguienteNumero(condicionActivo.tipoCondicion, condicionActivo.idEstacionTrabajo, condicionActivo.serie, condicionActivo.fecha.Year);
            condicionActivo.numero = xNumero;
            condicionActivo.id = 0;
            condicionActivo.fechaCreacion = DateTime.Now;

            //Guardamos los  recursos de imagen
            if (condicionActivo.ImagenFirmaPiloto  != null)
            {
                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(condicionActivo.ImagenFirmaPiloto, "condicionActivos",nameof(condicionActivo.ImagenFirmaPiloto));

                if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                    condicionActivo.idImagenRecursoFirma = imgRecurso.Id;

                condicionActivo.ImagenFirmaPiloto = null;
            }

            if (condicionActivo.Fotos != null)
            {
                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(condicionActivo.Fotos , "condicionActivos",nameof(condicionActivo.Fotos));

                if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                    condicionActivo.idImagenRecursoFotos  = imgRecurso.Id;

                condicionActivo.Fotos = null;
            }
            //  Fin de recurso de Imagen        


            //condicionActivos
            //if (condicionActivo.tipoCondicion== "CABEZAL")
                //SI SE PUEDE

            //Inserta condicion activos
            await _unitOfWork.condicionActivosRepository.Add(condicionActivo);
            await _unitOfWork.SaveChangeAsync();

            //Inicio Inserta un movimiento o evento para el activo
            var xEvento = condicionActivo.movimiento.ToUpper().Trim() == "INGRESO" ? ControlActivosEventos.CondicionIngreso : ControlActivosEventos.CondicionSalida;

            switch (condicionActivo.tipoCondicion.Trim().ToLower())
            {
                case "generador":
                    xEvento = ControlActivosEventos.CondicionGeneradorEstructura;
                    break;
                case "tecnica":
                    xEvento = ControlActivosEventos.CondicionGeneradorTecnica;
                    break;
            }

            var movimiento = await crearActivoMovimento(condicionActivo);
            await _activoMovimientosService.InsertActivoMovimiento(movimiento, xEvento);
            //Fin de creacion de movimiento

            return condicionActivo;
        }

        public long siguienteNumero(string tipoCondicion, int idEstacionTrabajo, string serie, int anio)
        {
            long siguiente = 0;
            var xNumero = _unitOfWork.condicionActivosRepository.GetAll()
                .Where(c => c.tipoCondicion.ToUpper().Trim() == tipoCondicion.ToUpper().Trim()
                && c.idEstacionTrabajo == idEstacionTrabajo
                && c.serie.ToUpper().Trim() == serie.ToUpper().Trim()
                && c.fecha.Year == anio);

            if (xNumero.LongCount() < 1)        
                return (DateTime.Now.Year - 2000) * 10000 + 1;

            siguiente = xNumero.Max(e => e.numero);
                                 
            return siguiente + 1;
        }

    }
}
