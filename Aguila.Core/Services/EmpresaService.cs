using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

//Aqui vamos a definir las reglas de negocio para la entidad Empresas

namespace Aguila.Core.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IImagenesRecursosService _imagenesRecursosService;
        public EmpresaService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,  IImagenesRecursosService imagenesRecursosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _imagenesRecursosService = imagenesRecursosService;
        }

        public async Task<PagedList<Empresas>> GetEmpresas(EmpresaQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var empresas = _unitOfWork.EmpresaRepository.GetAllIncludes();

            if (filter.Nombre != null)
            {
                empresas = empresas.Where(x=>x.Nombre.ToLower().Contains(filter.Nombre.ToLower()));
            }

            if (filter.Pais != null)
            {
                empresas = empresas.Where(x=>x.Pais.ToLower().Contains(filter.Pais.ToLower()));
            }

            if (filter.esEmpleador != null) 
            {
                empresas = empresas.Where(x => x.esEmpleador.Equals(filter.esEmpleador));
            }

            var pagedEmpresas = PagedList<Empresas>.create(empresas, filter.PageNumber, filter.PageSize);

            // Manejo de Imagenes, colocar la Url a las imagenes por defecto para toda la lista
            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedEmpresas.Select(e => e.ImagenLogo).ToList());

            return pagedEmpresas;
        }

        public async Task<Empresas> GetEmpresa(byte id)
        {
            var empresa =  await _unitOfWork.EmpresaRepository.GetByID(id);

            //Manejo de imagenes
            if (empresa != null && empresa.ImagenRecurso_IdLogo != null && empresa.ImagenRecurso_IdLogo != Guid.Empty && empresa.ImagenLogo == null)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(empresa.ImagenRecurso_IdLogo ?? Guid.Empty);
                empresa.ImagenLogo = imgRecurso;
            }
            //Fin Imagenes

            if (empresa == null)
            {
                throw new AguilaException("Empresa No Existente!....", 404);
            }

            return empresa;
        }

        public async Task InsertEmpresa(Empresas empresa)
        {
            //Guardamos el recurso de iamgen
            if (empresa.ImagenLogo != null)
            {
                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(empresa.ImagenLogo,"Empresas",nameof(empresa.ImagenLogo));

                if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                    empresa.ImagenRecurso_IdLogo = imgRecurso.Id;

                empresa.ImagenLogo = null;
            }
            //  Fin de recurso de Imagen          

            //reinicia el id a 0 si en caso viene en la peticion ya que es un Identity (generado por la BD)
            empresa.Id = 0;
            empresa.FchCreacion = DateTime.Now;
            var xCodigo = _unitOfWork.EmpresaRepository.GetAll().Max(e => e.Codigo);

            empresa.Codigo = (byte) (xCodigo+1);

            await _unitOfWork.EmpresaRepository.Add(empresa);
            await _unitOfWork.SaveChangeAsync();

        }

        public async Task<bool> updateEmpresa(Empresas empresa)
        {
            //valida que la empresa a actualizar exista
            var currentEmpresa = await _unitOfWork.EmpresaRepository.GetByID(empresa.Id);
            if (currentEmpresa == null)
            {
                throw new AguilaException("Empresa No Existente!....", 404);
            }

            currentEmpresa.Nombre = empresa.Nombre;
            //currentEmpresa.Abreviatura = empresa.Abreviatura;
            currentEmpresa.Aleas = empresa.Aleas;
            currentEmpresa.Activ = empresa.Activ;
            //currentEmpresa.FchCreacion = empresa.FchCreacion;
            currentEmpresa.Nit = empresa.Nit;
            currentEmpresa.Direccion = empresa.Direccion;
            currentEmpresa.Telefono = empresa.Telefono;
            currentEmpresa.Email = empresa.Email;
            currentEmpresa.WebPage = empresa.WebPage;
            currentEmpresa.Pais = empresa.Pais;
            currentEmpresa.Departamento = empresa.Departamento;
            currentEmpresa.Municipio = empresa.Municipio;
            currentEmpresa.esEmpleador = empresa.esEmpleador;

            // Guardamos el Recurso de Imagen
            if (empresa.ImagenLogo != null)
            {
                empresa.ImagenLogo.Id = currentEmpresa.ImagenRecurso_IdLogo ?? Guid.Empty;

                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(empresa.ImagenLogo,"Empresas",nameof(empresa.ImagenLogo));

                if (currentEmpresa.ImagenRecurso_IdLogo == null || currentEmpresa.ImagenRecurso_IdLogo == Guid.Empty)
                {
                    if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                        currentEmpresa.ImagenRecurso_IdLogo = imgRecurso.Id;
                }
            }
            //  Fin de recurso de Imagen            


            _unitOfWork.EmpresaRepository.Update(currentEmpresa);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteEmpresa(byte id)
        {
            //valida que la empresa a actualizar exista
            var currentEmpresa = await _unitOfWork.EmpresaRepository.GetByID(id);
            if (currentEmpresa == null)
            {
                throw new AguilaException("Empresa No Existente!....");
            }

            // Eliminamos el recurso de imagen
            if (currentEmpresa.ImagenRecurso_IdLogo != null && currentEmpresa.ImagenRecurso_IdLogo != Guid.Empty)
                await _imagenesRecursosService.EliminarImagenRecurso(currentEmpresa.ImagenRecurso_IdLogo ?? Guid.Empty);
            // fin recurso imagen

            await _unitOfWork.EmpresaRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }


    }
}
