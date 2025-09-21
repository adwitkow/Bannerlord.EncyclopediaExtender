using Bannerlord.UIExtenderEx;
using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace Bannerlord.EncyclopediaExtender
{
    public class SubModule : MBSubModuleBase
    {
        private const string ModuleId = "Bannerlord.EncyclopediaExtender";

        private readonly UIExtender _extender = new UIExtender(ModuleId);
        private readonly Harmony _harmony = new Harmony(ModuleId);

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            PrefabInjector.RegisterHeroItemTuple();
            PrefabInjector.RegisterSettlementProducedItem();

            _harmony.PatchAll();

            _extender.Register(typeof(SubModule).Assembly);
            _extender.Enable();
        }
    }
}