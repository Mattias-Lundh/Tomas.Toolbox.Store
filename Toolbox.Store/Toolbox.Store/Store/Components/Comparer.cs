using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Toolbox.Store.Extensions;

namespace Toolbox.Store.Components
{
    public class Comparer
    {
        public bool EqualTo(Entity subject, Entity contrast)
        {
            // handle null

            if (subject == null && contrast == null)
            {
                return true;
            }

            if (subject == null || contrast == null)
            {
                return false;
            }

            // both are not null

            if(subject.GetType() != contrast.GetType())
            {
                return false;
            }

            var props = subject.GetType().GetProperties();
            var simples = props.Where(p => p.PropertyType.IsSimple());
            
            var hasSimpleEquality = simples.Select(p => new Tuple<object, object>(p.GetValue(subject), p.GetValue(contrast)))
                .All(pairs => Equals(pairs.Item1, pairs.Item2));

            if (!hasSimpleEquality)
            {
                return false;
            }

            var entities = props.Where(p => p.PropertyType.BaseType == typeof(Entity));
            var hasEntityEquality = entities.Select(p =>new Tuple<Entity, Entity>(
                p.GetValue(subject) as Entity, 
                p.GetValue(contrast) as Entity))
                .All(pairs => EqualTo(pairs.Item1, pairs.Item2));

            if (!hasEntityEquality)
            {
                return false;
            }

            var collections = FilterGenericEntity(props);            

            var hasCollectionEquality = collections.Select(p => new Tuple<IEnumerable<Entity>, IEnumerable<Entity>>(
                p.GetValue(subject) as IEnumerable<Entity>,
                p.GetValue(contrast) as IEnumerable<Entity>))
                .All(pairs => EqualCollections(pairs));

            if (!hasCollectionEquality)
            {
                return false;
            }

            return true;
        }

        private IEnumerable<PropertyInfo> FilterGenericEntity(IEnumerable<PropertyInfo> properties)
        {
            return properties.Where(p => p.PropertyType.GenericTypeArguments
                                            .Select(g => g.BaseType)
                                            .Contains(typeof(Entity)));
        }

        private bool EqualCollections(Tuple<IEnumerable<Entity>, IEnumerable<Entity>> collections)
        {
            var col1 = collections.Item1.ToList();
            var col2 = collections.Item2.ToList();

            if(col1 == null && col2 == null)
            {
                return true;
            }

            if(
                (col1 == null && col2 != null) ||
                (col1 != null && col2 == null)
              )
            {
                return false;
            }

            if(col1.Count != col2.Count)
            {
                return false;
            }

            for(var i = 0; i < col1.Count(); i++)
            {
                if (!EqualTo(col1[i], col2[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
