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
    public class PerksForSkillVM : ViewModel
    {
        [DataSourceProperty]
        public string SkillName { get; set; }

        [DataSourceProperty]
        public MBBindingList<StringPairItemVM> Perks { get; set; }
        public PerksForSkillVM(SkillObject skill, List<PerkObject> perks)
        {
            SkillName = skill.ToString();
            Perks = new MBBindingList<StringPairItemVM>();
            foreach (var perk in perks.OrderBy((p) => p.RequiredSkillValue))
            {
                Perks.Add(new StringPairItemVM(perk.RequiredSkillValue.ToString() + ':', perk.ToString(),
                    new BasicTooltipViewModel(() => CampaignUIHelper.GetPerkEffectText(perk, false))));
            }
        }
    }
}
