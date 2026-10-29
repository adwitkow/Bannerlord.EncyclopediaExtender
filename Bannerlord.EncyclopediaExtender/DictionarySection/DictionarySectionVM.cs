using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;

namespace Bannerlord.EncyclopediaExtender.DictionarySection;

public class DictionarySectionVM : ViewModel
{
    public DictionarySectionVM()
    {
        Items = [];
        Title = string.Empty;
    }

    [DataSourceProperty]
    public string Title
    {
        get;
        set
        {
            if (value != field)
            {
                field = value;
                OnPropertyChangedWithValue(value, nameof(Title));
            }
        }
    }

    [DataSourceProperty]
    public MBBindingList<StringPairItemVM> Items { get; }
}