using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaClanPage.PrefabExtensions
{
    [PrefabExtension("EncyclopediaClanPage", "descendant::ListPanel[@Id='Leader']")]
    public class ClanDefectionPrefabExtension : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        [PrefabExtensionFileName(true)]
        public string File => "ClanPageDefectionPatch";
    }
}
