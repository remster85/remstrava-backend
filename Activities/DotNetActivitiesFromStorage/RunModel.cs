namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class RunModel
    {
        [JsonProperty("resource_state")]
        public long ResourceState { get; set; }

        [JsonProperty("athlete")]
        public Athlete Athlete { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("moving_time")]
        public long MovingTime { get; set; }

        [JsonProperty("elapsed_time")]
        public long ElapsedTime { get; set; }

        [JsonProperty("total_elevation_gain")]
        public double TotalElevationGain { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("workout_type")]
        public long? WorkoutType { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("upload_id")]
        public long? UploadId { get; set; }

        [JsonProperty("start_date")]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("start_date_local")]
        public String StartDateLocal { get; set; }

        [JsonProperty("timezone")]
        public String Timezone { get; set; }

        [JsonProperty("utc_offset")]
        public long UtcOffset { get; set; }

        [JsonProperty("start_latlng")]
        public double[] StartLatlng { get; set; }

        [JsonProperty("end_latlng")]
        public double[] EndLatlng { get; set; }

        [JsonProperty("location_city")]
        public object LocationCity { get; set; }

        [JsonProperty("location_state")]
        public object LocationState { get; set; }

        [JsonProperty("location_country")]
        public string LocationCountry { get; set; }

        [JsonProperty("start_latitude")]
        public double? StartLatitude { get; set; }

        [JsonProperty("start_longitude")]
        public double? StartLongitude { get; set; }

        [JsonProperty("achievement_count")]
        public long AchievementCount { get; set; }

        [JsonProperty("kudos_count")]
        public long KudosCount { get; set; }

        [JsonProperty("comment_count")]
        public long CommentCount { get; set; }

        [JsonProperty("athlete_count")]
        public long AthleteCount { get; set; }

        [JsonProperty("photo_count")]
        public long PhotoCount { get; set; }

        [JsonProperty("map")]
        public Map Map { get; set; }

        [JsonProperty("trainer")]
        public bool Trainer { get; set; }

        [JsonProperty("commute")]
        public bool Commute { get; set; }

        [JsonProperty("manual")]
        public bool Manual { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("visibility")]
        public Visibility Visibility { get; set; }

        [JsonProperty("flagged")]
        public bool Flagged { get; set; }

        [JsonProperty("from_accepted_tag")]
        public bool? FromAcceptedTag { get; set; }

        [JsonProperty("upload_id_str", NullValueHandling = NullValueHandling.Ignore)]
        public string UploadIdStr { get; set; }

        [JsonProperty("average_speed")]
        public double AverageSpeed { get; set; }

        [JsonProperty("max_speed")]
        public double MaxSpeed { get; set; }

        [JsonProperty("has_heartrate")]
        public bool HasHeartrate { get; set; }

        [JsonProperty("heartrate_opt_out")]
        public bool HeartrateOptOut { get; set; }

        [JsonProperty("display_hide_heartrate_option")]
        public bool DisplayHideHeartrateOption { get; set; }

        [JsonProperty("elev_high", NullValueHandling = NullValueHandling.Ignore)]
        public double? ElevHigh { get; set; }

        [JsonProperty("elev_low", NullValueHandling = NullValueHandling.Ignore)]
        public double? ElevLow { get; set; }

        [JsonProperty("pr_count")]
        public long PrCount { get; set; }

        [JsonProperty("total_photo_count")]
        public long TotalPhotoCount { get; set; }

        [JsonProperty("has_kudoed")]
        public bool HasKudoed { get; set; }

        [JsonProperty("average_watts", NullValueHandling = NullValueHandling.Ignore)]
        public double? AverageWatts { get; set; }

        [JsonProperty("kilojoules", NullValueHandling = NullValueHandling.Ignore)]
        public long? Kilojoules { get; set; }

        [JsonProperty("device_watts", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DeviceWatts { get; set; }

        [JsonProperty("average_cadence", NullValueHandling = NullValueHandling.Ignore)]
        public double? AverageCadence { get; set; }

        [JsonProperty("average_heartrate", NullValueHandling = NullValueHandling.Ignore)]
        public double? AverageHeartrate { get; set; }

        [JsonProperty("max_heartrate", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxHeartrate { get; set; }
    }

    public partial class Athlete
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("resource_state")]
        public long ResourceState { get; set; }
    }

    public partial class Map
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("summary_polyline")]
        public string SummaryPolyline { get; set; }

        [JsonProperty("resource_state")]
        public long ResourceState { get; set; }
    }

    public enum GearId { G3708773, G4379805 };

    public enum Timezone { Gmt0100EuropeAmsterdam, Gmt0100EuropeBudapest, Gmt0100EuropeParis, Gmt0100EuropeRome, Gmt0100EuropeVienna, Gmt0500AmericaNewYork, Gmt0700AsiaHoChiMinh, Gmt0800AmericaLosAngeles, Gmt0800AsiaKualaLumpur };

    public enum Visibility { Everyone };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                GearIdConverter.Singleton,
                TimezoneConverter.Singleton,
                VisibilityConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class GearIdConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(GearId) || t == typeof(GearId?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "g3708773":
                    return GearId.G3708773;
                case "g4379805":
                    return GearId.G4379805;
            }
            throw new Exception("Cannot unmarshal type GearId");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (GearId)untypedValue;
            switch (value)
            {
                case GearId.G3708773:
                    serializer.Serialize(writer, "g3708773");
                    return;
                case GearId.G4379805:
                    serializer.Serialize(writer, "g4379805");
                    return;
            }
            throw new Exception("Cannot marshal type GearId");
        }

        public static readonly GearIdConverter Singleton = new GearIdConverter();
    }

    internal class TimezoneConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Timezone) || t == typeof(Timezone?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "(GMT+01:00) Europe/Amsterdam":
                    return Timezone.Gmt0100EuropeAmsterdam;
                case "(GMT+01:00) Europe/Budapest":
                    return Timezone.Gmt0100EuropeBudapest;
                case "(GMT+01:00) Europe/Paris":
                    return Timezone.Gmt0100EuropeParis;
                case "(GMT+01:00) Europe/Rome":
                    return Timezone.Gmt0100EuropeRome;
                case "(GMT+01:00) Europe/Vienna":
                    return Timezone.Gmt0100EuropeVienna;
                case "(GMT+07:00) Asia/Ho_Chi_Minh":
                    return Timezone.Gmt0700AsiaHoChiMinh;
                case "(GMT+08:00) Asia/Kuala_Lumpur":
                    return Timezone.Gmt0800AsiaKualaLumpur;
                case "(GMT-05:00) America/New_York":
                    return Timezone.Gmt0500AmericaNewYork;
                case "(GMT-08:00) America/Los_Angeles":
                    return Timezone.Gmt0800AmericaLosAngeles;
            }
            throw new Exception("Cannot unmarshal type Timezone");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Timezone)untypedValue;
            switch (value)
            {
                case Timezone.Gmt0100EuropeAmsterdam:
                    serializer.Serialize(writer, "(GMT+01:00) Europe/Amsterdam");
                    return;
                case Timezone.Gmt0100EuropeBudapest:
                    serializer.Serialize(writer, "(GMT+01:00) Europe/Budapest");
                    return;
                case Timezone.Gmt0100EuropeParis:
                    serializer.Serialize(writer, "(GMT+01:00) Europe/Paris");
                    return;
                case Timezone.Gmt0100EuropeRome:
                    serializer.Serialize(writer, "(GMT+01:00) Europe/Rome");
                    return;
                case Timezone.Gmt0100EuropeVienna:
                    serializer.Serialize(writer, "(GMT+01:00) Europe/Vienna");
                    return;
                case Timezone.Gmt0700AsiaHoChiMinh:
                    serializer.Serialize(writer, "(GMT+07:00) Asia/Ho_Chi_Minh");
                    return;
                case Timezone.Gmt0800AsiaKualaLumpur:
                    serializer.Serialize(writer, "(GMT+08:00) Asia/Kuala_Lumpur");
                    return;
                case Timezone.Gmt0500AmericaNewYork:
                    serializer.Serialize(writer, "(GMT-05:00) America/New_York");
                    return;
                case Timezone.Gmt0800AmericaLosAngeles:
                    serializer.Serialize(writer, "(GMT-08:00) America/Los_Angeles");
                    return;
            }
            throw new Exception("Cannot marshal type Timezone");
        }

        public static readonly TimezoneConverter Singleton = new TimezoneConverter();
    }

    internal class VisibilityConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Visibility) || t == typeof(Visibility?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "everyone")
            {
                return Visibility.Everyone;
            }
            throw new Exception("Cannot unmarshal type Visibility");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Visibility)untypedValue;
            if (value == Visibility.Everyone)
            {
                serializer.Serialize(writer, "everyone");
                return;
            }
            throw new Exception("Cannot marshal type Visibility");
        }

        public static readonly VisibilityConverter Singleton = new VisibilityConverter();
    }
}
