using BepInEx;
using UnityEngine;
using System.Collections.Generic;
using BepInEx.Configuration;
using HarmonyLib;
using ServerSync;

namespace RestrictedAreas
{
    [BepInPlugin(PluginGUID, PluginGUID, Version)]
    public class RestrictedAreas : BaseUnityPlugin
    {
        public const string PluginGUID = "Detalhes.RestrictedAreas";
        public const string Name = "RestrictedAreas";
        public const string Version = "1.0.0";

        public static ConfigEntry<string> RestrictedAreasList;

        ConfigSync configSync = new ConfigSync("Detalhes.RestrictedAreas") { DisplayName = "RestrictedAreas", CurrentVersion = Version, MinimumRequiredVersion = Version };

        Harmony _harmony = new Harmony(PluginGUID);

        public static GameObject Menu;

        public void Awake()
        {
            InitConfigs();
            _harmony.PatchAll();
        }


        public void InitConfigs()
        {
            RestrictedAreasList = config("Server config", "RestrictedAreas", "x=100;y=100;radius=300;permittedIds=76561198053366464,7656119833896646;permissions=noBuild,noTerrain,noInteract,noTreeDamage | x=100;y=100;radius=300;permittedIds=76561198053366464,7656119833896646;permissions=noBuild,noTerrain,noInteract,noTreeDamage",
                new ConfigDescription("Example: x=100;y=100;radius=300;permittedIds=76561198053366464,7656119833896646;permissions=noBuild,noTerrain,noInteract,noTreeDamage", null));
        }

        ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description, bool synchronizedSetting = true)
        {
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = configSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }

        ConfigEntry<T> config<T>(string group, string name, T value, string description, bool synchronizedSetting = true) => config(group, name, value, new ConfigDescription(description), synchronizedSetting);

    }
}
