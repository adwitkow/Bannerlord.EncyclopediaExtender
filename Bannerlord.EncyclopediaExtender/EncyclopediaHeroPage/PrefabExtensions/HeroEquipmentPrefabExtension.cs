using System;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaHeroPage.PrefabExtensions
{
    [PrefabExtension("EncyclopediaHeroPage", "descendant::Widget[@Id='InfoContainer']")]
    public class HeroEquipmentPrefabExtension : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        public override int Index => 2;

        // TODO: Externalize this xml to a file outside of Module/GUI/Prefabs directory
        [PrefabExtensionText(true)]
        public string GetContent() => @"
<DummyRoot>
    <EncyclopediaDivider MarginTop=""20"" Parameter.Title=""@EquipmentText"" Parameter.ItemList=""..\EquipmentContainer""/>
    <Widget Id=""EquipmentContainer"" MarginTop=""15"" HeightSizePolicy=""CoverChildren"" WidthSizePolicy=""StretchToParent"">
        <Children>
            <ListPanel Id=""EquipmentList"" DataSource=""{HeroItems}"" WidthSizePolicy=""CoverChildren"" HeightSizePolicy=""CoverChildren"" StackLayout.LayoutMethod=""VerticalBottomToTop"">
                <ItemTemplate>
                    <!--AutoHideRichTextWidget HeightSizePolicy =""CoverChildren"" WidthSizePolicy=""CoverChildren"" VerticalAlignment=""Center"" Brush=""Encyclopedia.Stat.DefinitionText"" Text=""@ItemDescription"" MarginRight=""5""/-->
                    <HeroItemTuple />
                </ItemTemplate>
            </ListPanel>
        </Children>
    </Widget>
</DummyRoot>";
    }
}
