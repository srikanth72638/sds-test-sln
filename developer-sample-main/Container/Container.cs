using System;
using System.Collections.Generic;

namespace DeveloperSample.Container
{
    public class Container
    {
        private readonly Dictionary<Type, Type> _bindings = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();

        public void Bind(Type interfaceType, Type implementationType)
        {
            if (!interfaceType.IsAssignableFrom(implementationType))
            {
                throw new ArgumentException($"{implementationType.Name} does not implement {interfaceType.Name}");
            }

            _bindings[interfaceType] = implementationType;
        }

        public T Get<T>()
        {
            var type = typeof(T);

            if (_instances.TryGetValue(type, out var instance))
            {
                return (T)instance;
            }

            if (_bindings.TryGetValue(type, out var implementationType))
            {
                var constructor = implementationType.GetConstructors()[0];
                var parameters = constructor.GetParameters();
                var args = new object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    args[i] = Get(parameters[i].ParameterType);
                }

                var newInstance = Activator.CreateInstance(implementationType, args);
                _instances[type] = newInstance;
                return (T)newInstance;
            }

            throw new InvalidOperationException($"No binding found for type {type.Name}");
        }

        private object Get(Type serviceType)
        {
            if (serviceType.IsGenericType && _bindings.ContainsKey(serviceType.GetGenericTypeDefinition()))
            {
                var genericDefinition = serviceType.GetGenericTypeDefinition();
                var genericImplementation = _bindings[genericDefinition];
                var concreteType = genericImplementation.MakeGenericType(serviceType.GetGenericArguments());
                return Activator.CreateInstance(concreteType);
            }

            if (_bindings.TryGetValue(serviceType, out var implementationType))
            {
                return Activator.CreateInstance(implementationType);
            }

            throw new InvalidOperationException($"No binding found for type {serviceType.Name}");
        }
    }
}