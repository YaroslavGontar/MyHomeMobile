using ReactiveUI;

namespace MyHomeMobile.Core.ViewModels
{
    public class RegistryViewModel : ReactiveObject, IRoutableViewModel
    {
        public RegistryViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }

        public string UrlPathSegment { get; }
        public IScreen HostScreen { get; }
    }
}