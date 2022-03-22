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
    public class condicionEquipoService : IcondicionEquipoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IcondicionActivosService _condicionActivosService;
        private readonly IAguilaMap _aguilaMap;
        private readonly IImagenesRecursosService _imagenesRecursosService;
        private readonly IeventosControlEquipoService _eventosControlEquipoService;
        private readonly IUsuariosService _usuariosService;

        public condicionEquipoService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options, IAguilaMap aguilaMap,
                                      IcondicionActivosService condicionActivosService,
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

        public async Task<PagedList<condicionEquipo>> GetCondicionEquipo(condicionEquipoQueryFilter filter)
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

            var condicionEquipo = _unitOfWork.condicionEquipoRepository.GetAllIncludes();

            //Filtrado por fechas
            if (!filter.ignorarFechas)
                condicionEquipo = condicionEquipo.Where(e => e.condicionActivo.fecha >= filter.fechaInicio && e.condicionActivo.fecha < filter.fechaFin.Value.AddDays(1));

            if (filter.idCondicionActivo != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.idCondicionActivo == filter.idCondicionActivo);
            }

            if (filter.idEstacionTrabajo != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.condicionActivo.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            if (filter.movimiento != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.condicionActivo.movimiento.ToLower().Contains(filter.movimiento.ToLower()));
            }

            if (filter.disponible != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.condicionActivo.disponible == filter.disponible);
            }

            if (filter.cargado != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.condicionActivo.cargado == filter.cargado);
            }

            if (filter.inspecVeriOrden != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.condicionActivo.inspecVeriOrden == filter.inspecVeriOrden);
            }

            if (filter.lucesA != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.lucesA == filter.lucesA);
            }
            
            if (filter.lucesB != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.lucesB == filter.lucesB);
            }

            if (filter.lucesC != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.lucesC == filter.lucesC);
            }

            if (filter.lucesD != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.lucesD == filter.lucesD);
            }

            if (filter.lucesE != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.lucesE == filter.lucesE);
            }

            if (filter.lucesF != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.lucesF == filter.lucesF);
            }
            
            if (filter.pi != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.pi == filter.pi);
            }

            if (filter.pd != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.pd == filter.pd);
            }

            if (filter.si != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.si == filter.si);
            }

            if (filter.sd != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.sd == filter.sd);
            }

            if (filter.guardaFangosG != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.guardaFangosG.ToLower().Contains(filter.guardaFangosG.ToLower()));
            }

            if (filter.guardaFangosI != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.guardaFangosI.ToLower().Contains(filter.guardaFangosI.ToLower()));
            }

            if (filter.cintaReflectivaLat != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.cintaReflectivaLat.ToLower().Contains(filter.cintaReflectivaLat.ToLower()));
            }

            if (filter.cintaReflectivaFront != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.cintaReflectivaFront.ToLower().Contains(filter.cintaReflectivaFront.ToLower()));
            }

            if (filter.cintaReflectivaTra != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.cintaReflectivaTra.ToLower().Contains(filter.cintaReflectivaTra.ToLower()));
            }

            if (filter.manitas1 != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.manitas1.ToLower().Contains(filter.manitas1.ToLower()));
            }
            
            if (filter.manitas2 != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.manitas2.ToLower().Contains(filter.manitas2.ToLower()));
            }
            
            if (filter.bumper != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.bumper.ToLower().Contains(filter.bumper.ToLower()));
            }

            if (filter.fricciones != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.fricciones.ToLower().Contains(filter.fricciones.ToLower()));
            }

            if (filter.friccionesLlantas != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.friccionesLlantas.ToLower().Contains(filter.friccionesLlantas.ToLower()));
            }

            if (filter.patas != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.patas.ToLower().Contains(filter.patas.ToLower()));
            }

            if (filter.ganchos != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.ganchos.ToLower().Contains(filter.ganchos.ToLower()));
            }
            
            if (filter.balancines != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.balancines.ToLower().Contains(filter.balancines.ToLower()));
            }

            if (filter.hojasResortes != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.hojasResortes.ToLower().Contains(filter.hojasResortes.ToLower()));
            }

            if (filter.placaPatin != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.placaPatin==filter.placaPatin);
            }

            if (filter.llanta1 != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.llanta1.ToLower().Contains(filter.llanta1.ToLower()));
            }

            if (filter.llanta2 != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.llanta2.ToLower().Contains(filter.llanta2.ToLower()));
            }

            if (filter.llanta3 != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.llanta3.ToLower().Contains(filter.llanta3.ToLower()));
            }

            if (filter.llanta4 != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.llanta4.ToLower().Contains(filter.llanta4.ToLower()));
            }

            if (filter.llanta5 != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.llanta5.ToLower().Contains(filter.llanta5.ToLower()));
            }

            if (filter.llanta6 != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.llanta6.ToLower().Contains(filter.llanta6.ToLower()));
            }

            if (filter.llanta7 != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.llanta7.ToLower().Contains(filter.llanta7.ToLower()));
            }

            if (filter.llanta8 != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.llanta8.ToLower().Contains(filter.llanta8.ToLower()));
            }

            if (filter.llanta9 != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.llanta9.ToLower().Contains(filter.llanta9.ToLower()));
            }

            if (filter.llanta10 != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.llanta10.ToLower().Contains(filter.llanta10.ToLower()));
            }

            if (filter.llanta11 != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.llanta11.ToLower().Contains(filter.llanta11.ToLower()));
            }

            if (filter.llanta12 != null)
            {
                condicionEquipo = condicionEquipo.Where(e => e.llanta12.ToLower().Contains(filter.llanta12.ToLower()));
            }

            var pagedCondicionEquipo = PagedList<condicionEquipo>.create(condicionEquipo, filter.PageNumber, filter.PageSize);

            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedCondicionEquipo.Select(e => e.condicionActivo.ImagenFirmaPiloto).ToList());
            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedCondicionEquipo.Select(e => e.condicionActivo.Fotos).ToList());
            return pagedCondicionEquipo;
        }

        public  async Task<condicionEquipo> GetCondicionEquipo(long idCondicion)
        {
            var condicionEquipo = _unitOfWork.condicionEquipoRepository.GetAllIncludes().Where(c => c.idCondicionActivo == idCondicion).FirstOrDefault();
            //Manejo de imagenes
            if (condicionEquipo != null && condicionEquipo.condicionActivo.idImagenRecursoFirma != null && condicionEquipo.condicionActivo.idImagenRecursoFirma != Guid.Empty)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(condicionEquipo.condicionActivo.idImagenRecursoFirma ?? Guid.Empty);
                condicionEquipo.condicionActivo.ImagenFirmaPiloto = imgRecurso;
            }

            if (condicionEquipo != null && condicionEquipo.condicionActivo.idImagenRecursoFotos != null && condicionEquipo.condicionActivo.idImagenRecursoFotos != Guid.Empty)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(condicionEquipo.condicionActivo.idImagenRecursoFotos ?? Guid.Empty);
                condicionEquipo.condicionActivo.Fotos = imgRecurso;
            }
            //Fin Imagenes
            return condicionEquipo;
        }

        public async Task<condicionEquipoDto> InsertCondicionEquipo(condicionEquipoDto condicionEquipoDto)
        {
            //TODO: convencion de serie siempre A
            condicionEquipoDto.condicionActivo.tipoCondicion = "EQUIPO";
            condicionEquipoDto.condicionActivo.serie = "A";

            var xCondicionEquipo = _aguilaMap.Map<condicionEquipo>(condicionEquipoDto);

            var xNumeroLlantas = condicionEquipoDto.condicionesLlantas.Count;
            var xNumeroLlantaRepuesto = condicionEquipoDto.condicionesLlantasRepuesto.Count;

            xCondicionEquipo.llanta1 = xNumeroLlantas >= 1 ? condicionEquipoDto.condicionesLlantas.ElementAt(0).ToString() : null;
            xCondicionEquipo.llanta2 = xNumeroLlantas >= 2 ? condicionEquipoDto.condicionesLlantas.ElementAt(1).ToString() : null;
            xCondicionEquipo.llanta3 = xNumeroLlantas >= 3 ? condicionEquipoDto.condicionesLlantas.ElementAt(2).ToString() : null;
            xCondicionEquipo.llanta4 = xNumeroLlantas >= 4 ? condicionEquipoDto.condicionesLlantas.ElementAt(3).ToString() : null;
            xCondicionEquipo.llanta5 = xNumeroLlantas >= 5 ? condicionEquipoDto.condicionesLlantas.ElementAt(4).ToString() : null;
            xCondicionEquipo.llanta6 = xNumeroLlantas >= 6 ? condicionEquipoDto.condicionesLlantas.ElementAt(5).ToString() : null;
            xCondicionEquipo.llanta7 = xNumeroLlantas >= 7 ? condicionEquipoDto.condicionesLlantas.ElementAt(6).ToString() : null;
            xCondicionEquipo.llanta8 = xNumeroLlantas >= 8 ? condicionEquipoDto.condicionesLlantas.ElementAt(7).ToString() : null;
            xCondicionEquipo.llanta9 = xNumeroLlantas >= 9 ? condicionEquipoDto.condicionesLlantas.ElementAt(8).ToString() : null;
            xCondicionEquipo.llanta10 = xNumeroLlantas >= 10 ? condicionEquipoDto.condicionesLlantas.ElementAt(9).ToString() : null;
            xCondicionEquipo.llanta11 = xNumeroLlantas >= 11 ? condicionEquipoDto.condicionesLlantas.ElementAt(10).ToString() : null;
            xCondicionEquipo.llanta12 = xNumeroLlantas >= 12 ? condicionEquipoDto.condicionesLlantas.ElementAt(11).ToString() : null;

            xCondicionEquipo.llantaR = xNumeroLlantaRepuesto >= 1 ? condicionEquipoDto.condicionesLlantasRepuesto.ElementAt(0).ToString() : null;
            xCondicionEquipo.llantaR2 = xNumeroLlantaRepuesto >= 2 ? condicionEquipoDto.condicionesLlantasRepuesto.ElementAt(1).ToString() : null;

            var xCondicionActivo = xCondicionEquipo.condicionActivo;

            _unitOfWork.BeginTransaction();

            try
            {
                xCondicionActivo = await _condicionActivosService.insert(xCondicionActivo);

                if (xCondicionActivo.id == 0)
                    return null;

                await _unitOfWork.condicionEquipoRepository.Add(xCondicionEquipo);
                await _unitOfWork.SaveChangeAsync();

                _unitOfWork.CommitTransaction();


                //VERIFICA SI HAY QUE CREAR EVENTOS POR OBSERVACIONES DE LLANTAS            
                eventosControlEquipoDto evento = new eventosControlEquipoDto();

                foreach (var llanta in condicionEquipoDto.condicionesLlantas)
                {
                    if (!string.IsNullOrEmpty(llanta.observaciones))
                    {
                        evento.idUsuarioCreacion = condicionEquipoDto.condicionActivo.idUsuario;
                        evento.idEstacionTrabajo = condicionEquipoDto.condicionActivo.idEstacionTrabajo;
                        evento.idActivo = condicionEquipoDto.condicionActivo.idActivo;
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
                foreach (var llanta in condicionEquipoDto.condicionesLlantasRepuesto)
                {
                    if (!string.IsNullOrEmpty(llanta.observaciones))
                    {
                        evento.idUsuarioCreacion = condicionEquipoDto.condicionActivo.idUsuario;
                        evento.idEstacionTrabajo = condicionEquipoDto.condicionActivo.idEstacionTrabajo;
                        evento.idActivo = condicionEquipoDto.condicionActivo.idActivo;
                        evento.descripcionEvento = "Inspeccion " + xCondicionActivo.numero + ", Llanta Repuesto " + llanta.codigo;
                        evento.bitacoraObservaciones = llanta.observaciones;
                        evento.fechaCreacion = DateTime.Now;

                        var nombreUsuario = "";
                        var usuario = await _usuariosService.GetUsuario(evento.idUsuarioCreacion);
                        if (usuario != null) nombreUsuario = usuario.Nombre;

                        await _eventosControlEquipoService.InsertEventoControl(evento, nombreUsuario);
                    }

                }

            }
            catch (Exception e)
            {

                _unitOfWork.RollbackTransaction();

                throw new AguilaException(e.Message, 400);
            }

           

            var xCondicionActivoDto = _aguilaMap.Map<condicionActivosDto>(xCondicionActivo);

            condicionEquipoDto.idCondicionActivo = xCondicionActivo.id;
            condicionEquipoDto.condicionActivo = xCondicionActivoDto;

            //llenarCondicionLlantas(xCondicionCabezal,ref condicionCabezalDto);

            return condicionEquipoDto;
        }

        public async Task<bool> UpdateCondicionEquipo(condicionEquipo condicionEquipo)
        {
            var currentCondicionEquipo = await _unitOfWork.condicionEquipoRepository.GetByID(condicionEquipo.idCondicionActivo);
            if (currentCondicionEquipo == null)
            {
                throw new AguilaException("Condicion no existente...");
            }

            currentCondicionEquipo.idCondicionActivo = condicionEquipo.idCondicionActivo;
            currentCondicionEquipo.lucesA = condicionEquipo.lucesA;
            currentCondicionEquipo.lucesB = condicionEquipo.lucesB;
            currentCondicionEquipo.lucesC = condicionEquipo.lucesC;
            currentCondicionEquipo.lucesD = condicionEquipo.lucesD;
            currentCondicionEquipo.lucesE = condicionEquipo.lucesE;
            currentCondicionEquipo.lucesF = condicionEquipo.lucesF;
            currentCondicionEquipo.pi = condicionEquipo.pi;
            currentCondicionEquipo.pd = condicionEquipo.pd;
            currentCondicionEquipo.si = condicionEquipo.si;
            currentCondicionEquipo.sd = condicionEquipo.sd;
            currentCondicionEquipo.guardaFangosG = condicionEquipo.guardaFangosG;
            currentCondicionEquipo.guardaFangosI = condicionEquipo.guardaFangosI;
            currentCondicionEquipo.cintaReflectivaFront = condicionEquipo.cintaReflectivaFront;
            currentCondicionEquipo.cintaReflectivaLat = condicionEquipo.cintaReflectivaLat;
            currentCondicionEquipo.cintaReflectivaTra = condicionEquipo.cintaReflectivaTra;
            currentCondicionEquipo.manitas1 = condicionEquipo.manitas1;
            currentCondicionEquipo.manitas2 = condicionEquipo.manitas2;
            currentCondicionEquipo.bumper = condicionEquipo.bumper;
            currentCondicionEquipo.fricciones = condicionEquipo.fricciones;
            currentCondicionEquipo.friccionesLlantas = condicionEquipo.friccionesLlantas;
            currentCondicionEquipo.patas = condicionEquipo.patas;
            currentCondicionEquipo.ganchos = condicionEquipo.ganchos;
            currentCondicionEquipo.balancines = condicionEquipo.balancines;
            currentCondicionEquipo.placaPatin = condicionEquipo.placaPatin;
            currentCondicionEquipo.llanta1 = condicionEquipo.llanta1;
            currentCondicionEquipo.llanta2 = condicionEquipo.llanta2;
            currentCondicionEquipo.llanta3 = condicionEquipo.llanta3;
            currentCondicionEquipo.llanta4 = condicionEquipo.llanta4;
            currentCondicionEquipo.llanta5 = condicionEquipo.llanta5;
            currentCondicionEquipo.llanta6 = condicionEquipo.llanta6;
            currentCondicionEquipo.llanta7 = condicionEquipo.llanta7;
            currentCondicionEquipo.llanta8 = condicionEquipo.llanta8;
            currentCondicionEquipo.llanta9 = condicionEquipo.llanta9;
            currentCondicionEquipo.llanta10 = condicionEquipo.llanta10;
            currentCondicionEquipo.llanta11 = condicionEquipo.llanta11;
            currentCondicionEquipo.llanta12 = condicionEquipo.llanta12;

            var currentCondicionActivo = await _unitOfWork.condicionActivosRepository.GetByID(condicionEquipo.idCondicionActivo);
            if (currentCondicionActivo == null)
                throw new AguilaException("Condicion no existente...!");

            // Guardamos el Recurso de Imagen
            if (condicionEquipo.condicionActivo.Fotos != null)
            {
                //Obligatorio enviar el id de imagen recurso guardado en la tabla
                condicionEquipo.condicionActivo.Fotos.Id = currentCondicionActivo.idImagenRecursoFotos ?? Guid.Empty;

                var imgRecursoOp = await _imagenesRecursosService.GuardarImagenRecurso(condicionEquipo.condicionActivo.Fotos, "condicionEquipo", nameof(condicionEquipo.condicionActivo.Fotos));

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
                _unitOfWork.condicionEquipoRepository.Update(currentCondicionEquipo);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
            }

            return true;
        }

        public async Task<bool> DeleteCondicionEquipo(int id)
        {
            var currentCondicionEquipo = await _unitOfWork.condicionEquipoRepository.GetByID(id);
            if (currentCondicionEquipo == null)
            {
                throw new AguilaException("Condicion no existente");
            }

            await _unitOfWork.condicionEquipoRepository.Delete(id);
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

            await _unitOfWork.condicionActivosRepository.Add(condicionActivos);
            await _unitOfWork.SaveChangeAsync();
            return condicionActivos;
        }

        public condicionEquipo ultima(int idActivo)
        {
            var condicionEquipo = _unitOfWork.condicionEquipoRepository.GetUltima(idActivo);
            return condicionEquipo;
        }

        public void llenarCondicionLlantas(condicionEquipo condicionEquipo, ref condicionEquipoDto condicionEquipoDto)
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

            agregarLlanta(xLlantas, 1, condicionEquipo.llanta1);
            agregarLlanta(xLlantas, 2, condicionEquipo.llanta2);
            agregarLlanta(xLlantas, 3, condicionEquipo.llanta3);
            agregarLlanta(xLlantas, 4, condicionEquipo.llanta4);
            agregarLlanta(xLlantas, 5, condicionEquipo.llanta5);
            agregarLlanta(xLlantas, 6, condicionEquipo.llanta6);
            agregarLlanta(xLlantas, 7, condicionEquipo.llanta7);
            agregarLlanta(xLlantas, 8, condicionEquipo.llanta8);
            agregarLlanta(xLlantas, 9, condicionEquipo.llanta9);
            agregarLlanta(xLlantas, 10, condicionEquipo.llanta10);
            agregarLlanta(xLlantas, 11, condicionEquipo.llanta11);
            agregarLlanta(xLlantas, 12, condicionEquipo.llanta12);

            condicionEquipoDto.condicionesLlantas = xLlantas;

            var xLlantasRepuesto = new List<condicionLlantaDto>();

            agregarLlanta(xLlantasRepuesto, 1, condicionEquipo.llantaR);
            agregarLlanta(xLlantasRepuesto, 2, condicionEquipo.llantaR2);

            condicionEquipoDto.condicionesLlantasRepuesto = xLlantasRepuesto;
        }

        //REPORTE DE CONDICIONES DE EQUIPOS
        public IEnumerable<condicionActivos> reporteCondicionesEquipos(reporteCondicionesEquipoQueryFilter filter, int usuario)
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

            var condicionesQuery = _unitOfWork.condicionActivosRepository.reporteCondicionesEquipos((int)filter.idEmpresa, usuario).AsQueryable();

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

            condicionesQuery = condicionesQuery.Where(x => x.fecha >= filter.fechaInicio && x.fecha < Convert.ToDateTime(filter.fechaFin).AddDays(1));

            //ordenar por fecha
            condicionesQuery = condicionesQuery.OrderByDescending(x => x.fecha);

            var condiciones = condicionesQuery.ToList();
            return condiciones;
        }
    }
}
