# DefaultAgents CS2
DefaultAgents is a lightweight plugin that provides comprehensive control over player agent models with the ability to toggle between default and heavy skins during runtime.

> [!NOTE]
> DefaultAgents is **fully compatible** with WeaponPaints plugin. Both plugins can run simultaneously without conflicts

## 🚀 Installation
1. Install [CounterStrike Sharp](https://github.com/roflmuffin/CounterStrikeSharp) and [Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master)
2. Download [DefaultAgents.zip](https://github.com/wiruwiru/DefaultAgents-CS2/releases/latest) from releases
3. Extract and upload to your game server: `csgo/addons/counterstrikesharp/plugins/DefaultAgents/`
4. Start server and configure the generated config file at `csgo/addons/counterstrikesharp/configs/plugins/DefaultAgents/`

---

## 📋 Configuration parameters
| Parameter         | Description                                                                                        | Required |
|-------------------|----------------------------------------------------------------------------------------------------|----------|
| `PermissionFlag`  | Permission flag required to use admin commands. (**Default**: `"@css/root"`)                    | **YES**  |
| `EnableDefaultAgentsOnStart` | Enable default agents when the plugin loads. (**Default**: `true`)                   | **YES**  |
| `EnableHeavySkinsOnStart` | Enable heavy model skins when the plugin loads. (**Default**: `false`)                  | **YES**  |
| `EnableDebug`     | Enable detailed logging for troubleshooting. (**Default**: `false`)                            | **YES**  |

---

## 🎮 Available Commands
| Command | Description | Permission Required |
|---------|-------------|---------------------|
| `css_default_skins` | Toggle default skins on/off for all players | Yes (configured in `PermissionFlag`) |
| `css_heavy_models` | Toggle heavy model skins on/off for all players | Yes (configured in `PermissionFlag`) |
| `css_agents_status` | Display current status of default skins and heavy models | Yes (configured in `PermissionFlag`) |

---

## 📝 Support
For issues, questions, or feature requests, please visit our [GitHub Issues](https://github.com/wiruwiru/DefaultAgents-CS2) page.
## 📊 Credits
This plugin is inspired by the original concept from [Challengermode's DefaultSkins](https://github.com/Challengermode/cm-cs2-defaultskins).