using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace Bannerlord.EncyclopediaExtender;

internal static class Texts
{        
    #region Clan page
    public static readonly TextObject Defection = new(Ids.Defection);
    public static readonly TextObject Cash = new(Ids.Cash);
    public static readonly TextObject Debt = new(Ids.Debt);
    public static readonly TextObject ShareOfKingdomWealth = new(Ids.ShareOfKingdomWealth);
    public static readonly TextObject NominalTotalWealth = new(Ids.NominalTotalWealth);
    public static readonly TextObject DefectionPrice = new(Ids.DefectionPrice);
    public static readonly TextObject CashRequiredToPersuade = new(Ids.CashRequiredToPersuade);
    public static readonly TextObject HappyWithCurrentLiege = new(Ids.HappyWithCurrentLiege);
    public static readonly TextObject DailyDefectionChance = new(Ids.DailyDefectionChance);
#endregion
    #region Faction page
    public static readonly TextObject KingdomWealth = new(Ids.KingdomWealth);
    public static readonly TextObject CapturedHeroes = new(Ids.CapturedHeroes);
    public static readonly TextObject ImprisonedHeroes = new(Ids.ImprisonedHeroes);
    public static readonly TextObject KingdomBank = new(Ids.KingdomBank);
    public static readonly TextObject SumOfClanWealth = new(Ids.SumOfClanWealth);
    #endregion
    #region Hero page
    public static readonly TextObject Equipment = new(Ids.Equipment);
    public static readonly TextObject Attributes = new(Ids.Attributes);
    public static readonly TextObject Level = new(Ids.Level);
    public static readonly TextObject DowryPrices = new(Ids.DowryPrices);
    public static readonly TextObject Perks = new(Ids.Perks);
    public static readonly TextObject EquipmentSaleValue = new(Ids.EquipmentSaleValue);
    public static readonly TextObject Prisoner = new(Ids.Prisoner);
    public static readonly TextObject Free = new(Ids.Free);
    public static readonly TextObject Army = new(Ids.Army);
    public static readonly TextObject NotInArmy = new(Ids.NotInArmy);
    public static readonly TextObject PersuasionDowry = new(Ids.PersuasionDowry);
    public static readonly TextObject BarterDowry = new(Ids.BarterDowry);
    public static readonly TextObject Unknown = new(Ids.Unknown);
    #endregion
    #region Settlement page
    public static readonly TextObject Production = new(Ids.Production);
    public static readonly TextObject TradeBoundSettlement = new(Ids.TradeBoundSettlement);
    public static readonly TextObject TradeBoundVillages = new(Ids.TradeBoundVillages);
    #endregion

    internal static TextObject ToHeader(this TextObject header)
    {
        return new TextObject("{=EE_header_template}{HEADER}:")
            .SetTextVariable("HEADER", header);
    }

    internal static class Ids
    {
        #region Clan page
        public const string Defection = "{=uj3CXxKYK03}Defection";
        public const string Cash = "{=beBL5H1u2fu}Cash";
        public const string Debt = "{=C1SUFxYrMXk}Debt";
        public const string ShareOfKingdomWealth = "{=a2uZeyyIdQX}Share of Kingdom Wealth";
        public const string NominalTotalWealth = "{=nAr2HzgGiVn}Nominal Total Wealth";
        public const string DefectionPrice = "{=mfaNceRHqRk}Defection Price";
        public const string CashRequiredToPersuade = "{=vMSUkkSBqeO}Cash required to persuade";
        public const string HappyWithCurrentLiege = "{=9QU7uyLxhXJ}Happy with current liege";
        public const string DailyDefectionChance = "{=0w5RZCxnZ3B}Daily defection chance";
        #endregion
        #region Faction page
        public const string KingdomWealth = "{=VidmvRvXecq}Kingdom Wealth";
        public const string CapturedHeroes = "{=FwJELs27ac7}Captured Heroes";
        public const string ImprisonedHeroes = "{=1CYZYNXTfN9}Imprisoned Heroes";
        public const string KingdomBank = "{=z4z97KSX12m}Kingdom Bank";
        public const string SumOfClanWealth = "{=Ks1B7PO9DjP}Sum of Clan Wealth";
        #endregion
        #region Hero page
        public const string Equipment = "{=nDNOn5o2}Equipment";
        public const string Attributes = "{=NqtTPVzn}Attributes";
        public const string Level = "{=OKUTPdaa}Level";
        public const string DowryPrices = "{=vKsoAjVZRxL}Dowry Prices";
        public const string Perks = "{=fVmG6YTtQai}Perks";
        public const string EquipmentSaleValue = "{=oSsmAi5ejuy}Equipment Sale Value";
        public const string Prisoner = "{=MUOPLUL4Fru}Prisoner";
        public const string Free = "{=EfO4DVzClVp}Free";
        public const string Army = "{=2LlrWkeotbJ}Army";
        public const string NotInArmy = "{=nBA38eYcLkV}Not in Army";
        public const string PersuasionDowry = "{=d6gwqE9RW1q}Persuasion Dowry";
        public const string BarterDowry = "{=EJ8BsdSHZTv}Barter Dowry";
        public const string Unknown = "{=MT4b8H9h}Unknown";
        #endregion
        #region Settlement page
        public const string Production = "{=bGyrPe8c}Production";
        public const string TradeBoundSettlement = "{=2noOKM5N}Trade bound settlement";
        public const string TradeBoundVillages = "{=q7xpz1xb}Trade bound village(s)";
        #endregion
        #region Settings
        public const string ClanMemberDowries = "{=EE_clan_member_dowries}Clan member dowries";
        public const string Clan = "{=j4F7tTzy}Clan";
        public const string Kingdom = "{=03mPaWum}Kingdom";
        public const string Hero = "{=qc9Llr3x}Hero";
        public const string Settlement = "{=qKUjgS6r}Settlement";
        #endregion
    }
}
