﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversityProject.Domain.CustomEntities
{
    public class PagedList<T> : List<T>
    {
        public PagedList(List<T> items, int count, int pageNumber, int pageSizes)
        {
            TotalCount = count;
            PageSize = pageSizes;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSizes);

            AddRange(items);
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages; 
        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : null;
        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : null;

        public static PagedList<T> Create(IEnumerable<T> source,int pageNumber ,int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
