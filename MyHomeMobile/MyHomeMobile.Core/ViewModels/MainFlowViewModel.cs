using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHomeMobile.Core.Service;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MyHomeMobile.Core.ViewModels
{
    public class MainFlowViewModel : ReactiveObject, IRoutableViewModel
    {
        public MainFlowViewModel(IScreen hostScreen, IAuthService authService)
        {
            HostScreen = hostScreen;

            if (authService.IsAuthenticated)
                HostScreen.Router.Navigate.Execute(new ShellViewModel(HostScreen));

            RegistryCommand = ReactiveCommand.Create();
            Observable.ObserveOn(RegistryCommand, RxApp.MainThreadScheduler).Subscribe(_ =>
            {
                HostScreen.Router.Navigate.Execute(new RegistryViewModel(HostScreen));
            });

            var canAdd = this.WhenAny(x => x.UserLogin, x => x.Password,
                (user, password) => !string.IsNullOrWhiteSpace(user.Value) && !string.IsNullOrWhiteSpace(password.Value));

            LoginCommand = ReactiveCommand.CreateAsyncObservable<bool>(canAdd,
                _ => Observable.FromAsync<bool>(async () => await authService.LoginAsync(UserLogin, Password)));
            Observable.ObserveOn(LoginCommand, RxApp.MainThreadScheduler).Subscribe(isAuthenticated =>
            {
                if(isAuthenticated)
                    HostScreen.Router.Navigate.Execute(new ShellViewModel(HostScreen));
                // else show login error
            });
        }

        public ReactiveCommand<object> RegistryCommand { get; private set; }

        public ReactiveCommand<bool> LoginCommand { get; private set; }

        [Reactive]
        public string UserLogin { get; set; }

        [Reactive]
        public string Password { get; set; }

        public string UrlPathSegment => "LoginPage";

        public IScreen HostScreen { get; private set; }
    }
}
