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

namespace Aguila.Core.Services
{
    public class pilotosDocumentosService : IpilotosDocumentosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public pilotosDocumentosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<pilotosDocumentos> GetPilotosDocumentos(pilotosDocumentosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var pilotosDocumentos = _unitOfWork.pilotosDocumentosRepository.GetAll();

            if (filter.idPiloto != null)
            {
                pilotosDocumentos = pilotosDocumentos.Where(e => e.idPiloto == filter.idPiloto);
            }

            if (filter.nombreDocumento != null)
            {
                pilotosDocumentos = pilotosDocumentos.Where(e => e.nombreDocumento.ToLower().Contains(filter.nombreDocumento.ToLower()));
            }

            if (filter.tipoDocumento != null)
            {
                pilotosDocumentos = pilotosDocumentos.Where(e => e.tipoDocumento.ToLower().Contains(filter.tipoDocumento.ToLower()));
            }

            if (filter.fechaVigencia != null)
            {
                pilotosDocumentos = pilotosDocumentos.Where(e => e.fechaVigencia == filter.fechaVigencia);
            }

            if (filter.idImagenRecursoDocumentos != null)
            {
                pilotosDocumentos = pilotosDocumentos.Where(e => e.idImagenRecursoDocumentos == filter.idImagenRecursoDocumentos);
            }

            var pagedPilotosDocumentos = PagedList<pilotosDocumentos>.create(pilotosDocumentos, filter.PageNumber, filter.PageSize);
            return pagedPilotosDocumentos;

        }

        public async Task<pilotosDocumentos> GetPilotoDocumento(int id)
        {
            return await _unitOfWork.pilotosDocumentosRepository.GetByID(id);
        }

        public async Task InsertPilotoDocumento(pilotosDocumentos pilotoDocumento)
        {
            //Insertamos la fecha de ingreso del registro
            pilotoDocumento.id = 0;
            pilotoDocumento.fechaCreacion = DateTime.Now;

            await _unitOfWork.pilotosDocumentosRepository.Add(pilotoDocumento);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdatePilotoDocumento(pilotosDocumentos pilotoDocumento)
        {
            var currentPilotoDoc = await _unitOfWork.pilotosDocumentosRepository.GetByID(pilotoDocumento.id);
            if (currentPilotoDoc == null)
            {
                throw new AguilaException("Documento no existente...");
            }

            currentPilotoDoc.idPiloto = pilotoDocumento.idPiloto;
            currentPilotoDoc.nombreDocumento = pilotoDocumento.nombreDocumento;
            currentPilotoDoc.tipoDocumento = pilotoDocumento.tipoDocumento;
            currentPilotoDoc.fechaVigencia = pilotoDocumento.fechaVigencia;
            currentPilotoDoc.idImagenRecursoDocumentos = pilotoDocumento.idImagenRecursoDocumentos;

            _unitOfWork.pilotosDocumentosRepository.Update(currentPilotoDoc);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeletePilotoDocumento(int id)
        {
            var currentPilotoDoc = await _unitOfWork.pilotosDocumentosRepository.GetByID(id);
            if (currentPilotoDoc == null)
            {
                throw new AguilaException("Documento no existente...");
            }

            await _unitOfWork.pilotosDocumentosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

    }
}
