using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace Bannerlord.EncyclopediaExtender
{
    internal static class DCCCompatibility
    {
        static void FailedToPatchMessage(string m)
        {
            InformationManager.DisplayMessage(new InformationMessage("Encyclopedia Extender failed to patch Detailed Character Creation, " + m));
        }

        internal static readonly Color Orange = Color.FromUint(0x00F16D26);

        public static void DCCPatcher(Harmony harmony)
        {
            if (!Utilities.GetModulesNames().Contains("zzCharacterCreation")) return;
            InformationManager.DisplayMessage(new InformationMessage(
                "Encyclopedia Extender is patching Detailed Character Creation for compatibility", Orange));

            Type DCCPageChangedType = AccessTools.TypeByName("CharacterCreation.Models.EncyclopediaPageChangedAction");
            if (DCCPageChangedType == null) { FailedToPatchMessage("Access type"); return; }
            var DCCPageChangedMethod = AccessTools.Method(DCCPageChangedType, "OnEncyclopediaPageChanged");
            if (DCCPageChangedMethod == null) { FailedToPatchMessage("Access method"); return; }

            var transpiler = AccessTools.Method(typeof(DCCCompatibility), nameof(TranspileOnEncyclopediaPageChanged));
            harmony.Patch(DCCPageChangedMethod,
                transpiler: new HarmonyMethod(transpiler));
        }

        static IEnumerable<CodeInstruction> TranspileOnEncyclopediaPageChanged(IEnumerable<CodeInstruction> instructions)
        {
            var refresh = AccessTools.Method(typeof(EncyclopediaPageVM), "Refresh");
            var refreshValues = AccessTools.Method(typeof(EncyclopediaPageVM), "RefreshValues");
            foreach (var instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && instruction.operand == (object)refresh)
                {
                    instruction.operand = refreshValues;
                }
                yield return instruction;
            }
        }
    }
}
