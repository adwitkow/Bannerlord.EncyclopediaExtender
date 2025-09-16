using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
#if v130
using TaleWorlds.Core.ViewModelCollection.ImageIdentifiers;
#endif

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
