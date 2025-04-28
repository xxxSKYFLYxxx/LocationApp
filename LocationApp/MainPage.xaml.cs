using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using LocationApp.Services;
using Xamarin.Forms;

namespace LocationApp
{
    public partial class MainPage : ContentPage
    {
        private readonly LocationService _locationService;
        private CancellationTokenSource _cancellationTokenSource;


        public MainPage()
        {
            InitializeComponent();
            _locationService = new LocationService();
        }

        /// Обработка нажатия кнопки "Get Location".
        private async void OnGetLocationClicked(object sender, EventArgs e)
        {
            await UpdateLocationAsync();
        }

        /// Обновляет текущие координаты.
        private async Task UpdateLocationAsync()
        {
            try
            {
                // Получаем координаты через сервис
                var result = await _locationService.GetCurrentLocationAsync();

                // Обновляем UI в зависимости от результата
                if (!string.IsNullOrEmpty(result.Error))
                {
                    LatitudeLabel.Text = "Latitude: N/A";
                    LongitudeLabel.Text = "Longitude: N/A";
                    ErrorLabel.Text = result.Error;
                }

                else
                {
                    LatitudeLabel.Text = $"Latitude: {result.Latitude}";
                    LongitudeLabel.Text = $"Longitude: {result.Longitude}";
                    ErrorLabel.Text = "";
                }
            }

            catch (Exception)
            {
                // Обработка неизвестных ошибок
                LatitudeLabel.Text = "Latitude: N/A";
                LongitudeLabel.Text = "Longitude: N/A";
                ErrorLabel.Text = "An unexpected error occurred.";
                GpsStatusLabel.Text = "GPS Status: Error";
            }
        }

        /// Начинает автоматическое обновление координат.
        private async void OnStartAutoUpdateClicked(object sender, EventArgs e)
        {
            StopAutoUpdateButton.IsEnabled = true;
            _cancellationTokenSource = new CancellationTokenSource();

            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                await UpdateLocationAsync();
                await Task.Delay(5000); // Обновляем каждые 5 секунд
            }
        }

        /// Останавливает автоматическое обновление координат.
        private void OnStopAutoUpdateClicked(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            StopAutoUpdateButton.IsEnabled = false;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        protected override void OnTabIndexPropertyChanged(int oldValue, int newValue)
        {
            base.OnTabIndexPropertyChanged(oldValue, newValue);
        }

        protected override int TabIndexDefaultValueCreator()
        {
            return base.TabIndexDefaultValueCreator();
        }

        protected override void OnTabStopPropertyChanged(bool oldValue, bool newValue)
        {
            base.OnTabStopPropertyChanged(oldValue, newValue);
        }

        protected override bool TabStopDefaultValueCreator()
        {
            return base.TabStopDefaultValueCreator();
        }

        public override SizeRequest GetSizeRequest(double widthConstraint, double heightConstraint)
        {
            return base.GetSizeRequest(widthConstraint, heightConstraint);
        }

        protected override void InvalidateMeasure()
        {
            base.InvalidateMeasure();
        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            return base.OnSizeRequest(widthConstraint, heightConstraint);
        }

        protected override void ChangeVisualState()
        {
            base.ChangeVisualState();
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        protected override void OnChildMeasureInvalidated(object sender, EventArgs e)
        {
            base.OnChildMeasureInvalidated(sender, e);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override void OnChildRemoved(Element child)
        {
            base.OnChildRemoved(child);
        }

        protected override void OnChildRemoved(Element child, int oldLogicalIndex)
        {
            base.OnChildRemoved(child, oldLogicalIndex);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }
    }
}