using System.Windows;

namespace MathNumberGusserProject
{
    /// <summary>
    /// Interaction logic for RerunWindow.xaml
    /// </summary>
    public partial class RerunWindow : Window
    {

        public RerunWindow(string s)
        {
            InitializeComponent();
            resultbox.Text = s;
        }

        private void rerun(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
