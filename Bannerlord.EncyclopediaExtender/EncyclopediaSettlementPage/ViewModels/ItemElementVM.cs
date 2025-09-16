using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaSettlementPage.ViewModels
{
    public class ItemElementVM : ItemVM
    {
        public ItemElementVM(ItemObject item)
        {
#if v130
            base.ImageIdentifier = new ItemImageIdentifierVM(item);
#else
            base.ImageIdentifier = new ImageIdentifierVM(item);
#endif
            base.ItemDescription = item.Name.ToString();
        }
    }
}
