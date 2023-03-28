using System;
using System.Collections.Generic;
using System.Linq;
using Bannerlord.UIExtenderEx.Attributes;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using Bannerlord.UIExtenderEx.ViewModels;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaClanPage
{
    [ViewModelMixin("RefreshValues", true)]
    public class EncyclopediaClanPageVMMixin : BaseViewModelMixin<EncyclopediaClanPageVM>
    {
        private readonly Clan? _clan;

        public EncyclopediaClanPageVMMixin(EncyclopediaClanPageVM vm) : base(vm)
        {
            _clan = vm.Obj as Clan;

            DefectionText = "";
            DefectionInfo = new MBBindingList<StringPairItemVM>();
        }

        [DataSourceProperty]
        public string DefectionText { get; set; }

        [DataSourceProperty]
        public MBBindingList<StringPairItemVM> DefectionInfo { get; set; }

        int SimulateConsiderDefection(Clan clan, List<Clan> e)
        {
            if (MBRandom.RandomFloat >= 0.2f) return 0;
            var randomClan = e.GetRandomElement();
            var num = 0;

            while (randomClan.Kingdom == null || clan.Kingdom == randomClan.Kingdom || randomClan.IsEliminated)
            {
                randomClan = e.GetRandomElement();
                num++;
                if (num >= 20)
                {
                    break;
                }
            }

            if (randomClan.Kingdom != null && clan.Kingdom != randomClan.Kingdom && randomClan.MapFaction.IsKingdomFaction && !randomClan.IsEliminated && randomClan != Clan.PlayerClan && randomClan.MapFaction.Leader != Hero.MainHero)
            {
                var kingdom = randomClan.Kingdom;

                var joinKingdomAsClanBarterable = new JoinKingdomAsClanBarterable(clan.Leader, kingdom);
                var clanValue = joinKingdomAsClanBarterable.GetValueForFaction(clan);
                var kingdomValue = joinKingdomAsClanBarterable.GetValueForFaction(kingdom);
                var valueSum = clanValue + kingdomValue;
                if (clanValue < 0)
                {
                    clanValue = -clanValue;
                }
                if (valueSum > 0 && clanValue <= kingdom.Leader.Gold * 0.5f)
                {
                    return 1;
                }
            }
            return 0;
        }

        public override void OnRefresh()
        {
            DefectionText = new TextObject("{=uj3CXxKYK03}Defection").ToString();
            DefectionInfo.Clear();

            if (_clan is null || ViewModel is null)
            {
                return;
            }

            var cashHeader = new TextObject("{=beBL5H1u2fu}Cash:");
            var cashValue = _clan.Leader.Gold.ToString("N0");
            ViewModel.ClanInfo.AddPair(cashHeader, cashValue);

            var debtHeader = new TextObject("{=C1SUFxYrMXk}Debt:");
            var debtValue = _clan.DebtToKingdom;
            ViewModel.ClanInfo.AddPair(debtHeader, debtValue);

            var kingdom = _clan.Kingdom;
            int kingdomWealthShare = 0;
            if (kingdom != null && !_clan.IsMinorFaction)
            {
                kingdomWealthShare = kingdom.KingdomBudgetWallet / (kingdom.Clans.Count + 1) / 2;
                var kingdomWealthShareHeader = new TextObject("{=a2uZeyyIdQX}Share of Kingdom Wealth:");
                ViewModel.ClanInfo.AddPair(kingdomWealthShareHeader, kingdomWealthShare.ToString("N0"));
            }

            var totalWealthHeader = new TextObject("{=nAr2HzgGiVn}Nominal Total Wealth:");
            var totalWealth = _clan.Leader.Gold + kingdomWealthShare - _clan.DebtToKingdom;
            var totalWealthFormatted = totalWealth.ToString("N0");
            ViewModel.ClanInfo.AddPair(totalWealthHeader, totalWealthFormatted);

            if (_clan != Clan.PlayerClan && _clan.Kingdom != null && _clan.Leader != _clan.Kingdom.Leader && !_clan.IsUnderMercenaryService)
            {
                var leader = _clan.Leader;

                if (Hero.MainHero.MapFaction.IsKingdomFaction && !Clan.PlayerClan.IsUnderMercenaryService
                    && _clan.MapFaction != Hero.MainHero.MapFaction)
                {
                    var barter_val = -new JoinKingdomAsClanBarterable(leader, (Kingdom)Hero.MainHero.MapFaction).GetValueForFaction(_clan);

                    DefectionInfo.Add(new StringPairItemVM(new TextObject("{=mfaNceRHqRk}Defection Price:").ToString(), barter_val.ToString("N0")));
                    string cash_requirement = barter_val > 2000000f ? new TextObject("{=9QU7uyLxhXJ}Happy with current liege").ToString()
                        : Math.Max(0, barter_val * 3 - 750000).ToString("N0");

                    DefectionInfo.Add(new StringPairItemVM(new TextObject("{=vMSUkkSBqeO}Cash required to persuade:").ToString(), cash_requirement));
                }

                List<Clan> e = Clan.NonBanditFactions.ToList();
                int defections = 0;
                int iterations;
                for (iterations = 0; iterations < 5000; iterations++)
                {
                    defections += SimulateConsiderDefection(_clan, e);
                }

                GameTexts.SetVariable("NUMBER", (defections / (float)iterations * 100).ToString("N1"));

                DefectionInfo.Add(new StringPairItemVM(new TextObject("{=0w5RZCxnZ3B}Daily defection chance:").ToString(),
                    GameTexts.FindText("str_NUMBER_percent", null).ToString()));
            }
        }
    }
}
