using Edument.CQRS;
using NDatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBACNationals.ReadModels
{
    public abstract class AReadModel
    {
        private string _readModelFilePath;

        public AReadModel(string readModelFolder)
        {
            _readModelFilePath = Path.Combine(readModelFolder, this.GetType().Name);
        }
        
        protected void Create<T>(T entity)
            where T : AEntity
        {
            using (var odb = OdbFactory.Open(_readModelFilePath))
            {
                var exists = odb.QueryAndExecute<T>()
                                .Where(p => p.Id.Equals(entity.Id))
                                .FirstOrDefault();
                if (exists != null)
                    return;

                odb.Store(entity);
            }
        }

        protected IEnumerable<T> Read<T>()
        {
            using (var odb = OdbFactory.Open(_readModelFilePath))
            {
                return Read<T>(x => true, odb);
            }
        }

        protected IEnumerable<T> Read<T>(Func<T, bool> predicate)
        {
            using (var odb = OdbFactory.Open(_readModelFilePath))
            {
                return Read(predicate, odb);
            }
        }

        protected IEnumerable<T> Read<T>(Func<T, bool> predicate, NDatabase.Api.IOdb odb)
        {
            if (odb == null)
                return Read(predicate);

            return odb.QueryAndExecute<T>()
                .Where(p => predicate(p));    
        }

        protected void Update<T>(Guid id, Action<T> func)
            where T : AEntity
        {
            Update<T>(id, (t, odb) => { func(t); });
        }

        protected void Update<T>(Guid id, Action<T, NDatabase.Api.IOdb> func)
            where T : AEntity
        {
            using (var odb = OdbFactory.Open(_readModelFilePath))
            {
                var entity = odb.AsQueryable<T>()
                    .FirstOrDefault(p => p.Id.Equals(id));

                if (entity == null)
                    return;

                func(entity, odb);
                odb.Store(entity);
            }
        }

        protected void Delete<T>(Guid id)
            where T : AEntity
        {
            using (var odb = OdbFactory.Open(_readModelFilePath))
            {
                var entity = odb.AsQueryable<T>()
                    .FirstOrDefault(p => p.Id.Equals(id));

                if (entity == null)
                    return;

                odb.Delete(entity);
            }
        }
    }
}
