using MarcTron.Plugin;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace AdmobTest.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _isLoaded;
        private readonly string interstitialUnitId;

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";

            // 各ボタンのDelegateCommandにメソッドを割り当て

            ShowCommand = new DelegateCommand(Show);

            LoadCommand = new DelegateCommand(Load);

            IsLoadedCheckCommand = new DelegateCommand(IsLoadedCheck);

            // Interstitial広告のロード状況を示すラベル用プロパティを初期化
            _isLoaded = CrossMTAdmob.Current.IsInterstitialLoaded().ToString();

            // Interstitial広告用 対象プラットフォームのUnitIDを設定
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    interstitialUnitId = "ca-app-pub-3940256099942544/8691691433";
                    break;
                case Device.iOS:
                    interstitialUnitId = "ca-app-pub-3940256099942544/5135589807";
                    break;
            }
        }

        public DelegateCommand ShowCommand { get; private set; }

        public DelegateCommand LoadCommand { get; private set; }

        public DelegateCommand IsLoadedCheckCommand { get; private set; }

        // Interstitial広告のロード状況を示すラベルのプロパティ
        public string IsLoaded
        {
            get { return _isLoaded; }
            set { SetProperty(ref _isLoaded, value); }
        }

        // Interstitial広告の表示メソッド
        private void Show()
        {
            if (CrossMTAdmob.Current.IsInterstitialLoaded())
            {
                CrossMTAdmob.Current.ShowInterstitial();
            }
        }

        // Interstitial広告をロードするメソッド
        private void Load()
        {
            CrossMTAdmob.Current.LoadInterstitial(interstitialUnitId);
        }

        // Interstitial広告のロード状況をチェックするメソッド
        private void IsLoadedCheck()
        {
            IsLoaded = CrossMTAdmob.Current.IsInterstitialLoaded().ToString();
        }
    }
}
