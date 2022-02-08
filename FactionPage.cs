using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;
using Bannerlord.UIExtenderEx.ViewModels;
using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace EncyclopediaExtender
{
    [PrefabExtension("EncyclopediaFactionPage", "descendant::EncyclopediaSubPageElement[@Id='Leader']")]
    public class FactionPagePatch : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        [PrefabExtensionFileName(true)]
        public String File => "FactionPagePatch";
    }

    [ViewModelMixin("RefreshValues", true)]
    public class ExtendEncyclopediaFactionPageVM : BaseViewModelMixin<EncyclopediaFactionPageVM>
    {
        [DataSourceProperty]
        public MBBindingList<StringPairItemVM> WealthInfo { get; set; }
        public ExtendEncyclopediaFactionPageVM(EncyclopediaFactionPageVM vm) : base(vm)
        {
            WealthInfo = new MBBindingList<StringPairItemVM>();
            KingdomWealthText = "";
        }
        [DataSourceProperty]
        public String KingdomWealthText { get; set; }
        public override void OnRefresh()
        {
            KingdomWealthText = new TextObject("{=VidmvRvXecq}Kingdom Wealth").ToString();
            WealthInfo.Clear();
            var vm = base.ViewModel;
            if (vm != null)
            {
                var kingdom = Traverse.Create(vm).Field("_faction").GetValue<Kingdom>();

                var kingdomBankText = new TextObject("");
                WealthInfo.Add(new StringPairItemVM(new TextObject("{=z4z97KSX12m}Kingdom Bank:").ToString(), kingdom.KingdomBudgetWallet.ToString("N0")));
                int clans_wealth = 0;
                foreach (var clan in kingdom.Clans)
                {
                    if (clan.IsUnderMercenaryService || clan.IsMinorFaction) continue;
                    clans_wealth += clan.Leader.Gold - clan.DebtToKingdom;
                }
                WealthInfo.Add(new StringPairItemVM(new TextObject("{=Ks1B7PO9DjP}Sum of Clan Wealth:").ToString(), clans_wealth.ToString("N0")));
            }
        }
    }
}
