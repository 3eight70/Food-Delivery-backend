﻿using System.Text.Json.Serialization;

namespace webNET_Hits_backend_aspnet_project_1.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    InProcess,
    Delivered
}