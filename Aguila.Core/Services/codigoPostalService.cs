using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Services;
using Aguila.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class codigoPostalService : IcodigoPostalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public codigoPostalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //devuleve un codigo postal por medio de un id de Municipio
        public async Task<codigoPostal> getCodigo(int id)
        {
            //var codigo = _unitOfWork.codigoPostalRepository.GetAll()
            //                .Where(e => e.idMunicipio == id);

            var codigo =await  _unitOfWork.codigoPostalRepository.GetByIdIncludes(id);

            return codigo;
        }
        //devulve una lista de codigosPostales por medio de un id de departamentos
        public List<codigoPostal> getCodigosDepartamento(int id)
        {
            var municipios = _unitOfWork.codigoPostalRepository.GetAllIncludes()
                                .Where(e => e.municipio.idDepartamento == id).ToList();

            return municipios;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }
    }
}
