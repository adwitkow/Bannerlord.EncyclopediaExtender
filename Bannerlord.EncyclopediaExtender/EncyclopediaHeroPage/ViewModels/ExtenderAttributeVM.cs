using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaHeroPage.ViewModels
{
    public class ExtenderAttributeVM : ViewModel
    {
        public ExtenderAttributeVM(Hero hero, CharacterAttribute att)
        {
            Name = att.Abbreviation.ToString();
            AttributeValue = hero.GetAttributeValue(att);
        }
        [DataSourceProperty]
        public string Name { get; set; }
        [DataSourceProperty]
        public int AttributeValue { get; set; }
    }

}
