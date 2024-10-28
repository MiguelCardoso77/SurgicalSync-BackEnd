using System;
using DDDNetCore.Domain.Shared;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDDNetCore.Infraestructure.Shared;

public class GenericValueConverter<TValueObject> : ValueConverter<TValueObject, string> where TValueObject : IValueObject<string>
{
    public GenericValueConverter(ConverterMappingHints mappingHints = null) : base(
            vo => vo.Value,
            value => (TValueObject)Activator.CreateInstance(typeof(TValueObject), value),
            mappingHints)
    {
    }
}