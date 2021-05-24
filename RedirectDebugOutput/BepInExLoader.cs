using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using CellMenu;
using HarmonyLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace RedirectDebugOutput
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class BepInExLoader : BasePlugin
    {
        private bool _isInjected = false;

        public const string
         MODNAME = "ExternalLogger",
         AUTHOR = "Endskill",
         GUID = AUTHOR + "." + MODNAME,
         VERSION = "1.0.0";

        public static ConfigEntry<bool> IsActiveLocal;
        public static ConfigEntry<string> NetworkingPcName;

        public override void Load()
        {
            BepInExLoader.IsActiveLocal = Config.Bind("Networking", "IsActive", true, "Is the entire redirection Inactive, Useful for Devs or Error Handling");
            BepInExLoader.NetworkingPcName = Config.Bind("Networking", "PcName", "LocalHost", "Where it should send the Messages (Laptop in the same LAN as an example)");

            new Harmony("ExternalLogger").PatchAll();

            Start();
        }

        public void Start()
        {
            if (!_isInjected)
            {
                if (IsActiveLocal.Value)
                {
                    if (!(Process.GetProcessesByName(Path.GetFileNameWithoutExtension("RedirectDebugMessages.exe")).Length > 0))
                    {
                        Process.Start(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ExternalLogger\RedirectDebugMessages.exe"));
                    }
                }
                else
                {
                    TempLog.Debug("Somehow Deactivated o.o?");
                }
            }
        }

        //[HarmonyPatch(typeof(CM_PageRundown_New), nameof(CM_PageRundown_New.PlaceRundown))]
        //public class PrepareInjection
        //{
        //    private static bool _injected = false;
        //    private static Process _externalLogger;

        //    [HarmonyPostfix]
        //    public static void PostFix()
        //    {
        //        if (!_injected)
        //        {
        //            if (IsActiveLocal.Value)
        //            {
        //                Process[] processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension("RedirectDebugMessages.exe"));
        //                if (processes.Length > 0)
        //                {
        //                    processes[0].Kill();
        //                }

        //                //TempLog.Debug($"Starting exe \n{Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ExternalLogger\RedirectDebugMessages.exe")}");
        //                _externalLogger = Process.Start(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ExternalLogger\RedirectDebugMessages.exe"));
        //                var gtfoProcess = Process.GetProcessesByName(Path.GetFileNameWithoutExtension("GTFO.exe"))[0];

        //                gtfoProcess.Exited += GtfoProcess_Exited;
        //            }
        //            else
        //            {
        //                TempLog.Debug("Somehow Deactivated o.o?");
        //            }
        //        }
        //    }

        //    private static void GtfoProcess_Exited(object sender, EventArgs e)
        //    {
        //        _externalLogger.Kill();
        //    }
        //}
    }
}
