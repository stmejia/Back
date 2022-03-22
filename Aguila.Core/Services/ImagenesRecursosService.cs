using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces;
using Aguila.Core.Interfaces.Services;
using Aguila.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class ImagenesRecursosService : IImagenesRecursosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUriService _uriService;
        public ImagenesRecursosService(IUnitOfWork unitOfWork, IUriService uriService)
        {
            _unitOfWork = unitOfWork;
            _uriService = uriService;
        }

        public async Task<ImagenRecurso> GetByID(Guid id)
        {
            var imgRecurso = await  _unitOfWork.ImagenesRecursosRepository.GetByIdWithConfiguracion(id);
            var config = imgRecurso.ImagenRecursoConfiguracion;

            if (imgRecurso.Imagenes == null)
            {
                return null;
            }

            ICollection<Imagen> imgs = imgRecurso.Imagenes.Where(i => i.FchBorrada == null).ToList();

            var uriServer = _uriService.GetUriServer().ToString().Trim();            
            string pathServer = config.Servidor.ToUpper().Trim() == "LOCAL" ? uriServer : config.Servidor;
            string pathImagenesRecurso = Path.Combine(pathServer, config.Carpeta.Trim());            
            foreach (Imagen img in imgs)
            {
                img.UrlImagen = Path.Combine(pathImagenesRecurso, img.FileName);
                if (img.Id == imgRecurso.Imagen_IdDefault)
                    imgRecurso.ImagenDefault = img;
            }

            imgRecurso.Imagenes = imgs;
            imgRecurso.ImagenRecursoConfiguracion = null;
            imgRecurso.Usuario = null;

            if (config.MultiplesImagenes == false)
            {
                imgRecurso.Imagenes = null;
            }                    

            return imgRecurso;
        }

        public string GetImagenDefault(ImagenRecursoConfiguracion config)
        {
            // Retorna la Url de la imagen empty para todo el recurso
            var uriServer = _uriService.GetUriServer().ToString().Trim();
            string pathServer = config.Servidor.ToUpper().Trim() == "LOCAL" ? uriServer : config.Servidor;
            string pathImagenesRecurso = Path.Combine(pathServer, config.Carpeta.Trim());

            string UrlImgDefault = "";

            if (!string.IsNullOrEmpty(config.DefaultImagen))
            {
                UrlImgDefault = Path.Combine(pathImagenesRecurso, config.DefaultImagen);
            }
            return UrlImgDefault;
        }                

        public async Task<bool>  AsignarUrlImagenesDefault(List<ImagenRecurso> imagenesRecursos)
        {
            imagenesRecursos = imagenesRecursos.Select(e => e).OfType<ImagenRecurso>().ToList();

            if (imagenesRecursos == null || imagenesRecursos.Count == 0)
                return true ; 

            var configId = imagenesRecursos.Select(e => e.ImagenRecursoConfig_Id).FirstOrDefault();
            var config = _unitOfWork.ImagenesRecursosConfiguracionRepository.GetByID(configId).Result;

            var uriServer = _uriService.GetUriServer().ToString().Trim();
            string pathServer = config.Servidor.ToUpper().Trim() == "LOCAL" ? uriServer : config.Servidor;
            string pathImagenesRecurso = Path.Combine(pathServer, config.Carpeta.Trim());

            foreach(ImagenRecurso imgRec in imagenesRecursos.TakeWhile(e => e.Imagen_IdDefault != null && e.Imagen_IdDefault != Guid.Empty))
            {
                Imagen img = await _unitOfWork.ImagenesRepository.GetByID(imgRec.Imagen_IdDefault);
                if(img != null)
                    img.UrlImagen = Path.Combine(pathImagenesRecurso, img.FileName);

                imgRec.ImagenDefault = img;
            }

            foreach (ImagenRecurso imgRec in imagenesRecursos)
            {
                imgRec.Imagenes = null;
                imgRec.ImagenRecursoConfiguracion = null;
            }

            return true;
        }

        public async Task<ImagenRecursoConfiguracion> GetConfiguracion(string ControladorNombre, string propiedad)
        {
            var recurso = await _unitOfWork.RecursosRepository.GetByControladorNombre(ControladorNombre);

            if (recurso == null)
                throw new AguilaException("Recurso No Existente!....",404);

            propiedad = propiedad.Trim().ToUpper();

            // Buscamos si el recurso tiene configurado manejo de imagen, con recurso_ID y con el nombre de la propiedad 

            var imgConfig = _unitOfWork.ImagenesRecursosConfiguracionRepository.GetAll()
                            .Where(x => x.Recurso_Id == recurso.Id && x.Propiedad.Trim().ToUpper().Equals(propiedad))
                            .FirstOrDefault();

            if (imgConfig == null)
                throw new AguilaException("Este recurso no tiene una propiedad : " + propiedad + " Configurada para manejar imagen(es)", 404);

            imgConfig.UrlImagenDefaul = GetImagenDefault(imgConfig);

            return imgConfig; 
        }

        public async Task<ImagenRecurso> GuardarImagenRecurso(ImagenRecurso imagenRecurso, string controlador, string propiedad)
        {
            if (imagenRecurso == null || (imagenRecurso.Imagenes == null && imagenRecurso.ImagenesEliminar == null))
                return null;

            var imgRecursoCurrent = await _unitOfWork.ImagenesRecursosRepository.GetByIdWithConfiguracion(imagenRecurso.Id);

            // Si no existe el recurso de imagen  buscamos la configuracion del recurso por controlador y propiedad
            if (imgRecursoCurrent != null)
            {
                //si existe, tomamos la configuracion y seteamos la Imagen ID x Defecto
                imagenRecurso.Imagen_IdDefault = imgRecursoCurrent.Imagen_IdDefault;
                imagenRecurso.ImagenRecursoConfiguracion = imgRecursoCurrent.ImagenRecursoConfiguracion;                        
            }

            if (imagenRecurso.ImagenRecursoConfiguracion == null)
                imagenRecurso.ImagenRecursoConfiguracion = await GetConfiguracion(controlador, propiedad);

            var config = imagenRecurso.ImagenRecursoConfiguracion;

            if (config == null)
                throw new AguilaException("Este recurso no tiene una propiedad Configurada para manejar imagen(es)", 404);

            //if (imagenRecurso.Imagenes == null && imagenRecurso.ImagenesEliminar == null)             
                //throw new AguilaException("No hay imagenes para procesar", 400);

            if(!config.MultiplesImagenes && imagenRecurso.Imagenes != null && imagenRecurso.Imagenes.Count > 1)
                throw new AguilaException("Esta configuracion solo permite una imagen", 400);

            imagenRecurso.ImagenRecursoConfig_Id = config.Id;          

            // La propiedades de RecursoImagen.Imagenes  -> se usa solo para transportar las imagenes que se quieren guardar
            // La propiedad de RecursoImagen.ImagenesEliminas -> se usa solo para transportar el listado de imagenes que se van a eliminar

            // Nuevo Recurso de Imagen
            if (imagenRecurso.Id == default || imagenRecurso.Id == Guid.Empty)
            {
                imagenRecurso.Id = Guid.NewGuid();
                
                if(imagenRecurso.Imagenes.Count > config.NoMaxImagenes )
                    throw new AguilaException("Se supera el numero de imagenes permitidas ( " + config.NoMaxImagenes.ToString().Trim() +" )", 404);

                if (await GuardarImagenes(imagenRecurso)) 
                {
                    imagenRecurso.Imagenes = null;
                    imagenRecurso.ImagenDefault = null;
                    await _unitOfWork.ImagenesRecursosRepository.Add(imagenRecurso);
                }
            }                
            else
            {
                if (imagenRecurso.ImagenesEliminar != null)
                    await EliminarImagenes(imagenRecurso);

                // Modificar
                if (imagenRecurso.Imagenes != null)
                {
                    int noImgsNuevas = imagenRecurso.Imagenes.Where(i => i.Id == Guid.Empty || i.Id == default ||  i.Id == null ).Count();
                    int noImgsGuardadas = _unitOfWork.ImagenesRepository.GetAll().Where(i => i.ImagenRecurso_Id == imagenRecurso.Id && i.FchBorrada == null).Count();

                    if(noImgsGuardadas + noImgsNuevas > config.NoMaxImagenes)
                        throw new AguilaException("Se supera el numero de imagenes permitidas ( " + config.NoMaxImagenes.ToString().Trim() + " )", 404);

                    await GuardarImagenes(imagenRecurso);
                }                   
                
                if(imgRecursoCurrent.Imagen_IdDefault != imagenRecurso.Imagen_IdDefault)
                {
                    var imgRecursoUpdate = await _unitOfWork.ImagenesRecursosRepository.GetByID(imagenRecurso.Id);
                    imgRecursoUpdate.Imagen_IdDefault = imagenRecurso.Imagen_IdDefault;
                    _unitOfWork.ImagenesRecursosRepository.Update(imgRecursoUpdate);
                }
            }

            return imagenRecurso;
        }

        public async Task<bool> GuardarImagenes(ImagenRecurso imagenRecurso)
        {
            string pathServer = imagenRecurso.ImagenRecursoConfiguracion.Servidor.ToUpper().Trim()
                                  == "LOCAL" ? Directory.GetCurrentDirectory() : imagenRecurso.ImagenRecursoConfiguracion.Servidor;

            string pathImagenesRecurso = Path.Combine(pathServer, imagenRecurso.ImagenRecursoConfiguracion.Carpeta.Replace(@"/", @"\"));



            // lo que este en la lista de imagenes, se van a agregar o actualizar
            foreach (Imagen imagen in imagenRecurso.Imagenes.TakeWhile(i => !string.IsNullOrEmpty(i.SubirImagenBase64) || (i.Id != null && i.Id != Guid.Empty)))
            {            

                Imagen imagenOperar;
                bool agregarImagen = true;

                //Modificar
                if (imagen.Id != null && imagen.Id != Guid.Empty)
                {
                    imagenOperar = await _unitOfWork.ImagenesRepository.GetByID(imagen.Id);

                    if (imagenOperar == null)
                        throw new AguilaException("No se encontro la imagen a modificar", 404);

                    agregarImagen = false;

                    //NO SE PUEDE CAMBIAR LA IMAGEN
                    if (imagen.SubirImagenBase64 != null)
                        throw new AguilaException("No se puede cambiar la imagen, por favor envie una nueva", 400);
                    
                }
                else
                {
                    // Agregar
                    imagenOperar = new Imagen();
                    imagenOperar.Id = Guid.NewGuid();
                    imagenOperar.FchCreacion = DateTime.Now;
                    imagenOperar.ImagenRecurso_Id = imagenRecurso.Id;
                    imagenOperar.ArchivoEliminado = false;
                    imagenOperar.FileName = imagenOperar.Id.ToString().Trim() + "_" + imagen.FileName.Trim();

                    // Si es imagen unica, cada vez que envian una nueva borra la anterior, el borrado del registro solo es logico
                    if (imagenRecurso.ImagenRecursoConfiguracion.MultiplesImagenes == false && imagenRecurso.Imagen_IdDefault != null)
                    {
                        bool imagenEliminada = await EliminarImagen(imagenRecurso.Imagen_IdDefault ?? Guid.Empty, pathImagenesRecurso, imagenRecurso.ImagenRecursoConfiguracion.EliminacionFisica);
                        if (imagenEliminada)
                        {
                            imagenRecurso.Imagen_IdDefault = null;
                        }                    
                    }
                } 
                
                imagenOperar.Nombre = imagen.Nombre;
                imagenOperar.Descripcion = imagen.Descripcion;

                // Subir Imagen
                if (!string.IsNullOrEmpty(imagen.SubirImagenBase64))
                {
                    string fullPathFileName = Path.Combine(pathImagenesRecurso, imagenOperar.FileName);

                    var imagenGuardada = await Base64ToImagen(imagen.SubirImagenBase64.Trim(), fullPathFileName.Trim());

                    if (! imagenGuardada )
                        throw new AguilaException("Formato incorrecto de imagen", 406);
                }

                if(agregarImagen)
                    await _unitOfWork.ImagenesRepository.Add(imagenOperar);
                else
                    _unitOfWork.ImagenesRepository.Update(imagenOperar);


                // Asignamos una imagen por default
                if (imagenRecurso.Imagen_IdDefault == null || imagenRecurso.Imagen_IdDefault == Guid.Empty)
                {
                    imagenRecurso.Imagen_IdDefault = imagenOperar.Id;
                }
            }
            return true;
        }

        public async Task<bool> EliminarImagenes(ImagenRecurso imagenRecurso)
        {
            string pathServer = imagenRecurso.ImagenRecursoConfiguracion.Servidor.ToUpper().Trim()
                                  == "LOCAL" ? Directory.GetCurrentDirectory() : imagenRecurso.ImagenRecursoConfiguracion.Servidor;

            string pathImagenesRecurso = Path.Combine(pathServer, imagenRecurso.ImagenRecursoConfiguracion.Carpeta.Replace(@"/", @"\"));

            if (imagenRecurso.ImagenesEliminar == null)
                throw new AguilaException("No hay imagenes para eliminar", 404);

            // lo que este en la lista de imagenes, se van a agregar o actualizar
            foreach (Guid imagenId in imagenRecurso.ImagenesEliminar)
            {
                bool imagenEliminada = await EliminarImagen(imagenId, pathImagenesRecurso, imagenRecurso.ImagenRecursoConfiguracion.EliminacionFisica);

                if (!imagenEliminada)
                    throw new AguilaException("Error al eliminar la imagen", 404);

                if (imagenRecurso.Imagen_IdDefault == imagenId)
                    imagenRecurso.Imagen_IdDefault = null;
            }

            return true;
        }


        private async Task<bool> EliminarImagen(Guid imagenID, String pathImagenesRecurso, bool eliminacionFisica)
        {
            var imagenEliminar = await _unitOfWork.ImagenesRepository.GetByID(imagenID);

            if (imagenEliminar == null)
                return true;
                
                //throw new AguilaException("No se encontro la imagen a Eliminar", 404);

            return EliminarImagen(imagenEliminar, pathImagenesRecurso, eliminacionFisica);

        }

        private bool EliminarImagen(Imagen imagenEliminar, String pathImagenesRecurso, bool eliminacionFisica)
        {
            imagenEliminar.FchBorrada = DateTime.Now;
            imagenEliminar.ArchivoEliminado = eliminacionFisica;

            if(eliminacionFisica)
            {
                var fullPathFileName = Path.Combine(pathImagenesRecurso, imagenEliminar.FileName);

                try
                {
                    if (System.IO.File.Exists(fullPathFileName))
                        System.IO.File.Delete(fullPathFileName);
                }
                catch
                {
                    return false;
                }
            }

            _unitOfWork.ImagenesRepository.Update(imagenEliminar);

            return true;
        }


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
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> EliminarImagenRecurso(Guid id)
        {
            var imgRecursoCurrent = await _unitOfWork.ImagenesRecursosRepository.GetByIdWithConfiguracion(id);
                        
            if (imgRecursoCurrent == null)
            {
                return true;
            }

            if (imgRecursoCurrent.Imagenes == null)
                return true;

            string pathServer = imgRecursoCurrent.ImagenRecursoConfiguracion.Servidor.ToUpper().Trim()
                                 == "LOCAL" ? Directory.GetCurrentDirectory() : imgRecursoCurrent.ImagenRecursoConfiguracion.Servidor;

            string pathImagenesRecurso = Path.Combine(pathServer, imgRecursoCurrent.ImagenRecursoConfiguracion.Carpeta.Replace(@"/", @"\"));

            foreach (Imagen imagen in imgRecursoCurrent.Imagenes.TakeWhile(e => e.FchBorrada == null))
            {
                EliminarImagen(imagen, pathImagenesRecurso, imgRecursoCurrent.ImagenRecursoConfiguracion.EliminacionFisica);
            }

            var imgRecursoUpdate = await _unitOfWork.ImagenesRecursosRepository.GetByID(id);

            imgRecursoUpdate.Imagen_IdDefault = null;

            _unitOfWork.ImagenesRecursosRepository.Update(imgRecursoUpdate);

            return true;
        }

    }
}
