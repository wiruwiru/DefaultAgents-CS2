using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Core.Attributes.Registration;

using DefaultAgents.Utils;
using DefaultAgents.Config;
using DefaultAgents.Commands;

namespace DefaultAgents;

public class DefaultAgents : BasePlugin, IPluginConfig<BaseConfigs>
{
    public override string ModuleName => "DefaultAgents";
    public override string ModuleAuthor => "luca.uy";
    public override string ModuleVersion => "1.0.0";

    public static readonly string ModelCtmHeavy = "ctm_heavy/ctm_heavy";
    public static readonly string ModelCtmSas = "ctm_sas/ctm_sas";
    public static readonly string ModelTmHeavy = "tm_phoenix_heavy/tm_phoenix_heavy";
    public static readonly string ModelTmPhoenix = "tm_phoenix/tm_phoenix";

    public bool EnableDefaultAgents { get; set; }
    public bool HeavySkins { get; set; }

    public required BaseConfigs Config { get; set; }
    private AgentCommands? agentCommands;

    public void OnConfigParsed(BaseConfigs config)
    {
        Config = config;
        Debug.Config = config;
        EnableDefaultAgents = config.EnableDefaultAgentsOnStart;
        HeavySkins = config.EnableHeavySkinsOnStart;
    }

    public override void Load(bool hotReload)
    {
        RegisterListener<Listeners.OnMapStart>(map =>
        {
            Server.PrecacheModel($"characters/models/{ModelCtmHeavy}.vmdl");
            Server.PrecacheModel($"characters/models/{ModelCtmSas}.vmdl");
            Server.PrecacheModel($"characters/models/{ModelTmHeavy}.vmdl");
            Server.PrecacheModel($"characters/models/{ModelTmPhoenix}.vmdl");
            Debug.DebugMessage("Models precached on map start.");
        });

        RegisterEventHandler<EventPlayerSpawn>(OnPlayerSpawnEvent);
        RegisterListener<Listeners.OnMapEnd>(() => Unload(true));

        InitializeCommands();
        RegisterCommands();
    }

    private void InitializeCommands()
    {
        agentCommands = new AgentCommands(this, Config, Localizer);
    }

    private void RegisterCommands()
    {
        AddCommand("css_default_skins", "Toggle default skins", agentCommands!.ToggleDefaultSkinsCommand);
        AddCommand("css_heavy_models", "Toggle heavy model skins", agentCommands!.ToggleHeavyModelsCommand);
        AddCommand("css_agents_status", "Check current agents status", agentCommands!.StatusCommand);
    }

    [GameEventHandler]
    public HookResult OnPlayerSpawnEvent(EventPlayerSpawn @event, GameEventInfo info)
    {
        if (@event == null || !EnableDefaultAgents)
        {
            Debug.DebugMessage("Skipping OnPlayerSpawnEvent: event null or mod disabled.");
            return HookResult.Continue;
        }

        CCSPlayerController? player = @event.Userid;
        if (player == null || !player.IsValid)
        {
            Debug.DebugMessage("Skipping OnPlayerSpawnEvent: invalid player.");
            return HookResult.Continue;
        }

        if (player.PlayerPawn.Value == null)
        {
            Debug.DebugMessage("Skipping OnPlayerSpawnEvent: PlayerPawn is null.");
            return HookResult.Continue;
        }

        Debug.DebugMessage($"Applying skin to player {player.PlayerName} after spawn.");
        GivePlayerAgent(player);

        return HookResult.Continue;
    }

    public void GivePlayerAgent(CCSPlayerController player)
    {
        if (!EnableDefaultAgents)
        {
            Debug.DebugMessage("GivePlayerAgent skipped: DefaultAgents disabled.");
            return;
        }

        if (player == null || !player.IsValid)
        {
            Debug.DebugMessage("GivePlayerAgent skipped: Invalid player.");
            return;
        }

        if (player.PlayerPawn.Value == null)
        {
            Debug.DebugMessage("GivePlayerAgent skipped: PlayerPawn is null.");
            return;
        }

        string? model = null;

        if (player.TeamNum == (byte)CsTeam.CounterTerrorist)
        {
            model = HeavySkins ? ModelCtmHeavy : ModelCtmSas;
        }
        else if (player.TeamNum == (byte)CsTeam.Terrorist)
        {
            model = HeavySkins ? ModelTmHeavy : ModelTmPhoenix;
        }

        if (string.IsNullOrEmpty(model))
        {
            Debug.DebugMessage("GivePlayerAgent skipped: model is null.");
            return;
        }

        try
        {
            AddTimer(0.5f, () =>
            {
                if (player != null && player.IsValid && player.PlayerPawn.Value != null && player.PlayerPawn.Value.IsValid)
                {
                    player.PlayerPawn.Value.SetModel($"characters/models/{model}.vmdl");
                    Debug.DebugMessage($"Model applied to player {player.PlayerName}: {model}");
                }
                else
                {
                    Debug.DebugMessage($"Skipping model apply for {player?.PlayerName ?? "unknown"}: PlayerPawn invalid.");
                }
            });
        }
        catch (Exception ex)
        {
            Debug.DebugError($"Error applying model: {ex.Message}");
        }
    }
}