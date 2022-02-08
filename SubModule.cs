using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx;
using Bannerlord.UIExtenderEx.Prefabs2;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map;
using TaleWorlds.MountAndBlade;
using Bannerlord.UIExtenderEx.ViewModels;
using System.Xml;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.Library;
using TaleWorlds.Core.ViewModelCollection;
using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Barterables;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.CampaignSystem.Encyclopedia.Pages;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.Localization;

namespace EncyclopediaExtender
{
    public class SubModule : MBSubModuleBase
    {
        private UIExtender _extender = new UIExtender("EncyclopediaExtender");
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            Harmony harmony = new Harmony("EncyclopediaExtender");
            harmony.PatchAll();

            _extender.Register(typeof(SubModule).Assembly);
            _extender.Enable();
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
        }
    }

    [PrefabExtension("EncyclopediaHeroPage", "descendant::Widget[@Id='InfoContainer']")]
    public sealed class HeroPagePatch : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        [PrefabExtensionFileName(true)]
        public String File => "HeroPagePatch";
    }


    [PrefabExtension("EncyclopediaHeroPage", "descendant::GridWidget[@Id='FamilyGrid']")]
    public sealed class HeroPagePerksPatch : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        [PrefabExtensionFileName(true)]
        public String File => "HeroPagePerksPatch";
    }

    public class MyUtil
    {
        public static List<EquipmentElement> Equipment(Hero x)
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

        public static int EquipmentValue(Hero h)
        {
            var equipment = Equipment(h);
            int equipment_value = 0;
            Town town = Settlement.FindFirst((Settlement z) => z.IsTown).Town;
            equipment.ForEach((EquipmentElement e) => equipment_value += town.GetItemPrice(e, MobileParty.MainParty, true));
            return equipment_value;
        }
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
                Perks.Add(new StringPairItemVM(perk.RequiredSkillValue.ToString() + ':', perk.ToString() + ", " + perk.PrimaryDescription));
            }
        }
    }

    [ViewModelMixin("RefreshValues", true)]
    public class CustomEncyclopediaHeroPageVM : BaseViewModelMixin<EncyclopediaHeroPageVM>
    {

        [DataSourceProperty]
        public MBBindingList<StringPairItemVM> MarriagePrices { get; set; }

        [DataSourceProperty]
        public MBBindingList<SPItemVM> HeroItems { get; set; }

        [DataSourceProperty]
        public MBBindingList<PerksForSkillVM> PerksPerSkill { get; set; }

        public CustomEncyclopediaHeroPageVM(EncyclopediaHeroPageVM vm) : base(vm)
        {
            MarriagePrices = new MBBindingList<StringPairItemVM>();
            HeroItems = new MBBindingList<SPItemVM>();
            PerksPerSkill = new MBBindingList<PerksForSkillVM>();
        }

        public override void OnRefresh()
        {
            MarriagePrices.Clear();
            HeroItems.Clear();
            PerksPerSkill.Clear();

            var vm = base.ViewModel;
            if (vm != null)
            {
                var hero = Traverse.Create(vm).Field("_hero").GetValue<Hero>();

                // This is for compatibility with old UiExtender which ran OnRefresh more than once per refresh
                if (vm.Stats.Count <= 4)
                {
                    vm.Stats.Add(new StringPairItemVM("Level:", hero.Level.ToString()));
                    vm.Stats.Add(new StringPairItemVM("Equipment sale value:", MyUtil.EquipmentValue(hero).ToString("N0")));

                    vm.Stats.Add(new StringPairItemVM("Prisoner:", hero.IsPrisoner ? hero.PartyBelongedToAsPrisoner.Name.ToString() : "Free"));
                    vm.Stats.Add(new StringPairItemVM("Army:", hero.PartyBelongedTo != null && hero.PartyBelongedTo.Army != null ? hero.PartyBelongedTo.Army.Name.ToString() : "None"));
                }

                {
                    var res = new MBBindingList<StringPairItemVM>();
                    if (hero.Clan != null)
                    {
                        // PERSUATION DOWRY
                        MarriageBarterable mb1 = new MarriageBarterable(Hero.MainHero, PartyBase.MainParty, hero, Hero.MainHero);
                        MarriagePrices.Add(new StringPairItemVM("Persuasion dowry:", (-mb1.GetUnitValueForFaction(hero.Clan)).ToString("N0")));

                        // BARTER DOWRY
                        MarriageBarterable mb2 = new MarriageBarterable(Hero.MainHero, PartyBase.MainParty, Hero.MainHero, hero);
                        int dowry = -mb2.GetUnitValueForFaction(hero.Clan);
                        /*
                        // Normalize prices to 0 relation
                        int personal_relation = hero.GetRelation(Hero.MainHero);
                        dowry -= personal_relation * 1000;
                        */
                        MarriagePrices.Add(new StringPairItemVM("Barter dowry:", dowry.ToString("N0")));
                    }
                }

                {
                    Town town = Settlement.FindFirst((Settlement z) => z.IsTown).Town;

                    var equipment = MyUtil.Equipment(hero);

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
                            InventoryLogic.InventorySide.PlayerInventory, "", "",
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
                        PerksPerSkill.Add(new PerksForSkillVM(skill.Key, skill.Value));
                    }
                }
            }
        }
    }

    [ViewModelMixin("RefreshValues", true)]
    public class CustomEncyclopediaClanPageVM : BaseViewModelMixin<EncyclopediaClanPageVM>
    {
        public CustomEncyclopediaClanPageVM(EncyclopediaClanPageVM vm) : base(vm)
        {

        }
        public override void OnRefresh()
        {
            var vm = base.ViewModel;
            if (vm != null)
            {
                var clan = Traverse.Create(vm).Field("_clan").GetValue<Clan>();
                if (vm.ClanInfo.Count <= 3)
                {
                    vm.ClanInfo.Add(new StringPairItemVM("", ""));
                    vm.ClanInfo.Add(new StringPairItemVM("Cash:", clan.Leader.Gold.ToString("N0")));
                    vm.ClanInfo.Add(new StringPairItemVM("Debt:", clan.DebtToKingdom.ToString()));
                    var kingdom = clan.Kingdom;
                    int kingdom_wealth = 0;
                    if (kingdom != null && !clan.IsMinorFaction)
                    {
                        kingdom_wealth = kingdom.KingdomBudgetWallet / (kingdom.Clans.Count + 1) / 2;
                        vm.ClanInfo.Add(new StringPairItemVM("Share of Kingdom wealth:", kingdom_wealth.ToString("N0")));
                    }
                    vm.ClanInfo.Add(new StringPairItemVM("Nominal Total Wealth:", (clan.Leader.Gold + kingdom_wealth - clan.DebtToKingdom).ToString("N0")));
                }

            }
        }
    }

    [PrefabExtension("EncyclopediaFactionPage", "descendant::EncyclopediaSubPageElement[@Id='Leader']")]
    public sealed class FactionPagePatch : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        [PrefabExtensionFileName(true)]
        public String File => "FactionPagePatch";
    }

    [ViewModelMixin("RefreshValues", true)]
    public class CustomEncyclopediaFactionPageVM : BaseViewModelMixin<EncyclopediaFactionPageVM>
    {
        [DataSourceProperty]
        public MBBindingList<StringPairItemVM> WealthInfo { get; set; }
        public CustomEncyclopediaFactionPageVM(EncyclopediaFactionPageVM vm) : base(vm)
        {
            WealthInfo = new MBBindingList<StringPairItemVM>();
        }
        public override void OnRefresh()
        {
            WealthInfo.Clear();
            var vm = base.ViewModel;
            if (vm != null)
            {
                var kingdom = Traverse.Create(vm).Field("_faction").GetValue<Kingdom>();

                WealthInfo.Add(new StringPairItemVM("Kingdom Bank:", kingdom.KingdomBudgetWallet.ToString("N0")));
                int clans_wealth = 0;
                foreach (var clan in kingdom.Clans)
                {
                    if (clan.IsUnderMercenaryService || clan.IsMinorFaction) continue;
                    clans_wealth += clan.Leader.Gold - clan.DebtToKingdom;
                }
                WealthInfo.Add(new StringPairItemVM("Sum of Clan Wealth:", clans_wealth.ToString("N0")));
            }
        }
    }

    [HarmonyPatch(typeof(DefaultEncyclopediaHeroPage), "InitializeFilterItems")]
    class FilterPatch
    {
        static void Postfix(IEnumerable<EncyclopediaFilterGroup> __result)
        {
            var list = __result as List<EncyclopediaFilterGroup>;
            if (list != null)
            {
                var tr = Traverse.Create(list[4]).Field("Filters").GetValue<List<EncyclopediaFilterItem>>();
                tr.Add(new EncyclopediaFilterItem(new TextObject("Clan Leader", null), (object hero) =>
                {
                    Hero h = (Hero)hero;
                    return h.Clan != null && !h.Clan.IsMinorFaction && h.Clan.Leader == h;
                }));
            }
        }
    }
}