using MathNumberGusserProject.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace MathNumberGusserProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        GameWindow GameWindow { get; set; }
        public int Quizvalue { get; set; }
        public MainWindow()
        {
            InitializeComponent();
           

        }

        private void GenerateClick(object sender, RoutedEventArgs e)
        {
            Regex nonNumericRegex = new Regex(@"\D");
            if (!(quiznumber.Text == "" || nonNumericRegex.IsMatch(quiznumber.Text)))
            {
                Quizvalue = Convert.ToInt32(quiznumber.Text);
                GameWindow = new GameWindow(Quizvalue);
                GameWindow.Show();
                this.Close();
                
                
            }
            else
            {
                MessageBox.Show("please enter number between 0 to 10");
            }

           
        }
    }
}
