using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaSettlementPage.PrefabExtensions
{
    [PrefabExtension("EncyclopediaSettlementPage", "descendant::EncyclopediaDivider[@Id='SettlementsDivider']")]
    internal class SettlementPageProducedPrefabExtension : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Prepend;

        // TODO: Externalize this xml to a file outside of Module/GUI/Prefabs directory
        [PrefabExtensionText(true)]
        public string GetContent() => $@"
<DummyRoot>
  <EncyclopediaDivider MarginTop=""20"" Parameter.Title=""@ProducedText"" Parameter.ItemList=""..\ProducedItemsGrid""/>

  <NavigationScopeTargeter ScopeID=""EncyclopediaSettlementProducedItemsScope"" ScopeParent=""..\ProducedItemsGrid"" ScopeMovements=""Horizontal"" AlternateScopeMovements=""Vertical"" AlternateMovementStepSize=""7"" />
  <NavigatableGridWidget Id=""ProducedItemsGrid"" DataSource=""{{ProducedItems}}"" WidthSizePolicy = ""StretchToParent"" HeightSizePolicy = ""CoverChildren"" SuggestedWidth=""350"" SuggestedHeight=""350"" DefaultCellWidth=""100"" DefaultCellHeight=""100"" HorizontalAlignment=""Left"" ColumnCount=""7"" MarginTop=""10"" MarginLeft=""15"" AutoScrollYOffset=""35"" >
    <ItemTemplate>

        <SettlementProducedItem />

    </ItemTemplate>
  </NavigatableGridWidget>

  <EncyclopediaDivider MarginTop=""20"" Parameter.Title=""@TradeBoundText"" Parameter.ItemList=""..\TradeBoundSettlements"" />

  <NavigationScopeTargeter ScopeID=""EncyclopediaSettlementTradeBoundScope"" ScopeParent=""..\TradeBoundSettlements"" ScopeMovements=""Horizontal"" AlternateScopeMovements=""Vertical"" AlternateMovementStepSize=""7"" />
  <NavigatableGridWidget Id=""TradeBoundSettlements"" DataSource=""{{TradeBoundSettlements}}"" WidthSizePolicy = ""StretchToParent"" HeightSizePolicy = ""CoverChildren"" SuggestedWidth=""350"" SuggestedHeight=""350"" DefaultCellWidth=""100"" DefaultCellHeight=""100"" HorizontalAlignment=""Left"" ColumnCount=""7"" MarginTop=""10"" MarginLeft=""15"" AutoScrollYOffset=""35"" >
    <ItemTemplate>

      <ButtonWidget DoNotPassEventsToChildren=""true"" WidthSizePolicy=""Fixed"" HeightSizePolicy=""Fixed"" SuggestedWidth=""90"" SuggestedHeight=""90"" Brush=""Encyclopedia.SubPage.Element"" Command.Click=""ExecuteLink"">
        <Children>

          <Widget Id=""VillageImage"" WidthSizePolicy=""Fixed"" HeightSizePolicy=""Fixed"" SuggestedWidth=""80"" SuggestedHeight=""80"" HorizontalAlignment=""Center"" VerticalAlignment=""Center"" Sprite=""@FileName"" />
          <HintWidget WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""StretchToParent"" Command.HoverBegin=""ExecuteBeginHint"" Command.HoverEnd=""ExecuteEndHint"" />
          <TextWidget WidthSizePolicy=""StretchToParent"" HeightSizePolicy=""CoverChildren"" VerticalAlignment=""Top"" PositionYOffset=""95"" Brush=""Encyclopedia.SubPage.Element.Name.Text"" Brush.FontSize=""18"" Text=""@NameText"" />

        </Children>
      </ButtonWidget>

    </ItemTemplate>
  </NavigatableGridWidget>
</DummyRoot>";
    }
}
