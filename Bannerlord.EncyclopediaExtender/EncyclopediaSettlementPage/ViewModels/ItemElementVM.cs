using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
#if !LOWER_THAN_1_3
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
#endif

namespace Bannerlord.EncyclopediaExtender.EncyclopediaSettlementPage.ViewModels
{
    public class ItemElementVM : ItemVM
    {
        public ItemElementVM(ItemObject item)
        {
#if LOWER_THAN_1_3
            base.ImageIdentifier = new ImageIdentifierVM(item);
#else
            base.ImageIdentifier = new ItemImageIdentifierVM(item);
#endif
            base.ItemDescription = item.Name.ToString();
        }
    }
}
