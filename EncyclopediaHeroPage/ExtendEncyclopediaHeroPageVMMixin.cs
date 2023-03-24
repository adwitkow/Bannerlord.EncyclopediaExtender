using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using EncyclopediaExtender.EncyclopediaHeroPage.ViewModels;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.CampaignSystem.ViewModelCollection.Inventory;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace EncyclopediaExtender.EncyclopediaHeroPage
{
    [ViewModelMixin("RefreshValues", true)]
    public class EncyclopediaHeroPageVMMixin : BaseViewModelMixin<EncyclopediaHeroPageVM>
    {
        [DataSourceProperty]
        public string AttributesText { get; set; }

        [DataSourceProperty]
        public string DowryPricesText { get; set; }

        [DataSourceProperty]
        public string EquipmentText { get; set; }

        [DataSourceProperty]
        public string PerksText { get; set; }

        [DataSourceProperty]
        public MBBindingList<StringPairItemVM> MarriagePrices { get; set; }

        [DataSourceProperty]
        public MBBindingList<SPItemVM> HeroItems { get; set; }

        [DataSourceProperty]
        public MBBindingList<PerksForSkillVM> PerksPerSkillLeftSide { get; set; }

        [DataSourceProperty]
        public MBBindingList<PerksForSkillVM> PerksPerSkillRightSide { get; set; }

        [DataSourceProperty]
        public MBBindingList<ExtenderAttributeVM> Attributes { get; set; }

        public EncyclopediaHeroPageVMMixin(EncyclopediaHeroPageVM vm) : base(vm)
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

            Town town = Settlement.FindFirst((z) => z.IsTown).Town;
            equipment = equipment.OrderBy((e) => -town.GetItemPrice(e, MobileParty.MainParty, true)).ToList();

            return equipment;
        }

        public static int HeroEquipmentValue(Hero h)
        {
            var equipment = HeroEquipment(h);
            int equipment_value = 0;
            Town town = Settlement.FindFirst((z) => z.IsTown).Town;
            equipment.ForEach((e) => equipment_value += town.GetItemPrice(e, MobileParty.MainParty, true));
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
            Attributes.Clear();

            var vm = ViewModel;
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

                    foreach (var clanHero in Hero.MainHero.Clan.Lords)
                    {
                        if (clanHero != mh && MyCanMarry(clanHero) && clanHero.IsFemale != hero.IsFemale)
                        {
                            MarriageBarterable mb3 = new MarriageBarterable(mh, PartyBase.MainParty, clanHero, hero);
                            int dowry = -mb3.GetUnitValueForFaction(hero.Clan);
                            MarriagePrices.Add(new StringPairItemVM(
                                string.Format("{0}({1}):", clanHero.Name, CampaignUIHelper.GetHeroRelationToHeroText(clanHero, mh, false)),
                                dowry.ToString("N0")));
                        }
                    }
                }
            }

            Town town = Settlement.FindFirst((z) => z.IsTown).Town;

            var equipment = HeroEquipment(hero);

            var itemRoster = new ItemRoster();
            var inventoryLogic = new InventoryLogic(null);
            inventoryLogic.Initialize(itemRoster, MobileParty.MainParty, false, true,
                                      CharacterObject.PlayerCharacter, InventoryManager.InventoryCategoryType.None,
                                      town.MarketData, false);

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

            Dictionary<SkillObject, List<PerkObject>> perks_per_skill = new Dictionary<SkillObject, List<PerkObject>>();
            Traverse.IterateFields(Campaign.Current.DefaultPerks, (tr) =>
            {
                var uncasted = tr.GetValue();

                if (uncasted is PerkObject perk && hero.GetPerkValue(perk))
                {
                    List<PerkObject> skill_group;
                    if (perks_per_skill.ContainsKey(perk.Skill))
                    {
                        skill_group = perks_per_skill[perk.Skill];
                    }
                    else
                    {
                        skill_group = new List<PerkObject>();
                    }

                    skill_group.Add(perk);
                    perks_per_skill[perk.Skill] = skill_group;
                }
            });

            var rowsLeftSide = 0;
            var rowsRightSide = 0;
            foreach (var skill in perks_per_skill)
            {
                var skill_vm = new PerksForSkillVM(skill.Key, skill.Value);
                int rows = skill_vm.Perks.Count + 1;
                if (rowsLeftSide <= rowsRightSide)
                {
                    PerksPerSkillLeftSide.Add(skill_vm);
                    rowsLeftSide += rows;
                }
                else
                {
                    PerksPerSkillRightSide.Add(skill_vm);
                    rowsRightSide += rows;
                }
            }

            foreach (CharacterAttribute att in TaleWorlds.CampaignSystem.Extensions.Attributes.All)
            {
                Attributes.Add(new ExtenderAttributeVM(hero, att));
            }
        }
    }

}
