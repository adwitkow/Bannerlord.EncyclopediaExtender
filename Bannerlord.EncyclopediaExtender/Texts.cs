using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace Bannerlord.EncyclopediaExtender;

internal static class Texts
{
    #region Clan page
    public static readonly TextObject Defection = new("{=uj3CXxKYK03}Defection");
    public static readonly TextObject Cash = new("{=beBL5H1u2fu}Cash");
    public static readonly TextObject Debt = new("{=C1SUFxYrMXk}Debt");
    public static readonly TextObject ShareOfKingdomWealth = new("{=a2uZeyyIdQX}Share of Kingdom Wealth");
    public static readonly TextObject NominalTotalWealth = new("{=nAr2HzgGiVn}Nominal Total Wealth");
    public static readonly TextObject DefectionPrice = new("{=mfaNceRHqRk}Defection Price");
    public static readonly TextObject CashRequiredToPersuade = new("{=vMSUkkSBqeO}Cash required to persuade");
    public static readonly TextObject HappyWithCurrentLiege = new("{=9QU7uyLxhXJ}Happy with current liege");
    public static readonly TextObject DailyDefectionChance = new("{=0w5RZCxnZ3B}Daily defection chance");
    #endregion
    #region Faction page
    public static readonly TextObject KingdomWealth = new("{=VidmvRvXecq}Kingdom Wealth");
    public static readonly TextObject CapturedHeroes = new("{=FwJELs27ac7}Captured Heroes");
    public static readonly TextObject ImprisonedHeroes = new("{=1CYZYNXTfN9}Imprisoned Heroes");
    public static readonly TextObject KingdomBank = new("{=z4z97KSX12m}Kingdom Bank");
    public static readonly TextObject SumOfClanWealth = new("{=Ks1B7PO9DjP}Sum of Clan Wealth");
    #endregion
    #region Hero page
    public static readonly TextObject Equipment = GameTexts.FindText("str_equipment");
    public static readonly TextObject Attributes = GameTexts.FindText("str_attributes");
    public static readonly TextObject Level = GameTexts.FindText("str_level");
    public static readonly TextObject DowryPrices = new("{=vKsoAjVZRxL}Dowry Prices");
    public static readonly TextObject Perks = new("{=fVmG6YTtQai}Perks");
    public static readonly TextObject EquipmentSaleValue = new("{=oSsmAi5ejuy}Equipment Sale Value");
    public static readonly TextObject Prisoner = new("{=MUOPLUL4Fru}Prisoner");
    public static readonly TextObject Free = new("{=EfO4DVzClVp}Free");
    public static readonly TextObject Army = new("{=2LlrWkeotbJ}Army");
    public static readonly TextObject NotInArmy = new("{=nBA38eYcLkV}Not in Army");
    public static readonly TextObject PersuasionDowry = new("{=d6gwqE9RW1q}Persuasion Dowry");
    public static readonly TextObject BarterDowry = new("{=EJ8BsdSHZTv}Barter Dowry");
    public static readonly TextObject Unknown = new("{=MT4b8H9h}Unknown");
    #endregion
    #region Settlement page
    public static readonly TextObject Production = new("{=bGyrPe8c}Production");
    public static readonly TextObject TradeBoundSettlement = new("{=2noOKM5N}Trade bound settlement");
    public static readonly TextObject TradeBoundVillages = new("{=q7xpz1xb}Trade bound village(s)");
    #endregion

    internal static TextObject ToHeader(this TextObject header)
    {
        return new TextObject("{=EE_header_template}{HEADER}:")
            .SetTextVariable("HEADER", header);
    }
}
