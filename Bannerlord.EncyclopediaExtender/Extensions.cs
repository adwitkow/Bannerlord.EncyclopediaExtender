using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace Bannerlord.EncyclopediaExtender
{
    public static class Extensions
    {
        public static void AddPair<T>(
            this MBBindingList<StringPairItemVM> bindingList,
            string header,
            T value,
            BasicTooltipViewModel? hint = null)
        {
            if (value is null)
            {
                return;
            }

            bindingList.Add(new StringPairItemVM(header, value.ToString(), hint));
        }

        public static void AddPair<T>(
            this MBBindingList<StringPairItemVM> bindingList,
            TextObject header,
            T value,
            BasicTooltipViewModel? hint = null)
        {
            AddPair(bindingList, header.ToHeader().ToString(), value, hint);
        }
    }
}
