using System.Collections.Generic;
/// <summary>
/// Representa las funcionalidades básicas de un servicio genérico.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IPaginateService<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Permite paginar una determinada entidad.
        /// </summary>
        /// <param name="page">número de la página que se desea obtener.</param>
        /// <param name="limit">límite de ítems dentro de la página.</param>
        /// <param name="filters">lista de filtros a ser aplicados en la consulta.</param>
        Page<TEntity> Paginate(int page, int limit, Dictionary<string, string> filters = null);
    }
