using HarmonyLib;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.Encyclopedia.Pages;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaHeroPage.Patches
{
    [HarmonyPatch(typeof(DefaultEncyclopediaHeroPage), "InitializeSortControllers")]
    class HeroPageSortControllerPatch
    {
        public static void Postfix(IEnumerable<EncyclopediaSortController> __result)
        {
            if (__result is List<EncyclopediaSortController> list)
            {
                TextObject LevelText = GameTexts.FindText("str_level", null);

                list.Add(new EncyclopediaSortController(LevelText, new ListLevelComparer()));
            }
        }

        private sealed class ListLevelComparer : DefaultEncyclopediaHeroPage.EncyclopediaListHeroComparer
        {
            public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
            {
                return CompareHeroes(x, y, _comparison);
            }

            public override string GetComparedValueText(EncyclopediaListItem item)
            {
                if (item.Object is not Hero hero)
                {
                    return string.Empty;
                }
                return hero.Level.ToString();
            }

            private static readonly Func<Hero, Hero, int> _comparison = (h1, h2) => h1.Level.CompareTo(h2.Level);
        }
    }
}
