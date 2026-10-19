using TaleWorlds.Library;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaHeroPage.ViewModels;

public class PerkColumnVM : ViewModel
{
    public PerkColumnVM(MBBindingList<PerkSkillGroupVM> groups)
    {
        Groups = groups;
    }

    [DataSourceProperty]
    public MBBindingList<PerkSkillGroupVM> Groups { get; }
}
