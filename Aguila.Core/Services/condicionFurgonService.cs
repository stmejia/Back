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
    public class condicionFurgonService : IcondicionFurgonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IcondicionActivosService _condicionActivosService;
        private readonly IAguilaMap _aguilaMap;
        private readonly IImagenesRecursosService _imagenesRecursosService;
        private readonly IeventosControlEquipoService _eventosControlEquipoService;
        private readonly IUsuariosService _usuariosService;

        public condicionFurgonService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,
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

        public async Task<PagedList<condicionFurgon>> GetCondicionFurgon(condicionFurgonQueryFilter filter)
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

            var condicionFurgon = _unitOfWork.condicionFurgonRepository.GetAllIncludes();

            //Filtrado por fechas
            if (!filter.ignorarFechas)
                condicionFurgon = condicionFurgon.Where(e => e.condicionActivo.fecha >= filter.fechaInicio && e.condicionActivo.fecha < filter.fechaFin.Value.AddDays(1));

            if (filter.idCondicionActivo != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.idCondicionActivo == filter.idCondicionActivo);
            }

            if (filter.idEstacionTrabajo != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.condicionActivo.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            if (filter.movimiento != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.condicionActivo.movimiento.ToLower().Contains(filter.movimiento.ToLower()));
            }

            if (filter.disponible != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.condicionActivo.disponible == filter.disponible);
            }

            if (filter.cargado != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.condicionActivo.cargado == filter.cargado);
            }

            if (filter.inspecVeriOrden != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.condicionActivo.inspecVeriOrden == filter.inspecVeriOrden);
            }

            if (filter.revExtGolpe != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.revExtGolpe == filter.revExtGolpe);
            }

            if (filter.revExtSeparacion != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.revExtSeparacion == filter.revExtSeparacion);
            }

            if (filter.revExtRoturas != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.revExtRoturas == filter.revExtRoturas);
            }

            if (filter.revIntGolpes != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.revIntGolpes == filter.revIntGolpes);
            }

            if (filter.revIntSeparacion != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.revIntSeparacion == filter.revIntSeparacion);
            }

            if (filter.revIntFiltra != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.revIntFiltra == filter.revIntFiltra);
            }

            if (filter.revIntRotura != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.revIntRotura == filter.revIntRotura);
            }

            if (filter.revIntPisoH != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.revIntPisoH == filter.revIntPisoH);
            }

            if (filter.revIntManchas != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.revIntManchas == filter.revIntManchas);
            }

            if (filter.revIntOlores != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.revIntOlores == filter.revIntOlores);
            }

            if (filter.revPuertaCerrado != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.revPuertaCerrado.ToLower().Contains(filter.revPuertaCerrado.ToLower()));
            }

            if (filter.revPuertaEmpaque != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.revPuertaEmpaque.ToLower().Contains(filter.revPuertaEmpaque.ToLower()));
            }

            if (filter.revPuertaCinta != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.revPuertaCinta.ToLower().Contains(filter.revPuertaCinta.ToLower()));
            }

            if (filter.limpPiso != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.limpPiso == filter.limpPiso);
            }

            if (filter.limpTecho != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.limpTecho == filter.limpTecho);
            }

            if (filter.limpLateral != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.limpLateral == filter.limpLateral);
            }

            if (filter.limpExt != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.limpExt == filter.limpExt);
            }

            if (filter.limpPuerta != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.limpPuerta == filter.limpPuerta);
            }

            if (filter.limpMancha != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.limpMancha == filter.limpMancha);
            }

            if (filter.limpOlor != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.limpOlor == filter.limpOlor);
            }

            if (filter.limpRefuerzo != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.limpRefuerzo == filter.limpRefuerzo);
            }

            if (filter.lucesA != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesA == filter.lucesA);
            }

            if (filter.lucesB != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesB == filter.lucesB);
            }

            if (filter.lucesC != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesC == filter.lucesC);
            }

            if (filter.lucesD != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesD == filter.lucesD);
            }

            if (filter.lucesE != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesE == filter.lucesE);
            }

            if (filter.lucesF != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesF == filter.lucesF);
            }

            if (filter.lucesG != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesG == filter.lucesG);
            }

            if (filter.lucesH != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesH == filter.lucesH);
            }

            if (filter.lucesI != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesI == filter.lucesI);
            }

            if (filter.lucesJ != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesJ == filter.lucesJ);
            }

            if (filter.lucesK != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesK == filter.lucesK);
            }

            if (filter.lucesL != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesL == filter.lucesL);
            }

            if (filter.lucesM != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesM == filter.lucesM);
            }

            if (filter.lucesN != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesN == filter.lucesN);
            }

            if (filter.lucesO != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.lucesO == filter.lucesO);
            }

            if (filter.guardaFangosI != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.guardaFangosI == filter.guardaFangosI);
            }

            if (filter.guardaFangosD != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.guardaFangosD == filter.guardaFangosD);
            }

            if (filter.fricciones != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.fricciones == filter.fricciones);
            }

            if (filter.senalizacion != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.senalizacion == filter.senalizacion);
            }

            if (filter.placaPatin != null)
            {
                condicionFurgon = condicionFurgon.Where(e => e.placaPatin==filter.placaPatin);
            }

            var pagedCondicionFurgon = PagedList<condicionFurgon>.create(condicionFurgon, filter.PageNumber, filter.PageSize);

            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedCondicionFurgon.Select(e => e.condicionActivo.ImagenFirmaPiloto).ToList());
            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedCondicionFurgon.Select(e => e.condicionActivo.Fotos).ToList());

            return pagedCondicionFurgon;
        }

        public async Task<condicionFurgon> GetCondicionFurgon(long idCondicion)
        {
            var condicionFurgon =  _unitOfWork.condicionFurgonRepository.GetAllIncludes().Where(c => c.idCondicionActivo == idCondicion).FirstOrDefault();
            //Manejo de imagenes
            if (condicionFurgon != null && condicionFurgon.condicionActivo.idImagenRecursoFirma != null && condicionFurgon.condicionActivo.idImagenRecursoFirma != Guid.Empty)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(condicionFurgon.condicionActivo.idImagenRecursoFirma ?? Guid.Empty);
                condicionFurgon.condicionActivo.ImagenFirmaPiloto = imgRecurso;
            }

            if (condicionFurgon != null && condicionFurgon.condicionActivo.idImagenRecursoFotos != null && condicionFurgon.condicionActivo.idImagenRecursoFotos != Guid.Empty)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(condicionFurgon.condicionActivo.idImagenRecursoFotos ?? Guid.Empty);
                condicionFurgon.condicionActivo.Fotos = imgRecurso;
            }
            //Fin Imagenes
            return condicionFurgon;
        }

        public async Task<condicionFurgonDto> InsertCondicionFurgon(condicionFurgonDto condicionFurgonDto)
        {
            //TODO: convencion de serie siempre A
            condicionFurgonDto.condicionActivo.tipoCondicion = "FURGON";
            condicionFurgonDto.condicionActivo.serie = "A";

            var xCondicionFurgon = _aguilaMap.Map<condicionFurgon>(condicionFurgonDto);

            var xNumeroLlantas = condicionFurgonDto.condicionesLlantas.Count;
            var xNumeroLlantaRepuesto = condicionFurgonDto.condicionesLlantasRepuesto.Count;

            xCondicionFurgon.llanta1 = xNumeroLlantas >= 1 ? condicionFurgonDto.condicionesLlantas.ElementAt(0).ToString() : null;
            xCondicionFurgon.llanta2 = xNumeroLlantas >= 2 ? condicionFurgonDto.condicionesLlantas.ElementAt(1).ToString() : null;
            xCondicionFurgon.llanta3 = xNumeroLlantas >= 3 ? condicionFurgonDto.condicionesLlantas.ElementAt(2).ToString() : null;
            xCondicionFurgon.llanta4 = xNumeroLlantas >= 4 ? condicionFurgonDto.condicionesLlantas.ElementAt(3).ToString() : null;
            xCondicionFurgon.llanta5 = xNumeroLlantas >= 5 ? condicionFurgonDto.condicionesLlantas.ElementAt(4).ToString() : null;
            xCondicionFurgon.llanta6 = xNumeroLlantas >= 6 ? condicionFurgonDto.condicionesLlantas.ElementAt(5).ToString() : null;
            xCondicionFurgon.llanta7 = xNumeroLlantas >= 7 ? condicionFurgonDto.condicionesLlantas.ElementAt(6).ToString() : null;
            xCondicionFurgon.llanta8 = xNumeroLlantas >= 8 ? condicionFurgonDto.condicionesLlantas.ElementAt(7).ToString() : null;
            xCondicionFurgon.llanta9 = xNumeroLlantas >= 9 ? condicionFurgonDto.condicionesLlantas.ElementAt(8).ToString() : null;
            xCondicionFurgon.llanta10 = xNumeroLlantas >= 10 ? condicionFurgonDto.condicionesLlantas.ElementAt(9).ToString() : null;
            xCondicionFurgon.llanta11 = xNumeroLlantas >= 11 ? condicionFurgonDto.condicionesLlantas.ElementAt(10).ToString() : null;

            xCondicionFurgon.llantaR = xNumeroLlantaRepuesto >= 1 ? condicionFurgonDto.condicionesLlantasRepuesto.ElementAt(0).ToString() : null;
            xCondicionFurgon.llantaR2 = xNumeroLlantaRepuesto >= 2 ? condicionFurgonDto.condicionesLlantasRepuesto.ElementAt(1).ToString() : null;

            var xCondicionActivo = xCondicionFurgon.condicionActivo;

            _unitOfWork.BeginTransaction();

            try
            {
                xCondicionActivo = await _condicionActivosService.insert(xCondicionActivo);

                if (xCondicionActivo.id == 0)
                    throw new AguilaException("No se pudo insertar la condicion", 400);

                await _unitOfWork.condicionFurgonRepository.Add(xCondicionFurgon);
                await _unitOfWork.SaveChangeAsync();

                _unitOfWork.CommitTransaction();

                //VERIFICA SI HAY QUE CREAR EVENTOS POR OBSERVACIONES DE LLANTAS            
                eventosControlEquipoDto evento = new eventosControlEquipoDto();

                foreach (var llanta in condicionFurgonDto.condicionesLlantas)
                {
                    if (!string.IsNullOrEmpty(llanta.observaciones))
                    {
                        evento.idUsuarioCreacion = condicionFurgonDto.condicionActivo.idUsuario;
                        evento.idEstacionTrabajo = condicionFurgonDto.condicionActivo.idEstacionTrabajo;
                        evento.idActivo = condicionFurgonDto.condicionActivo.idActivo;
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
                foreach (var llanta in condicionFurgonDto.condicionesLlantasRepuesto)
                {
                    if (!string.IsNullOrEmpty(llanta.observaciones))
                    {
                        evento.idUsuarioCreacion = condicionFurgonDto.condicionActivo.idUsuario;
                        evento.idEstacionTrabajo = condicionFurgonDto.condicionActivo.idEstacionTrabajo;
                        evento.idActivo = condicionFurgonDto.condicionActivo.idActivo;
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

            condicionFurgonDto.idCondicionActivo = xCondicionActivo.id;
            condicionFurgonDto.condicionActivo = xCondicionActivoDto;

            //llenarCondicionLlantas(xCondicionCabezal,ref condicionCabezalDto);

            return condicionFurgonDto;
        }

        public async Task<bool> UpdateCondicionFurgon(condicionFurgon condicionFurgon)
        {
            var currentCondicionFurgon = await _unitOfWork.condicionFurgonRepository.GetByID(condicionFurgon.idCondicionActivo);
            if (currentCondicionFurgon == null)
            {
                throw new AguilaException("Condicion no existente...");
            }

            currentCondicionFurgon.idCondicionActivo = condicionFurgon.idCondicionActivo;
            currentCondicionFurgon.lucesA = condicionFurgon.lucesA;
            currentCondicionFurgon.lucesB = condicionFurgon.lucesB;
            currentCondicionFurgon.lucesC = condicionFurgon.lucesC;
            currentCondicionFurgon.lucesD = condicionFurgon.lucesD;
            currentCondicionFurgon.lucesE = condicionFurgon.lucesE;
            currentCondicionFurgon.lucesF = condicionFurgon.lucesF;
            currentCondicionFurgon.guardaFangosD = condicionFurgon.guardaFangosD;
            currentCondicionFurgon.guardaFangosI = condicionFurgon.guardaFangosI;
            currentCondicionFurgon.fricciones = condicionFurgon.fricciones;
            currentCondicionFurgon.placaPatin = condicionFurgon.placaPatin;
            currentCondicionFurgon.llanta1 = condicionFurgon.llanta1;
            currentCondicionFurgon.llanta2 = condicionFurgon.llanta2;
            currentCondicionFurgon.llanta3 = condicionFurgon.llanta3;
            currentCondicionFurgon.llanta4 = condicionFurgon.llanta4;
            currentCondicionFurgon.llanta5 = condicionFurgon.llanta5;
            currentCondicionFurgon.llanta6 = condicionFurgon.llanta6;
            currentCondicionFurgon.llanta7 = condicionFurgon.llanta7;
            currentCondicionFurgon.llanta8 = condicionFurgon.llanta8;
            currentCondicionFurgon.llanta9 = condicionFurgon.llanta9;
            currentCondicionFurgon.llanta10 = condicionFurgon.llanta10;
            currentCondicionFurgon.llanta11 = condicionFurgon.llanta11;

            var currentCondicionActivo = await _unitOfWork.condicionActivosRepository.GetByID(condicionFurgon.idCondicionActivo);
            if (currentCondicionActivo == null)
                throw new AguilaException("Condicion No Existente");

            // Guardamos el Recurso de Imagen
            if (condicionFurgon.condicionActivo.Fotos != null)
            {
                //Obligatorio enviar el id de imagen recurso guardado en la tabla
                condicionFurgon.condicionActivo.Fotos.Id = currentCondicionActivo.idImagenRecursoFotos ?? Guid.Empty;

                var imgRecursoOp = await _imagenesRecursosService.GuardarImagenRecurso(condicionFurgon.condicionActivo.Fotos, "condicionFurgon", nameof(condicionFurgon.condicionActivo.Fotos));

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
                _unitOfWork.condicionFurgonRepository.Update(currentCondicionFurgon);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();                
            }

            return true;
        }

        public async Task<bool> DeleteCondicionFurgon(int id)
        {
            var currentCondicionFurgon = await _unitOfWork.condicionFurgonRepository.GetByID(id);
            if (currentCondicionFurgon == null)
            {
                throw new AguilaException("Condicion no existente");
            }

            await _unitOfWork.condicionFurgonRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        public void llenarCondicionLlantas(condicionFurgon condicionFurgon, ref condicionFurgonDto condicionFurgonDto)
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

            agregarLlanta(xLlantas, 1, condicionFurgon.llanta1);
            agregarLlanta(xLlantas, 2, condicionFurgon.llanta2);
            agregarLlanta(xLlantas, 3, condicionFurgon.llanta3);
            agregarLlanta(xLlantas, 4, condicionFurgon.llanta4);
            agregarLlanta(xLlantas, 5, condicionFurgon.llanta5);
            agregarLlanta(xLlantas, 6, condicionFurgon.llanta6);
            agregarLlanta(xLlantas, 7, condicionFurgon.llanta7);
            agregarLlanta(xLlantas, 8, condicionFurgon.llanta8);
            agregarLlanta(xLlantas, 9, condicionFurgon.llanta9);
            agregarLlanta(xLlantas, 10, condicionFurgon.llanta10);
            agregarLlanta(xLlantas, 11, condicionFurgon.llanta11);

            condicionFurgonDto.condicionesLlantas = xLlantas;

            var xLlantasRepuesto = new List<condicionLlantaDto>();

            agregarLlanta(xLlantasRepuesto, 1, condicionFurgon.llantaR);
            agregarLlanta(xLlantasRepuesto, 2, condicionFurgon.llantaR2);

            condicionFurgonDto.condicionesLlantasRepuesto = xLlantasRepuesto;
        }

        public condicionFurgon ultima(int idActivo)
        {
            var condicionFurgon = _unitOfWork.condicionFurgonRepository.GetUltima(idActivo);
            return condicionFurgon;
        }


        //REPORTE DE CONDICIONES DE FURGONES
        public IEnumerable<condicionActivos> reporteCondicionesFurgones(reporteCondicionesFurgonesQueryFilter filter, int usuario)
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

            var condicionesQuery = _unitOfWork.condicionActivosRepository.reporteCondicionesFurgones((int)filter.idEmpresa, usuario).AsQueryable();

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
