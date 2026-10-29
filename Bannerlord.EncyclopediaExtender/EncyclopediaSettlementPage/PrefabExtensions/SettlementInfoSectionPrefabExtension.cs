using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaSettlementPage.PrefabExtensions;

[PrefabExtension("EncyclopediaSettlementPage", "descendant::EncyclopediaDivider[@Id='OwnerDivider']")]
public class SettlementInfoSectionPrefabExtension : PrefabExtensionInsertPatch
{
    public override InsertType Type => InsertType.Prepend;

    // TODO: Externalize this xml to a file outside of Module/GUI/Prefabs directory
    [PrefabExtensionText(true)]
    public string GetContent() => @"
<DummyRoot>
  <EncyclopediaDictionarySection
    DataSource=""{InfoSection}""
    Parameter.ScopeId=""SettlementInfoSectionScope""
    Parameter.MarginTop=""50"" />
</DummyRoot>";
}
