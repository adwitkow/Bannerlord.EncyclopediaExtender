using System;
using System.Collections.Generic;
using System.Linq;
using Bannerlord.UIExtenderEx.Attributes;
using HarmonyLib;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using Bannerlord.UIExtenderEx.ViewModels;

namespace EncyclopediaExtender.EncyclopediaClanPage
{
    [ViewModelMixin("RefreshValues", true)]
    public class EncyclopediaClanPageVMMixin : BaseViewModelMixin<EncyclopediaClanPageVM>
    {
        public EncyclopediaClanPageVMMixin(EncyclopediaClanPageVM vm) : base(vm)
        {
            DefectionText = "";
            DefectionInfo = new MBBindingList<StringPairItemVM>();
        }

        [DataSourceProperty]
        public String DefectionText { get; set; }

        [DataSourceProperty]
        public MBBindingList<StringPairItemVM> DefectionInfo { get; set; }

        int SimulateConsiderDefection(Clan clan, List<Clan> e)
        {
            if (MBRandom.RandomFloat >= 0.2f) return 0;
            Clan randomElement2 = e.GetRandomElement<Clan>();
            {
                int num = 0;
                while (randomElement2.Kingdom == null || clan.Kingdom == randomElement2.Kingdom || randomElement2.IsEliminated)
                {
                    randomElement2 = e.GetRandomElement<Clan>();
                    num++;
                    if (num >= 20)
                    {
                        break;
                    }
                }
            }
            if (randomElement2.Kingdom != null && clan.Kingdom != randomElement2.Kingdom && randomElement2.MapFaction.IsKingdomFaction && !randomElement2.IsEliminated && randomElement2 != Clan.PlayerClan && randomElement2.MapFaction.Leader != Hero.MainHero)
            {
                var kingdom = randomElement2.Kingdom;

                JoinKingdomAsClanBarterable joinKingdomAsClanBarterable = new JoinKingdomAsClanBarterable(clan.Leader, kingdom);
                int valueForFaction = joinKingdomAsClanBarterable.GetValueForFaction(clan);
                int valueForFaction2 = joinKingdomAsClanBarterable.GetValueForFaction(kingdom);
                int num = valueForFaction + valueForFaction2;
                int num2 = 0;
                if (valueForFaction < 0)
                {
                    num2 = -valueForFaction;
                }
                if (num > 0 && (float)num2 <= (float)kingdom.Leader.Gold * 0.5f)
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

                if (clan != Clan.PlayerClan && clan.Kingdom != null && clan.Leader != clan.Kingdom.Leader && !clan.IsUnderMercenaryService)
                {
                    var leader = clan.Leader;

                    if (Hero.MainHero.MapFaction.IsKingdomFaction && !Clan.PlayerClan.IsUnderMercenaryService
                        && clan.MapFaction != Hero.MainHero.MapFaction)
                    {
                        var barter_val = -(new JoinKingdomAsClanBarterable(leader, (Kingdom)Hero.MainHero.MapFaction).GetValueForFaction(clan));

                        DefectionInfo.Add(new StringPairItemVM(new TextObject("{=mfaNceRHqRk}Defection Price:").ToString(), barter_val.ToString("N0")));
                        string cash_requirement = barter_val > 2000000f ? new TextObject("{=9QU7uyLxhXJ}Happy with current liege").ToString()
                            : Math.Max(0, barter_val * 3 - 750000).ToString("N0");

                        DefectionInfo.Add(new StringPairItemVM(new TextObject("{=vMSUkkSBqeO}Cash required to persuade:").ToString(), cash_requirement));
                    }

                    List<Clan> e = Enumerable.ToList<Clan>(Clan.NonBanditFactions);
                    int iterations = 0;
                    int defections = 0;
                    for (iterations = 0; iterations < 5000; iterations++)
                    {
                        defections += SimulateConsiderDefection(clan, e);
                    }

                    GameTexts.SetVariable("NUMBER", (defections / (float)iterations * 100).ToString("N1"));

                    DefectionInfo.Add(new StringPairItemVM(new TextObject("{=0w5RZCxnZ3B}Daily defection chance:").ToString(),
                        GameTexts.FindText("str_NUMBER_percent", null).ToString()));
                }
            }
        }
    }
}
