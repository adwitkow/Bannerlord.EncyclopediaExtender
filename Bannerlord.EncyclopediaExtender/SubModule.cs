using Bannerlord.UIExtenderEx;
using TaleWorlds.MountAndBlade;
using HarmonyLib;

namespace Bannerlord.EncyclopediaExtender
{
    public class SubModule : MBSubModuleBase
    {
        private readonly UIExtender _extender = new UIExtender("EncyclopediaExtender");
        private readonly Harmony _harmony = new Harmony("EncyclopediaExtender");

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            _harmony.PatchAll();

            _extender.Register(typeof(SubModule).Assembly);
            _extender.Enable();
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            //DCCCompatibility.DCCPatcher(_harmony);
        }
    }
}