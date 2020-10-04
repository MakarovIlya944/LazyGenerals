using System;
using System.Windows;
using System.Windows.Controls;
using LazyGenerals.Client.Services;
using Microsoft.Extensions.Logging;

namespace LazyGenerals.Client.View
{
    /// <summary>
    /// Логика взаимодействия для LobbyWindow.xaml
    /// </summary>
    public partial class LobbyWindow : Page
    {
        private readonly Broker _broker;

        private readonly ILogger<LobbyWindow> _logger;

        public LobbyWindow(Broker broker, ILogger<LobbyWindow> logger)
        {
            _broker = broker;
            _logger = logger;
            InitializeComponent();
        }

        private async void GetLobbyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button bt))
            {
                return;
            }

            try
            {
                bt.IsEnabled = false;
                await _broker.UpdateLobby();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "");
            }
            finally
            {
                bt.IsEnabled = true;
            }
        }
    }
}