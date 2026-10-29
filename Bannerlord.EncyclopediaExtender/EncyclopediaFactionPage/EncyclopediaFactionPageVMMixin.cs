using Bannerlord.UIExtenderEx.ViewModels;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using Bannerlord.UIExtenderEx.Attributes;
using System.Linq;
using Bannerlord.EncyclopediaExtender.Configuration;
using Bannerlord.EncyclopediaExtender.DictionarySection;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaFactionPage
{
    [ViewModelMixin(nameof(EncyclopediaFactionPageVM.RefreshValues), true)]
    public class EncyclopediaFactionPageVMMixin : BaseViewModelMixin<EncyclopediaFactionPageVM>
    {
        private readonly Kingdom? _kingdom;

        public EncyclopediaFactionPageVMMixin(EncyclopediaFactionPageVM vm) : base(vm)
        {
            _kingdom = vm.Obj as Kingdom;

            InfoSection = new DictionarySectionVM();

            CapturedHeroes = new MBBindingList<HeroVM>();
            ImprisonedHeroes = new MBBindingList<HeroVM>();

            CapturedHeroesText = "";
            ImprisonedHeroesText = "";
        }

        [DataSourceProperty]
        public DictionarySectionVM InfoSection { get; set; }

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
            InfoSection.Title = Texts.Info.ToString();
            CapturedHeroesText = Texts.CapturedHeroes.ToString();
            ImprisonedHeroesText = Texts.ImprisonedHeroes.ToString();

            InfoSection.Items.Clear();
            CapturedHeroes.Clear();
            ImprisonedHeroes.Clear();

            if (_kingdom is null)
            {
                return;
            }

            if (Settings.KingdomBank)
            {
                var kingdomBank = GetFormattedKingdomBank(_kingdom);
                InfoSection.Items.AddPair(Texts.KingdomBank, kingdomBank);
            }

            if (Settings.KingdomClansWealthSum)
            {
                var sumOfClanWealth = CalculateSumOfClanWealth(_kingdom);
                InfoSection.Items.AddPair(Texts.SumOfClanWealth, sumOfClanWealth);
            }

            if (Settings.CapturedHeroes)
            {
                CapturedHeroes = GetCapturedHeroes(_kingdom);
            }

            if (Settings.ImprisonedHeroes)
            {
                ImprisonedHeroes = GetImprisonedHeroes(_kingdom);
            }
        }

        private static string CalculateSumOfClanWealth(Kingdom kingdom)
        {
            return kingdom.Clans
                .Where(clan => !clan.IsUnderMercenaryService && !clan.IsMinorFaction)
                .Sum(clan => clan.Leader.Gold - clan.DebtToKingdom)
                .ToString(Constants.WholeNumberFormat);
        }

        private static string GetFormattedKingdomBank(Kingdom kingdom)
        {
            return kingdom.KingdomBudgetWallet.ToString(Constants.WholeNumberFormat);
        }

        private static MBBindingList<HeroVM> GetCapturedHeroes(IFaction capturerFaction)
        {
            MBBindingList<HeroVM> list = new MBBindingList<HeroVM>();

            var prisoners = Hero.AllAliveHeroes
                .Where(hero => IsPrisonerOfFaction(hero, capturerFaction));

            foreach (Hero hero in prisoners)
            {
                list.Add(new HeroVM(hero));
            }

            return list;
        }
        private static MBBindingList<HeroVM> GetImprisonedHeroes(IFaction capturedFaction)
        {
            MBBindingList<HeroVM> list = new MBBindingList<HeroVM>();

            var prisoners = capturedFaction.Heroes.Where(hero => hero.IsPrisoner);
            foreach (Hero hero in prisoners)
            {
                list.Add(new HeroVM(hero));
            }

            return list;
        }

        private static bool IsPrisonerOfFaction(Hero hero, IFaction capturerFaction)
        {
            if (!hero.IsPrisoner)
            {
                return false;
            }

            var capturerParty = hero.PartyBelongedToAsPrisoner;
            if (capturerParty is null)
            {
                return false;
            }

            return capturerParty.MapFaction == capturerFaction;
        }
    }
}
