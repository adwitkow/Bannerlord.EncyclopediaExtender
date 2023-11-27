using Bannerlord.EncyclopediaExtender.EncyclopediaHeroPage.ViewModels;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
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

namespace Bannerlord.EncyclopediaExtender.EncyclopediaHeroPage
{
    [ViewModelMixin(nameof(EncyclopediaHeroPageVM.RefreshValues), true)]
    public class EncyclopediaHeroPageVMMixin : BaseViewModelMixin<EncyclopediaHeroPageVM>
    {
        private static readonly TextObject _equipmentTextObject = GameTexts.FindText("str_equipment", null);
        private static readonly TextObject _attributesTextObject = GameTexts.FindText("str_attributes", null);
        private static readonly TextObject _levelTextObject = GameTexts.FindText("str_level", null);

        private static readonly TextObject _dowryPricesTextObject = new TextObject("{=vKsoAjVZRxL}Dowry Prices");
        private static readonly TextObject _perksTextObject = new TextObject("{=fVmG6YTtQai}Perks");
        private static readonly TextObject _equipmentValueTextObject = new TextObject("{=oSsmAi5ejuy}Equipment Sale Value:");
        private static readonly TextObject _prisonerTextObject = new TextObject("{=MUOPLUL4Fru}Prisoner:");
        private static readonly TextObject _freeTextObject = new TextObject("{=EfO4DVzClVp}Free");
        private static readonly TextObject _armyTextObject = new TextObject("{=2LlrWkeotbJ}Army:");
        private static readonly TextObject _notInArmyTextObject = new TextObject("{=nBA38eYcLkV}Not in Army");
        private static readonly TextObject _persuasionDowryTextObject = new TextObject("{=d6gwqE9RW1q}Persuasion Dowry:");
        private static readonly TextObject _barterDowryTextObject = new TextObject("{=EJ8BsdSHZTv}Barter Dowry:");

        private readonly Hero? _hero;

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
            _hero = vm.Obj as Hero;

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

        public override void OnRefresh()
        {
            DowryPricesText = _dowryPricesTextObject.ToString();
            EquipmentText = _equipmentTextObject.ToString();
            PerksText = _perksTextObject.ToString();
            AttributesText = _attributesTextObject.ToString();

            MarriagePrices.Clear();
            HeroItems.Clear();
            PerksPerSkillLeftSide.Clear();
            PerksPerSkillRightSide.Clear();
            Attributes.Clear();

            if (_hero is null || ViewModel is null)
            {
                return;
            }

            AddViewModelStats(_hero);
            AddDowryValues(_hero);
            AddHeroItems(_hero);
            AddPerks(_hero);
            AddAttributes(_hero);
        }

        private void AddViewModelStats(Hero hero)
        {
            AddViewModelStatPair($"{_levelTextObject}:", hero.Level);

            var equipmentValue = CalculateEquipmentValue(hero);
            AddViewModelStatPair(_equipmentValueTextObject, equipmentValue);

            var prisonerValue = hero.IsPrisoner
                ? hero.PartyBelongedToAsPrisoner.Name.ToString()
                : _freeTextObject.ToString();
            AddViewModelStatPair(_prisonerTextObject, prisonerValue);

            var armyValue = hero.PartyBelongedTo != null && hero.PartyBelongedTo.Army != null
                ? hero.PartyBelongedTo.Army.Name.ToString()
                : _notInArmyTextObject.ToString();
            AddViewModelStatPair(_armyTextObject, armyValue);
        }

        private void AddDowryValues(Hero hero)
        {
            if (hero.Clan is null || hero.Clan.Leader is null || !CanMarry(hero))
            {
                return;
            }

            var mainHero = Hero.MainHero;

            if (CanMarry(mainHero) && mainHero.IsFemale != hero.IsFemale)
            {
                var mainHeroToHero = new MarriageBarterable(mainHero, PartyBase.MainParty, hero, mainHero);
                var persuasionDowryValue = -mainHeroToHero.GetUnitValueForFaction(hero.Clan);
                var persuasionDowryFormatted = persuasionDowryValue.ToString("N0");
                MarriagePrices.AddPair(_persuasionDowryTextObject, persuasionDowryFormatted);

                var heroToMainHero = new MarriageBarterable(mainHero, PartyBase.MainParty, mainHero, hero);
                int dowry = -heroToMainHero.GetUnitValueForFaction(hero.Clan);
                MarriagePrices.AddPair(_barterDowryTextObject, dowry.ToString("N0"));
            }

            foreach (var clanHero in mainHero.Clan.Lords)
            {
                if (clanHero != mainHero && CanMarry(clanHero) && clanHero.IsFemale != hero.IsFemale)
                {
                    MarriageBarterable heroToClanHero = new MarriageBarterable(mainHero, PartyBase.MainParty, clanHero, hero);
                    int dowry = -heroToClanHero.GetUnitValueForFaction(hero.Clan);
                    var relation = CampaignUIHelper.GetHeroRelationToHeroText(clanHero, mainHero, false);
                    MarriagePrices.AddPair($"{clanHero.Name} ({relation}):", dowry.ToString("N0"));
                }
            }
        }

