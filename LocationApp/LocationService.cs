using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LocationApp.Services
{
    public class LocationService
    {
        /// Получает текущие координаты пользователя.
        /// <param name="desiredAccuracy"> Точность получения местоположения.</param>
        /// <param name="timeout"> Таймаут ожидания координат.</param>
        /// <returns>Кортеж с широтой, долготой и сообщением об ошибке (если есть).</returns>
        public async Task<(double Latitude, double Longitude, string Error)> GetCurrentLocationAsync(
            GeolocationAccuracy desiredAccuracy = GeolocationAccuracy.Medium,
            TimeSpan? timeout = null)
        {
            try
            {
                // Запрашиваем разрешение на использование геолокации
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    return (0, 0, "Permission Denied");
                }

                // Проверяем поддержку геолокации через попытку получить координаты
                try
                {
                    // Сначала пытаемся получить последнее известное местоположение
                    var location = await Geolocation.GetLastKnownLocationAsync();
                    if (location == null)
                    {
                        // Если нет последнего известного местоположения, запрашиваем новое
                        location = await Geolocation.GetLocationAsync(new GeolocationRequest
                        {
                            DesiredAccuracy = desiredAccuracy,
                            Timeout = timeout ?? TimeSpan.FromSeconds(30) // Используем таймаут по умолчанию, если не указан
                        });
                    }

                    // Проверяем, получены ли координаты
                    if (location != null)
                    {
                        return (location.Latitude, location.Longitude, null);
                    }
                    else
                    {
                        return (0, 0, "Unable to get location");
                    }
                }
                catch (FeatureNotSupportedException)
                {
                    return (0, 0, "GPS is not supported on this device");
                }
                catch (Exception ex)
                {
                    return (0, 0, $"An error occurred: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return (0, 0, $"An unexpected error occurred: {ex.Message}");
            }
        }

        /// Проверяет, включена ли геолокация на устройстве.
        /// <returns> true, если геолокация доступна; иначе false </returns>
        public async Task<bool> IsGpsEnabledAsync()
        {
            try
            {
                // Пытаемся получить разрешение на использование геолокации
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    return false;
                }

                // Проверяем, поддерживается ли геолокация на устройстве
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();
                    if (location == null)
                    {
                        // Если нет последнего известного местоположения, пытаемся получить новое
                        location = await Geolocation.GetLocationAsync(new GeolocationRequest
                        {
                            DesiredAccuracy = GeolocationAccuracy.Medium,
                            Timeout = TimeSpan.FromSeconds(5) // Короткий таймаут для проверки
                        });
                    }

                    // Если координаты успешно получены, считаем, что GPS включен
                    return location != null;
                }
                catch (FeatureNotSupportedException)
                {
                    return false; // Геолокация не поддерживается
                }
                catch (Exception)
                {
                    return false; // Произошла ошибка при получении координат
                }
            }
            catch (Exception)
            {
                return false; // Произошла ошибка при проверке разрешений
            }
        }
    }
}