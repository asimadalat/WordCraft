using System.Text;
using System.Windows;
using WordCraft.ViewModels;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WordCraft.Interfaces;
using WordCraft.Services;

namespace WordCraft
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Set data binding context to MainPageViewModel, passing scoreService.
            IScoreService scoreService = new ScoreService();
            DataContext = new MainPageViewModel(scoreService);
        }
    }
}