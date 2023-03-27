using System;
using Bannerlord.UIExtenderEx.ViewModels;
using HarmonyLib;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using Bannerlord.UIExtenderEx.Attributes;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaFactionPage
{
    [ViewModelMixin("RefreshValues", true)]
    public class EncyclopediaFactionPageVMMixin : BaseViewModelMixin<EncyclopediaFactionPageVM>
    {
        public EncyclopediaFactionPageVMMixin(EncyclopediaFactionPageVM vm) : base(vm)
        {
            WealthInfo = new MBBindingList<StringPairItemVM>();
            CapturedHeroes = new MBBindingList<HeroVM>();
            ImprisonedHeroes = new MBBindingList<HeroVM>();
            KingdomWealthText = "";
            CapturedHeroesText = "";
            ImprisonedHeroesText = "";
        }
        [DataSourceProperty]
        public string KingdomWealthText { get; set; }
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
            var vm = ViewModel;
            if (vm != null)
            {
                var kingdom = Traverse.Create(vm).Field("_faction").GetValue<Kingdom>();
                WealthInfo.Add(new StringPairItemVM(new TextObject("{=z4z97KSX12m}Kingdom Bank:").ToString(), kingdom.KingdomBudgetWallet.ToString("N0")));
                int clans_wealth = 0;
                foreach (var clan in kingdom.Clans)
                {
                    if (clan.IsUnderMercenaryService || clan.IsMinorFaction) continue;
                    clans_wealth += clan.Leader.Gold - clan.DebtToKingdom;
                }
                WealthInfo.Add(new StringPairItemVM(new TextObject("{=Ks1B7PO9DjP}Sum of Clan Wealth:").ToString(), clans_wealth.ToString("N0")));

                CapturedHeroes = GetHeroesCapturedBy(kingdom);
                ImprisonedHeroes = GetHeroesImprisoned(kingdom);
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
                    if (partyBelongedToAsPrisoner?.MapFaction == capturerFaction)
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
