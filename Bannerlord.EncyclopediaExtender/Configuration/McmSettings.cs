using MCM.Abstractions;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Bannerlord.EncyclopediaExtender.Configuration;

internal sealed class McmSettings : AttributeGlobalSettings<McmSettings>
{
    private bool _clanId = false;
    private bool _clanCash = true;
    private bool _clanDebt = true;
    private bool _clanShareOfKingdomWealth = true;
    private bool _clanTotalWealth = true;
    private bool _clanDefectionPrice = true;
    private bool _clanCashToPersuade = true;
    private bool _clanDefectionChance = true;
    private bool _clanRenown = true;

    private bool _kingdomId = false;
    private bool _kingdomBank = true;
    private bool _kingdomClansWealthSum = true;
    private bool _capturedHeroes = true;
    private bool _imprisonedHeroes = true;

    private bool _heroId = false;
    private bool _heroLevel = true;
    private bool _heroEquipmentSaleValue = true;
    private bool _heroImprisonment = true;
    private bool _heroArmy = true;
    private bool _heroPersuasionDowry = true;
    private bool _heroBarterDowry = true;
    private bool _heroClanMemberDowry = true;
    private bool _heroEquipmentList = true;
    private bool _heroPerks = true;
    private bool _heroAttributes = true;

    private bool _settlementId = false;
    private bool _settlementProducedItems = true;
    private bool _tradeBoundSettlement = true;
    private bool _tradeBoundVillages = true;

    public override string Id => "Bannerlord.EncyclopediaExtender";
    public override string DisplayName => "Encyclopedia Extender";
    public override string FolderName => "Bannerlord.EncyclopediaExtender";
    public override string FormatType => "json2";

