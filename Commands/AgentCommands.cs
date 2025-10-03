using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Microsoft.Extensions.Localization;

using DefaultAgents.Config;
using DefaultAgents.Utils;

namespace DefaultAgents.Commands;

public class AgentCommands
{
    private readonly DefaultAgents plugin;
    private readonly BaseConfigs config;
    private readonly IStringLocalizer localizer;

    public AgentCommands(DefaultAgents plugin, BaseConfigs config, IStringLocalizer localizer)
    {
        this.plugin = plugin;
        this.config = config;
        this.localizer = localizer;
    }

    public void ToggleDefaultSkinsCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player != null && player.IsValid && !CommandUtils.IsPlayerAuthorized(player, config))
        {
            player.PrintToChat($"{localizer["prefix"]} {localizer["no_permission"]}");
            return;
        }

        plugin.EnableDefaultAgents = !plugin.EnableDefaultAgents;

        string status = plugin.EnableDefaultAgents ? localizer["enabled"] : localizer["disabled"];
        string message = $"{localizer["prefix"]} {localizer["default_skins_status", status]}";

        if (player != null && player.IsValid)
        {
            player.PrintToChat(message);
        }
        else
        {
            command.ReplyToCommand(message);
        }

        Debug.DebugMessage($"Default skins toggled: {plugin.EnableDefaultAgents}");

        if (plugin.EnableDefaultAgents)
        {
            ApplySkinsToAllPlayers();
        }
    }

    public void ToggleHeavyModelsCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player != null && player.IsValid && !CommandUtils.IsPlayerAuthorized(player, config))
        {
            player.PrintToChat($"{localizer["prefix"]} {localizer["no_permission"]}");
            return;
        }

        plugin.HeavySkins = !plugin.HeavySkins;

        string status = plugin.HeavySkins ? localizer["enabled"] : localizer["disabled"];
        string message = $"{localizer["prefix"]} {localizer["heavy_models_status", status]}";

        if (player != null && player.IsValid)
        {
            player.PrintToChat(message);
        }
        else
        {
            command.ReplyToCommand(message);
        }

        Debug.DebugMessage($"Heavy models toggled: {plugin.HeavySkins}");

        if (plugin.EnableDefaultAgents)
        {
            ApplySkinsToAllPlayers();
        }
    }

    public void StatusCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player != null && player.IsValid && !CommandUtils.IsPlayerAuthorized(player, config))
        {
            player.PrintToChat($"{localizer["prefix"]} {localizer["no_permission"]}");
            return;
        }

        string defaultSkinsStatus = plugin.EnableDefaultAgents ? localizer["enabled"] : localizer["disabled"];
        string heavyModelsStatus = plugin.HeavySkins ? localizer["enabled"] : localizer["disabled"];

        if (player != null && player.IsValid)
        {
            player.PrintToChat($"{localizer["prefix"]} {localizer["status_title"]}");
            player.PrintToChat($"{localizer["subprefix"]} {localizer["default_skins_label"]}: {defaultSkinsStatus}");
            player.PrintToChat($"{localizer["subprefix"]} {localizer["heavy_models_label"]}: {heavyModelsStatus}");
        }
        else
        {
            command.ReplyToCommand($"{localizer["prefix"]} {localizer["status_title"]}");
            command.ReplyToCommand($"{localizer["subprefix"]} {localizer["default_skins_label"]}: {defaultSkinsStatus}");
            command.ReplyToCommand($"{localizer["subprefix"]} {localizer["heavy_models_label"]}: {heavyModelsStatus}");
        }
    }

    private void ApplySkinsToAllPlayers()
    {
        var players = Utilities.GetPlayers();
        foreach (var player in players)
        {
            if (player != null && player.IsValid && player.PlayerPawn.Value != null)
            {
                plugin.GivePlayerAgent(player);
            }
        }
    }
}