﻿using HarmonyLib;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.Encyclopedia.Pages;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace EncyclopediaExtender.EncyclopediaHeroPage.Patches
{
    [HarmonyPatch(typeof(DefaultEncyclopediaHeroPage), "InitializeFilterItems")]
    class HeroPageFilterPatch
    {
        static void Postfix(IEnumerable<EncyclopediaFilterGroup> __result)
        {
            var list = __result as List<EncyclopediaFilterGroup>;
            if (list != null)
            {
                {
                    var tr = Traverse.Create(list[4]).Field("Filters").GetValue<List<EncyclopediaFilterItem>>();
                    tr.Add(new EncyclopediaFilterItem(new TextObject("{=dkQJzg3erHZ}Clan Leader", null), (hero) =>
                    {
                        Hero h = (Hero)hero;
                        return h.Clan != null && !h.Clan.IsMinorFaction && h.Clan.Leader == h;
                    }));
                }
                {
                    List<EncyclopediaFilterItem> prisonerList = new List<EncyclopediaFilterItem>();
                    prisonerList.Add(new EncyclopediaFilterItem(new TextObject("{=visGHcNwvj7}Not Prisoner", null),
                        (h) => ((Hero)h).PartyBelongedToAsPrisoner == null));
                    prisonerList.Add(new EncyclopediaFilterItem(new TextObject("{=E9b41bY9PnC}Prisoner", null),
                        (h) => ((Hero)h).PartyBelongedToAsPrisoner != null));
                    list.Add(new EncyclopediaFilterGroup(prisonerList, new TextObject("{=ggFT1tTOMeK}Prisoner Status", null)));
                }
                {
                    List<EncyclopediaFilterItem> clanStatusList = new List<EncyclopediaFilterItem>();

                    clanStatusList.Add(new EncyclopediaFilterItem(GameTexts.FindText("str_other"),
                        (h) => ((Hero)h).Clan != Clan.PlayerClan));
                    clanStatusList.Add(new EncyclopediaFilterItem(new TextObject("{=1kouok0blVC}Player Clan", null),
                        (h) => ((Hero)h).Clan == Clan.PlayerClan));

                    list.Add(new EncyclopediaFilterGroup(clanStatusList, GameTexts.FindText("str_clan", null)));
                }
                {
                    List<EncyclopediaFilterItem> KingdomList = new List<EncyclopediaFilterItem>();

                    foreach (var k in Kingdom.All)
                    {
                        var cond = (Clan c) => c != null && c.Kingdom == k;
                        KingdomList.Add(new EncyclopediaFilterItem(k.Name,
                        (h) => cond(((Hero)h).Clan)));
                    }

                    list.Add(new EncyclopediaFilterGroup(KingdomList, GameTexts.FindText("str_kingdom", null)));
                }
            }
        }
    }

}
