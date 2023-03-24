using System;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.Prefabs2;

namespace EncyclopediaExtender.EncyclopediaFactionPage.PrefabExtensions
{
    [PrefabExtension("EncyclopediaFactionPage", "descendant::NavigatableGridWidget[@Id='EnemiesGrid']")]
    internal class FactionPrisonerPrefabExtension : PrefabExtensionInsertPatch
    {
        public override InsertType Type => InsertType.Append;

        [PrefabExtensionFileName(true)]
        public string File => "FactionPagePrisonerPatch";
    }
}
