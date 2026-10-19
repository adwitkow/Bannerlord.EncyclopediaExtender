using Bannerlord.EncyclopediaExtender.Configuration;
using Bannerlord.EncyclopediaExtender.EncyclopediaHeroPage.ViewModels;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using HarmonyLib;
using Helpers;
using System;
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
        public MBBindingList<PerkColumnVM> PerkColumns { get; set; }

        [DataSourceProperty]
        public MBBindingList<ExtenderAttributeVM> Attributes { get; set; }

        public EncyclopediaHeroPageVMMixin(EncyclopediaHeroPageVM vm) : base(vm)
        {
            _hero = vm.Obj as Hero;

            MarriagePrices = new MBBindingList<StringPairItemVM>();
            HeroItems = new MBBindingList<SPItemVM>();
            PerkColumns = new MBBindingList<PerkColumnVM>();
            Attributes = new MBBindingList<ExtenderAttributeVM>();

            DowryPricesText = "";
            EquipmentText = "";
            PerksText = "";
            AttributesText = "";
        }

        public override void OnRefresh()
        {
            DowryPricesText = Texts.DowryPrices.ToString();
            EquipmentText = Texts.Equipment.ToString();
            PerksText = Texts.Perks.ToString();
            AttributesText = Texts.Attributes.ToString();

            MarriagePrices.Clear();
            HeroItems.Clear();
            PerkColumns.Clear();
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
            if (Settings.HeroLevel)
            {
                AddViewModelStatPair(Texts.Level, hero.Level);
            }

            if (Settings.HeroEquipmentSaleValue)
            {
                var equipmentValue = CalculateEquipmentValue(hero);
                AddViewModelStatPair(Texts.EquipmentSaleValue, equipmentValue);
            }

            if (Settings.HeroImprisonment)
            {
                string prisonerValue = GetImprisonmentValue(hero);
                AddViewModelStatPair(Texts.Prisoner, prisonerValue);
            }

            if (Settings.HeroArmy)
            {
                var armyValue = hero.PartyBelongedTo != null && hero.PartyBelongedTo.Army != null
                    ? hero.PartyBelongedTo.Army.Name.ToString()
                    : Texts.NotInArmy.ToString();
                AddViewModelStatPair(Texts.Army, armyValue);
            }
        }

        private void AddDowryValues(Hero hero)
        {
            if (hero.Clan is null || hero.Clan.Leader is null)
            {
                return;
            }

            var mainHero = Hero.MainHero;

            if (CanMarry(mainHero) && mainHero.IsFemale != hero.IsFemale)
            {
                if (Settings.HeroPersuasionDowry)
                {
                    var mainHeroToHero = new MarriageBarterable(mainHero, PartyBase.MainParty, hero, mainHero);
                    var persuasionDowryValue = -mainHeroToHero.GetUnitValueForFaction(hero.Clan);
                    var persuasionDowryFormatted = persuasionDowryValue.ToString(Constants.WholeNumberFormat);
                    MarriagePrices.AddPair(Texts.PersuasionDowry, persuasionDowryFormatted);
                }

                if (Settings.HeroBarterDowry)
                {
                    var heroToMainHero = new MarriageBarterable(mainHero, PartyBase.MainParty, mainHero, hero);
                    int dowry = -heroToMainHero.GetUnitValueForFaction(hero.Clan);
                    MarriagePrices.AddPair(Texts.BarterDowry, dowry.ToString(Constants.WholeNumberFormat));
                }
            }

            if (!Settings.HeroClanMemberDowry)
            {
                return;
            }

            foreach (var clanHero in GetClanHeroes(mainHero))
            {
                if (clanHero != mainHero && CanMarry(clanHero) && clanHero.IsFemale != hero.IsFemale)
                {
                    MarriageBarterable heroToClanHero = new MarriageBarterable(mainHero, PartyBase.MainParty, clanHero, hero);
                    int dowry = -heroToClanHero.GetUnitValueForFaction(hero.Clan);
                    var relation = CampaignUIHelper.GetHeroRelationToHeroText(clanHero, mainHero, false);
                    MarriagePrices.AddPair($"{clanHero.Name} ({relation}):", dowry.ToString(Constants.WholeNumberFormat));
                }
            }
        }

        private void AddHeroItems(Hero hero)
        {
            if (!Settings.HeroEquipmentList)
            {
                return;
            }

            Town town = Settlement.FindFirst(z => z.IsTown).Town;

            var equipment = GetHeroEquipment(hero);

            var itemRoster = new ItemRoster();
            var inventoryLogic = CreateNewInventoryLogic(town, itemRoster);

            foreach (var element in equipment)
            {
                itemRoster.AddToCounts(element, 1);
            }

            foreach (var element in itemRoster)
            {
                var price = town.GetItemPrice(element.EquipmentElement, MobileParty.MainParty, true);
                var itemVm = CreateNewSPItemVM(hero, inventoryLogic, element, price);

                HeroItems.Add(itemVm);
            }
        }

        private void AddPerks(Hero hero)
        {
            if (!Settings.HeroPerks)
            {
                return;
            }

            var perksPerSkill = new Dictionary<SkillObject, List<PerkObject>>();

            // TODO: Maybe this can finally be refactored
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

            var perkSkillGroups = new List<PerkSkillGroupVM>();
            foreach (var pair in perksPerSkill)
            {
                var skill = pair.Key;
                var perks = pair.Value;
                perkSkillGroups.Add(new PerkSkillGroupVM(skill, perks));
            }

            foreach (var column in BuildColumns(perkSkillGroups))
            {
                PerkColumns.Add(column);
            }
        }

        private void AddAttributes(Hero hero)
        {
            if (!Settings.HeroAttributes)
            {
                return;
            }

            foreach (CharacterAttribute att in TaleWorlds.CampaignSystem.Extensions.Attributes.All)
            {
                Attributes.Add(new ExtenderAttributeVM(hero, att));
            }
        }

        private static string GetImprisonmentValue(Hero hero)
        {
            string result;

            if (hero.IsPrisoner)
            {
                var capturerParty = hero.PartyBelongedToAsPrisoner;

                if (capturerParty is not null)
                {
                    result = capturerParty.Name.ToString();
                }
                else
                {
                    result = Texts.Unknown.ToString();
                }
            }
            else
            {
                result = Texts.Free.ToString();
            }

            return result;
        }

        private static SPItemVM CreateNewSPItemVM(Hero hero, InventoryLogic inventoryLogic, ItemRosterElement element, int price)
        {
#if LOWER_THAN_1_3
            return new SPItemVM(inventoryLogic,
                hero.IsFemale,
                true,
                InventoryMode.Default,
                element,
                InventoryLogic.InventorySide.OtherInventory,
                string.Empty,
                string.Empty,
                price);
#else
            return new SPItemVM(inventoryLogic,
                hero.IsFemale,
                true,
                InventoryScreenHelper.InventoryMode.Default,
                element,
                InventoryLogic.InventorySide.OtherInventory,
                price);
#endif
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

        private void AddViewModelStatPair<T>(TextObject header, T value)
        {
            if (ViewModel is null)
            {
                return;
            }

            ViewModel.Stats.AddPair(header, value);
        }

        private static IEnumerable<Hero> GetClanHeroes(Hero hero)
        {
#if LOWER_THAN_1_3
            return hero.Clan.Lords;
#else
            return hero.Clan.Heroes;
#endif
        }

        private static InventoryLogic CreateNewInventoryLogic(Town town, ItemRoster itemRoster)
        {
            var inventoryLogic = new InventoryLogic(null);

#if LOWER_THAN_1_3
            inventoryLogic.Initialize(itemRoster,
                MobileParty.MainParty,
                false,
                true,
                CharacterObject.PlayerCharacter,
                InventoryManager.InventoryCategoryType.None,
                town.MarketData,
                false);
#else
            inventoryLogic.Initialize(itemRoster,
                MobileParty.MainParty,
                false,
                true,
                CharacterObject.PlayerCharacter,
                InventoryScreenHelper.InventoryCategoryType.None,
                town.MarketData,
                false,
                InventoryScreenHelper.InventoryMode.Default);
#endif

            return inventoryLogic;
        }

        private static MBBindingList<PerkColumnVM> BuildColumns(
            IReadOnlyList<PerkSkillGroupVM> items,
            int columnCount = 2)
        {
            var columns = new MBBindingList<PerkColumnVM>();
            if (items.Count == 0)
            {
                return columns;
            }

            int perColumn = (int)Math.Ceiling(items.Count / (double)columnCount);
            for (int i = 0; i < items.Count; i += perColumn)
            {
                var chunk = new MBBindingList<PerkSkillGroupVM>();

                foreach (var item in items.Skip(i).Take(perColumn))
                {
                    chunk.Add(item);
                }

                columns.Add(new PerkColumnVM(chunk));
            }

            return columns;
        }
    }
}