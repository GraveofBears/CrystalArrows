using System;
using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using ItemManager;
using LocalizationManager;
using ServerSync;
using UnityEngine;
using PrefabManager = ItemManager.PrefabManager;
using System.Text;


namespace CrystalArrows
{
	[BepInPlugin(ModGUID, ModName, ModVersion)]
	[BepInDependency("org.bepinex.plugins.jewelcrafting")]
	public class CrystalArrows : BaseUnityPlugin
	{
		private const string ModName = "CrystalArrows";
		private const string ModVersion = "1.0.1";
		private const string ModGUID = "org.bepinex.plugins.crystalarrows";


        private static readonly ConfigSync configSync = new(ModName) { DisplayName = ModName, CurrentVersion = ModVersion, MinimumRequiredVersion = ModVersion };

        private static ConfigEntry<Toggle> serverConfigLocked = null!;

        private ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description, bool synchronizedSetting = true)
        {
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = configSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }

        private ConfigEntry<T> config<T>(string group, string name, T value, string description, bool synchronizedSetting = true) => config(group, name, value, new ConfigDescription(description), synchronizedSetting);

        private enum Toggle
        {
            On = 1,
            Off = 0
        }



        public void Awake()
		{
			Localizer.Load();


            Item JC_Crystal_Arrow_Black = new("jewelarrows", "JC_Crystal_Arrow_Black");
            JC_Crystal_Arrow_Black.Configurable = Configurability.Full;
            JC_Crystal_Arrow_Black["Shard Recipe"].Crafting.Add("op_transmution_table", 3);
            JC_Crystal_Arrow_Black["Shard Recipe"].RequiredItems.Add("Shattered_Black_Crystal", 10);
            JC_Crystal_Arrow_Black["Shard Recipe"].RequiredItems.Add("Wood", 5);
            JC_Crystal_Arrow_Black["Shard Recipe"].RequiredItems.Add("Feathers", 2);
            JC_Crystal_Arrow_Black["Shard Recipe"].CraftAmount = 20;
            JC_Crystal_Arrow_Black["Crystal Recipe"].Crafting.Add("op_transmution_table", 3);
            JC_Crystal_Arrow_Black["Crystal Recipe"].RequiredItems.Add("Simple_Black_Socket", 1);
            JC_Crystal_Arrow_Black["Crystal Recipe"].RequiredItems.Add("Wood", 5);
            JC_Crystal_Arrow_Black["Crystal Recipe"].RequiredItems.Add("Feathers", 2);
            JC_Crystal_Arrow_Black["Crystal Recipe"].CraftAmount = 20;
            PrefabManager.RegisterPrefab("jewelarrows", "JC_Projectile_Black");

            Item JC_Crystal_Arrow_Blue = new("jewelarrows", "JC_Crystal_Arrow_Blue");
            JC_Crystal_Arrow_Blue.Configurable = Configurability.Full;
            JC_Crystal_Arrow_Blue["Shard Recipe"].Crafting.Add("op_transmution_table", 3);
            JC_Crystal_Arrow_Blue["Shard Recipe"].RequiredItems.Add("Shattered_Blue_Crystal", 10);
            JC_Crystal_Arrow_Blue["Shard Recipe"].RequiredItems.Add("Wood", 5);
            JC_Crystal_Arrow_Blue["Shard Recipe"].RequiredItems.Add("Feathers", 2);
            JC_Crystal_Arrow_Blue["Shard Recipe"].CraftAmount = 20;
            JC_Crystal_Arrow_Blue["Crystal Recipe"].Crafting.Add("op_transmution_table", 3);
            JC_Crystal_Arrow_Blue["Crystal Recipe"].RequiredItems.Add("Simple_Blue_Socket", 1);
            JC_Crystal_Arrow_Blue["Crystal Recipe"].RequiredItems.Add("Wood", 5);
            JC_Crystal_Arrow_Blue["Crystal Recipe"].RequiredItems.Add("Feathers", 2);
            JC_Crystal_Arrow_Blue["Crystal Recipe"].CraftAmount = 20;
            PrefabManager.RegisterPrefab("jewelarrows", "JC_Projectile_Blue");

            Item JC_Crystal_Arrow_Green = new("jewelarrows", "JC_Crystal_Arrow_Green");
            JC_Crystal_Arrow_Green.Configurable = Configurability.Full;
            JC_Crystal_Arrow_Green["Shard Recipe"].Crafting.Add("op_transmution_table", 3);
            JC_Crystal_Arrow_Green["Shard Recipe"].RequiredItems.Add("Shattered_Green_Crystal", 10);
            JC_Crystal_Arrow_Green["Shard Recipe"].RequiredItems.Add("Wood", 5);
            JC_Crystal_Arrow_Green["Shard Recipe"].RequiredItems.Add("Feathers", 2);
            JC_Crystal_Arrow_Green["Shard Recipe"].CraftAmount = 20;
            JC_Crystal_Arrow_Green["Crystal Recipe"].Crafting.Add("op_transmution_table", 3);
            JC_Crystal_Arrow_Green["Crystal Recipe"].RequiredItems.Add("Simple_Green_Socket", 1);
            JC_Crystal_Arrow_Green["Crystal Recipe"].RequiredItems.Add("Wood", 5);
            JC_Crystal_Arrow_Green["Crystal Recipe"].RequiredItems.Add("Feathers", 2);
            JC_Crystal_Arrow_Green["Crystal Recipe"].CraftAmount = 20;
            PrefabManager.RegisterPrefab("jewelarrows", "JC_Projectile_Green");

            Item JC_Crystal_Arrow_Orange = new("jewelarrows", "JC_Crystal_Arrow_Orange");
            JC_Crystal_Arrow_Orange.Configurable = Configurability.Full;
            JC_Crystal_Arrow_Orange["Shard Recipe"].Crafting.Add("op_transmution_table", 3);
            JC_Crystal_Arrow_Orange["Shard Recipe"].RequiredItems.Add("Shattered_Orange_Crystal", 10);
            JC_Crystal_Arrow_Orange["Shard Recipe"].RequiredItems.Add("Wood", 5);
            JC_Crystal_Arrow_Orange["Shard Recipe"].RequiredItems.Add("Feathers", 2);
            JC_Crystal_Arrow_Orange["Shard Recipe"].CraftAmount = 20;
            JC_Crystal_Arrow_Orange["Crystal Recipe"].Crafting.Add("op_transmution_table", 3);
            JC_Crystal_Arrow_Orange["Crystal Recipe"].RequiredItems.Add("Simple_Orange_Socket", 1);
            JC_Crystal_Arrow_Orange["Crystal Recipe"].RequiredItems.Add("Wood", 5);
            JC_Crystal_Arrow_Orange["Crystal Recipe"].RequiredItems.Add("Feathers", 2);
            JC_Crystal_Arrow_Orange["Crystal Recipe"].CraftAmount = 20;
            PrefabManager.RegisterPrefab("jewelarrows", "JC_Projectile_Orange");

            Item JC_Crystal_Arrow_Purple = new("jewelarrows", "JC_Crystal_Arrow_Purple");
            JC_Crystal_Arrow_Purple.Configurable = Configurability.Full;
            JC_Crystal_Arrow_Purple["Shard Recipe"].Crafting.Add("op_transmution_table", 3);
            JC_Crystal_Arrow_Purple["Shard Recipe"].RequiredItems.Add("Shattered_Purple_Crystal", 10);
            JC_Crystal_Arrow_Purple["Shard Recipe"].RequiredItems.Add("Wood", 5);
            JC_Crystal_Arrow_Purple["Shard Recipe"].RequiredItems.Add("Feathers", 2);
            JC_Crystal_Arrow_Purple["Shard Recipe"].CraftAmount = 20;
            JC_Crystal_Arrow_Purple["Crystal Recipe"].Crafting.Add("op_transmution_table", 3);
            JC_Crystal_Arrow_Purple["Crystal Recipe"].RequiredItems.Add("Simple_Purple_Socket", 1);
            JC_Crystal_Arrow_Purple["Crystal Recipe"].RequiredItems.Add("Wood", 5);
            JC_Crystal_Arrow_Purple["Crystal Recipe"].RequiredItems.Add("Feathers", 2);
            JC_Crystal_Arrow_Purple["Crystal Recipe"].CraftAmount = 20;
            PrefabManager.RegisterPrefab("jewelarrows", "JC_Projectile_Purple");

            Item JC_Crystal_Arrow_Red = new("jewelarrows", "JC_Crystal_Arrow_Red");
            JC_Crystal_Arrow_Red.Configurable = Configurability.Full;
            JC_Crystal_Arrow_Red["Shard Recipe"].Crafting.Add("op_transmution_table", 3);
            JC_Crystal_Arrow_Red["Shard Recipe"].RequiredItems.Add("Shattered_Red_Crystal", 10);
            JC_Crystal_Arrow_Red["Shard Recipe"].RequiredItems.Add("Wood", 5);
            JC_Crystal_Arrow_Red["Shard Recipe"].RequiredItems.Add("Feathers", 2);
            JC_Crystal_Arrow_Red["Shard Recipe"].CraftAmount = 20;
            JC_Crystal_Arrow_Red["Crystal Recipe"].Crafting.Add("op_transmution_table", 3);
            JC_Crystal_Arrow_Red["Crystal Recipe"].RequiredItems.Add("Simple_Red_Socket", 1);
            JC_Crystal_Arrow_Red["Crystal Recipe"].RequiredItems.Add("Wood", 5);
            JC_Crystal_Arrow_Red["Crystal Recipe"].RequiredItems.Add("Feathers", 2);
            JC_Crystal_Arrow_Red["Crystal Recipe"].CraftAmount = 20;
            PrefabManager.RegisterPrefab("jewelarrows", "JC_Projectile_Red");

            Item JC_Crystal_Arrow_Yellow = new("jewelarrows", "JC_Crystal_Arrow_Yellow");
            JC_Crystal_Arrow_Yellow.Configurable = Configurability.Full;
            JC_Crystal_Arrow_Yellow["Shard Recipe"].Crafting.Add("op_transmution_table", 3);
            JC_Crystal_Arrow_Yellow["Shard Recipe"].RequiredItems.Add("Shattered_Yellow_Crystal", 10);
            JC_Crystal_Arrow_Yellow["Shard Recipe"].RequiredItems.Add("Wood", 5);
            JC_Crystal_Arrow_Yellow["Shard Recipe"].RequiredItems.Add("Feathers", 2);
            JC_Crystal_Arrow_Yellow["Shard Recipe"].CraftAmount = 20;
            JC_Crystal_Arrow_Yellow["Crystal Recipe"].Crafting.Add("op_transmution_table", 3);
            JC_Crystal_Arrow_Yellow["Crystal Recipe"].RequiredItems.Add("Simple_Yellow_Socket", 1);
            JC_Crystal_Arrow_Yellow["Crystal Recipe"].RequiredItems.Add("Wood", 5);
            JC_Crystal_Arrow_Yellow["Crystal Recipe"].RequiredItems.Add("Feathers", 2);
            JC_Crystal_Arrow_Yellow["Crystal Recipe"].CraftAmount = 20;
            PrefabManager.RegisterPrefab("jewelarrows", "JC_Projectile_Yellow");

            PrefabManager.RegisterPrefab("jewelarrows", "sfx_jc_arrow_hit");
            PrefabManager.RegisterPrefab("jewelarrows", "VFX_JC_Arrow_Hit");

            Assembly assembly = Assembly.GetExecutingAssembly();
			Harmony harmony = new(ModGUID);
			harmony.PatchAll(assembly);

		}
		
	}
}
