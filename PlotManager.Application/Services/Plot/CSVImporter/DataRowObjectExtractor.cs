using System;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace PlotManager.Application.Services
{
    internal class DataRowObjectExtractor<T> where T : class
    {
        public T InitiateObjectFromDataRow(DataRow row)
        {
            var objType = typeof(T);
            var firstConstructor = objType.GetConstructors().First();
            var constructorParameters = firstConstructor.GetParameters();
            T objInstance;
            if (constructorParameters.Count() == 0)
            {
                objInstance = (T)Activator.CreateInstance(objType);
            }
            else
            {
                throw new Exception($"Parameterless constructor not available for type: {objType}");
            }
            var objProperties = typeof(T).GetProperties();
            foreach (var property in objProperties)
            {
                if (row.Table.Columns.Contains(property.Name))
                {
                    var value = GetValueForProperty(property.PropertyType, row[property.Name].ToString());
                    property.SetValue(objInstance, value);
                }
            }
            return objInstance;
        }

        private object GetValueForProperty(Type type, string value)
        {
            if (type == typeof(string))
            {
                return value;
            }
            var converter = TypeDescriptor.GetConverter(type);
            return converter.ConvertFromInvariantString(value);
        }
    }
}