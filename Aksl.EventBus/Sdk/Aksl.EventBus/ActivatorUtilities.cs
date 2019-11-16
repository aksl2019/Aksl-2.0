using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Aksl.EventBus
{
    public static class ActivatorUtilities
    {
        public static object CreateInstance(IServiceProvider provider, Type instanceType, params object[] parameters)
        {
            

          //  ConstructorMatcher bestMatcher = null;

            if (!instanceType.GetTypeInfo().IsAbstract)
            {
                //遍历所有结构体
                foreach (var constructor in instanceType
                                            .GetTypeInfo()
                                            .DeclaredConstructors
                                            .Where(c => !c.IsStatic && c.IsPublic))
                {
                    var construct = constructor;

                }
            }

         

            //  return bestMatcher.CreateInstance(provider);
            return null;
        }

        public static MethodInfo FindMethod(Type eventHandlerType, Type eventType, IntegrationEvent @event, string methodName, object instance, Type returnType = null, bool required = true)
        {
           // var methodName= string.Format(CultureInfo.InvariantCulture, methodName, "");

            var methods = eventHandlerType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            var selectedMethods = methods.Where(method => method.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase)).ToList();
            //if (selectedMethods.Count > 1)
            //{
            //    throw new InvalidOperationException(string.Format("Having multiple overloads of method '{0}' is not supported.", methodNameWithNoEnv));
            //}
            foreach (var methodInfo in selectedMethods)
            {
                var parameterInfos = methodInfo.GetParameters();
                var parameters = new object[parameterInfos.Length];
                for (var index = 0; index < parameterInfos.Length; index++)
                {
                    var parameterInfo = parameterInfos[index];
                    var parameterType = parameterInfo.ParameterType;
                  //  var hasDefaultValue = ParameterDefaultValue.TryGetDefaultValue(parameterInfo, out var defaultValue);

                    if (parameterInfo.ParameterType == eventType)
                    {
                        parameters[index] = @event;

                        methodInfo.Invoke(instance, parameters);
                    }
                }

                // var methodInfo = selectedMethods.FirstOrDefault();
                if (methodInfo == null)
                {
                   

                   if (required)
                    {
                        throw new InvalidOperationException(string.Format("A public method named '{0}' or '{1}' could not be found in the '{2}' type.",
                            methodName,
                            methodName,
                         eventHandlerType.FullName));

                    }
                    return null;
                }
                if (returnType != null && methodInfo.ReturnType != returnType)
                {
                    if (required)
                    {
                        throw new InvalidOperationException(string.Format("The '{0}' method in the type '{1}' must have a return type of '{2}'.",
                            methodInfo.Name,
                            eventHandlerType.FullName,
                            returnType.Name));
                    }
                    return null;
                }

            }

            return null;
        }
    }

    internal class ParameterDefaultValue
    {
        public static bool TryGetDefaultValue(ParameterInfo parameter, out object defaultValue)
        {
            bool hasDefaultValue;
            var tryToGetDefaultValue = true;
            defaultValue = null;

            try
            {
                hasDefaultValue = parameter.HasDefaultValue;
            }
            catch (FormatException) when (parameter.ParameterType == typeof(DateTime))
            {
                // Workaround for https://github.com/dotnet/corefx/issues/12338
                // If HasDefaultValue throws FormatException for DateTime
                // we expect it to have default value
                hasDefaultValue = true;
                tryToGetDefaultValue = false;
            }

            if (hasDefaultValue)
            {
                if (tryToGetDefaultValue)
                {
                    defaultValue = parameter.DefaultValue;
                }

                // Workaround for https://github.com/dotnet/corefx/issues/11797
                if (defaultValue == null && parameter.ParameterType.IsValueType)
                {
                    defaultValue = Activator.CreateInstance(parameter.ParameterType);
                }
            }

            return hasDefaultValue;
        }
    }
}
