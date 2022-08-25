
using System.Collections.Generic;
/// <summary>
/// Representa una página de una entidad determinada con una lista de elementos de dicha entidad.
/// </summary>
/// <typeparam name="TEntity">entidad correspondiente a la página.</typeparam>
public class Page<TEntity>
    {
        /// <summary>
        /// Obtiene o establece el número de página actual.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Obtiene o establece el total de elementos en la base de datos correspondiente a esta entidad.
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Obtiene o establece el total de páginas de acuerdo a la cantidad de elementos en la base de datos.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Obtiene o establece los valores resumidos correspondientes.
        /// </summary>
        public List<KeyValuePair<object, object>> Summary { get; set; } = new List<KeyValuePair<object, object>>();

        /// <summary>
        /// Obtiene o establece los ítems correspondientes a esta página.
        /// </summary>
        public IList<TEntity> Items { get; set; }
    }
