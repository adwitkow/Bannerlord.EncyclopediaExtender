using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using HarmonyLib;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Localization;

namespace EncyclopediaExtender
{

    [ViewModelMixin("RefreshValues", true)]
    public class ExtendEncyclopediaClanPageVM : BaseViewModelMixin<EncyclopediaClanPageVM>
    {
        public ExtendEncyclopediaClanPageVM(EncyclopediaClanPageVM vm) : base(vm)
        {

        }
        public override void OnRefresh()
        {
            var vm = base.ViewModel;
            if (vm != null)
            {
                var clan = Traverse.Create(vm).Field("_clan").GetValue<Clan>();
                if (vm.ClanInfo.Count <= 3)
                {
                    vm.ClanInfo.Add(new StringPairItemVM("", ""));
                    vm.ClanInfo.Add(new StringPairItemVM(new TextObject("{=beBL5H1u2fu}Cash:").ToString(), clan.Leader.Gold.ToString("N0")));
                    vm.ClanInfo.Add(new StringPairItemVM(new TextObject("{=C1SUFxYrMXk}Debt:").ToString(), clan.DebtToKingdom.ToString()));
                    var kingdom = clan.Kingdom;
                    int kingdom_wealth = 0;
                    if (kingdom != null && !clan.IsMinorFaction)
                    {
                        kingdom_wealth = kingdom.KingdomBudgetWallet / (kingdom.Clans.Count + 1) / 2;
                        vm.ClanInfo.Add(new StringPairItemVM(new TextObject("{=a2uZeyyIdQX}Share of Kingdom Wealth:").ToString(),
                            kingdom_wealth.ToString("N0")));
                    }
                    vm.ClanInfo.Add(new StringPairItemVM(new TextObject("{=nAr2HzgGiVn}Nominal Total Wealth:").ToString(),
                        (clan.Leader.Gold + kingdom_wealth - clan.DebtToKingdom).ToString("N0")));
                }

            }
        }
    }
}
