using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;
using Bannerlord.UIExtenderEx.ViewModels;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.Encyclopedia.Pages;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.CampaignSystem.ViewModelCollection.Inventory;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;


namespace EncyclopediaExtender
{

    [PrefabExtension("EncyclopediaHeroPage", "descendant::Widget[@Id='InfoContainer']")]
    public sealed class HeroPagePatch : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        [PrefabExtensionFileName(true)]
        public String File => "HeroPagePatch";
    }


    [PrefabExtension("EncyclopediaHeroPage", "descendant::NavigatableGridWidget[@Id='FamilyGrid']")]
    public sealed class HeroPagePerksPatch : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        [PrefabExtensionFileName(true)]
        public String File => "HeroPagePerksPatch";
    }

    [PrefabExtension("EncyclopediaHeroPage", "descendant::TextWidget[@Text='@SkillsText']")]
    public sealed class HeroPageAttributesPatch : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Prepend;

        [PrefabExtensionFileName(true)]
        public String File => "HeroPageAttributesPatch";
    }

    public class PerksForSkillVM : ViewModel
    {
        [DataSourceProperty]
        public string SkillName { get; set; }

        [DataSourceProperty]
        public MBBindingList<StringPairItemVM> Perks { get; set; }
        public PerksForSkillVM(SkillObject skill, List<PerkObject> perks)
        {
            SkillName = skill.ToString();
            Perks = new MBBindingList<StringPairItemVM>();
            foreach (var perk in perks.OrderBy((PerkObject p) => p.RequiredSkillValue))
            {
                Perks.Add(new StringPairItemVM(perk.RequiredSkillValue.ToString() + ':', perk.ToString(),
                    new BasicTooltipViewModel(() => CampaignUIHelper.GetPerkEffectText(perk, false))));
            }
        }
    }

    public class ExtenderAttributeVM : ViewModel
    {
        public ExtenderAttributeVM(Hero hero, CharacterAttribute att)
        {
            Name = att.Abbreviation.ToString();
            AttributeValue = hero.GetAttributeValue(att);
        }
        [DataSourceProperty]
        public String Name { get; set; }
        [DataSourceProperty]
        public int AttributeValue { get; set; }
    }

    [ViewModelMixin("RefreshValues", true)]
    public class ExtendEncyclopediaHeroPageVM : BaseViewModelMixin<EncyclopediaHeroPageVM>
    {
        [DataSourceProperty]
        public MBBindingList<StringPairItemVM> MarriagePrices { get; set; }

        [DataSourceProperty]
        public MBBindingList<SPItemVM> HeroItems { get; set; }

        [DataSourceProperty]
        public MBBindingList<PerksForSkillVM> PerksPerSkillLeftSide { get; set; }
        [DataSourceProperty]
        public MBBindingList<PerksForSkillVM> PerksPerSkillRightSide { get; set; }
        int rows_left_side = 0, rows_right_side = 0;

        [DataSourceProperty]
        public string AttributesText { get; set; }
        [DataSourceProperty]
        public MBBindingList<ExtenderAttributeVM> Attributes { get; set; }
        public ExtendEncyclopediaHeroPageVM(EncyclopediaHeroPageVM vm) : base(vm)
        {
            MarriagePrices = new MBBindingList<StringPairItemVM>();
            HeroItems = new MBBindingList<SPItemVM>();
            PerksPerSkillLeftSide = new MBBindingList<PerksForSkillVM>();
            PerksPerSkillRightSide = new MBBindingList<PerksForSkillVM>();
            Attributes = new MBBindingList<ExtenderAttributeVM>();

            DowryPricesText = "";
            EquipmentText = "";
            PerksText = "";
            AttributesText = "";
        }

        [DataSourceProperty]
        public string DowryPricesText { get; set; }
        [DataSourceProperty]
        public string EquipmentText { get; set; }
        [DataSourceProperty]
        public string PerksText { get; set; }

        public static List<EquipmentElement> HeroEquipment(Hero x)
        {
            var equipment = new List<EquipmentElement>();
            for (int i = 0; i < 12; i++)
            {
                if (!x.BattleEquipment[i].IsEmpty)
                {
                    equipment.Add(x.BattleEquipment[i]);
                }
                if (!x.CivilianEquipment[i].IsEmpty)
                {
                    equipment.Add(x.CivilianEquipment[i]);
                }
            }

            Town town = Settlement.FindFirst((Settlement z) => z.IsTown).Town;
            equipment = equipment.OrderBy((EquipmentElement e) => -town.GetItemPrice(e, MobileParty.MainParty, true)).ToList();

            return equipment;
        }

        public static int HeroEquipmentValue(Hero h)
        {
            var equipment = HeroEquipment(h);
            int equipment_value = 0;
            Town town = Settlement.FindFirst((Settlement z) => z.IsTown).Town;
            equipment.ForEach((EquipmentElement e) => equipment_value += town.GetItemPrice(e, MobileParty.MainParty, true));
            return equipment_value;
        }

        public bool MyCanMarry(Hero maidenOrSuitor)
        {
            return maidenOrSuitor.IsAlive && !maidenOrSuitor.IsPrisoner && maidenOrSuitor.Spouse == null && maidenOrSuitor.IsLord &&
                !maidenOrSuitor.IsMinorFactionHero && !maidenOrSuitor.IsNotable && !maidenOrSuitor.IsTemplate &&
                maidenOrSuitor.CharacterObject.Age >= 18;
        }
        public override void OnRefresh()
        {
            DowryPricesText = new TextObject("{=vKsoAjVZRxL}Dowry Prices").ToString();
            EquipmentText = GameTexts.FindText("str_equipment", null).ToString();
            PerksText = new TextObject("{=fVmG6YTtQai}Perks").ToString();
            AttributesText = GameTexts.FindText("str_attributes", null).ToString();

            MarriagePrices.Clear();
            HeroItems.Clear();
            PerksPerSkillLeftSide.Clear();
            PerksPerSkillRightSide.Clear();
            rows_left_side = rows_right_side = 0;
            Attributes.Clear();

            var vm = base.ViewModel;
            if (vm == null) return;
            var hero = Traverse.Create(vm).Field("_hero").GetValue<Hero>();
            if (hero == null) return;

            // This is for compatibility with old UiExtender which ran OnRefresh more than once per refresh
            if (vm.Stats.Count <= 4)
            {
                TextObject levelText = GameTexts.FindText("str_level", null);
                vm.Stats.Add(new StringPairItemVM(levelText.ToString() + ':', hero.Level.ToString()));
                TextObject equipmentSaleValueText = new TextObject("{=oSsmAi5ejuy}Equipment Sale Value:");
                vm.Stats.Add(new StringPairItemVM(equipmentSaleValueText.ToString(), HeroEquipmentValue(hero).ToString("N0")));

                TextObject prisonerText = new TextObject("{=MUOPLUL4Fru}Prisoner:");
                TextObject freeText = new TextObject("{=EfO4DVzClVp}Free");
                vm.Stats.Add(new StringPairItemVM(prisonerText.ToString(), hero.IsPrisoner ? hero.PartyBelongedToAsPrisoner.Name.ToString() : freeText.ToString()));
                TextObject armyText = new TextObject("{=2LlrWkeotbJ}Army:");
                TextObject noneText = new TextObject("{=nBA38eYcLkV}Not in Army");
                vm.Stats.Add(new StringPairItemVM(armyText.ToString(), hero.PartyBelongedTo != null && hero.PartyBelongedTo.Army != null ? hero.PartyBelongedTo.Army.Name.ToString() : noneText.ToString()));
            }

            {
                var res = new MBBindingList<StringPairItemVM>();
                if (hero.Clan != null && hero.Clan.Leader != null)
                {
                    var mh = Hero.MainHero;
                    if (MyCanMarry(hero))
                    {
                        if (MyCanMarry(mh) && mh.IsFemale != hero.IsFemale)
                        {
                            // PERSUATION DOWRY
                            MarriageBarterable mb1 = new MarriageBarterable(mh, PartyBase.MainParty, hero, mh);
                            TextObject persuasionDowryText = new TextObject("{=d6gwqE9RW1q}Persuasion Dowry:");
                            MarriagePrices.Add(new StringPairItemVM(persuasionDowryText.ToString(), (-mb1.GetUnitValueForFaction(hero.Clan)).ToString("N0")));

                            // BARTER DOWRY
                            MarriageBarterable mb2 = new MarriageBarterable(mh, PartyBase.MainParty, mh, hero);
                            int dowry = -mb2.GetUnitValueForFaction(hero.Clan);
                            /*
                            // Normalize prices to 0 relation
                            int personal_relation = hero.GetRelation(mh);
                            dowry -= personal_relation * 1000;
                            */
                            TextObject barterDowryText = new TextObject("{=EJ8BsdSHZTv}Barter Dowry:");
                            MarriagePrices.Add(new StringPairItemVM(barterDowryText.ToString(), dowry.ToString("N0")));
                        }
                        {
                            RomanceModel romanceModel = Campaign.Current.Models.RomanceModel;
                            foreach (var clanHero in Hero.MainHero.Clan.Lords)
                            {
                                if (clanHero != mh && MyCanMarry(clanHero) && clanHero.IsFemale != hero.IsFemale)
                                {
                                    MarriageBarterable mb3 = new MarriageBarterable(mh, PartyBase.MainParty, clanHero, hero);
                                    int dowry = -mb3.GetUnitValueForFaction(hero.Clan);
                                    MarriagePrices.Add(new StringPairItemVM(
                                        String.Format("{0}({1}):", clanHero.Name, CampaignUIHelper.GetHeroRelationToHeroText(clanHero, mh, false)),
                                        dowry.ToString("N0")));
                                }
                            }
                        }
                    }
                }
            }

            {
                Town town = Settlement.FindFirst((Settlement z) => z.IsTown).Town;

                var equipment = HeroEquipment(hero);

                var itemRoster = new ItemRoster();
                var inventoryLogic = new InventoryLogic(null);
                inventoryLogic.Initialize(itemRoster, MobileParty.MainParty, false, true, CharacterObject.PlayerCharacter, InventoryManager.InventoryCategoryType.None, town.MarketData, false, new TaleWorlds.Localization.TextObject("nothingburger"));

                foreach (var e in equipment)
                {
                    itemRoster.AddToCounts(e, 1);
                }

                foreach (var e in itemRoster)
                {
                    var item_vm = new SPItemVM(inventoryLogic, hero.IsFemale, true, InventoryMode.Default, e,
                        InventoryLogic.InventorySide.OtherInventory, "", "",
                        town.GetItemPrice(e.EquipmentElement, MobileParty.MainParty, true));

                    HeroItems.Add(item_vm);
                }
            }
            {
                Dictionary<SkillObject, List<PerkObject>> perks_per_skill = new Dictionary<SkillObject, List<PerkObject>>();
                Traverse.IterateFields(Campaign.Current.DefaultPerks, (Traverse tr) =>
                {
                    var uncasted = tr.GetValue();
                    var val = uncasted as PerkObject;

                    if (val != null)
                    {
                        if (hero.GetPerkValue(val))
                        {
                            List<PerkObject> skill_group;
                            if (perks_per_skill.ContainsKey(val.Skill))
                            {
                                skill_group = perks_per_skill[val.Skill];
                            }
                            else
                            {
                                skill_group = new List<PerkObject>();
                            }
                            skill_group.Add(val);
                            perks_per_skill[val.Skill] = skill_group;
                        }
                    }
                });
                foreach (var skill in perks_per_skill)
                {
                    var skill_vm = new PerksForSkillVM(skill.Key, skill.Value);
                    int rows = skill_vm.Perks.Count + 1;
                    if (rows_left_side <= rows_right_side)
                    {
                        PerksPerSkillLeftSide.Add(skill_vm);
                        rows_left_side += rows;
                    }
                    else
                    {
                        PerksPerSkillRightSide.Add(skill_vm);
                        rows_right_side += rows;
                    }
                }
            }
            {
                foreach (CharacterAttribute att in TaleWorlds.CampaignSystem.Extensions.Attributes.All)
                {
                    Attributes.Add(new ExtenderAttributeVM(hero, att));
                }
            }
        }
    }

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
                    tr.Add(new EncyclopediaFilterItem(new TextObject("{=dkQJzg3erHZ}Clan Leader", null), (object hero) =>
                    {
                        Hero h = (Hero)hero;
                        return h.Clan != null && !h.Clan.IsMinorFaction && h.Clan.Leader == h;
                    }));
                }
                {
                    List<EncyclopediaFilterItem> prisonerList = new List<EncyclopediaFilterItem>();
                    prisonerList.Add(new EncyclopediaFilterItem(new TextObject("{=visGHcNwvj7}Not Prisoner", null),
                        (object h) => ((Hero)h).PartyBelongedToAsPrisoner == null));
                    prisonerList.Add(new EncyclopediaFilterItem(new TextObject("{=E9b41bY9PnC}Prisoner", null),
                        (object h) => ((Hero)h).PartyBelongedToAsPrisoner != null));
                    list.Add(new EncyclopediaFilterGroup(prisonerList, new TextObject("{=ggFT1tTOMeK}Prisoner Status", null)));
                }
                {
                    List<EncyclopediaFilterItem> clanStatusList = new List<EncyclopediaFilterItem>();

                    clanStatusList.Add(new EncyclopediaFilterItem(GameTexts.FindText("str_other"),
                        (object h) => ((Hero)h).Clan != Clan.PlayerClan));
                    clanStatusList.Add(new EncyclopediaFilterItem(new TextObject("{=1kouok0blVC}Player Clan", null),
                        (object h) => ((Hero)h).Clan == Clan.PlayerClan));

                    list.Add(new EncyclopediaFilterGroup(clanStatusList, GameTexts.FindText("str_clan", null)));
                }
                {
                    List<EncyclopediaFilterItem> KingdomList = new List<EncyclopediaFilterItem>();

                    foreach (var k in Kingdom.All)
                    {
                        var cond = (Clan c) => c != null && c.Kingdom == k;
                        KingdomList.Add(new EncyclopediaFilterItem(k.Name,
                        (object h) => cond(((Hero)h).Clan)));
                    }

                    list.Add(new EncyclopediaFilterGroup(KingdomList, GameTexts.FindText("str_kingdom", null)));
                }
            }
        }
    }

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
    }

    class ListLevelComparer : DefaultEncyclopediaHeroPage.EncyclopediaListHeroComparer
    {
        public override int Compare(EncyclopediaListItem x, EncyclopediaListItem y)
        {
            return base.CompareHeroes(x, y, _comparison);
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

        private static Func<Hero, Hero, int> _comparison = (Hero h1, Hero h2) => h1.Level.CompareTo(h2.Level);
    }

}
