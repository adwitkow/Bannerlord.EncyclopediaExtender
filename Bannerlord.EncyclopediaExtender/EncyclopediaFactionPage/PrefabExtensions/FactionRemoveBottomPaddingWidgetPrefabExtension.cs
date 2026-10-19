using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaFactionPage.PrefabExtensions;

[PrefabExtension(
    "EncyclopediaFactionPage",
    "descendant::ListPanel[@Id='RightSideList']/Children/Widget/Children/ListPanel/Children/Widget")]
internal class FactionRemoveBottomPaddingWidgetPrefabExtension : PrefabExtensionInsertPatch
{
    public override InsertType Type => InsertType.Remove;
}
