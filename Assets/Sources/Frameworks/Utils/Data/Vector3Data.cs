﻿
using Newtonsoft.Json;

namespace Sources.Frameworks.Utils.Data
{
    public class Vector3Data
    {
        [JsonProperty("x")]
        public float X { get; set; }

        [JsonProperty("y")]
        public float Y { get; set; }

        [JsonProperty("z")]
        public float Z { get; set; }
    }
}