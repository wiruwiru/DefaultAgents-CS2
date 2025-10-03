using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;

namespace DefaultAgents.Config;

public class BaseConfigs : BasePluginConfig
{
    [JsonPropertyName("PermissionFlag")]
    public string PermissionFlag { get; set; } = "@css/root";

    [JsonPropertyName("EnableDefaultAgentsOnStart")]
    public bool EnableDefaultAgentsOnStart { get; set; } = true;

    [JsonPropertyName("EnableHeavySkinsOnStart")]
    public bool EnableHeavySkinsOnStart { get; set; } = false;

    [JsonPropertyName("EnableDebug")]
    public bool EnableDebug { get; set; } = false;

    [JsonPropertyName("ConfigVersion")]
    public override int Version { get; set; } = 1;
}