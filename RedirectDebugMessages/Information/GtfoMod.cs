using RedirectDebugMessages.MVVM;
using System.Collections.ObjectModel;

namespace RedirectDebugMessages.Information
{
    public class GtfoMod : BindableBase
    {
        private string _modName;
        private ObservableCollection<MessageObj> _messages = new ObservableCollection<MessageObj>();

        public GtfoMod(string modName)
        {
            ModName = modName;
        }

        public ObservableCollection<MessageObj> Messages
        {
            get => _messages;
            set => Set(ref _messages, value, nameof(Messages));
        }

        public string ModName 
        {
            get => _modName;
            set => Set(ref _modName, value, nameof(ModName));
        }
    }
}
