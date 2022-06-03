using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie4U.Utilities
{
    public class PaginatedListFactory<T> : List<T>
    {
        public int pageIndex { get; set; }
        public int totalPages { get; set; }

        private PaginatedListFactory(List<T> items, int count, int pageIndex, int pageSize)
        {
            this.pageIndex = pageIndex;
            this.totalPages = (int)Math.Ceiling(count / (double) pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage => pageIndex > 1;
        public bool HasNextPage => pageIndex < totalPages;


        public static Task<PaginatedListFactory<T>> Create(List<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();

            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return Task.FromResult(new PaginatedListFactory<T>(items, count, pageIndex, pageSize));
        }
    }
}
