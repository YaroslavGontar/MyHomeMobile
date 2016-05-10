using ReactiveUI;

namespace MyHomeMobile.Core.ViewModels
{
    public class ShellViewModel : ReactiveObject, IRoutableViewModel
    {
        public ShellViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }

        public string UrlPathSegment => "Shell";
        public IScreen HostScreen { get; }
    }
}