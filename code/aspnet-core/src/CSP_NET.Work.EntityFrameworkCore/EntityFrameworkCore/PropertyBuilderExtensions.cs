using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CSP_NET.Work.EntityFrameworkCore;
public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<T> IsJson<T>(this PropertyBuilder<T> build) where T : class
    {
        return build.HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<T>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
    }

    public static void JsonConverter<T>(this T build) where T : IMutableProperty
    {
        var valueConverter = new ValueConverter<JObject, string>(
            v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
            v => JsonConvert.DeserializeObject<JObject>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
        );
        build.SetValueConverter(valueConverter);
    }
}

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public class JsonDataAttribute : Attribute { }
