﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Crime;
//
//    var crimeUsa = CrimeUsa.FromJson(jsonString);

namespace Crime
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class CrimeUsa
    {
        [JsonProperty("instanceid")]
        public string Instanceid { get; set; }

        [JsonProperty("incident_no")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long IncidentNo { get; set; }

        [JsonProperty("date_reported")]
        public DateTimeOffset DateReported { get; set; }

        [JsonProperty("date_from")]
        public DateTimeOffset DateFrom { get; set; }

        [JsonProperty("date_to")]
        public DateTimeOffset DateTo { get; set; }

        [JsonProperty("clsd")]
        public string Clsd { get; set; }

        [JsonProperty("ucr")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Ucr { get; set; }

        [JsonProperty("dst")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Dst { get; set; }

        [JsonProperty("beat")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Beat { get; set; }

        [JsonProperty("offense")]
        public string Offense { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("hate_bias")]
        public string HateBias { get; set; }

        [JsonProperty("dayofweek")]
        public string Dayofweek { get; set; }

        [JsonProperty("rpt_area")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long RptArea { get; set; }

        [JsonProperty("cpd_neighborhood")]
        public string CpdNeighborhood { get; set; }

        [JsonProperty("weapons")]
        public string Weapons { get; set; }

        [JsonProperty("date_of_clearance")]
        public DateTimeOffset DateOfClearance { get; set; }

        [JsonProperty("hour_from")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long HourFrom { get; set; }

        [JsonProperty("hour_to")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long HourTo { get; set; }

        [JsonProperty("address_x")]
        public string AddressX { get; set; }

        [JsonProperty("longitude_x")]
        public string LongitudeX { get; set; }

        [JsonProperty("latitude_x")]
        public string LatitudeX { get; set; }

        [JsonProperty("victim_age")]
        public string VictimAge { get; set; }

        [JsonProperty("victim_ethnicity")]
        public string VictimEthnicity { get; set; }

        [JsonProperty("victim_gender")]
        public string VictimGender { get; set; }

        [JsonProperty("suspect_age")]
        public string SuspectAge { get; set; }

        [JsonProperty("suspect_ethnicity")]
        public string SuspectEthnicity { get; set; }

        [JsonProperty("suspect_gender")]
        public string SuspectGender { get; set; }

        [JsonProperty("totalnumbervictims")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Totalnumbervictims { get; set; }

        [JsonProperty("totalsuspects")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Totalsuspects { get; set; }

        [JsonProperty("ucr_group")]
        public string UcrGroup { get; set; }

        [JsonProperty("zip")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Zip { get; set; }

        [JsonProperty("community_council_neighborhood")]
        public string CommunityCouncilNeighborhood { get; set; }

        [JsonProperty("sna_neighborhood")]
        public string SnaNeighborhood { get; set; }

        internal int Count()
        {
            throw new NotImplementedException();
        }
    }

    public partial class CrimeUsa
    {
        public static CrimeUsa[] FromJson(string json) => JsonConvert.DeserializeObject<CrimeUsa[]>(json, Crime.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CrimeUsa[] self) => JsonConvert.SerializeObject(self, Crime.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            else
            {
                // Handle the exception by providing a default or fallback value
                return 0L;
            }
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}