using Bannerlord.EncyclopediaExtender.EncyclopediaSettlementPage.ViewModels;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.Library;
using TaleWorlds.Localization;

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
        public bool IsTradeBound { get; set; }

        [DataSourceProperty]
        public MBBindingList<ItemElementVM> ProducedItems { get; set; }

        [DataSourceProperty]
        public MBBindingList<EncyclopediaSettlementVM> TradeBoundSettlements { get; private set; }

        public override void OnRefresh()
        {
            ProducedText = new TextObject("{=bGyrPe8c}Production").ToString();
            ProducedItems.Clear();
            TradeBoundSettlements.Clear();

            if (_settlement is null)
            {
                return;
            }

            IsTradeBound = _settlement.IsVillage || _settlement.IsTown;
            
            if (_settlement.IsVillage)
            {
                TradeBoundText = new TextObject("{=2noOKM5N}Trade bound settlement").ToString();

                var itemElementVm = CreateItemElementVM(_settlement.Village);
                ProducedItems.Add(itemElementVm);
                var settlementVm = new EncyclopediaSettlementVM(_settlement.Village.TradeBound);
                TradeBoundSettlements.Add(settlementVm);
            }
            else if (_settlement.IsTown)
            {
                TradeBoundText = new TextObject("{=q7xpz1xb}Trade bound village(s)").ToString();

                foreach (var tradeBound in _settlement.Town.TradeBoundVillages)
                {
                    var itemElementVm = CreateItemElementVM(tradeBound);
                    ProducedItems.Add(itemElementVm);

                    var settlementVm = new EncyclopediaSettlementVM(tradeBound.Settlement);
                    TradeBoundSettlements.Add(settlementVm);
                }
            }
            else
            {
                foreach (var bound in _settlement.BoundVillages)
                {
                    var itemElementVm = CreateItemElementVM(bound);
                    ProducedItems.Add(itemElementVm);
                }
            }
        }

        private static ItemElementVM CreateItemElementVM(Village village)
        {
            var primaryProduction = village.VillageType.PrimaryProduction;
            return new ItemElementVM(primaryProduction);
        }
    }
}
