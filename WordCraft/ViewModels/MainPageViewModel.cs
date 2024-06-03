using _6002CEM_Maui_App.ViewModels;
using System;
using System.Windows;
using System.IO;
using System.Windows.Shapes;

namespace WordCraft.ViewModels
{
    public partial class MainPageViewModel : PageViewModel
    {
        public MainPageViewModel()
        {
            List<string> specials = getSpecialWords();
            MessageBox.Show(specials[21]);
        }

        private List<string> getSpecialWords()
        {
            List<string> words = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader("../../../Resources/Words50.csv"))
                {
                    if (!reader.EndOfStream)
                    {
                        reader.ReadLine(); 
                    }

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //MessageBox.Show((line.Split(','))[0]);
                        words.Add((line.Split(','))[0]);
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return words;
        }
    }
}

