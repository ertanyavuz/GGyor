using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StorMan.Data.Repositories
{
    public class RepositoryBase
    {
        protected StorManEntities _context;

        public RepositoryBase()
        {
            _context = new StorManEntities();
        }

        protected void Sync<T1, T2>(List<T1> memSet, List<T2> dbSet, Func<T1, T2, int> comparer, Func<T1, T2, bool> updater)
            where T1 : class
            where T2 : class, new()
        {

            foreach (var memObj in memSet)
            {
                var dbEntity = dbSet.FirstOrDefault(x => comparer(memObj, x) == 0);
                if (dbEntity == null)
                {
                    // Insert
                    dbEntity = new T2();
                    updater(memObj, dbEntity);
                    _context.Set<T2>().Add(dbEntity);
                }
                else
                {
                    // Do an update
                    updater(memObj, dbEntity);
                }
            }

            foreach (var dbEntity in dbSet.Where(x => !memSet.Any(y => comparer(y, x) == 0)))
            {
                //_context.Set(dbEntity.GetType()).Remove(dbEntity);
                _context.Set<T2>().Remove(dbEntity);
            }

        }

        //protected bool DoAssign(IList leftSideList, IList rightSideList)
        //{
        //    var updated = false;
        //    for (var i = 0; i < leftSideList.Count; i++)
        //    {
        //        var leftHS = leftSideList[i];
        //        var rightHS = rightSideList[i];
        //        if (leftHS != rightHS)
        //        {
        //            leftHS = rightHS;
        //            updated = true;
        //        }
        //    }

        //    return updated;
        //}

    }
}