        private void AddHeroItems(Hero hero)
        {
            Town town = Settlement.FindFirst(z => z.IsTown).Town;

            var equipment = GetHeroEquipment(hero);

            var itemRoster = new ItemRoster();
            var inventoryLogic = new InventoryLogic(null);
            inventoryLogic.Initialize(itemRoster,
                MobileParty.MainParty,
                false,
                true,
                CharacterObject.PlayerCharacter,
                InventoryManager.InventoryCategoryType.None,
                town.MarketData,
                false);

            foreach (var element in equipment)
            {
                itemRoster.AddToCounts(element, 1);
            }

            foreach (var element in itemRoster)
            {

                var price = town.GetItemPrice(element.EquipmentElement, MobileParty.MainParty, true);
                var itemVm = new SPItemVM(inventoryLogic,
                    hero.IsFemale,
                    true,
                    InventoryMode.Default,
                    element,
                    InventoryLogic.InventorySide.OtherInventory,
                    string.Empty,
                    string.Empty,
                    price);

                HeroItems.Add(itemVm);
            }
        }

        private void AddPerks(Hero hero)
        {
            var perksPerSkill = new Dictionary<SkillObject, List<PerkObject>>();

            Traverse.IterateFields(Campaign.Current.DefaultPerks, tr =>
            {
                var uncasted = tr.GetValue();

                if (uncasted is PerkObject perk && hero.GetPerkValue(perk))
                {
                    List<PerkObject> skillGroup;
                    if (perksPerSkill.ContainsKey(perk.Skill))
                    {
                        skillGroup = perksPerSkill[perk.Skill];
                    }
                    else
                    {
                        skillGroup = new List<PerkObject>();
                    }

                    skillGroup.Add(perk);
                    perksPerSkill[perk.Skill] = skillGroup;
                }
            });

            var rowsLeftSide = 0;
            var rowsRightSide = 0;
            foreach (var skill in perksPerSkill)
            {
                var skillVm = new PerksForSkillVM(skill.Key, skill.Value);
                int rows = skillVm.Perks.Count + 1;
                if (rowsLeftSide <= rowsRightSide)
                {
                    PerksPerSkillLeftSide.Add(skillVm);
                    rowsLeftSide += rows;
                }
                else
                {
                    PerksPerSkillRightSide.Add(skillVm);
                    rowsRightSide += rows;
                }
            }
        }

        private void AddAttributes(Hero hero)
        {
            foreach (CharacterAttribute att in TaleWorlds.CampaignSystem.Extensions.Attributes.All)
            {
                Attributes.Add(new ExtenderAttributeVM(hero, att));
            }
        }

        private static IEnumerable<EquipmentElement> GetHeroEquipment(Hero hero)
        {
            var equipment = new List<EquipmentElement>();
            for (int i = 0; i < 12; i++)
            {
                if (!hero.BattleEquipment[i].IsEmpty)
                {
                    equipment.Add(hero.BattleEquipment[i]);
                }

                if (!hero.CivilianEquipment[i].IsEmpty)
                {
                    equipment.Add(hero.CivilianEquipment[i]);
                }
            }

            Town town = Settlement.FindFirst(z => z.IsTown).Town;
            return equipment.OrderByDescending(element => town.GetItemPrice(element, MobileParty.MainParty, true));
        }

        private static int CalculateEquipmentValue(Hero hero)
        {
            var equipment = GetHeroEquipment(hero);
            Town town = Settlement.FindFirst(settlement => settlement.IsTown).Town;

            int result = 0;
            foreach (var element in equipment)
            {
                result += town.GetItemPrice(element, MobileParty.MainParty, true);
            }

            return result;
        }

        private static bool CanMarry(Hero maidenOrSuitor)
        {
            return maidenOrSuitor.IsAlive && !maidenOrSuitor.IsPrisoner
                && maidenOrSuitor.Spouse == null && maidenOrSuitor.IsLord
                && !maidenOrSuitor.IsMinorFactionHero && !maidenOrSuitor.IsNotable
                && !maidenOrSuitor.IsTemplate && maidenOrSuitor.CharacterObject.Age >= 18;
        }

        private void AddViewModelStatPair<T>(string header, T value)
        {
            if (ViewModel is null)
            {
                return;
            }

            ViewModel.Stats.AddPair(header, value);
        }

        private void AddViewModelStatPair<T>(TextObject header, T value)
        {
            AddViewModelStatPair(header.ToString(), value);
        }
    }
}