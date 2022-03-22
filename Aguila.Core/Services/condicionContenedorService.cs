using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class condicionContenedorService : IcondicionContenedorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IcondicionActivosService _condicionActivosService;
        private readonly IAguilaMap _aguilaMap;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public condicionContenedorService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options, IAguilaMap aguilaMap,
                                          IcondicionActivosService condicionActivosService, IImagenesRecursosService imagenesRecursosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _aguilaMap = aguilaMap;
            _condicionActivosService = condicionActivosService;
            _imagenesRecursosService = imagenesRecursosService; 
        }

        public async Task<PagedList<condicionContenedor>> GetCondicionContenedor(condicionContenedorQueryFilter filter)
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

            var condicionContenedor = _unitOfWork.condicionContenedorRepository.GetAllIncludes();

            //Filtrado por fechas
            if (!filter.ignorarFechas)
                condicionContenedor = condicionContenedor.Where(e => e.condicionActivo.fecha >= filter.fechaInicio && e.condicionActivo.fecha < filter.fechaFin.Value.AddDays(1));

            if (filter.idCondicionActivo != null)
                condicionContenedor = condicionContenedor.Where(e => e.idCondicionActivo == filter.idCondicionActivo);

            if (filter.idEstacionTrabajo != null)
                condicionContenedor = condicionContenedor.Where(e => e.condicionActivo.idEstacionTrabajo == filter.idEstacionTrabajo);

            if (filter.movimiento != null)
                condicionContenedor = condicionContenedor.Where(e => e.condicionActivo.movimiento.ToUpper().Trim().Contains(filter.movimiento.ToUpper().Trim()));

            if (filter.disponible != null)
                condicionContenedor = condicionContenedor.Where(e => e.condicionActivo.disponible == filter.disponible);

            if (filter.disponible != null)
                condicionContenedor = condicionContenedor.Where(e => e.condicionActivo.disponible == filter.disponible);

            if (filter.cargado != null)
                condicionContenedor = condicionContenedor.Where(e => e.condicionActivo.cargado == filter.cargado);

            if (filter.inspecVeriOrden != null)            
                condicionContenedor = condicionContenedor.Where(e => e.condicionActivo.inspecVeriOrden == filter.inspecVeriOrden);

            if (filter.tipoContenedor != null)
                condicionContenedor = condicionContenedor.Where(e => e.tipoContenedor.ToUpper().Trim().Contains(filter.tipoContenedor.ToUpper().Trim()));

            if (filter.exteriorMarcos != null)
                condicionContenedor = condicionContenedor.Where(e => e.exteriorMarcos == filter.exteriorMarcos);

            if (filter.puertasInteriorExterior != null)
                condicionContenedor = condicionContenedor.Where(e => e.puertasInteriorExterior == filter.puertasInteriorExterior);

            if (filter.pisoInterior != null)
                condicionContenedor = condicionContenedor.Where(e => e.pisoInterior == filter.pisoInterior);

            if (filter.techoCubierta != null)
                condicionContenedor = condicionContenedor.Where(e => e.techoCubierta == filter.techoCubierta);

            if (filter.ladosIzquierdoDerecho != null)
                condicionContenedor = condicionContenedor.Where(e => e.ladosIzquierdoDerecho == filter.ladosIzquierdoDerecho);

            if (filter.paredFrontal != null)
                condicionContenedor = condicionContenedor.Where(e => e.paredFrontal == filter.paredFrontal);

            if (filter.areaCondensadorCompresor != null)
                condicionContenedor = condicionContenedor.Where(e => e.areaCondensadorCompresor == filter.areaCondensadorCompresor);

            if (filter.areaEvaporador != null)
                condicionContenedor = condicionContenedor.Where(e => e.areaEvaporador == filter.areaEvaporador);

            if (filter.areaBateria != null)
                condicionContenedor = condicionContenedor.Where(e => e.areaBateria == filter.areaBateria);

            if (filter.cajaControlElectricoAutomatico != null)
                condicionContenedor = condicionContenedor.Where(e => e.cajaControlElectricoAutomatico == filter.cajaControlElectricoAutomatico);

            if (filter.cablesConexionElectrica != null)
                condicionContenedor = condicionContenedor.Where(e => e.cablesConexionElectrica == filter.cablesConexionElectrica);

            var pagedCondicionContenedor = PagedList<condicionContenedor>.create(condicionContenedor, filter.PageNumber, filter.PageSize);

            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedCondicionContenedor.Select(e => e.condicionActivo.ImagenFirmaPiloto).ToList());
            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedCondicionContenedor.Select(e => e.condicionActivo.Fotos).ToList());
            return pagedCondicionContenedor;
        }

        public async Task<condicionContenedor> GetCondicionContenedor(long idCondicion)
        {
            var condicionContenedor = _unitOfWork.condicionContenedorRepository.GetAllIncludes().Where(c => c.idCondicionActivo == idCondicion).FirstOrDefault();

            //Manejo de imagenes
            if (condicionContenedor != null && condicionContenedor.condicionActivo.idImagenRecursoFirma != null && condicionContenedor.condicionActivo.idImagenRecursoFirma != Guid.Empty)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(condicionContenedor.condicionActivo.idImagenRecursoFirma ?? Guid.Empty);
                condicionContenedor.condicionActivo.ImagenFirmaPiloto = imgRecurso;
            }

            if (condicionContenedor != null && condicionContenedor.condicionActivo.idImagenRecursoFotos != null && condicionContenedor.condicionActivo.idImagenRecursoFotos != Guid.Empty)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(condicionContenedor.condicionActivo.idImagenRecursoFotos ?? Guid.Empty);
                condicionContenedor.condicionActivo.Fotos = imgRecurso;
            }
            //Fin Imagenes

            return condicionContenedor;
        }

        public async Task<condicionContenedorDto> InsertCondicionContenedor(condicionContenedorDto condicionContenedorDto)
        {
            //TODO: convencion de serie siempre A
            condicionContenedorDto.condicionActivo.tipoCondicion = "CONTENEDOR";
            condicionContenedorDto.condicionActivo.serie = "A";

            var xCondicionContenedor = _aguilaMap.Map<condicionContenedor>(condicionContenedorDto);
            var xCondicionActivo = xCondicionContenedor.condicionActivo;

            xCondicionActivo = await _condicionActivosService.insert(xCondicionActivo);
            if (xCondicionActivo.id == 0)
                return null;

            await _unitOfWork.condicionContenedorRepository.Add(xCondicionContenedor);
            await _unitOfWork.SaveChangeAsync();

            var xCondicionActivoDto = _aguilaMap.Map<condicionActivosDto>(xCondicionActivo);
            condicionContenedorDto.idCondicionActivo = xCondicionActivo.id;
            condicionContenedorDto.condicionActivo = xCondicionActivoDto;

            return condicionContenedorDto;
        }

        public async Task<bool> UpdateCondicionContenedor(condicionContenedor condicionContenedor)
        {
            var currentContenedor = await _unitOfWork.condicionContenedorRepository.GetByID(condicionContenedor.idCondicionActivo);
            if (currentContenedor == null)
                throw new AguilaException("Condicion no existente");

            currentContenedor.idCondicionActivo = condicionContenedor.idCondicionActivo;

            var currentCondicionActivo = await _unitOfWork.condicionActivosRepository.GetByID(condicionContenedor.idCondicionActivo);
            if (currentCondicionActivo == null)
                throw new AguilaException("Condicion no existente");

            // Guardamos el Recurso de Imagen
            if (condicionContenedor.condicionActivo.Fotos != null)
            {
                //Obligatorio enviar el id de imagen recurso guardado en la tabla
                condicionContenedor.condicionActivo.Fotos.Id = currentCondicionActivo.idImagenRecursoFotos ?? Guid.Empty;

                var imgRecursoOp = await _imagenesRecursosService.GuardarImagenRecurso(condicionContenedor.condicionActivo.Fotos, "condicionActivos", nameof(condicionContenedor.condicionActivo.Fotos));

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
                _unitOfWork.condicionContenedorRepository.Update(currentContenedor);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
            }

            return true;
        }

        public async Task<bool> DeleteCondicionContenedor(int id)
        {
            var currentContenedor = await _unitOfWork.condicionContenedorRepository.GetByID(id);
            if (currentContenedor == null)
                throw new AguilaException("Condicion no existente");

            await _unitOfWork.condicionContenedorRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        public condicionContenedor ultima(int idActivo)
        {
            var condicionContenedor = _unitOfWork.condicionContenedorRepository.GetUltima(idActivo);
            return condicionContenedor;
        }
    }
}
