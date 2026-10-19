using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaHeroPage.ViewModels
{
    public class PerkSkillGroupVM : ViewModel
    {
        public PerkSkillGroupVM(SkillObject skill, List<PerkObject> perks)
        {
            SkillName = skill.ToString();
            Perks = new MBBindingList<StringPairItemVM>();

            foreach (var perk in perks.OrderBy(p => p.RequiredSkillValue))
            {
                Perks.AddPair(
                    perk.RequiredSkillValue.ToString(),
                    perk,
                    new BasicTooltipViewModel(() => CampaignUIHelper.GetPerkEffectText(perk, false)));
            }
        }

        [DataSourceProperty]
        public string SkillName { get; set; }

        [DataSourceProperty]
        public MBBindingList<StringPairItemVM> Perks { get; set; }
    }
}
