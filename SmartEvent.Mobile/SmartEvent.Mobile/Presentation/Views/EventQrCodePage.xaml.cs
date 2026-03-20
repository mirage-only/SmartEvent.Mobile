using SmartEvent.Mobile.Presentation.ViewModels;

namespace SmartEvent.Mobile.Presentation.Views
{
    public partial class EventQrCodePage : ContentPage
    {
        public EventQrCodePage(EventQrGeneratorViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is EventQrGeneratorViewModel vm)
            {
                vm.StopPolling();
            }
        }
    }
}