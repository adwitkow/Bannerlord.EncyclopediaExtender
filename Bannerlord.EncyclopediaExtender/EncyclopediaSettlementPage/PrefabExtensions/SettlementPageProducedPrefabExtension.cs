using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaSettlementPage.PrefabExtensions
{
    [PrefabExtension("EncyclopediaSettlementPage", "descendant::EncyclopediaDivider[@Id='SettlementsDivider']")]
    internal class SettlementPageProducedPrefabExtension : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Prepend;

        [PrefabExtensionFileName(true)]
        public string File => "SettlementPage";
    }
}
