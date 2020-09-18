using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LazyGeneralsClient
{
    /// <summary>
    /// Логика взаимодействия для LobbyWindow.xaml
    /// </summary>
    public partial class LobbyWindow : Page
    {
        public LobbyWindow()
        {
            InitializeComponent();
        }

        private void GetLobbyButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Broker.UpdateLobby());
            int f = 1;
            f += 1;
        }
    }
}
