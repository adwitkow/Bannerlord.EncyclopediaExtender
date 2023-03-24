using HarmonyLib;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.Encyclopedia.Pages;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace EncyclopediaExtender.EncyclopediaHeroPage.Patches
{
    [HarmonyPatch(typeof(DefaultEncyclopediaHeroPage), "InitializeSortControllers")]
    class HeroPageSortControllerPatch
    {
        static void Postfix(IEnumerable<EncyclopediaSortController> __result)
        {
            var list = __result as List<EncyclopediaSortController>;
            if (list != null)
            {
                TextObject LevelText = GameTexts.FindText("str_level", null);

                list.Add(new EncyclopediaSortController(LevelText, new ListLevelComparer()));
            }
        }

        class ListLevelComparer : DefaultEncyclopediaHeroPage.EncyclopediaListHeroComparer
        {
            public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
            {
                return CompareHeroes(x, y, _comparison);
            }

            public override string GetComparedValueText(EncyclopediaListItem item)
            {
                Hero? hero = item.Object as Hero;
                if (hero == null)
                {
                    Debug.FailedAssert("Unable to get the hero level.", "EncyclopediaExtender\\HeroPage.cs", "GetComparedValueText", 355);
                    return "";
                }
                return hero.Level.ToString();
            }

            private static Func<Hero, Hero, int> _comparison = (h1, h2) => h1.Level.CompareTo(h2.Level);
        }
    }
}
