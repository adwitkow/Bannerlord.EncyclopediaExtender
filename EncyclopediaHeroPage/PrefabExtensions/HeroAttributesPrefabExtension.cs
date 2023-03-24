using System;
using System.Linq;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace EncyclopediaExtender.EncyclopediaHeroPage.PrefabExtensions
{
    [PrefabExtension("EncyclopediaHeroPage", "descendant::TextWidget[@Text='@SkillsText']")]
    public class HeroAttributesPrefabExtension : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Prepend;

        [PrefabExtensionFileName(true)]
        public string File => "HeroPageAttributesPatch";
    }
}
