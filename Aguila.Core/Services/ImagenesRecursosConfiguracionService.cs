using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Aguila.Core.Services
{
    public class ImagenesRecursosConfiguracionService : IImagenesRecursosConfiguracionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;


        public ImagenesRecursosConfiguracionService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        //obtiene todos los registros, se envia filter para filtrar los resultados devueltos
        public PagedList<ImagenRecursoConfiguracion> GetImagenRecursoConfiguracion(ImagenRecursoConfiguracionQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var imagenes = _unitOfWork.ImagenesRecursosConfiguracionRepository.GetAll();

            if (filter.Recurso_Id != null)
            {
                imagenes = imagenes.Where(x => x.Recurso_Id == filter.Recurso_Id);
            }

            if (filter.Propiedad != null)
            {
                imagenes = imagenes.Where(x => x.Propiedad.ToLower().Contains(filter.Propiedad.ToLower()));
            }

            if (filter.DefaultImagen != null)
            {
                imagenes = imagenes.Where(x => x.DefaultImagen.ToLower().Contains(filter.DefaultImagen.ToLower()));
            }

            var pagedImagenes = PagedList<ImagenRecursoConfiguracion>.create(imagenes, filter.PageNumber, filter.PageSize);


            return pagedImagenes;

        }

        //consulta de un registro especifico por ID
        public async Task<ImagenRecursoConfiguracion> GetImagen(long id)
        {
            return await _unitOfWork.ImagenesRecursosConfiguracionRepository.GetByID(id);
        }

        //Inserta una nueva imagen configuracion
        public async Task insertImagenRecursosConfiguracion(ImagenRecursoConfiguracion imagenRecursoConfiguracion, string imagenBase64)
        {
            //se valida que el recurso exista
            var currentRecurso = await _unitOfWork.RecursosRepository.GetByID(imagenRecursoConfiguracion.Recurso_Id);
            if (currentRecurso == null)
            {
                throw new AguilaException("Recurso No Existente!....", 404);
            }

            //validamos si el valor de "Server" es local se guarda en el directorio local, de lo contrario en la ubicacion especificada en "Server"
            string pathServer = imagenRecursoConfiguracion.Servidor.ToUpper().Trim()
                         == "LOCAL" ? Directory.GetCurrentDirectory() : imagenRecursoConfiguracion.Servidor;

            string pathImagenesRecurso = Path.Combine(pathServer, imagenRecursoConfiguracion.Carpeta.Replace(@"/", @"\"));


            //se  valida que venga la imagen convertida en texto en base64
            if (!string.IsNullOrEmpty(imagenBase64))
            {
                string fullPathFileName = Path.Combine(pathImagenesRecurso, imagenRecursoConfiguracion.DefaultImagen);

                var imagenGuardada = await Base64ToImagen(imagenBase64.Trim(), fullPathFileName.Trim());

                if (!imagenGuardada)
                    throw new AguilaException("Formato incorrecto de imagen", 406);
            }
            //reinicia el id a 0 si en caso viene en la peticion ya que es un Identity (generado por la BD)
            imagenRecursoConfiguracion.Id = 0;

            await _unitOfWork.ImagenesRecursosConfiguracionRepository.Add(imagenRecursoConfiguracion);
            await _unitOfWork.SaveChangeAsync();


        }

        //actualiza una imagen configuracion
        public async Task<bool> UpdateImagenRecursosConfiguracion(ImagenRecursoConfiguracion imagenRecursoConf, string imagenBase64)
        {


            var currentImagenCong = await _unitOfWork.ImagenesRecursosConfiguracionRepository.GetByID(imagenRecursoConf.Id);
            if (currentImagenCong == null)
            {
                throw new AguilaException("Configuracion de Imagen No Existente!....", 404);
            }

            //Variables de control para verificar si se guardara una nueva imagen con distinto nombre 
            Boolean cambioNombre = false;
            string nameOld = currentImagenCong.DefaultImagen;

            if (!currentImagenCong.DefaultImagen.Equals(imagenRecursoConf.DefaultImagen))
            {
                cambioNombre = true;
            }

            currentImagenCong.Propiedad = imagenRecursoConf.Propiedad;
            currentImagenCong.FchCreacion = imagenRecursoConf.FchCreacion;
            currentImagenCong.Servidor = imagenRecursoConf.Servidor;
            currentImagenCong.Carpeta = imagenRecursoConf.Carpeta;
            currentImagenCong.PesoMaxMb = imagenRecursoConf.PesoMaxMb;
            currentImagenCong.EliminacionFisica = imagenRecursoConf.EliminacionFisica;
            currentImagenCong.MultiplesImagenes = imagenRecursoConf.MultiplesImagenes;
            currentImagenCong.DefaultImagen = imagenRecursoConf.DefaultImagen;
            currentImagenCong.NoMaxImagenes = imagenRecursoConf.NoMaxImagenes;

            //Si se envio una nueva imagen en base 64 se realiza el proceso de guardado
            if (!string.IsNullOrEmpty(imagenBase64))
            {
                string pathServer = currentImagenCong.Servidor.ToUpper().Trim()
                             == "LOCAL" ? Directory.GetCurrentDirectory() : currentImagenCong.Servidor;

                string pathImagenesRecurso = Path.Combine(pathServer, currentImagenCong.Carpeta.Replace(@"/", @"\"));
                string fullPathFileName = Path.Combine(pathImagenesRecurso, currentImagenCong.DefaultImagen);

                var imagenGuardada = await Base64ToImagen(imagenBase64.Trim(), fullPathFileName.Trim());

                if (!imagenGuardada)
                {
                    throw new AguilaException("Formato incorrecto de imagen", 406);
                }
                else
                {
                    //se borra la imagen anterior en el caso que cambiara de nombre
                    if (cambioNombre) 
                    {                         
                        try
                        {
                            File.Delete(Path.Combine(pathImagenesRecurso, nameOld));
                        }
                        catch (IOException ex)
                        {
                            throw new AguilaException("Error al eliminar imagen: " + ex.Message, 404);
                        }
                    }
                        
                }
            }

            _unitOfWork.ImagenesRecursosConfiguracionRepository.Update(currentImagenCong);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteImagenRecursoConfiguracion(long id)
        {
            var currentImagenCong = await _unitOfWork.ImagenesRecursosConfiguracionRepository.GetByID(id);
            if (currentImagenCong == null)
            {
                throw new AguilaException("Configuracion de Imagen No Existente!....", 404);
            }

            //se elimina la imagen fisicamente del directorio
            string pathServer = currentImagenCong.Servidor.ToUpper().Trim()
                             == "LOCAL" ? Directory.GetCurrentDirectory() : currentImagenCong.Servidor;

            string pathImagenesRecurso = Path.Combine(pathServer, currentImagenCong.Carpeta.Replace(@"/", @"\"));
            string fullPathFileName = Path.Combine(pathImagenesRecurso, currentImagenCong.DefaultImagen);

            try {
                File.Delete(fullPathFileName);
            }
            catch(IOException ex)
            {
                throw new AguilaException("Error al eliminar imagen: " + ex.Message, 404);
            }
           


            await _unitOfWork.ImagenesRecursosConfiguracionRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        //se encarga de recibir el codigo en base 64 de la imagen  y convertirlo a una imagen guardandola en la ubicacion indicada
        private async Task<bool> Base64ToImagen(string base64Content, string fullPathFileName)
        {
            try
            {
                int indexOfSemiColon = base64Content.IndexOf(";", StringComparison.OrdinalIgnoreCase);
                string dataLabel = base64Content.Substring(0, indexOfSemiColon);
                string contentType = dataLabel.Split(':').Last();
                var startIndex = base64Content.IndexOf("base64,", StringComparison.OrdinalIgnoreCase) + 7;
                var fileContents = base64Content.Substring(startIndex);
                byte[] bytes = Convert.FromBase64String(fileContents);
                if (bytes.Length > 0)
                {
                    using (var stream = new FileStream(fullPathFileName, FileMode.Create))
                    {
                        await stream.WriteAsync(bytes, 0, bytes.Length);
                        await stream.FlushAsync();
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

    }
}
