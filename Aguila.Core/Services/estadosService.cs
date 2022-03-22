using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.Enumeraciones;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class estadosService : IestadosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public estadosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<estados> GetEstados(estadosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var estados = _unitOfWork.estadosRepository.GetAll();

            if (filter.idEmpresa != null)
            {
                estados = estados.Where(e => e.idEmpresa == filter.idEmpresa);
            }

            if (filter.codigo != null)
            {
                estados = estados.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.tipo != null)
            {
                estados = estados.Where(e => e.tipo.ToLower().Contains(filter.tipo.ToLower()));
            }

            if (filter.nombre != null)
            {
                estados = estados.Where(e => e.nombre.ToLower().Contains(filter.nombre.ToLower()));
            }

            if (filter.numeroOrden != null)
            {
                estados = estados.Where(e => e.numeroOrden == filter.numeroOrden);
            }

            if (filter.disponible != null)
            {
                estados = estados.Where(e => e.disponible == filter.disponible);
            }

            if (filter.evento != null)
            {
                estados = estados.Where(e => e.evento.ToLower().Contains(filter.evento.ToLower()));
            }


            var pagedEstados = PagedList<estados>.create(estados, filter.PageNumber, filter.PageSize);
            return pagedEstados;
        }

        public async Task<estados> GetEstado(int id)
        {
            return await _unitOfWork.estadosRepository.GetByID(id);
        }

        public async Task InsertEstado(estados estado)
        {
            //Insertamos la fecha de ingreso del registro
            estado.id = 0;
            estado.fechaCreacion = DateTime.Now;

            await _unitOfWork.estadosRepository.Add(estado);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateEstado(estados estado)
        {
            var currentEstado = await _unitOfWork.estadosRepository.GetByID(estado.id);
            if (currentEstado == null)
            {
                throw new AguilaException("Estado no existente...");
            }

            currentEstado.idEmpresa = estado.idEmpresa;
            currentEstado.tipo = estado.tipo;
            currentEstado.nombre = estado.nombre;
            currentEstado.numeroOrden = estado.numeroOrden;
            currentEstado.disponible = estado.disponible;

            _unitOfWork.estadosRepository.Update(currentEstado);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteEstado(int id)
        {
            var currentEstado = await _unitOfWork.estadosRepository.GetByID(id);
            if (currentEstado == null)
            {
                throw new AguilaException("Estado no existente...");
            }

            await _unitOfWork.estadosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public bool existeDato(string codigo, byte empresa, int numeroOrden)
        {
            var filterCodigo = new estadosQueryFilter();
            var filterOrden = new estadosQueryFilter();
            filterCodigo.codigo = codigo; filterCodigo.idEmpresa = empresa;
            filterOrden.numeroOrden = numeroOrden; filterOrden.idEmpresa = empresa;

            var estado = GetEstados(filterCodigo);
            var estadoOrden = GetEstados(filterOrden);

            if (estado.Count > 0)
            {
                return true;
            }
            if (estadoOrden.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }     

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        public estados GetEstadoByEvento(int empresaId, string tipo, string evento)
        {
            return _unitOfWork.estadosRepository.GetEstadoByEvento(empresaId, tipo, evento);
        }

        public IEnumerable<estados> GetEstadosByEvento(int idEmpresa, string tipo, List<string> eventos)
        {
            Func<estados, bool> condicionEventos = (estado) => {
                foreach (var xEvento in eventos)
                {
                    if (estado.evento.ToUpper().ToString().Contains(xEvento.ToString().ToUpper().Trim()))
                        return true;
                }
                return false;
            };

            //var xEstados = _unitOfWork.estadosRepository.GetAll()
            //    .Where(
            //    e => e.idEmpresa == idEmpresa & e.tipo.ToUpper().ToString() == tipo.ToUpper().ToString()
            //    && condicionEventos(e)
            //).ToList();

            var filtro = new estadosQueryFilter { idEmpresa =(byte)idEmpresa,  tipo=tipo};
            var xxEstados = GetEstados(filtro);
            var estadosResponse = new List<estados>();

            foreach(var state in xxEstados)
            {
                if (condicionEventos(state))
                    estadosResponse.Add(state);
            }


            return estadosResponse;
        }
    }
}
