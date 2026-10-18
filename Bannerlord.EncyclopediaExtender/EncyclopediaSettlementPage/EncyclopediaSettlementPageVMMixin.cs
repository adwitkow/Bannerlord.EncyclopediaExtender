using Bannerlord.EncyclopediaExtender.EncyclopediaSettlementPage.ViewModels;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.Library;

namespace Bannerlord.EncyclopediaExtender.EncyclopediaSettlementPage
{
    [ViewModelMixin(nameof(EncyclopediaSettlementPageVM.RefreshValues), true)]
    public class EncyclopediaSettlementPageVMMixin : BaseViewModelMixin<EncyclopediaSettlementPageVM>
    {
        private readonly Settlement? _settlement;

        public EncyclopediaSettlementPageVMMixin(EncyclopediaSettlementPageVM vm) : base(vm)
        {
            _settlement = vm.Obj as Settlement;

            ProducedText = string.Empty;
            TradeBoundText = string.Empty;
            ProducedItems = new MBBindingList<ItemElementVM>();
            TradeBoundSettlements = new MBBindingList<EncyclopediaSettlementVM>();
        }

        [DataSourceProperty]
        public string ProducedText { get; set; }

        [DataSourceProperty]
        public string TradeBoundText { get; set; }

        [DataSourceProperty]
        public MBBindingList<ItemElementVM> ProducedItems { get; set; }

        [DataSourceProperty]
        public MBBindingList<EncyclopediaSettlementVM> TradeBoundSettlements { get; private set; }

        public override void OnRefresh()
        {
            ProducedText = Texts.Production.ToString();
            ProducedItems.Clear();
            TradeBoundSettlements.Clear();

            if (_settlement is null)
            {
                return;
            }

            if (_settlement.IsVillage)
            {
                PopulateBindingsForVillage(_settlement.Village);
            }
            else if (_settlement.IsTown)
            {
                PopulateBindingsForTown(_settlement.Town);
            }
            else
            {
                PopulateGenericBindings(_settlement);
            }
        }

        private void PopulateBindingsForVillage(Village village)
        {
            TradeBoundText = Texts.TradeBoundSettlement.ToString();

            var itemElementVm = CreateItemElementVM(village);
            ProducedItems.Add(itemElementVm);
            var settlementVm = new EncyclopediaSettlementVM(village.TradeBound);
            TradeBoundSettlements.Add(settlementVm);
        }

        private void PopulateBindingsForTown(Town town)
        {
            TradeBoundText = Texts.TradeBoundVillages.ToString();

            foreach (var village in town.Villages)
            {
                var itemElementVm = CreateItemElementVM(village);
                ProducedItems.Add(itemElementVm);
            }

            foreach (var tradeBound in town.TradeBoundVillages)
            {
                var itemElementVm = CreateItemElementVM(tradeBound);
                ProducedItems.Add(itemElementVm);

                var settlementVm = new EncyclopediaSettlementVM(tradeBound.Settlement);
                TradeBoundSettlements.Add(settlementVm);
            }
        }

        private void PopulateGenericBindings(Settlement settlement)
        {
            foreach (var bound in settlement.BoundVillages)
            {
                var itemElementVm = CreateItemElementVM(bound);
                ProducedItems.Add(itemElementVm);
            }
        }

        private static ItemElementVM CreateItemElementVM(Village village)
        {
            var primaryProduction = village.VillageType.PrimaryProduction;
            return new ItemElementVM(primaryProduction);
        }
    }
}
