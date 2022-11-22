using System.Reflection;

namespace Toolbox.Store.Components
{
    public class Navigator
    {
        private readonly object _root;

        public Navigator(object root)
        {
            _root = root;
        }

        public Entity FindEntity(Guid guid)
        {
            var entity = GetEntity(_root, guid);

            if(entity is null)
            {
                throw new Exception($"no entity with id {guid} is part of the tree");
            }

            return entity;
        }

        private Entity GetEntity(object lookIn, Guid guid)
        {
            // in object
            var entity = lookIn.GetType()
                .GetProperties()
                .Where(p => p.PropertyType.BaseType == typeof(Entity))                
                .Select(p => p.GetValue(lookIn))
                .Where(o => IsMatch(o, guid))
                .FirstOrDefault();

            if(entity != null)
            {
                return entity as Entity;
            }

            // in nested objects
            entity = GetEntities(lookIn)
                .Select(e => 
                {
                    var target = GetEntity(e, guid);
                    if (target != null) 
                    {
                        return target;                        
                    } 
                    return null; 
                }).FirstOrDefault();

            if (entity != null)
            {
                return entity as Entity;
            }

            // in collections

            var a = GetCollections(lookIn);
            return GetCollections(lookIn)
                .Select(e =>
                {
                    var target = GetEntity(e, guid);
                    if (target != null)
                    {
                        return target;
                    }
                    return null;
                }).FirstOrDefault();
                
        }

        private IEnumerable<object> GetEntities(object obj)
        {
            return obj.GetType()
                .GetProperties()
                .Where(p => p.PropertyType.BaseType == typeof(Entity))
                .Select(p => p.GetValue(obj))
                .Where(p => p != null);
        }
        
        private IEnumerable<object> GetCollections(object obj)
        {
            return obj.GetType()
                .GetProperties()
                .Where(p => p.PropertyType.IsGenericType)                
                .Where(p => p.PropertyType.GenericTypeArguments.Any(t => t.BaseType == typeof(Entity)))
                .Select(p => (IEnumerable<object>)p.GetValue(obj))
                .SelectMany(c => c)
                .Where(e => e != null);
        }

        private Guid GetId(PropertyInfo propertyInfo, object obj)
        {
            var entity = propertyInfo
                .GetValue(obj);
            return (Guid)entity
                .GetType()
                .GetProperties()
                .Where(p => p.Name == "Id")
                .First()
                .GetValue(entity);
        }
        private bool IsMatch(object obj, Guid guid)
        {
            if(obj is null)
            {
                return false;
            }
            
            if ((Guid)obj.GetType().GetProperty("Id").GetValue(obj) == guid)
            {
                return true;
            }

            return false;
        }
    }
}