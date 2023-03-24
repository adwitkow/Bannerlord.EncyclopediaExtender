using System;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace EncyclopediaExtender.EncyclopediaHeroPage.PrefabExtensions
{
    [PrefabExtension("EncyclopediaHeroPage", "descendant::NavigatableGridWidget[@Id='FamilyGrid']")]
    public class HeroPerksPrefabExtension : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        [PrefabExtensionFileName(true)]
        public string File => "HeroPagePerksPatch";
    }
}
