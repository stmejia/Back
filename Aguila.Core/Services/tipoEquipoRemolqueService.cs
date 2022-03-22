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
    public class tipoEquipoRemolqueService : ItipoEquipoRemolqueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public tipoEquipoRemolqueService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<tipoEquipoRemolque> GetTipoEquipoRemolque(tipoEquipoRemolqueQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var tipoEquipoRemolque = _unitOfWork.tipoEquipoRemolqueRepository.GetAll();

            if (filter.idEmpresa != null)
            {
                tipoEquipoRemolque = tipoEquipoRemolque.Where(x => x.idEmpresa == filter.idEmpresa);
            }

            if (filter.codigo != null)
            {
                tipoEquipoRemolque = tipoEquipoRemolque.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.prefijo != null)
            {
                tipoEquipoRemolque = tipoEquipoRemolque.Where(x => x.prefijo.ToLower().Equals(filter.prefijo.ToLower()));
            }


            if (filter.descripcion != null)
            {
                tipoEquipoRemolque = tipoEquipoRemolque.Where(e => e.descripcion.ToLower().Contains(filter.descripcion.ToLower()));
            }

           
            tipoEquipoRemolque = tipoEquipoRemolque.OrderByDescending(e => e.fechaCreacion);

            var pagedTipoEquipoRemolque = PagedList<tipoEquipoRemolque>.create(tipoEquipoRemolque, filter.PageNumber, filter.PageSize);
            return pagedTipoEquipoRemolque;
        }

        public async Task<tipoEquipoRemolque> GetTipoEquipoRemolque(int id)
        {
            return await _unitOfWork.tipoEquipoRemolqueRepository.GetByID(id);
        }

        public async Task InsertTipoEquipoRemolque(tipoEquipoRemolque tipoEquipoRemolque)
        {
            //Insertamos la fecha de ingreso del registro
            tipoEquipoRemolque.id = 0;
            tipoEquipoRemolque.fechaCreacion = DateTime.Now;

            switch (tipoEquipoRemolque.prefijo.ToUpper())
            {
                case "CH20":
                    tipoEquipoRemolque.estructuraCoc = "noEjes,tandemCorredizo,chasisExtensible,tipoCuello,acopleGenset,acopleDolly,flota";
                    break;

                case "CH40":
                    tipoEquipoRemolque.estructuraCoc = "noEjes,tandemCorredizo,chasisExtensible,tipoCuello,acopleGenset,acopleDolly,flota";
                    break;

                case "CH24":
                    tipoEquipoRemolque.estructuraCoc = "noEjes,tandemCorredizo,chasisExtensible,tipoCuello,acopleGenset,acopleDolly,flota";
                    break;

                case "PL40":
                    tipoEquipoRemolque.estructuraCoc = "noEjes,medidaPlataforma,tandemCorredizo,plataformaExtensible,pechera,acopleDolly,flota";
                    break;

                case "LB01":
                    tipoEquipoRemolque.estructuraCoc = "noEjes,capacidadCargaLB,lbExtensible,00,flota";
                    break;

                case "DL01":
                    tipoEquipoRemolque.estructuraCoc = "noEjes,00000,flota";
                    break;

                case "CN20":
                    tipoEquipoRemolque.estructuraCoc = "alturaContenedor,tipoContenedor,marcaUR,000,flota";
                    break;

                case "CN40":
                    tipoEquipoRemolque.estructuraCoc = "alturaContenedor,tipoContenedor,marcaUR,000,flota";
                    break;

                case "FUSE":
                    tipoEquipoRemolque.estructuraCoc = "noEjes,ejeCorredizo,largoFurgon,medidasFurgon,rielesHorizontales,rielesVerticales,flota";
                    break;

                case "FURE":
                    tipoEquipoRemolque.estructuraCoc = "noEjes,ejeCorredizo,largoFurgon,medidasFurgon,rielesHorizontales,rielesVerticales,flota";
                    break;

                //case "EA01":
                //    tipoEquipoRemolque.estructuraCoc = "tipoMaquina,00000,flota";
                //    break;
            }

            await _unitOfWork.tipoEquipoRemolqueRepository.Add(tipoEquipoRemolque);
            await _unitOfWork.SaveChangeAsync();

            

        }


        public async Task<bool> UpdateTipoEquipoRemolque(tipoEquipoRemolque tipoEquipoRemolque)
        {
            var currentTipoEquipoRemolque = await _unitOfWork.tipoEquipoRemolqueRepository.GetByID(tipoEquipoRemolque.id);

            if (currentTipoEquipoRemolque == null)
            {
                throw new AguilaException("Tipo no existente...");
            }

            //currentTipoEquipoRemolque.codigo = tipoEquipoRemolque.codigo;
            currentTipoEquipoRemolque.descripcion = tipoEquipoRemolque.descripcion;
            currentTipoEquipoRemolque.prefijo = tipoEquipoRemolque.prefijo;
            currentTipoEquipoRemolque.estructuraCoc = tipoEquipoRemolque.estructuraCoc;
            //currentTipoEquipoRemolque.ejes = tipoEquipoRemolque.ejes;
            //currentTipoEquipoRemolque.llantas = tipoEquipoRemolque.llantas;

            switch (currentTipoEquipoRemolque.prefijo.ToUpper())
            {
                case "CH20":
                    currentTipoEquipoRemolque.estructuraCoc = "noEjes,tandemCorredizo,chasisExtensible,tipoCuello,acopleGenset,acopleDolly,flota";
                    break;

                case "CH40":
                    currentTipoEquipoRemolque.estructuraCoc = "noEjes,tandemCorredizo,chasisExtensible,tipoCuello,acopleGenset,acopleDolly,flota";
                    break;

                case "CH24":
                    currentTipoEquipoRemolque.estructuraCoc = "noEjes,tandemCorredizo,chasisExtensible,tipoCuello,acopleGenset,acopleDolly,flota";
                    break;

                case "PL40":
                    currentTipoEquipoRemolque.estructuraCoc = "noEjes,medidaPlataforma,tandemCorredizo,plataformaExtensible,pechera,acopleDolly,flota";
                    break;

                case "LB01":
                    currentTipoEquipoRemolque.estructuraCoc = "noEjes,capacidaCargaLB,lbExtensible,00,flota";
                    break;

                case "DL01":
                    currentTipoEquipoRemolque.estructuraCoc = "noEjes,00000,flota";
                    break;

                case "CN20":
                    currentTipoEquipoRemolque.estructuraCoc = "alturaContenedor,tipoContenedor,marcaUR,000,flota";
                    break;

                case "CN40":
                    currentTipoEquipoRemolque.estructuraCoc = "alturaContenedor,tipoContenedor,marcaUR,000,flota";
                    break;

                case "FUSE":
                    currentTipoEquipoRemolque.estructuraCoc = "noEjes,ejeCorredizo,largoFurgon,medidasFurgon,rielesHorizontales,rielesVerticales,flota";
                    break;

                case "FURE":
                    currentTipoEquipoRemolque.estructuraCoc = "noEjes,ejeCorredizo,largoFurgon,medidasFurgon,rielesHorizontales,rielesVerticales,flota";
                    break;

                    //case "EA01":
                    //    tipoEquipoRemolque.estructuraCoc = "tipoMaquina,00000,flota";
                    //    break;
            }


            _unitOfWork.tipoEquipoRemolqueRepository.Update(currentTipoEquipoRemolque);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteTipoEquipoRemolque(int id)
        {
            var currentTipoEquipoRemolque = await _unitOfWork.tipoEquipoRemolqueRepository.GetByID(id);
            if (currentTipoEquipoRemolque == null)
            {
                throw new AguilaException("Tipo no existente...");
            }

            await _unitOfWork.tipoEquipoRemolqueRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
