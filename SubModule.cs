using Bannerlord.UIExtenderEx;
using TaleWorlds.MountAndBlade;
using HarmonyLib;

namespace EncyclopediaExtender
{
    public class SubModule : MBSubModuleBase
    {
        private UIExtender _extender = new UIExtender("EncyclopediaExtender");
        Harmony _harmony = new Harmony("EncyclopediaExtender");
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            _harmony.PatchAll();

            _extender.Register(typeof(SubModule).Assembly);
            _extender.Enable();
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            DCCCompatibility.DCCPatcher(_harmony);
        }
    }
}