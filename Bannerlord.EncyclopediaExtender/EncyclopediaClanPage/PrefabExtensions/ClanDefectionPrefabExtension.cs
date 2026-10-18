using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaClanPage.PrefabExtensions
{
    [PrefabExtension("EncyclopediaClanPage", "descendant::ListPanel[@Id='Leader']")]
    public class ClanDefectionPrefabExtension : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        // TODO: Externalize this xml to a file outside of Module/GUI/Prefabs directory
        [PrefabExtensionText(true)]
        public string GetContent() => @"
<DummyRoot>
  <EncyclopediaDivider
      MarginTop=""30""
      Parameter.Title=""@DefectionText""
      Parameter.ItemList=""..\DefectionGrid"" />

  <GridWidget
      Id=""DefectionGrid""
      DataSource=""{DefectionInfo}""
      WidthSizePolicy=""CoverChildren""
      HeightSizePolicy=""CoverChildren""
      DefaultCellWidth=""300""
      DefaultCellHeight=""30""
      HorizontalAlignment=""Left""
      ColumnCount=""2""
      MarginTop=""10""
      MarginLeft=""15"">
    
    <ItemTemplate>
      <ListPanel
          HeightSizePolicy=""CoverChildren""
          WidthSizePolicy=""StretchToParent""
          MarginLeft=""15""
          MarginTop=""3"">
        <Children>
          <AutoHideRichTextWidget
              HeightSizePolicy=""CoverChildren""
              WidthSizePolicy=""CoverChildren""
              VerticalAlignment=""Center""
              Brush=""Encyclopedia.Stat.DefinitionText""
              Text=""@Definition""
              MarginRight=""5"" />
          <AutoHideRichTextWidget
              HeightSizePolicy=""CoverChildren""
              WidthSizePolicy=""CoverChildren""
              VerticalAlignment=""Center""
              Brush=""Encyclopedia.Stat.ValueText""
              Text=""@Value""
              PositionYOffset=""2"" />
        </Children>
      </ListPanel>
    </ItemTemplate>
  </GridWidget>
</DummyRoot>";
    }
}
