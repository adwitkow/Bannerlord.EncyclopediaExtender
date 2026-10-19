using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaFactionPage.PrefabExtensions
{
    [PrefabExtension(
        "EncyclopediaFactionPage",
        "descendant::ListPanel[@Id='RightSideList']/Children/Widget/Children/ListPanel/Children/*[last()]")]
    internal class FactionPrisonerPrefabExtension : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Prepend;

        // TODO: Externalize this xml to a file outside of Module/GUI/Prefabs directory
        [PrefabExtensionText(true)]
        public string GetContent() => @"
<DummyRoot>
  <EncyclopediaDivider
      MarginTop=""35""
      MarginBottom=""15""
      Parameter.Title=""@CapturedHeroesText""
      Parameter.ItemList=""..\CapturedHeroesGrid"" />

  <GridWidget
      Id=""CapturedHeroesGrid""
      DataSource=""{CapturedHeroes}""
      WidthSizePolicy=""CoverChildren""
      HeightSizePolicy=""CoverChildren""
      SuggestedWidth=""350""
      SuggestedHeight=""150""
      DefaultCellWidth=""100""
      DefaultCellHeight=""100""
      HorizontalAlignment=""Left""
      MarginLeft=""25""
      ColumnCount=""7"">

    <ItemTemplate>
      <EncyclopediaSubPageElement />
    </ItemTemplate>
  </GridWidget>

  <EncyclopediaDivider
      MarginTop=""35""
      MarginBottom=""15""
      Parameter.Title=""@ImprisonedHeroesText""
      Parameter.ItemList=""..\ImprisonedHeroesGrid"" />

  <GridWidget
      Id=""ImprisonedHeroesGrid""
      DataSource=""{ImprisonedHeroes}""
      WidthSizePolicy=""CoverChildren""
      HeightSizePolicy=""CoverChildren""
      SuggestedWidth=""350""
      SuggestedHeight=""150""
      DefaultCellWidth=""100""
      DefaultCellHeight=""100""
      HorizontalAlignment=""Left""
      MarginLeft=""25""
      ColumnCount=""7"">
    
    <ItemTemplate>
      <EncyclopediaSubPageElement />
    </ItemTemplate>
  </GridWidget>
</DummyRoot>";
    }
}