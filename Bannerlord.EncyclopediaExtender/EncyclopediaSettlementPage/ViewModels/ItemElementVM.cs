using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaSettlementPage.ViewModels
{
    public class ItemElementVM : ItemVM
    {
        public ItemElementVM(ItemObject item)
        {
            base.ImageIdentifier = new ImageIdentifierVM(item);
            base.ItemDescription = item.Name.ToString();
        }
    }
}
