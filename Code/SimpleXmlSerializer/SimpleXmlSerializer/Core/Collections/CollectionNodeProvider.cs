using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimpleXmlSerializer.Extensions;

namespace SimpleXmlSerializer.Core
{
    public class CollectionNodeProvider : ICollectionNodeProvider
    {
        private static readonly HashSet<Type> collectionTypes = new HashSet<Type>
            {
                typeof(IEnumerable), typeof(IEnumerable<>), 
                typeof(ICollection), typeof(ICollection<>), typeof(Collection<>),
                typeof(IList), typeof(IList<>), typeof(List<>)
            };

        private static readonly HashSet<Type> dictionaryTypes = new HashSet<Type>
            {
                typeof(IDictionary), typeof(IDictionary<,>), typeof(Dictionary<,>)
            };

        public bool TryGetDescription(Type valueType, out CollectionNodeDescription collectionDescription)
        {
            if (valueType.IsGenericType)
            {
                var genericTypeDefinition = valueType.GetGenericTypeDefinition();
                
                if (collectionTypes.Contains(genericTypeDefinition.UnderlyingSystemType))
                {
                    var genericArguments = valueType.GetGenericArguments();
                    collectionDescription = new CollectionNodeDescription(genericArguments[0],
                        
                            items =>
                                {
                                    var value = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(genericArguments));
                                    value.AddRange(items);
                                    return value;
                                }
                        );
                    return true;
                }

                if (dictionaryTypes.Contains(genericTypeDefinition.UnderlyingSystemType))
                {
                    var genericArguments = valueType.GetGenericArguments();
                    var itemType = typeof(KeyValuePair<,>).MakeGenericType(genericArguments);
                    collectionDescription = new CollectionNodeDescription(itemType,
                            items =>
                                {
                                    var type = typeof(Dictionary<,>).MakeGenericType(genericArguments);
                                    var collectionType = typeof(ICollection<>).MakeGenericType(itemType);
                                    var value = Activator.CreateInstance(type);
                                    var add = collectionType.GetMethod("Add", new[] { itemType });
                                    foreach (var item in items)
                                    {
                                        add.Invoke(value, new []{ item });
                                    }

                                    return value;
                                }
                        );
                    return true;
                }
            }

            if (valueType.IsArray)
            {
                var elementType = valueType.GetElementType();
                collectionDescription = new CollectionNodeDescription(elementType,
                        items =>
                            {
                                var value = Array.CreateInstance(elementType, items.Count);
                                items.CopyTo(value, 0);
                                return value;
                            }
                        );

                return true;
            }

            collectionDescription = null;
            return false;
        }
    }
}