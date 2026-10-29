
using MCM.Abstractions;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using System.Collections.Generic;

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
        set
        {
            if (_clanId != value)
            {
                _clanId = value;
                Settings.ClanId = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Cash, Order = 1, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanCash
    {
        get => _clanCash;
        set
        {
            if (_clanCash != value)
            {
                _clanCash = value;
                Settings.ClanCash = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Debt, Order = 2, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanDebt
    {
        get => _clanDebt;
        set
        {
            if (_clanDebt != value)
            {
                _clanDebt = value;
                Settings.ClanDebt = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.ShareOfKingdomWealth, Order = 3, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanShareOfKingdomWealth
    {
        get => _clanShareOfKingdomWealth;
        set
        {
            if (_clanShareOfKingdomWealth != value)
            {
                _clanShareOfKingdomWealth = value;
                Settings.ClanShareOfKingdomWealth = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.NominalTotalWealth, Order = 4, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanTotalWealth
    {
        get => _clanTotalWealth;
        set
        {
            if (_clanTotalWealth != value)
            {
                _clanTotalWealth = value;
                Settings.ClanTotalWealth = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.DefectionPrice, Order = 5, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanDefectionPrice
    {
        get => _clanDefectionPrice;
        set
        {
            if (_clanDefectionPrice != value)
            {
                _clanDefectionPrice = value;
                Settings.ClanDefectionPrice = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.CashRequiredToPersuade, Order = 6, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanCashToPersuade
    {
        get => _clanCashToPersuade;
        set
        {
            if (_clanCashToPersuade != value)
            {
                _clanCashToPersuade = value;
                Settings.ClanCashToPersuade = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.DailyDefectionChance, Order = 7, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanDefectionChance
    {
        get => _clanDefectionChance;
        set
        {
            if (_clanDefectionChance != value)
            {
                _clanDefectionChance = value;
                Settings.ClanDefectionChance = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Renown, Order = 8, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanRenown
    {
        get => _clanRenown;
        set
        {
            if (_clanRenown != value)
            {
                _clanRenown = value;
                Settings.ClanRenown = value;
                OnPropertyChanged();
            }
        }
    }

    #endregion

    #region Kingdom
    [SettingPropertyBool(Texts.Ids.Id, Order = 0, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Kingdom)]
    public bool KingdomId
    {
        get => _kingdomId;
        set
        {
            if (_kingdomId != value)
            {
                _kingdomId = value;
                Settings.KingdomId = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.KingdomBank, Order = 1, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Kingdom)]
    public bool KingdomBank
    {
        get => _kingdomBank;
        set
        {
            if (_kingdomBank != value)
            {
                _kingdomBank = value;
                Settings.KingdomBank = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.SumOfClanWealth, Order = 2, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Kingdom)]
    public bool KingdomClansWealthSum
    {
        get => _kingdomClansWealthSum;
        set
        {
            if (_kingdomClansWealthSum != value)
            {
                _kingdomClansWealthSum = value;
                Settings.KingdomClansWealthSum = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.CapturedHeroes, Order = 3, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Kingdom)]
    public bool CapturedHeroes
    {
        get => _capturedHeroes;
        set
        {
            if (_capturedHeroes != value)
            {
                _capturedHeroes = value;
                Settings.CapturedHeroes = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.ImprisonedHeroes, Order = 4, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Kingdom)]
    public bool ImprisonedHeroes
    {
        get => _imprisonedHeroes;
        set
        {
            if (_imprisonedHeroes != value)
            {
                _imprisonedHeroes = value;
                Settings.ImprisonedHeroes = value;
                OnPropertyChanged();
            }
        }
    }
    #endregion

    #region Hero
    [SettingPropertyBool(Texts.Ids.Id, Order = 0, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroId
    {
        get => _heroId;
        set
        {
            if (_heroId != value)
            {
                _heroId = value;
                Settings.HeroId = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Level, Order = 1, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroLevel
    {
        get => _heroLevel;
        set
        {
            if (_heroLevel != value)
            {
                _heroLevel = value;
                Settings.HeroLevel = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.EquipmentSaleValue, Order = 2, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroEquipmentSaleValue
    {
        get => _heroEquipmentSaleValue;
        set
        {
            if (_heroEquipmentSaleValue != value)
            {
                _heroEquipmentSaleValue = value;
                Settings.HeroEquipmentSaleValue = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Prisoner, Order = 3, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroImprisonment
    {
        get => _heroImprisonment;
        set
        {
            if (_heroImprisonment != value)
            {
                _heroImprisonment = value;
                Settings.HeroImprisonment = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Army, Order = 4, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroArmy
    {
        get => _heroArmy;
        set
        {
            if (_heroArmy != value)
            {
                _heroArmy = value;
                Settings.HeroArmy = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.PersuasionDowry, Order = 5, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroPersuasionDowry
    {
        get => _heroPersuasionDowry;
        set
        {
            if (_heroPersuasionDowry != value)
            {
                _heroPersuasionDowry = value;
                Settings.HeroPersuasionDowry = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.BarterDowry, Order = 6, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroBarterDowry
    {
        get => _heroBarterDowry;
        set
        {
            if (_heroBarterDowry != value)
            {
                _heroBarterDowry = value;
                Settings.HeroBarterDowry = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.ClanMemberDowries, Order = 7, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroClanMemberDowry
    {
        get => _heroClanMemberDowry;
        set
        {
            if (_heroClanMemberDowry != value)
            {
                _heroClanMemberDowry = value;
                Settings.HeroClanMemberDowry = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Equipment, Order = 8, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroEquipmentList
    {
        get => _heroEquipmentList;
        set
        {
            if (_heroEquipmentList != value)
            {
                _heroEquipmentList = value;
                Settings.HeroEquipmentList = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Perks, Order = 9, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroPerks
    {
        get => _heroPerks;
        set
        {
            if (_heroPerks != value)
            {
                _heroPerks = value;
                Settings.HeroPerks = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Attributes, Order = 10, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroAttributes
    {
        get => _heroAttributes;
        set
        {
            if (_heroAttributes != value)
            {
                _heroAttributes = value;
                Settings.HeroAttributes = value;
                OnPropertyChanged();
            }
        }
    }
    #endregion

    #region Settlement
    [SettingPropertyBool(Texts.Ids.Id, Order = 0, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Settlement)]
    public bool SettlementId
    {
        get => _settlementId;
        set
        {
            if (_settlementId != value)
            {
                _settlementId = value;
                Settings.SettlementId = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Production, Order = 1, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Settlement)]
    public bool SettlementProducedItems
    {
        get => _settlementProducedItems;
        set
        {
            if (_settlementProducedItems != value)
            {
                _settlementProducedItems = value;
                Settings.SettlementProducedItems = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.TradeBoundSettlement, Order = 2, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Settlement)]
    public bool TradeBoundSettlement
    {
        get => _tradeBoundSettlement;
        set
        {
            if (_tradeBoundSettlement != value)
            {
                _tradeBoundSettlement = value;
                Settings.TradeBoundSettlement = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.TradeBoundVillages, Order = 3, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Settlement)]
    public bool TradeBoundVillages
    {
        get => _tradeBoundVillages;
        set
        {
            if (_tradeBoundVillages != value)
            {
                _tradeBoundVillages = value;
                Settings.TradeBoundVillages = value;
                OnPropertyChanged();
            }
        }
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
}