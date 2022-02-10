using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;
using Bannerlord.UIExtenderEx.ViewModels;
using HarmonyLib;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace EncyclopediaExtender
{
    [PrefabExtension("EncyclopediaFactionPage", "descendant::EncyclopediaSubPageElement[@Id='Leader']")]
    public class FactionPageWealthPatch : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        [PrefabExtensionFileName(true)]
        public String File => "FactionPageWealthPatch";
    }
    [PrefabExtension("EncyclopediaFactionPage", "descendant::GridWidget[@Id='EnemiesGrid']")]
    public class FactionPagePrisonerPatch : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        [PrefabExtensionFileName(true)]
        public String File =>   "FactionPagePrisonerPatch";
    }

    [ViewModelMixin("RefreshValues", true)]
    public class ExtendEncyclopediaFactionPageVM : BaseViewModelMixin<EncyclopediaFactionPageVM>
    {
        public ExtendEncyclopediaFactionPageVM(EncyclopediaFactionPageVM vm) : base(vm)
        {
            WealthInfo = new MBBindingList<StringPairItemVM>();
            CapturedHeroes = new MBBindingList<HeroVM>();
            ImprisonedHeroes = new MBBindingList<HeroVM>();
            KingdomWealthText = "";
            CapturedHeroesText = "";
            ImprisonedHeroesText = "";
        }
        [DataSourceProperty]
        public String KingdomWealthText { get; set; }
        [DataSourceProperty]
        public MBBindingList<StringPairItemVM> WealthInfo { get; set; }
        

        [DataSourceProperty]
        public string CapturedHeroesText { get; set; }
        [DataSourceProperty]
        public MBBindingList<HeroVM> CapturedHeroes { get; set; }
        [DataSourceProperty]
        public string ImprisonedHeroesText { get; set; }
        [DataSourceProperty]
        public MBBindingList<HeroVM> ImprisonedHeroes { get; set; }

        public override void OnRefresh()
        {
            KingdomWealthText = new TextObject("{=VidmvRvXecq}Kingdom Wealth").ToString();
            CapturedHeroesText = new TextObject("{=FwJELs27ac7}Captured Heroes").ToString();
            ImprisonedHeroesText = new TextObject("{=1CYZYNXTfN9}Imprisoned Heroes").ToString();
            WealthInfo.Clear();
            CapturedHeroes.Clear();
            ImprisonedHeroes.Clear();
            var vm = base.ViewModel;
            if (vm != null)
            {
                var kingdom = Traverse.Create(vm).Field("_faction").GetValue<Kingdom>();
                {
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
                {
                    CapturedHeroes = GetHeroesCapturedBy(kingdom);
                    ImprisonedHeroes = GetHeroesImprisoned(kingdom);
                }
            }
        }
        public static MBBindingList<HeroVM> GetHeroesCapturedBy(IFaction capturerFaction)
        {
            MBBindingList<HeroVM> list = new MBBindingList<HeroVM>();
            foreach (Hero hero in Hero.AllAliveHeroes)
            {
                if (hero.IsPrisoner)
                {
                    PartyBase partyBelongedToAsPrisoner = hero.PartyBelongedToAsPrisoner;
                    if (((partyBelongedToAsPrisoner != null) ? partyBelongedToAsPrisoner.MapFaction : null) == capturerFaction)
                    {
                        list.Add(new HeroVM(hero));
                    }
                }
            }
            return list;
        }
        public static MBBindingList<HeroVM> GetHeroesImprisoned(IFaction capturedFaction)
        {
            MBBindingList<HeroVM> list = new MBBindingList<HeroVM>();
            foreach (Hero hero in capturedFaction.Heroes)
            {
                if (hero.IsPrisoner)
                {
                    list.Add(new HeroVM(hero));
                }
            }
            return list;
        }
    }
}
