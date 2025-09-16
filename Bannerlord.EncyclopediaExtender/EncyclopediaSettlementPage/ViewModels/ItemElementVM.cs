using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaSettlementPage.ViewModels
{
    public class ItemElementVM : ItemVM
    {
        public ItemElementVM(ItemObject item)
        {
            base.ImageIdentifier = new ItemImageIdentifierVM(item);
            base.ItemDescription = item.Name.ToString();
        }
    }
}
