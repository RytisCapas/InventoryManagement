using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace WarehouseInventoryManagement.Services
{
    public class CommonUtils
    {
        
        public static IQueryOver<troot, tsubtype> AddQueryOverSearchCriterias<troot, tsubtype>(IQueryOver<troot, tsubtype> input, IList<ICriterion> criteria)
        {
            if (criteria.Count == 0)
            {
                return input;
            }

            if (criteria.Count == 1)
            {
                return input.Where(criteria[0]);
            }

            var or = Restrictions.Or(criteria[0], criteria[1]);

            for (var i = 2; i < criteria.Count; i++)
            {
                or = Restrictions.Or(or, criteria[i]);
            }

            return input.Where(or);
        }
    }
}
