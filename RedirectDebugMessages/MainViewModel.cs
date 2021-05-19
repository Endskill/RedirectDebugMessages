using RedirectDebugMessages.BusinessLogic;
using RedirectDebugMessages.Information;
using RedirectDebugMessages.MVVM;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace RedirectDebugMessages
{
    public class MainViewModel : BindableBase
    {
        private readonly CommunicationService _communicationService;
        private ObservableCollection<GtfoMod> _mods = new ObservableCollection<GtfoMod>();
        private GtfoMod _selectedMod;
        private Dispatcher _dispatcher;

        public MainViewModel()
        {
            _communicationService = new CommunicationService();
            _communicationService.ReceivedMessage += ReceivedMessageHandler;
            _dispatcher = Dispatcher.CurrentDispatcher;
            Task.Run(_communicationService.StartServerStackableAsync);
            
            //Task.Run(() =>
            //{
            //    _communicationService.StartServerStackable();
            //});

            //ReceivedMessageHandler(null, new PipeMessage("Hi", "CheatMenu"));
            //ReceivedMessageHandler(null, new PipeMessage("Deutschland", "CheatMenu"));
            //ReceivedMessageHandler(null, new PipeMessage("Custom Debuggin :O", "DexMod"));
            //ReceivedMessageHandler(null, new PipeMessage("Reeeee", "Peelz"));
            //ReceivedMessageHandler(null, new PipeMessage("Brumm", "CheatMenu"));
        }

        public ObservableCollection<GtfoMod> Mods 
        { 
            get => _mods;
            set => Set(ref _mods, value, nameof(Mods));
        }

        public GtfoMod SelectedMod 
        { 
            get => _selectedMod; 
            set => Set(ref _selectedMod, value,nameof(SelectedMod)); 
        }

        public void ReceivedMessageHandler(object sender, PipeMessage message)
        {
            _dispatcher.Invoke(() =>
            {
                GtfoMod correspondingMod = null;
                if (Mods.All(it => it.ModName != message.ModName))
                {
                    correspondingMod = new GtfoMod(message.ModName);
                    Mods.Add(correspondingMod);
                }

            (correspondingMod ?? Mods.First(it => it.ModName == message.ModName)).Messages.Add(new MessageObj(message.Message, message.ForeGroundColor, message.BackGroundColor));
            });
        }
    }
}
