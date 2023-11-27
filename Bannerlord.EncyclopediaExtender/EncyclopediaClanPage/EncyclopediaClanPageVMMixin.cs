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
    [ViewModelMixin(nameof(EncyclopediaClanPageVM.RefreshValues), true)]
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

        public override void OnRefresh()
        {
            DefectionText = new TextObject("{=uj3CXxKYK03}Defection").ToString();
            DefectionInfo.Clear();

            if (_clan is null || ViewModel is null)
            {
                return;
            }

            var cashHeader = new TextObject("{=beBL5H1u2fu}Cash:");
            var cashValue = _clan.Leader.Gold.ToString(Constants.WholeNumberFormat);
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
                ViewModel.ClanInfo.AddPair(kingdomWealthShareHeader, kingdomWealthShare.ToString(Constants.WholeNumberFormat));
            }

            var totalWealthHeader = new TextObject("{=nAr2HzgGiVn}Nominal Total Wealth:");
            var totalWealth = _clan.Leader.Gold + kingdomWealthShare - _clan.DebtToKingdom;
            var totalWealthFormatted = totalWealth.ToString(Constants.WholeNumberFormat);
            ViewModel.ClanInfo.AddPair(totalWealthHeader, totalWealthFormatted);

            if (CanClanDefect(_clan))
            {
                var leader = _clan.Leader;

                if (IsPlayerVassal() && !IsClanInSameFactionAsPlayer(_clan))
                {
                    var barterable = new JoinKingdomAsClanBarterable(leader, (Kingdom)Hero.MainHero.MapFaction);
                    var barterValue = -barterable.GetValueForFaction(_clan);

                    var defectionPriceHeader = new TextObject("{=mfaNceRHqRk}Defection Price:");
                    DefectionInfo.AddPair(defectionPriceHeader, barterValue.ToString(Constants.WholeNumberFormat));

                    var cashRequiredHeader = new TextObject("{=vMSUkkSBqeO}Cash required to persuade:");
                    var cashRequired = barterValue > 2000000f
                        ? new TextObject("{=9QU7uyLxhXJ}Happy with current liege").ToString()
                        : Math.Max(0, barterValue * 3 - 750000).ToString(Constants.WholeNumberFormat);
                    DefectionInfo.AddPair(cashRequiredHeader, cashRequired);
                }

                var defectionChance = BruteForceDefectionChance(_clan);
                GameTexts.SetVariable("NUMBER", defectionChance.ToString("N1"));

                var defectionChanceHeader = new TextObject("{=0w5RZCxnZ3B}Daily defection chance:");
                var defectionChanceFormatted = GameTexts.FindText("str_NUMBER_percent", null);
                DefectionInfo.AddPair(defectionChanceHeader, defectionChanceFormatted);
            }
        }

        private float BruteForceDefectionChance(Clan clan)
        {
            // I really don't like this.
            var nonBanditClans = Clan.NonBanditFactions.ToArray();
            var defections = 0;
            int iterations;
            for (iterations = 0; iterations < 5000; iterations++)
            {
                defections += SimulateConsiderDefection(clan, nonBanditClans);
            }

            return defections / (float)iterations * 100;
        }

        private bool IsPlayerVassal()
        {
            return Hero.MainHero.MapFaction.IsKingdomFaction
                && !Clan.PlayerClan.IsUnderMercenaryService;
        }

        private bool IsClanInSameFactionAsPlayer(Clan clan)
        {
            return clan.MapFaction == Hero.MainHero.MapFaction;
        }

        private bool CanClanDefect(Clan clan)
        {
            return clan != Clan.PlayerClan
                && clan.Kingdom != null
                && clan.Leader != clan.Kingdom.Leader
                && !clan.IsUnderMercenaryService;
        }

        int SimulateConsiderDefection(Clan clan, Clan[] nonBanditClans)
        {
            if (MBRandom.RandomFloat >= 0.2f)
            {
                return 0;
            }

            var randomClan = GetRandomClan(clan, nonBanditClans);

            if (IsPossibleToJoinOtherClan(clan, randomClan))
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

        private static Clan GetRandomClan(Clan clan, Clan[] nonBanditClans)
        {
            var randomClan = nonBanditClans.GetRandomElement();
            var num = 0;

            while (randomClan.Kingdom == null || clan.Kingdom == randomClan.Kingdom || randomClan.IsEliminated)
            {
                randomClan = nonBanditClans.GetRandomElement();
                num++;
                if (num >= 20)
                {
                    break;
                }
            }

            return randomClan;
        }

        private static bool IsPossibleToJoinOtherClan(Clan clan, Clan otherClan)
        {
            return otherClan.Kingdom != null
                && clan.Kingdom != otherClan.Kingdom
                && otherClan.MapFaction.IsKingdomFaction
                && !otherClan.IsEliminated
                && otherClan != Clan.PlayerClan
                && otherClan.MapFaction.Leader != Hero.MainHero;
        }
    }
}
