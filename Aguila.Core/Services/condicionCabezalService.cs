using Aguila.Core.Interfaces.Services;
using Aguila.Core.Entities;
using Aguila.Core.CustomEntities;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Aguila.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Aguila.Core.DTOs;
using Aguila.Core.Interfaces;

namespace Aguila.Core.Services
{
    public class condicionCabezalService : IcondicionCabezalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IcondicionActivosService _condicionActivosService;
        private readonly IAguilaMap _aguilaMap;
        private readonly IImagenesRecursosService _imagenesRecursosService;
        private readonly IeventosControlEquipoService _eventosControlEquipoService;
        private readonly IUsuariosService _usuariosService;


        public condicionCabezalService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,
                                       IcondicionActivosService condicionActivosService,
                                       IAguilaMap aguilaMap,
                                       IImagenesRecursosService imagenesRecursosService,
                                       IeventosControlEquipoService eventosControlEquipoService,
                                       IUsuariosService usuariosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _condicionActivosService = condicionActivosService;
            _aguilaMap = aguilaMap;
            _imagenesRecursosService = imagenesRecursosService;
            _eventosControlEquipoService = eventosControlEquipoService;
            _usuariosService = usuariosService;
        }

        public async Task<PagedList<condicionCabezal>> GetCondicionCabezal(condicionCabezalQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            //Se valida que vengan los filtros de fechas
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

            var condicionCabezal = _unitOfWork.condicionCabezalRepository.GetAllIncludes();

            //Filtrado por fechas
            if (!filter.ignorarFechas)
                condicionCabezal = condicionCabezal.Where(e => e.condicionActivo.fecha >= filter.fechaInicio && e.condicionActivo.fecha < filter.fechaFin.Value.AddDays(1));

            if (filter.idCondicionActivo != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.idCondicionActivo == filter.idCondicionActivo);
            }

            if (filter.idEstacionTrabajo != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.condicionActivo.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            if (filter.movimiento != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.condicionActivo.movimiento.ToLower().Contains(filter.movimiento.ToLower()));
            }

            if (filter.disponible != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.condicionActivo.disponible == filter.disponible);
            }

            if (filter.cargado != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.condicionActivo.cargado == filter.cargado);
            }

            if (filter.inspecVeriOrden != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.condicionActivo.inspecVeriOrden == filter.inspecVeriOrden);
            }

            if (filter.windShield != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.windShield.ToLower().Contains(filter.windShield.ToLower()));
            }

            if (filter.plumillas != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.plumillas.ToLower().Contains(filter.plumillas.ToLower()));
            }

            if (filter.viscera != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.viscera.ToLower().Contains(filter.viscera.ToLower()));
            }

            if (filter.rompeVientos != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.rompeVientos.ToLower().Contains(filter.rompeVientos.ToLower()));
            }

            if (filter.persiana != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.persiana.ToLower().Contains(filter.persiana.ToLower()));
            }

            if (filter.bumper != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.bumper.ToLower().Contains(filter.bumper.ToLower()));
            }

            if (filter.capo != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.capo.ToLower().Contains(filter.capo.ToLower()));
            }

            if (filter.retrovisor != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.retrovisor.ToLower().Contains(filter.retrovisor.ToLower()));
            }

            if (filter.ojoBuey != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.ojoBuey.ToLower().Contains(filter.ojoBuey.ToLower()));
            }

            if (filter.pataGallo != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.pataGallo.ToLower().Contains(filter.pataGallo.ToLower()));
            }

            if (filter.portaLlanta != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.portaLlanta.ToLower().Contains(filter.portaLlanta.ToLower()));
            }

            if (filter.spoilers != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.spoilers.ToLower().Contains(filter.spoilers.ToLower()));
            }

            if (filter.salpicadera != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.salpicadera.ToLower().Contains(filter.salpicadera.ToLower()));
            }

            if (filter.guardaFango != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.guardaFango.ToLower().Contains(filter.guardaFango.ToLower()));
            }

            if (filter.taponCombustible != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.taponCombustible.ToLower().Contains(filter.taponCombustible.ToLower()));
            }

            if (filter.baterias != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.baterias.ToLower().Contains(filter.baterias.ToLower()));
            }

            if (filter.lucesDelanteras != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.lucesDelanteras.ToLower().Contains(filter.lucesDelanteras.ToLower()));
            }

            if (filter.lucesTraseras != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.lucesTraseras.ToLower().Contains(filter.lucesTraseras.ToLower()));
            }

            if (filter.pintura != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.pintura.ToLower().Contains(filter.pintura.ToLower()));
            }

            if (filter.llanta1 != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.llanta1.ToLower().Contains(filter.llanta1.ToLower()));
            }

            if (filter.llanta2 != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.llanta2.ToLower().Contains(filter.llanta2.ToLower()));
            }

            if (filter.llanta3 != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.llanta3.ToLower().Contains(filter.llanta3.ToLower()));
            }

            if (filter.llanta4 != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.llanta4.ToLower().Contains(filter.llanta4.ToLower()));
            }

            if (filter.llanta5 != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.llanta5.ToLower().Contains(filter.llanta5.ToLower()));
            }

            if (filter.llanta6 != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.llanta6.ToLower().Contains(filter.llanta6.ToLower()));
            }

            if (filter.llanta7 != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.llanta7.ToLower().Contains(filter.llanta7.ToLower()));
            }

            if (filter.llanta8 != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.llanta8.ToLower().Contains(filter.llanta8.ToLower()));
            }

            if (filter.llanta9 != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.llanta9.ToLower().Contains(filter.llanta9.ToLower()));
            }

            if (filter.llanta10 != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.llanta10.ToLower().Contains(filter.llanta10.ToLower()));
            }

            if (filter.llantaR != null)
            {
                condicionCabezal = condicionCabezal.Where(e => e.llantaR.ToLower().Contains(filter.llantaR.ToLower()));
            }

            var pagedCondicionCabezal = PagedList<condicionCabezal>.create(condicionCabezal, filter.PageNumber, filter.PageSize);

            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedCondicionCabezal.Select(e => e.condicionActivo.ImagenFirmaPiloto).ToList());
            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedCondicionCabezal.Select(e => e.condicionActivo.Fotos).ToList());
            return pagedCondicionCabezal;
        }

        public async  Task<condicionCabezal> GetCondicionCabezal(long idCondicion)
        {
            var condicionCabezal = _unitOfWork.condicionCabezalRepository.GetAllIncludes().Where(c => c.idCondicionActivo == idCondicion).FirstOrDefault();

            //Manejo de imagenes
            if (condicionCabezal != null && condicionCabezal.condicionActivo.idImagenRecursoFirma  != null && condicionCabezal.condicionActivo.idImagenRecursoFirma != Guid.Empty )
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(condicionCabezal.condicionActivo.idImagenRecursoFirma ?? Guid.Empty);
                condicionCabezal.condicionActivo.ImagenFirmaPiloto  = imgRecurso;
            }

            if (condicionCabezal != null && condicionCabezal.condicionActivo.idImagenRecursoFotos  != null && condicionCabezal.condicionActivo.idImagenRecursoFotos != Guid.Empty )
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(condicionCabezal.condicionActivo.idImagenRecursoFotos ?? Guid.Empty);
                condicionCabezal.condicionActivo.Fotos  = imgRecurso;
            }
            //Fin Imagenes

            return condicionCabezal;
        }

        public async Task<condicionCabezalDto> InsertCondicionCabezal(condicionCabezalDto condicionCabezalDto)
        {
            if (!_unitOfWork.vehiculosRepository.esTipo("CA", condicionCabezalDto.condicionActivo.idActivo))
                throw new AguilaException("El activo no es un cabezal", 404);

            //TODO: convencion de serie siempre A
            condicionCabezalDto.condicionActivo.tipoCondicion = "CABEZAL";
            condicionCabezalDto.condicionActivo.serie = "A";

            var xCondicionCabezal = _aguilaMap.Map<condicionCabezal>(condicionCabezalDto);

            var xNumeroLlantas = condicionCabezalDto.condicionesLlantas.Count;
            var xNumeroLlantaRepuesto = condicionCabezalDto.condicionesLlantasRepuesto.Count;

            xCondicionCabezal.llanta1 = xNumeroLlantas >= 1 ? condicionCabezalDto.condicionesLlantas.ElementAt(0).ToString() : null;
            xCondicionCabezal.llanta2 = xNumeroLlantas >= 2 ? condicionCabezalDto.condicionesLlantas.ElementAt(1).ToString() : null;
            xCondicionCabezal.llanta3 = xNumeroLlantas >= 3 ? condicionCabezalDto.condicionesLlantas.ElementAt(2).ToString() : null;
            xCondicionCabezal.llanta4 = xNumeroLlantas >= 4 ? condicionCabezalDto.condicionesLlantas.ElementAt(3).ToString() : null;
            xCondicionCabezal.llanta5 = xNumeroLlantas >= 5 ? condicionCabezalDto.condicionesLlantas.ElementAt(4).ToString() : null;
            xCondicionCabezal.llanta6 = xNumeroLlantas >= 6 ? condicionCabezalDto.condicionesLlantas.ElementAt(5).ToString() : null;
            xCondicionCabezal.llanta7 = xNumeroLlantas >= 7 ? condicionCabezalDto.condicionesLlantas.ElementAt(6).ToString() : null;
            xCondicionCabezal.llanta8 = xNumeroLlantas >= 8 ? condicionCabezalDto.condicionesLlantas.ElementAt(7).ToString() : null;
            xCondicionCabezal.llanta9 = xNumeroLlantas >= 9 ? condicionCabezalDto.condicionesLlantas.ElementAt(8).ToString() : null;
            xCondicionCabezal.llanta10 = xNumeroLlantas >= 10 ? condicionCabezalDto.condicionesLlantas.ElementAt(9).ToString() : null;

            xCondicionCabezal.llantaR = xNumeroLlantaRepuesto >= 1 ? condicionCabezalDto.condicionesLlantasRepuesto.ElementAt(0).ToString() : null;
            xCondicionCabezal.llantaR2 = xNumeroLlantaRepuesto >= 2 ? condicionCabezalDto.condicionesLlantasRepuesto.ElementAt(1).ToString() : null;

            var xCondicionActivo = xCondicionCabezal.condicionActivo;           
            

            _unitOfWork.BeginTransaction();


            try
            {
                xCondicionActivo = await _condicionActivosService.insert(xCondicionActivo);

                if (xCondicionActivo.id == 0)
                    throw new AguilaException("No se pudo insertar la condicion", 400);

                await _unitOfWork.condicionCabezalRepository.Add(xCondicionCabezal);
                await _unitOfWork.SaveChangeAsync();

                _unitOfWork.CommitTransaction();

                //VERIFICA SI HAY QUE CREAR EVENTOS POR OBSERVACIONES DE LLANTAS            
                eventosControlEquipoDto evento = new eventosControlEquipoDto();

                foreach (var llanta in condicionCabezalDto.condicionesLlantas)
                {
                    if (!string.IsNullOrEmpty(llanta.observaciones))
                    {
                        evento.idUsuarioCreacion = condicionCabezalDto.condicionActivo.idUsuario;
                        evento.idEstacionTrabajo = condicionCabezalDto.condicionActivo.idEstacionTrabajo;
                        evento.idActivo = condicionCabezalDto.condicionActivo.idActivo;
                        evento.descripcionEvento = "Inspeccion " + xCondicionActivo.numero + ", Llanta " + llanta.codigo;
                        evento.bitacoraObservaciones = llanta.observaciones;
                        evento.fechaCreacion = DateTime.Now;

                        var nombreUsuario = "";
                        var usuario = await _usuariosService.GetUsuario(evento.idUsuarioCreacion);
                        if (usuario != null) nombreUsuario = usuario.Nombre;

                        await _eventosControlEquipoService.InsertEventoControl(evento, nombreUsuario);
                    }

                }

                //VERIFICA SI HAY QUE CREAR EVENTOS POR OBSERVACIONES EN LLANTAS DE REPUESTO
                foreach (var llanta in condicionCabezalDto.condicionesLlantasRepuesto)
                {
                    if (!string.IsNullOrEmpty(llanta.observaciones))
                    {
                        evento.idUsuarioCreacion = condicionCabezalDto.condicionActivo.idUsuario;
                        evento.idEstacionTrabajo = condicionCabezalDto.condicionActivo.idEstacionTrabajo;
                        evento.idActivo = condicionCabezalDto.condicionActivo.idActivo;
                        evento.descripcionEvento = "Inspeccion " + xCondicionActivo.numero + ", Llanta Repuesto " + llanta.codigo;
                        evento.bitacoraObservaciones = llanta.observaciones;
                        evento.fechaCreacion = DateTime.Now;

                        var nombreUsuario = "";
                        var usuario = await _usuariosService.GetUsuario(evento.idUsuarioCreacion);
                        if (usuario != null) nombreUsuario = usuario.Nombre;

                        await _eventosControlEquipoService.InsertEventoControl(evento, nombreUsuario);
                    }

                }


                //llenarCondicionLlantas(xCondicionCabezal,ref condicionCabezalDto);
               // _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                _unitOfWork.RollbackTransaction();

                throw new AguilaException(e.Message, 400);
            }

           

            var xCondicionActivoDto = _aguilaMap.Map<condicionActivosDto>(xCondicionActivo);

            condicionCabezalDto.idCondicionActivo = xCondicionActivo.id;
            condicionCabezalDto.condicionActivo = xCondicionActivoDto;
            return condicionCabezalDto;
        }

        public async Task<bool> UpdateCondicionCabezal(condicionCabezal condicionCabezal)
        {
            var currentCondicionCabezal = await _unitOfWork.condicionCabezalRepository.GetByID(condicionCabezal.idCondicionActivo);
            if (currentCondicionCabezal == null)
            {
                throw new AguilaException("Condicion no existente...");
            }

            currentCondicionCabezal.idCondicionActivo = condicionCabezal.idCondicionActivo;     
            currentCondicionCabezal.windShield = condicionCabezal.windShield;
            currentCondicionCabezal.plumillas = condicionCabezal.plumillas;
            currentCondicionCabezal.viscera = condicionCabezal.viscera;
            currentCondicionCabezal.rompeVientos = condicionCabezal.rompeVientos;
            currentCondicionCabezal.persiana = condicionCabezal.persiana;
            currentCondicionCabezal.bumper = condicionCabezal.bumper;
            currentCondicionCabezal.capo = condicionCabezal.capo;
            currentCondicionCabezal.retrovisor = condicionCabezal.retrovisor;
            currentCondicionCabezal.ojoBuey = condicionCabezal.ojoBuey;
            currentCondicionCabezal.pataGallo = condicionCabezal.pataGallo;
            currentCondicionCabezal.portaLlanta = condicionCabezal.portaLlanta;
            currentCondicionCabezal.spoilers = condicionCabezal.spoilers;
            currentCondicionCabezal.salpicadera = condicionCabezal.salpicadera;
            currentCondicionCabezal.guardaFango = condicionCabezal.guardaFango;
            currentCondicionCabezal.taponCombustible = condicionCabezal.taponCombustible;
            currentCondicionCabezal.baterias = condicionCabezal.baterias;
            currentCondicionCabezal.lucesDelanteras = condicionCabezal.lucesDelanteras;
            currentCondicionCabezal.lucesTraseras = condicionCabezal.lucesTraseras;
            currentCondicionCabezal.pintura = condicionCabezal.pintura;
            //currentCondicionCabezal.inspecVeriOrden = condicionCabezal.inspecVeriOrden;
            currentCondicionCabezal.llanta1 = condicionCabezal.llanta1;
            currentCondicionCabezal.llanta2 = condicionCabezal.llanta2;
            currentCondicionCabezal.llanta3 = condicionCabezal.llanta3;
            currentCondicionCabezal.llanta4 = condicionCabezal.llanta4;
            currentCondicionCabezal.llanta5 = condicionCabezal.llanta5;
            currentCondicionCabezal.llanta6 = condicionCabezal.llanta6;
            currentCondicionCabezal.llanta7 = condicionCabezal.llanta7;
            currentCondicionCabezal.llanta8 = condicionCabezal.llanta8;
            currentCondicionCabezal.llanta9 = condicionCabezal.llanta9;
            currentCondicionCabezal.llanta10 = condicionCabezal.llanta10;
            currentCondicionCabezal.llantaR = condicionCabezal.llantaR;

            var currentCondicionActivo = await _unitOfWork.condicionActivosRepository.GetByID(condicionCabezal.idCondicionActivo);
            if (currentCondicionActivo == null)
                throw new AguilaException("Condicion No Existente...!");

            // Guardamos el Recurso de Imagen
            if (condicionCabezal.condicionActivo.Fotos != null)
            {
                //Obligatorio enviar el id de imagen recurso guardado en la tabla
                condicionCabezal.condicionActivo.Fotos.Id = currentCondicionActivo.idImagenRecursoFotos ?? Guid.Empty;

                var imgRecursoOp = await _imagenesRecursosService.GuardarImagenRecurso(condicionCabezal.condicionActivo.Fotos, "condicionCabezal", nameof(condicionCabezal.condicionActivo.Fotos));

                if (currentCondicionActivo.idImagenRecursoFotos == null || currentCondicionActivo.idImagenRecursoFotos == Guid.Empty)
                {
                    if (imgRecursoOp.Id != null && imgRecursoOp.Id != Guid.Empty)
                        currentCondicionActivo.idImagenRecursoFotos = imgRecursoOp.Id;
                }
            }
            //  Fin de recurso de Imagen   

            _unitOfWork.BeginTransaction();
            try
            {
                _unitOfWork.condicionCabezalRepository.Update(currentCondicionCabezal);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
            }

            return true;
        }

        public async Task<bool> DeleteCondicionCabezal(int id)
        {
            var currentCondicionCabezal = await _unitOfWork.condicionCabezalRepository.GetByID(id);
            if (currentCondicionCabezal == null)
            {
                throw new AguilaException("Condicion no existente");
            }

            await _unitOfWork.condicionCabezalRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        public async Task<condicionActivos> prepararCondicionActivo(condicionActivosDto condicion)
        {
            var filter = new condicionActivosQueryFilter
            {
                idEstacionTrabajo = condicion.idEstacionTrabajo,
                serie = condicion.serie,
                tipoCondicion = condicion.tipoCondicion,
                anio = condicion.fecha.Year
            };

            var condicionActivos = new condicionActivos
            {
                tipoCondicion = condicion.tipoCondicion,
                idActivo = condicion.idActivo,
                idEstacionTrabajo = condicion.idEstacionTrabajo,
                idEmpleado = condicion.idEmpleado,
                idReparacion = condicion.idReparacion,
                idImagenRecursoFirma = condicion.idImagenRecursoFirma,
                idImagenRecursoFotos = condicion.idImagenRecursoFotos,
                ubicacionIdEntrega = condicion.ubicacionIdEntrega,
                idUsuario = condicion.idUsuario,
                movimiento = condicion.movimiento,
                disponible = condicion.disponible,
                cargado = condicion.cargado,
                serie = condicion.serie,
                numero = condicion.numero,
                observaciones = condicion.observaciones,
                fecha = condicion.fecha,
                fechaCreacion = DateTime.Now
            };



            //await _unitOfWork.condicionActivosRepository.Add(condicionActivos);
            //await _condicionActivosService.InsertCondicion(condicion);
            //await _unitOfWork.SaveChangeAsync();
            //condicionActivos.id = condicion.id;

            return condicionActivos;
           
        }

        public string setLlanta(ICollection<condicionLlantaDto> llantas, int index)
        {
            return llantas.ElementAt(index).marca +","+ llantas.ElementAt(index).codigo + "," + llantas.ElementAt(index).profundidadIzq +
                "," + llantas.ElementAt(index).profundidadCto + "," + llantas.ElementAt(index).profundidadDer + "," + llantas.ElementAt(index).psi +
                "," + llantas.ElementAt(index).estado + "," + llantas.ElementAt(index).observaciones;
        }


        public condicionCabezal ultima(int idActivo)
        {
            var condicionCabezal = _unitOfWork.condicionCabezalRepository.GetUltima(idActivo);
            return condicionCabezal;
        }

        public void llenarCondicionLlantas(condicionCabezal condicionCabezal,ref condicionCabezalDto condicionCabezalDto)
        {
            Func<List<condicionLlantaDto>, int, string, bool> agregarLlanta = (lista, id, llantaString) => {
                var xLlanta = new condicionLlantaDto(llantaString);
                if (!string.IsNullOrEmpty(xLlanta.codigo))
                {
                    xLlanta.id = id;
                    lista.Add(xLlanta);
                }
                return false;
            };

            var xLlantas = new List<condicionLlantaDto>();

            agregarLlanta(xLlantas, 1, condicionCabezal.llanta1);
            agregarLlanta(xLlantas, 2, condicionCabezal.llanta2);
            agregarLlanta(xLlantas, 3, condicionCabezal.llanta3);
            agregarLlanta(xLlantas, 4, condicionCabezal.llanta4);
            agregarLlanta(xLlantas, 5, condicionCabezal.llanta5);
            agregarLlanta(xLlantas, 6, condicionCabezal.llanta6);
            agregarLlanta(xLlantas, 7, condicionCabezal.llanta7);
            agregarLlanta(xLlantas, 8, condicionCabezal.llanta8);
            agregarLlanta(xLlantas, 9, condicionCabezal.llanta9);
            agregarLlanta(xLlantas, 10, condicionCabezal.llanta10);

            condicionCabezalDto.condicionesLlantas = xLlantas;

            var xLlantasRepuesto = new List<condicionLlantaDto>();

            agregarLlanta(xLlantasRepuesto, 11, condicionCabezal.llantaR);
            agregarLlanta(xLlantasRepuesto, 12, condicionCabezal.llantaR2);

            condicionCabezalDto.condicionesLlantasRepuesto = xLlantasRepuesto;
        }



        //REPORTE DE CONDICIONES DE CABEZALES
        public IEnumerable<condicionActivos> reporteCondicionesVehiculos(reporteCondicionesCabezalesQueryFilter filter, int usuario)
        {
            if (filter.idEmpresa is null)
                throw new AguilaException("Debe Especificar una empresa", 404);

            if (filter.fechaInicio == null || filter.fechaFin == null)
            {
                throw new AguilaException("Debe Especificar una Fecha de Inicio y una Fecha Final para Obtener los datos.", 404);
            }
            else
            {
                var dias = (filter.fechaFin - filter.fechaInicio).Value.Days;
                if (dias > 92) throw new AguilaException("Debe Especificar un Rango de Fechas No Mayor a 91 días.", 404);
            }

            var condicionesQuery = _unitOfWork.condicionActivosRepository.reporteCondicionesVehiculos((int)filter.idEmpresa, usuario).AsQueryable();

            if (filter.idEstado != null)
            {
                condicionesQuery = condicionesQuery.Where(x => x.idEstado == filter.idEstado);
            }

            if (filter.movimiento != null)
            {
                condicionesQuery = condicionesQuery.Where(x => x.movimiento.ToLower().Trim().Equals(filter.movimiento.ToLower().Trim()));
            }

            if (filter.idEstacionTrabajo != null)
            {
                condicionesQuery = condicionesQuery.Where(x => x.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            if (filter.listaIdEstados != null)
            {
                var xEstados = filter.listaIdEstados.Split(",").Select(Int32.Parse).ToList();
                if (xEstados.Count > 0)
                    condicionesQuery = condicionesQuery.Where(x => xEstados.Contains((int)x.idEstado));
            }

            if (filter.codigo != null)
            {
                condicionesQuery = condicionesQuery.Where(x => x.activoOperacion.codigo.ToLower().Trim().StartsWith(filter.codigo.ToLower().Trim()));
            }

            //FILTRADO DE FECHAS

            condicionesQuery = condicionesQuery.Where(x=>x.fecha >= filter.fechaInicio && x.fecha < Convert.ToDateTime(filter.fechaFin).AddDays(1));

            //ordenar por fecha
            condicionesQuery = condicionesQuery.OrderByDescending(x=>x.fecha);

           var condiciones = condicionesQuery.ToList();
            return condiciones;
        }

    }
}