    #region Clan
    [SettingPropertyBool(Texts.Ids.Id, Order = 0, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanId
    {
        get => _clanId;
        set => Set(ref _clanId, ref Settings.ClanId, value);
    }

    [SettingPropertyBool(Texts.Ids.Cash, Order = 1, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanCash
    {
        get => _clanCash;
        set => Set(ref _clanCash, ref Settings.ClanCash, value);
    }

    [SettingPropertyBool(Texts.Ids.Debt, Order = 2, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanDebt
    {
        get => _clanDebt;
        set => Set(ref _clanDebt, ref Settings.ClanDebt, value);
    }

    [SettingPropertyBool(Texts.Ids.ShareOfKingdomWealth, Order = 3, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanShareOfKingdomWealth
    {
        get => _clanShareOfKingdomWealth;
        set => Set(ref _clanShareOfKingdomWealth, ref Settings.ClanShareOfKingdomWealth, value);
    }

    [SettingPropertyBool(Texts.Ids.NominalTotalWealth, Order = 4, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanTotalWealth
    {
        get => _clanTotalWealth;
        set => Set(ref _clanTotalWealth, ref Settings.ClanTotalWealth, value);
    }

    [SettingPropertyBool(Texts.Ids.DefectionPrice, Order = 5, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanDefectionPrice
    {
        get => _clanDefectionPrice;
        set => Set(ref _clanDefectionPrice, ref Settings.ClanDefectionPrice, value);
    }

    [SettingPropertyBool(Texts.Ids.CashRequiredToPersuade, Order = 6, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanCashToPersuade
    {
        get => _clanCashToPersuade;
        set => Set(ref _clanCashToPersuade, ref Settings.ClanCashToPersuade, value);
    }

    [SettingPropertyBool(Texts.Ids.DailyDefectionChance, Order = 7, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanDefectionChance
    {
        get => _clanDefectionChance;
        set => Set(ref _clanDefectionChance, ref Settings.ClanDefectionChance, value);
    }

    [SettingPropertyBool(Texts.Ids.Renown, Order = 8, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanRenown
    {
        get => _clanRenown;
        set => Set(ref _clanRenown, ref Settings.ClanRenown, value);
    }
    #endregion

    #region Kingdom
    [SettingPropertyBool(Texts.Ids.Id, Order = 0, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Kingdom)]
    public bool KingdomId
    {
        get => _kingdomId;
        set => Set(ref _kingdomId, ref Settings.KingdomId, value);
    }

    [SettingPropertyBool(Texts.Ids.KingdomBank, Order = 1, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Kingdom)]
    public bool KingdomBank
    {
        get => _kingdomBank;
        set => Set(ref _kingdomBank, ref Settings.KingdomBank, value);
    }

    [SettingPropertyBool(Texts.Ids.SumOfClanWealth, Order = 2, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Kingdom)]
    public bool KingdomClansWealthSum
    {
        get => _kingdomClansWealthSum;
        set => Set(ref _kingdomClansWealthSum, ref Settings.KingdomClansWealthSum, value);
    }

    [SettingPropertyBool(Texts.Ids.CapturedHeroes, Order = 3, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Kingdom)]
    public bool CapturedHeroes
    {
        get => _capturedHeroes;
        set => Set(ref _capturedHeroes, ref Settings.CapturedHeroes, value);
    }

    [SettingPropertyBool(Texts.Ids.ImprisonedHeroes, Order = 4, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Kingdom)]
    public bool ImprisonedHeroes
    {
        get => _imprisonedHeroes;
        set => Set(ref _imprisonedHeroes, ref Settings.ImprisonedHeroes, value);
    }
    #endregion

    #region Hero
    [SettingPropertyBool(Texts.Ids.Id, Order = 0, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroId
    {
        get => _heroId;
        set => Set(ref _heroId, ref Settings.HeroId, value);
    }

    [SettingPropertyBool(Texts.Ids.Level, Order = 1, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroLevel
    {
        get => _heroLevel;
        set => Set(ref _heroLevel, ref Settings.HeroLevel, value);
    }

    [SettingPropertyBool(Texts.Ids.EquipmentSaleValue, Order = 2, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroEquipmentSaleValue
    {
        get => _heroEquipmentSaleValue;
        set => Set(ref _heroEquipmentSaleValue, ref Settings.HeroEquipmentSaleValue, value);
    }

    [SettingPropertyBool(Texts.Ids.Prisoner, Order = 3, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroImprisonment
    {
        get => _heroImprisonment;
        set => Set(ref _heroImprisonment, ref Settings.HeroImprisonment, value);
    }

    [SettingPropertyBool(Texts.Ids.Army, Order = 4, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroArmy
    {
        get => _heroArmy;
        set => Set(ref _heroArmy, ref Settings.HeroArmy, value);
    }

    [SettingPropertyBool(Texts.Ids.PersuasionDowry, Order = 5, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroPersuasionDowry
    {
        get => _heroPersuasionDowry;
        set => Set(ref _heroPersuasionDowry, ref Settings.HeroPersuasionDowry, value);
    }

    [SettingPropertyBool(Texts.Ids.BarterDowry, Order = 6, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroBarterDowry
    {
        get => _heroBarterDowry;
        set => Set(ref _heroBarterDowry, ref Settings.HeroBarterDowry, value);
    }

    [SettingPropertyBool(Texts.Ids.ClanMemberDowries, Order = 7, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroClanMemberDowry
    {
        get => _heroClanMemberDowry;
        set => Set(ref _heroClanMemberDowry, ref Settings.HeroClanMemberDowry, value);
    }

    [SettingPropertyBool(Texts.Ids.Equipment, Order = 8, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroEquipmentList
    {
        get => _heroEquipmentList;
        set => Set(ref _heroEquipmentList, ref Settings.HeroEquipmentList, value);
    }

    [SettingPropertyBool(Texts.Ids.Perks, Order = 9, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroPerks
    {
        get => _heroPerks;
        set => Set(ref _heroPerks, ref Settings.HeroPerks, value);
    }

    [SettingPropertyBool(Texts.Ids.Attributes, Order = 10, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroAttributes
    {
        get => _heroAttributes;
        set => Set(ref _heroAttributes, ref Settings.HeroAttributes, value);
    }
    #endregion

    #region Settlement
    [SettingPropertyBool(Texts.Ids.Id, Order = 0, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Settlement)]
    public bool SettlementId
    {
        get => _settlementId;
        set => Set(ref _settlementId, ref Settings.SettlementId, value);
    }

    [SettingPropertyBool(Texts.Ids.Production, Order = 1, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Settlement)]
    public bool SettlementProducedItems
    {
        get => _settlementProducedItems;
        set => Set(ref _settlementProducedItems, ref Settings.SettlementProducedItems, value);
    }

    [SettingPropertyBool(Texts.Ids.TradeBoundSettlement, Order = 2, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Settlement)]
    public bool TradeBoundSettlement
    {
        get => _tradeBoundSettlement;
        set => Set(ref _tradeBoundSettlement, ref Settings.TradeBoundSettlement, value);
    }

    [SettingPropertyBool(Texts.Ids.TradeBoundVillages, Order = 3, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Settlement)]
    public bool TradeBoundVillages
    {
        get => _tradeBoundVillages;
        set => Set(ref _tradeBoundVillages, ref Settings.TradeBoundVillages, value);
    }
    #endregion

    public override IEnumerable<ISettingsPreset> GetBuiltInPresets()
    {
        foreach (var preset in base.GetBuiltInPresets())
        {
            yield return preset;
        }

        yield return new MemorySettingsPreset(
            Id,
            "false",
            "Everything off",
            () => CreateNew(false));

        yield return new MemorySettingsPreset(
            Id,
            "true",
            "Everything on",
            () => CreateNew(true));
    }

    private McmSettings CreateNew(bool value)
    {
        return new McmSettings
        {
            _clanId = value,
            _clanCash = value,
            _clanDebt = value,
            _clanShareOfKingdomWealth = value,
            _clanTotalWealth = value,
            _clanDefectionPrice = value,
            _clanCashToPersuade = value,
            _clanDefectionChance = value,
            _clanRenown = value,

            _kingdomId = value,
            _kingdomBank = value,
            _kingdomClansWealthSum = value,
            _capturedHeroes = value,
            _imprisonedHeroes = value,

            _heroId = value,
            _heroLevel = value,
            _heroEquipmentSaleValue = value,
            _heroImprisonment = value,
            _heroArmy = value,
            _heroPersuasionDowry = value,
            _heroBarterDowry = value,
            _heroClanMemberDowry = value,
            _heroEquipmentList = value,
            _heroPerks = value,
            _heroAttributes = value,

            _settlementId = value,
            _settlementProducedItems = value,
            _tradeBoundSettlement = value,
            _tradeBoundVillages = value,
        };
    }

    private void Set(
        ref bool field,
        ref bool proxyField,
        bool value,
        [CallerMemberName] string? name = null)
    {
        proxyField = value;

        if (field != value)
        {
            field = value;
            OnPropertyChanged(name);
        }
    }
}