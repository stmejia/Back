using System;
using System.Collections.Generic;
using System.Linq;

namespace Aguila.Core.CustomEntities
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?)null;
        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : (int?)null;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }
        public static PagedList<T> create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            // Skip salta los itemes que no quiero tomar en cuenta  , Take solo toma un grupo de registros de la lista, 
            // items siempre tendra solo una pagina
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        public static PagedList<T> create(IQueryable<T> querySource, int pageNumber, int pageSize)
        {
            var source = querySource.ToList();
            var count = source.Count();
            // Skip salta los itemes que no quiero tomar en cuenta  , Take solo toma un grupo de registros de la lista, 
            // items siempre tendra solo una pagina
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
