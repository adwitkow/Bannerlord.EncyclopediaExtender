using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace Bannerlord.EncyclopediaExtender
{
    public static class Extensions
    {
        public static void AddPair<T>(this MBBindingList<StringPairItemVM> bindingList, string header, T value)
        {
            if (value is null)
            {
                return;
            }

            bindingList.Add(new StringPairItemVM(header, value.ToString()));
        }

        public static void AddPair<T>(this MBBindingList<StringPairItemVM> bindingList, TextObject header, T value)
        {
            AddPair(bindingList, header.ToString(), value);
        }
    }
}
