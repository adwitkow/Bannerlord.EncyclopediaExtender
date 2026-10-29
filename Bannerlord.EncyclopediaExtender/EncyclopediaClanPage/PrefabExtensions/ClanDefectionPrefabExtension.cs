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
  <EncyclopediaDictionarySection
    DataSource=""{DefectionSection}""
    Parameter.ScopeId=""ClanDefectionSectionScope"" />
</DummyRoot>";
    }
}
