using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaHeroPage.PrefabExtensions
{
    [PrefabExtension("EncyclopediaHeroPage", "descendant::TextWidget[@Text='@SkillsText']")]
    public class HeroAttributesPrefabExtension : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Prepend;

        // TODO: Externalize this xml to a file outside of Module/GUI/Prefabs directory
        [PrefabExtensionText(true)]
        public string GetContent() => $@"
<DummyRoot>
  <TextWidget
      HeightSizePolicy=""CoverChildren""
      WidthSizePolicy=""StretchToParent""
      Text=""@AttributesText""
      HorizontalAlignment=""Center""
      ClipContents=""false"" />

  <GridWidget
      DataSource=""{{Attributes}}""
      WidthSizePolicy=""CoverChildren""
      HeightSizePolicy=""CoverChildren""
      DefaultCellWidth=""55""
      HorizontalAlignment=""Center""
      MarginTop=""10""
      MarginLeft=""5""
      MarginRight=""20""
      DefaultCellHeight=""50""
      ColumnCount=""6""
      LayoutImp.VerticalLayoutMethod=""TopToBottom"">

    <ItemTemplate>
      <ListPanel
          WidthSizePolicy=""StretchToParent""
          HeightSizePolicy=""StretchToParent""
          StackLayout.LayoutMethod=""{Constants.VerticalTopToBottom}"">

        <Children>
          <TextWidget
              WidthSizePolicy=""CoverChildren""
              HeightSizePolicy=""CoverChildren""
              Text=""@Name""
              HorizontalAlignment=""Center""
              Brush.FontSize=""20"" />
          <TextWidget
              WidthSizePolicy=""CoverChildren""
              HeightSizePolicy=""CoverChildren""
              IntText=""@AttributeValue""
              HorizontalAlignment=""Center""
              Brush=""Encyclopedia.Skills.Text""
              Brush.FontSize=""25"" />
        </Children>
      </ListPanel>
    </ItemTemplate>
  </GridWidget>
</DummyRoot>";
    }
}
