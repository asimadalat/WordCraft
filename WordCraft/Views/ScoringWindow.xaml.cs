using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WordCraft.Interfaces;
using WordCraft.Services;
using WordCraft.ViewModels;

namespace WordCraft.Views
{

    public partial class ScoringWindow : Window
    {
        public ScoringWindow()
        {
            InitializeComponent();

            // Set data binding context to ScoringPageViewModel, passing scoreService.
            IScoreService scoreService = new ScoreService();
            DataContext = new ScoringPageViewModel(scoreService);

        }
    }
}
