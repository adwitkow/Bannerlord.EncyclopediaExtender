using Bannerlord.EncyclopediaExtender.Configuration;
using Bannerlord.EncyclopediaExtender.EncyclopediaSettlementPage.ViewModels;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using System.Collections.Generic;
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

            AddProducedItems(village);
            AddTradeBoundSettlement(village.TradeBound);
        }

        private void PopulateBindingsForTown(Town town)
        {
            TradeBoundText = Texts.TradeBoundVillages.ToString();

            AddProducedItems(town.Villages);
            AddProducedItems(town.TradeBoundVillages);
            AddTradeBoundVillages(town.TradeBoundVillages);
        }

        private void PopulateGenericBindings(Settlement settlement)
        {
            AddProducedItems(settlement.BoundVillages);
        }

        private void AddProducedItems(params IEnumerable<Village> villages)
        {
            if (!Settings.SettlementProducedItems)
            {
                return;
            }

            foreach (var village in villages)
            {
                var itemElementVm = CreateItemElementVM(village);
                ProducedItems.Add(itemElementVm);
            }
        }

        private void AddTradeBoundSettlement(Settlement settlement)
        {
            if (!Settings.TradeBoundSettlement)
            {
                return;
            }

            var settlementVm = new EncyclopediaSettlementVM(settlement);
            TradeBoundSettlements.Add(settlementVm);
        }

        private void AddTradeBoundVillages(params IEnumerable<Village> villages)
        {
            if (!Settings.TradeBoundVillages)
            {
                return;
            }

            foreach (var village in villages)
            {
                var settlementVm = new EncyclopediaSettlementVM(village.Settlement);
                TradeBoundSettlements.Add(settlementVm);
            }
        }

        private static ItemElementVM CreateItemElementVM(Village village)
        {
            var primaryProduction = village.VillageType.PrimaryProduction;
            return new ItemElementVM(primaryProduction);
        }
    }
}
