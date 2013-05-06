using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WarehouseInventoryManagement.Models.Mappers
{
    /// <summary>
    /// Base class for all mappers.
    /// </summary>
    public abstract class MapperBase
    {
        /// <summary>
        /// Object mapping delegate.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDest">The type of the destination.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns>Mapped object.</returns>
        protected delegate TDest MapDelegatep<TSource, TDest>(TSource source, TDest destination);

        /// <summary>
        /// Maps a list.
        /// </summary>
        /// <typeparam name="TSource">The type of the source object.</typeparam>
        /// <typeparam name="TDest">The type of the destination object.</typeparam>
        /// <param name="map">The map delegate.</param>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns>Mapped list.</returns>
        protected List<TDest> MapList<TSource, TDest>(MapDelegatep<TSource, TDest> map, IList<TSource> source, IList<TDest> destination = null)
            where TDest : class, new()
        {
            if (source == null)
            {
                return new List<TDest>();
            }

            if (destination == null)
            {
                destination = new List<TDest>();
            }
            else
            {
                destination.Clear();
            }

            foreach (var sourceItem in source)
            {
                destination.Add(map(sourceItem, null));
            }

            return destination.ToList();
        }
    }
}