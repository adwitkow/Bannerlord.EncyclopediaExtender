using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;
using System.Collections.Generic;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaHeroPage.PrefabExtensions;

[PrefabExtension("EncyclopediaHeroPage", "descendant::NavigatableGridWidget[@Id='FamilyGrid']")]
internal class FamilyGridNoBottomMarginPrefabExtension : PrefabExtensionSetAttributePatch
{
    public override List<Attribute> Attributes => [
        new("MarginBottom", "0")
    ];
}
