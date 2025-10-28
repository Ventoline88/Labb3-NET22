using System.Windows;

namespace Labb3_NET22
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Creates a new main window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            MainContent.Content = new MainView();
        }
    }
}
