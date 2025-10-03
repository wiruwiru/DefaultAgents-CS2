using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;

using DefaultAgents.Config;

namespace DefaultAgents.Utils
{
    public static class CommandUtils
    {
        public static bool IsPlayerAuthorized(CCSPlayerController player, BaseConfigs config)
        {
            return AdminManager.PlayerHasPermissions(player, config.PermissionFlag);
        }
    }
}