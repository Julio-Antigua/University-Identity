using System;
using System.Linq;

public static class DataPagerExtension
    {
        public static Page<TEntity> Paginate<TEntity>(this IQueryable<TEntity> query, int page, int limit)
           where TEntity : class, new()
        {
            var paged = new Page<TEntity>();

            page = (page < 0) ? 1 : page;

            paged.CurrentPage = page;
            paged.TotalItems = query.Count();

            var startRow = (page - 1) * limit;
            paged.Items = query.Skip(startRow).Take(limit).ToList();
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

            return paged;
        }
    }
