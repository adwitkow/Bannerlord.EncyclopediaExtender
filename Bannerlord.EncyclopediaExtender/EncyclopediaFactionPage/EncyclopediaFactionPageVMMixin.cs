using Bannerlord.UIExtenderEx.ViewModels;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using Bannerlord.UIExtenderEx.Attributes;
using System.Linq;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaFactionPage
{
    [ViewModelMixin(nameof(EncyclopediaFactionPageVM.RefreshValues), true)]
    public class EncyclopediaFactionPageVMMixin : BaseViewModelMixin<EncyclopediaFactionPageVM>
    {
        private readonly Kingdom? _kingdom;

        public EncyclopediaFactionPageVMMixin(EncyclopediaFactionPageVM vm) : base(vm)
        {
            _kingdom = vm.Obj as Kingdom;

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

            if (_kingdom is null)
            {
                return;
            }

            var kingdomBankHeader = new TextObject("{=z4z97KSX12m}Kingdom Bank:");
            var kingdomBankFormatted = _kingdom.KingdomBudgetWallet.ToString("N0");
            WealthInfo.AddPair(kingdomBankHeader, kingdomBankFormatted);

            var clansWealthHeader = new TextObject("{=Ks1B7PO9DjP}Sum of Clan Wealth:");
            var clansWealthFormatted = _kingdom.Clans
                .Where(clan => !clan.IsUnderMercenaryService && !clan.IsMinorFaction)
                .Sum(clan => clan.Leader.Gold - clan.DebtToKingdom)
                .ToString("N0");
            WealthInfo.AddPair(clansWealthHeader, clansWealthFormatted);

            CapturedHeroes = GetHeroesCapturedBy(_kingdom);
            ImprisonedHeroes = GetHeroesImprisoned(_kingdom);
        }
        public static MBBindingList<HeroVM> GetHeroesCapturedBy(IFaction capturerFaction)
        {
            MBBindingList<HeroVM> list = new MBBindingList<HeroVM>();

            var prisoners = Hero.AllAliveHeroes.Where(hero => hero.IsPrisoner);
            foreach (Hero hero in prisoners)
            {
                PartyBase partyBelongedToAsPrisoner = hero.PartyBelongedToAsPrisoner;
                if (partyBelongedToAsPrisoner.MapFaction == capturerFaction)
                {
                    list.Add(new HeroVM(hero));
                }
            }

            return list;
        }
        public static MBBindingList<HeroVM> GetHeroesImprisoned(IFaction capturedFaction)
        {
            MBBindingList<HeroVM> list = new MBBindingList<HeroVM>();

            var prisoners = capturedFaction.Heroes.Where(hero => hero.IsPrisoner);
            foreach (Hero hero in prisoners)
            {
                list.Add(new HeroVM(hero));
            }

            return list;
        }
    }
}
