using Aguila.Core.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Api.Responses
{
    //se especifica el tipo Generico T para que la respuesta pueda recibir
    //cualquier tipo
    public class AguilaResponse<T>
    {
        public AguilaResponse(T data)
        {
            AguilaData = data;
        }

        public T AguilaData { get; set; }
        public Metadata Meta { get; set; }
    }
}
