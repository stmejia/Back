using Aguila.Core.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Infrastructure.Mappings
{
    public class AguilaMap : IAguilaMap
    {
        private readonly IMapper _mapper;
        public AguilaMap(IMapper mapper)
        {
            _mapper = mapper;
        }

        public T Map<T>(object source)
        {
            var ConvertSource = _mapper.Map<T>(source);
            return ConvertSource;
        }

    }
}
