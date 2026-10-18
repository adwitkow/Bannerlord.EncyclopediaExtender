using MCM.Abstractions;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using System.Collections.Generic;

namespace Bannerlord.EncyclopediaExtender.Configuration;

internal sealed class McmSettings : AttributeGlobalSettings<McmSettings>
{
    public McmSettings()
    {
        ClanCash = Settings.ClanCash;
        ClanDebt = Settings.ClanDebt;
        ClanShareOfKingdomWealth = Settings.ClanShareOfKingdomWealth;
        ClanTotalWealth = Settings.ClanTotalWealth;
        ClanDefectionPrice = Settings.ClanDefectionPrice;
        ClanCashToPersuade = Settings.ClanCashToPersuade;
        ClanDefectionChance = Settings.ClanDefectionChance;
        KingdomBank = Settings.KingdomBank;
        KingdomClansWealthSum = Settings.KingdomClansWealthSum;
        CapturedHeroes = Settings.CapturedHeroes;
        ImprisonedHeroes = Settings.ImprisonedHeroes;
        HeroLevel = Settings.HeroLevel;
        HeroEquipmentSaleValue = Settings.HeroEquipmentSaleValue;
        HeroImprisonment = Settings.HeroImprisonment;
        HeroArmy = Settings.HeroArmy;
        HeroPersuasionDowry = Settings.HeroPersuasionDowry;
        HeroBarterDowry = Settings.HeroBarterDowry;
        HeroClanMemberDowry = Settings.HeroClanMemberDowry;
        HeroEquipmentList = Settings.HeroEquipmentList;
        HeroPerks = Settings.HeroPerks;
        HeroAttributes = Settings.HeroAttributes;
        SettlementProducedItems = Settings.SettlementProducedItems;
        TradeBoundSettlement = Settings.TradeBoundSettlement;
        TradeBoundVillages = Settings.TradeBoundVillages;
    }

    public override string Id => "Bannerlord.EncyclopediaExtender";
    public override string DisplayName => "Encyclopedia Extender";
    public override string FolderName => "Bannerlord.EncyclopediaExtender";
    public override string FormatType => "json2";

    [SettingPropertyBool(Texts.Ids.Cash, Order = 1, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanCash
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.ClanCash = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Debt, Order = 2, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanDebt
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.ClanDebt = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.ShareOfKingdomWealth, Order = 3, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanShareOfKingdomWealth
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.ClanShareOfKingdomWealth = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.NominalTotalWealth, Order = 4, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanTotalWealth
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.ClanTotalWealth = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.DefectionPrice, Order = 5, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanDefectionPrice
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.ClanDefectionPrice = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.CashRequiredToPersuade, Order = 6, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanCashToPersuade
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.ClanCashToPersuade = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.DailyDefectionChance, Order = 7, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Clan)]
    public bool ClanDefectionChance
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.ClanDefectionChance = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.KingdomBank, Order = 1, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Kingdom)]
    public bool KingdomBank
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.KingdomBank = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.SumOfClanWealth, Order = 2, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Kingdom)]
    public bool KingdomClansWealthSum
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.KingdomClansWealthSum = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.CapturedHeroes, Order = 3, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Kingdom)]
    public bool CapturedHeroes
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.CapturedHeroes = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.ImprisonedHeroes, Order = 4, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Kingdom)]
    public bool ImprisonedHeroes
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.ImprisonedHeroes = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Level, Order = 1, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroLevel
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.HeroLevel = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.EquipmentSaleValue, Order = 2, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroEquipmentSaleValue
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.HeroEquipmentSaleValue = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Prisoner, Order = 3, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroImprisonment
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.HeroImprisonment = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Army, Order = 4, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroArmy
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.HeroArmy = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.PersuasionDowry, Order = 5, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroPersuasionDowry
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.HeroPersuasionDowry = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.BarterDowry, Order = 6, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroBarterDowry
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.HeroBarterDowry = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.ClanMemberDowries, Order = 7, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroClanMemberDowry
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.HeroClanMemberDowry = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Equipment, Order = 8, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroEquipmentList
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.HeroEquipmentList = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Perks, Order = 9, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroPerks
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.HeroPerks = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Attributes, Order = 10, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Hero)]
    public bool HeroAttributes
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.HeroAttributes = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.Production, Order = 1, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Settlement)]
    public bool SettlementProducedItems
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.SettlementProducedItems = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.TradeBoundSettlement, Order = 2, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Settlement)]
    public bool TradeBoundSettlement
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.TradeBoundSettlement = value;
                OnPropertyChanged();
            }
        }
    }

    [SettingPropertyBool(Texts.Ids.TradeBoundVillages, Order = 3, RequireRestart = false)]
    [SettingPropertyGroup(Texts.Ids.Settlement)]
    public bool TradeBoundVillages
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                Settings.TradeBoundVillages = value;
                OnPropertyChanged();
            }
        }
    }

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
            () => new McmSettings()
            {
                ClanCash = false,
                ClanDebt = false,
                ClanShareOfKingdomWealth = false,
                ClanTotalWealth = false,
                ClanDefectionPrice = false,
                ClanCashToPersuade = false,
                ClanDefectionChance = false,
                KingdomBank = false,
                KingdomClansWealthSum = false,
                CapturedHeroes = false,
                ImprisonedHeroes = false,
                HeroLevel = false,
                HeroEquipmentSaleValue = false,
                HeroImprisonment = false,
                HeroArmy = false,
                HeroPersuasionDowry = false,
                HeroBarterDowry = false,
                HeroClanMemberDowry = false,
                HeroEquipmentList = false,
                HeroPerks = false,
                HeroAttributes = false,
                SettlementProducedItems = false,
                TradeBoundSettlement = false,
                TradeBoundVillages = false,
            });

        yield return new MemorySettingsPreset(
            Id,
            "true",
            "Everything on",
            () => new McmSettings()
            {
                ClanCash = true,
                ClanDebt = true,
                ClanShareOfKingdomWealth = true,
                ClanTotalWealth = true,
                ClanDefectionPrice = true,
                ClanCashToPersuade = true,
                ClanDefectionChance = true,
                KingdomBank = true,
                KingdomClansWealthSum = true,
                CapturedHeroes = true,
                ImprisonedHeroes = true,
                HeroLevel = true,
                HeroEquipmentSaleValue = true,
                HeroImprisonment = true,
                HeroArmy = true,
                HeroPersuasionDowry = true,
                HeroBarterDowry = true,
                HeroClanMemberDowry = true,
                HeroEquipmentList = true,
                HeroPerks = true,
                HeroAttributes = true,
                SettlementProducedItems = true,
                TradeBoundSettlement = true,
                TradeBoundVillages = true,
            });
    }
}
