using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaHeroPage.PrefabExtensions
{
    [PrefabExtension("EncyclopediaHeroPage", "descendant::NavigatableGridWidget[@Id='FamilyGrid']")]
    public class HeroPerksPrefabExtension : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        // TODO: Externalize this xml to a file outside of Module/GUI/Prefabs directory
        [PrefabExtensionText(true)]
        public string GetContent() => $@"
<DummyRoot>
  <EncyclopediaDivider
    MarginTop=""20""
    Parameter.Title=""@PerksText""
    Parameter.ItemList=""..\PerksColumns"" />

  <ListPanel
      Id=""PerksColumns""
      DataSource=""{{PerkColumns}}""
      WidthSizePolicy=""CoverChildren""
      HeightSizePolicy=""CoverChildren""
      HorizontalAlignment=""Left""
      MarginTop=""10""
      MarginLeft=""15""
      StackLayout.LayoutMethod=""HorizontalLeftToRight"">

    <ItemTemplate>
      <Widget
          WidthSizePolicy=""Fixed""
          SuggestedWidth=""260""
          HeightSizePolicy=""CoverChildren""
          VerticalAlignment=""Top""
          MarginRight=""20"">
        <Children>
          <ListPanel
              DataSource=""{{Groups}}""
              WidthSizePolicy=""StretchToParent""
              HeightSizePolicy=""CoverChildren""
              StackLayout.LayoutMethod=""{Constants.VerticalTopToBottom}"">

            <ItemTemplate>
              <ListPanel
                  WidthSizePolicy=""StretchToParent""
                  HeightSizePolicy=""CoverChildren""
                  MarginLeft=""15""
                  MarginTop=""6""
                  StackLayout.LayoutMethod=""{Constants.VerticalTopToBottom}"">
                <Children>
                  <AutoHideRichTextWidget
                      WidthSizePolicy=""CoverChildren""
                      HeightSizePolicy=""CoverChildren""
                      Brush=""Encyclopedia.Stat.DefinitionText""
                      Text=""@SkillName"" />

                  <ListPanel
                      DataSource=""{{Perks}}""
                      WidthSizePolicy=""CoverChildren""
                      HeightSizePolicy=""CoverChildren""
                      StackLayout.LayoutMethod=""{Constants.VerticalTopToBottom}"">

                    <ItemTemplate>
                      <ListPanel
                          DoNotPassEventsToChildren=""true""
                          WidthSizePolicy=""CoverChildren""
                          HeightSizePolicy=""CoverChildren""
                          MarginLeft=""15""
                          MarginTop=""3"">
                        <Children>
                          <RichTextWidget
                              WidthSizePolicy=""CoverChildren""
                              HeightSizePolicy=""CoverChildren""
                              VerticalAlignment=""Center""
                              Brush=""Encyclopedia.Stat.DefinitionText""
                              Text=""@Definition""
                              MarginRight=""5"" />
                          <RichTextWidget
                              WidthSizePolicy=""CoverChildren""
                              HeightSizePolicy=""CoverChildren""
                              VerticalAlignment=""Center""
                              Brush=""Encyclopedia.Stat.ValueText""
                              Text=""@Value"" />
                          <HintWidget
                              DataSource=""{{Hint}}""
                              WidthSizePolicy=""CoverChildren""
                              HeightSizePolicy=""CoverChildren""
                              Command.HoverBegin=""ExecuteBeginHint""
                              Command.HoverEnd=""ExecuteEndHint""
                              IsEnabled=""false"" />
                        </Children>
                      </ListPanel>
                    </ItemTemplate>
                  </ListPanel>
                </Children>
              </ListPanel>
            </ItemTemplate>
          </ListPanel>
        </Children>
      </Widget>
    </ItemTemplate>
  </ListPanel>
  
  <!--Bottom Padding Widget-->
  <Widget
    DoNotAcceptEvents=""true""
    WidthSizePolicy=""StretchToParent""
    HeightSizePolicy=""Fixed""
    SuggestedHeight=""100""/>
</DummyRoot>";
    }
}
