namespace Bannerlord.EncyclopediaExtender.Configuration;

internal static class Settings
{
    #region Clan page
    internal static bool ClanCash { get; set; } = true;
    internal static bool ClanDebt { get; set; } = true;
    internal static bool ClanShareOfKingdomWealth { get; set; } = true;
    internal static bool ClanTotalWealth { get; set; } = true;
    internal static bool ClanDefectionPrice { get; set; } = true;
    internal static bool ClanCashToPersuade { get; set; } = true;
    internal static bool ClanDefectionChance { get; set; } = true;
    internal static bool ClanRenown { get; set; } = true;
    #endregion
    #region Faction page
    internal static bool KingdomBank { get; set; } = true;
    internal static bool KingdomClansWealthSum { get; set; } = true;
    internal static bool CapturedHeroes { get; set; } = true;
    internal static bool ImprisonedHeroes { get; set; } = true;
    #endregion
    #region Hero page
    internal static bool HeroLevel { get; set; } = true;
    internal static bool HeroEquipmentSaleValue { get; set; } = true;
    internal static bool HeroImprisonment { get; set; } = true;
    internal static bool HeroArmy { get; set; } = true;
    internal static bool HeroPersuasionDowry { get; set; } = true;
    internal static bool HeroBarterDowry { get; set; } = true;
    internal static bool HeroClanMemberDowry { get; set; } = true;
    internal static bool HeroEquipmentList { get; set; } = true;
    internal static bool HeroPerks { get; set; } = true;
    internal static bool HeroAttributes { get; set; } = true;
    #endregion
    #region Settlement page
    internal static bool SettlementProducedItems { get; set; } = true;
    internal static bool TradeBoundSettlement { get; set; } = true;
    internal static bool TradeBoundVillages { get; set; } = true;
    #endregion
}
