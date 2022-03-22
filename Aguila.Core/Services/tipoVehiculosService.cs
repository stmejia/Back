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
    public class tipoVehiculosService : ItipoVehiculosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public tipoVehiculosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<tipoVehiculos> GetTipoVehiculos(tipoVehiculosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var tipoVehiculos = _unitOfWork.tipoVehiculosRepository.GetAll();

            if(filter.idEmpresa != null)
            {
                tipoVehiculos = tipoVehiculos.Where(x => x.idEmpresa == filter.idEmpresa);
            }

            if (filter.prefijo != null)
            {
                tipoVehiculos = tipoVehiculos.Where(x => x.prefijo.ToLower().Equals(filter.prefijo.ToLower()));
            }

            if (filter.codigo != null)
            {
                tipoVehiculos = tipoVehiculos.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.descripcion != null)
            {
                tipoVehiculos = tipoVehiculos.Where(e => e.descripcion.ToLower().Contains(filter.descripcion.ToLower()));
            }

            var pagedTipoVehiculos = PagedList<tipoVehiculos>.create(tipoVehiculos, filter.PageNumber, filter.PageSize);
            return pagedTipoVehiculos;
        }

        public async Task<tipoVehiculos> GetTipoVehiculo(int id)
        {
            return await _unitOfWork.tipoVehiculosRepository.GetByID(id);
        }

        public async Task InsertTipoVehiculo(tipoVehiculos tipoVehiculo)
        {
            //Insertamos la fecha de ingreso del registro
            tipoVehiculo.id = 0;
            tipoVehiculo.fechaCreacion = DateTime.Now;

            switch (tipoVehiculo.prefijo.ToUpper())
            {
                case "CA01": 
                    tipoVehiculo.estructuraCoc = "distancia,potencia,tornamesaGraduable,000,flota";
                    break;

                case "CA02":
                    tipoVehiculo.estructuraCoc = "distancia,potencia,tornamesaGraduable,000,flota";
                    break;

                case "CA03":
                    tipoVehiculo.estructuraCoc = "distancia,potencia,tornamesaGraduable,000,flota";
                    break;

                case "CM01":
                    tipoVehiculo.estructuraCoc = "capacidadCarga,carroceria,tipoCarga,00,flota";
                    break;

                case "MC01":
                    tipoVehiculo.estructuraCoc = "capacidadMontacarga,tipoMotor,0000,flota";
                    break;

                case "VELI":
                    tipoVehiculo.estructuraCoc = "tipoVehiculo,00000,flota";
                    break;

                case "MA01":
                    tipoVehiculo.estructuraCoc = "tipoMaquina,00000,flota";
                    break;
            }

            await _unitOfWork.tipoVehiculosRepository.Add(tipoVehiculo);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateTipoVehiculo(tipoVehiculos tipoVehiculo)
        {
            var currentTipoVehiculo = await _unitOfWork.tipoVehiculosRepository.GetByID(tipoVehiculo.id);

            if (currentTipoVehiculo == null)
            {
                throw new AguilaException("Tipo no existente...");
            }

            //currentTipoVehiculo.codigo = tipoVehiculo.codigo;
            currentTipoVehiculo.descripcion = tipoVehiculo.descripcion;
            currentTipoVehiculo.prefijo = tipoVehiculo.prefijo;
            //currentTipoVehiculo.correlativoLongitud = tipoVehiculo.correlativoLongitud;
            currentTipoVehiculo.estructuraCoc = tipoVehiculo.estructuraCoc;

            switch (tipoVehiculo.prefijo.ToUpper())
            {
                case "CA01":
                    tipoVehiculo.estructuraCoc = "distancia,potencia,tornamesaGraduable,000,flota";
                    break;

                case "CA02":
                    tipoVehiculo.estructuraCoc = "distancia,potencia,tornamesaGraduable,000,flota";
                    break;

                case "CA03":
                    tipoVehiculo.estructuraCoc = "distancia,potencia,tornamesaGraduable,000,flota";
                    break;

                case "CM01":
                    tipoVehiculo.estructuraCoc = "capacidadCarga,carroceria,tipoCarga,00,flota";
                    break;

                case "MC01":
                    tipoVehiculo.estructuraCoc = "capacidadMontacarga,tipoMotor,0000,flota";
                    break;

                case "VELI":
                    tipoVehiculo.estructuraCoc = "tipoVehiculo,00000,flota";
                    break;

                case "MA01":
                    tipoVehiculo.estructuraCoc = "tipoMaquina,00000,flota";
                    break;
            }

            currentTipoVehiculo.estructuraCoc = tipoVehiculo.estructuraCoc;

            _unitOfWork.tipoVehiculosRepository.Update(currentTipoVehiculo);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteTipoVehiculo(int id)
        {
            var currentTipoVehiculo = await _unitOfWork.tipoVehiculosRepository.GetByID(id);
            if (currentTipoVehiculo == null)
            {
                throw new AguilaException("Tipo no existente...");
            }

            await _unitOfWork.tipoVehiculosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
        
    }
}
