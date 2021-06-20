﻿/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Hl7.Fhir.Serialization
{
    public static class FhirJsonBuilderExtensions
    {
        /// <inheritdoc cref="writeToAsync(JObject, JsonWriter, string)" />
        [Obsolete("Use writeToAsync(JObject, JsonWriter, string) instead.")]
        private static void writeTo(this JObject root, JsonWriter destination, string rootName = null)
        {
            root.WriteTo(destination);
            destination.Flush();
        }

        private static async Task writeToAsync(this JObject root, JsonWriter destination, string rootName = null)
        {
            await root.WriteToAsync(destination);
            await destination.FlushAsync();
        }

        /// <inheritdoc cref="WriteToAsync(ITypedElement, JsonWriter, FhirJsonSerializationSettings)" />
        [Obsolete("Use WriteToAsync(ITypedElement, JsonWriter, FhirJsonSerializationSettings) instead.")]
        public static void WriteTo(this ITypedElement source, JsonWriter destination, FhirJsonSerializationSettings settings = null) =>
            new FhirJsonBuilder(settings).Build(source).writeTo(destination);

        public static async Task WriteToAsync(this ITypedElement source, JsonWriter destination, FhirJsonSerializationSettings settings = null) =>
            await new FhirJsonBuilder(settings).Build(source).writeToAsync(destination);

        /// <inheritdoc cref="WriteToAsync(ISourceNode, JsonWriter, FhirJsonSerializationSettings)" />
        [Obsolete("Use WriteToAsync(ISourceNode, JsonWriter, FhirJsonSerializationSettings) instead.")]
        public static void WriteTo(this ISourceNode source, JsonWriter destination, FhirJsonSerializationSettings settings = null) =>
            new FhirJsonBuilder(settings).Build(source).writeTo(destination);

        public static async Task WriteToAsync(this ISourceNode source, JsonWriter destination, FhirJsonSerializationSettings settings = null) =>
            await new FhirJsonBuilder(settings).Build(source).writeToAsync(destination);

        public static JObject ToJObject(this ISourceNode source, FhirJsonSerializationSettings settings = null) =>
            new FhirJsonBuilder(settings).Build(source);

        public static JObject ToJObject(this ITypedElement source, FhirJsonSerializationSettings settings = null) =>
            new FhirJsonBuilder(settings).Build(source);

        /// <inheritdoc cref="ToJsonAsync(ITypedElement, FhirJsonSerializationSettings)" />
        [Obsolete("Use ToJsonAsync(ITypedElement, FhirJsonSerializationSettings) instead.")]
        public static string ToJson(this ITypedElement source, FhirJsonSerializationSettings settings = null)
            => SerializationUtil.WriteJsonToString(writer => source.WriteTo(writer, settings), settings?.Pretty ?? false, settings?.AppendNewLine ?? false);

        public static async Task<string> ToJsonAsync(this ITypedElement source, FhirJsonSerializationSettings settings = null)
            => await SerializationUtil.WriteJsonToStringAsync(async writer => await source.WriteToAsync(writer, settings), settings?.Pretty ?? false, settings?.AppendNewLine ?? false);

        /// <inheritdoc cref="ToJsonAsync(ISourceNode, FhirJsonSerializationSettings)" />
        [Obsolete("Use ToJsonAsync(ISourceNode, FhirJsonSerializationSettings) instead.")]
        public static string ToJson(this ISourceNode source, FhirJsonSerializationSettings settings = null)
            => SerializationUtil.WriteJsonToString(writer => source.WriteTo(writer, settings), settings?.Pretty ?? false, settings?.AppendNewLine ?? false);

        public static async Task<string> ToJsonAsync(this ISourceNode source, FhirJsonSerializationSettings settings = null)
            => await SerializationUtil.WriteJsonToStringAsync(async writer => await source.WriteToAsync(writer, settings), settings?.Pretty ?? false, settings?.AppendNewLine ?? false);

        /// <inheritdoc cref="ToJsonBytesAsync(ITypedElement, FhirJsonSerializationSettings)" />
        [Obsolete("Use ToJsonBytesAsync(ITypedElement, FhirJsonSerializationSettings) instead.")]
        public static byte[] ToJsonBytes(this ITypedElement source, FhirJsonSerializationSettings settings = null)
                => SerializationUtil.WriteJsonToBytes(writer => source.WriteTo(writer, settings));

        public static async Task<byte[]> ToJsonBytesAsync(this ITypedElement source, FhirJsonSerializationSettings settings = null)
                => await SerializationUtil.WriteJsonToBytesAsync(async writer => await source.WriteToAsync(writer, settings));
    }
}
