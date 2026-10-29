using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaFactionPage.PrefabExtensions
{
    [PrefabExtension("EncyclopediaFactionPage", "descendant::EncyclopediaSubPageElement[@Id='Leader']")]
    public class FactionInfoPrefabExtension : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        // TODO: Externalize this xml to a file outside of Module/GUI/Prefabs directory
        [PrefabExtensionText(true)]
        public string GetContent() => @"
<DummyRoot>
  <EncyclopediaDictionarySection
    DataSource=""{InfoSection}""
    Parameter.ScopeId=""KingomInfoSectionScope""
    Parameter.MarginTop=""50"" />
</DummyRoot>";
    }
}
