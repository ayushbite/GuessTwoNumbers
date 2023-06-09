using MathNumberGusserProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MathNumberGusserProject
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Random random;
        DispatcherTimer StartedTimer;
        int timerCounter = 0;
        public int QuizNumber { get; set; }
        /*List<bool> buttonissubmitted { get; set; }*/
        List<bool> Scores { get; set; }
        List<GameValueModel> GameValues { get; set; }
        List<Grid> GameGrids { get; set; }
        public GameWindow(int Quiznumber)
        {
            InitializeComponent();
            //initiate all the lists
             GameValues = new List<GameValueModel>();   
            GameGrids = new List<Grid>();
            Scores = new List<bool>();
            random = new Random();
           
            this.QuizNumber = Quiznumber;
            GenerateDefaultSocres(QuizNumber);
            GenerateGameModels(QuizNumber);
            GenerateAndPopulateButtons(QuizNumber);
            GenerateGameGrids(QuizNumber);
        }

        private void GenerateDefaultSocres(int quizNumber)
        {
            for(int i = 0; i < QuizNumber; i++)
            {
                Scores.Add(false);
            }
        }

        private void GenerateGameModels(int quizNumber)
        {
            for(int i = 0; i < QuizNumber; i++)
            {
               
                GameValueModel model = new GameValueModel(Convert.ToInt32(random.Next(50,100)),Convert.ToInt32( random.Next(10, 50)),i);
                GameValues.Add(model);
            }
        }

        private void GenerateGameGrids(int quizNumber)
        {
            for(int i = 0; i < quizNumber; i++)
            {
                Grid g = new Grid();
                //adding all the coulmns and rows
                ColumnDefinition gCol1 = new ColumnDefinition();

                ColumnDefinition gCol2 = new ColumnDefinition();
                g.ColumnDefinitions.Add(gCol1);
                g.ColumnDefinitions.Add(gCol2);


                RowDefinition gRow1 = new RowDefinition();
                RowDefinition gRow2 = new RowDefinition();
                RowDefinition gRow3 = new RowDefinition();
                g.RowDefinitions.Add(gRow1);
                g.RowDefinitions.Add(gRow2);    
                g.RowDefinitions.Add(gRow3);
                //setting grid number tag
                g.Tag = GameValues[i].Gridnumber.ToString();

                //generate textblock for sum
                TextBlock sumtb = new TextBlock();
                sumtb.Text = "SUMMATION : " + GameValues[i].Sum.ToString();
                sumtb.Tag   = GameValues[i].Sum.ToString(); 
                Grid.SetColumn(sumtb, 0);
                Grid.SetRow(sumtb, 0);
                sumtb.FontSize = 20;
                sumtb.FontWeight = FontWeights.Bold;
                sumtb.VerticalAlignment = VerticalAlignment.Center;
                sumtb.HorizontalAlignment = HorizontalAlignment.Center; 
                
                TextBlock subtb = new TextBlock();
                subtb.Text = "DEVITION : " + GameValues[i].Sub.ToString();
                subtb.Tag = GameValues[i].Sub.ToString(); 
                Grid.SetColumn(subtb, 1);
                Grid.SetRow(sumtb, 0);
                subtb.FontSize = 20;
                subtb.FontWeight = FontWeights.Bold;
                subtb.VerticalAlignment = VerticalAlignment.Center;
                subtb.HorizontalAlignment = HorizontalAlignment.Center;
                TextBox valonetb = new TextBox();
                valonetb.Tag = GameValues[i].ValueOne.ToString();
                Grid.SetRow(valonetb, 1);
                Grid.SetColumn(valonetb, 0);
                TextBox valtwotb = new TextBox();
                valtwotb.Tag = GameValues[i].ValueTwo.ToString();
                Grid.SetRow(valtwotb, 1);
                Grid.SetColumn(valonetb, 1);

                //adding a next btn
                Button nextbtn = new Button();
                nextbtn.Content = "CHECK";
                nextbtn.Foreground = new SolidColorBrush(Colors.Red);
                nextbtn.Click += Nextbtn_Click;
                Grid.SetRow(nextbtn, 2);
                Grid.SetColumn(nextbtn, 0);
                


                g.Children.Add(sumtb);
                g.Children.Add(subtb);
                g.Children.Add(valonetb);
                g.Children.Add(valtwotb);
                g.Children.Add(nextbtn);
                GameGrids.Add(g);

            }
          

        }

        private void Nextbtn_Click(object sender, RoutedEventArgs e)
        {
           

            //go to next question

            //
            var btn = sender as Button;
            var parentgrid = VisualTreeHelper.GetParent(btn);
            var convparentgrid = parentgrid as Grid;
            //get 2 and 3 elemtns tag
           var valoneval= convparentgrid.Children[2] as TextBox;
            var valtwoval = convparentgrid.Children[3] as TextBox;


            //validations
            Regex nonNumericRegex = new Regex(@"\D");
            if (valoneval.Text == "" || nonNumericRegex.IsMatch(valoneval.Text))
            {
                MessageBox.Show("Please Enter the Correct Input");
                return;
            }
            if (valtwoval.Text == "" || nonNumericRegex.IsMatch(valtwoval.Text))
            {
                MessageBox.Show("Please Enter the Correct Input");
                return;
            }



            Console.WriteLine(valoneval.Tag);
            Console.WriteLine(valtwoval.Tag);

            //getting the grid number so that is can acess the scores current location 
            //next bttons current grid array location

            int gridindex =  Convert.ToInt32(convparentgrid.Tag);

            //checking if textbox values are true if its then storing in scores array
            //original value
            int valonebox = Convert.ToInt32(valoneval.Tag);
            int valtwobox = Convert.ToInt32(valtwoval.Tag);
            //user entered values
            int uservalonebox = Convert.ToInt32(valoneval.Text);
            int uservaltwobox = Convert.ToInt32(valtwoval.Text);


            if (uservalonebox == valtwobox && uservaltwobox == valonebox)
            {
                Scores[gridindex] = true;
            }
            btn.Foreground = new SolidColorBrush(Colors.Green);
            btn.Content = "CHECKED";
            btn.IsEnabled = false;



        }

        private void GenerateAndPopulateButtons(int quizNumber)
        {
            for (int i = 1; i <= quizNumber; i++)
            {

                Button btn = new Button();
                btn.Content = i.ToString();
/*                btn.Width = 40;
                btn.Height = 30;*/
                //template styling dynamic
                
               /* Style st = new Style();
                st.TargetType = typeof(Button);
                //conrol template
                ControlTemplate template = new ControlTemplate(typeof(Button));

                //adding contetn presenter
                ContentPresenter cp = new ContentPresenter();
                template.Triggers.Add(cp);
                //ADDING TRIGGER
                Trigger ismousePressedtrig = new Trigger();
                ismousePressedtrig.Property = Button.IsPressedProperty;
                ismousePressedtrig.Value = true;
                Setter  stispressed = new Setter();
                stispressed.Property = ForegroundProperty;
                stispressed.Value = Colors.Green;
                template.Triggers.Add(ismousePressedtrig);  

                Setter tempsetter = new Setter(TemplateProperty,template);
                st.Setters.Add(tempsetter);
                btn.Style = st;*/
                btn.Click += ActionClick;
                sp.Children.Add(btn);
            }
        }

        private void ActionClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
           for(int i = 1;i<=QuizNumber;i++)
            {
                string v = i.ToString();
                if (btn.Content.ToString() == v)
                {
                    MainGameGrid.Children.Clear();
                    MainGameGrid.Children.Add(GameGrids[i-1]);
                    Console.WriteLine(i);
                }
            }
        }

        private void GameStartButtonClickEvent(object sender, RoutedEventArgs e)
        {
            string btntext = "";
            string newgame = "NEW GAME";
            string endgame = "END GAME";
            var btn = sender as Button;
            var sp = btn.Content as StackPanel;
            foreach (var item in sp.Children)
            {
                if (item is TextBlock)
                {
                    btntext = (item as TextBlock).Text;
                }
            }
            if (btntext.Equals(newgame))
            {
                Debug.WriteLine(btntext);

                foreach (var item in sp.Children)
                {
                    if (item is TextBlock)
                    {
                        var currbtn = (item as TextBlock);
                        currbtn.Text = endgame;
                        currbtn.Foreground = new SolidColorBrush(Colors.Red);

                    }
                }



                //visible hide and seek control
                hideandseek.Visibility = Visibility.Visible;
                SetTimer();


                







            }
            if (btntext.Equals(endgame))
            {
                Debug.WriteLine(btntext);
                foreach (var item in sp.Children)
                {
                    if (item is TextBlock)
                    {
                        var currbtn = (item as TextBlock);
                        currbtn.Text = newgame;
                        currbtn.Foreground = new SolidColorBrush(Colors.Green);

                    }
                }
              

                //close hide and seek visibility
                hideandseek.Visibility = Visibility.Hidden;
                string s = "YOUR SCORE IS " + CalcScore() + " AND TOOK " + timerCounter.ToString() + " SECONDS TO COMPLETE";
                EndTimer();
                
                RerunWindow r = new RerunWindow(s);
                r.Show();
                this.Close();





            }
        }
        //timers
        private void SetTimer()
        {
            StartedTimer = new DispatcherTimer();
            StartedTimer.Interval = TimeSpan.FromMilliseconds(1000);
            StartedTimer.IsEnabled = true;
            StartedTimer.Tick += StartedTimer_Elapsed;

        }
        private void EndTimer()
        {
            StartedTimer.Stop();
            timerCounter = 0;
        }

        private void StartedTimer_Elapsed(object sender, EventArgs e)
        {
            timerCounter += 1;
            timerBlock.Text = timerCounter.ToString() + " " + "SECONDS";
            Debug.WriteLine(timerCounter.ToString());
        }

       /* public void IsGameCompleted()
        {
            resultbox.Visibility = Visibility.Visible;
            
            resultbox.Text = "YOUR SCORE IS " + CalcScore() + " AND TOOK " + timerCounter.ToString() + " SECONDS TO COMPLETE"; 
        }*/

        private string CalcScore()
        {
            int CorrectedCounter = 0;
            foreach(var item in Scores)
            {
                if(item == true) CorrectedCounter++;
            }
            return CorrectedCounter.ToString();
        }
    }
}
