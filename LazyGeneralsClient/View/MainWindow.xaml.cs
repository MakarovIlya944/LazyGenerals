using System.Windows;
using System.Windows.Controls;
using LazyGenerals.Client.Services;
using LazyGenerals.Client.Utility;

namespace LazyGenerals.Client.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Broker _broker;

        public NotifyPropertyField<Page> Lobby = new NotifyPropertyField<Page>();

        public MainWindow(Broker broker, LobbyWindow lobby)
        {
            _broker = broker;
            InitializeComponent();
            Lobby.Value = lobby;
            LobbyFrame.Navigate(Lobby.Value);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            LobbyFrame.Visibility = Visibility.Visible;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}