using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region PrivateMembers

        private MarkType[] mResults;
        private bool mPlayer1Turn;
        private bool mGameEnded;

        #endregion

        #region Cunstructor

        /// <summary>
        /// Default Cunstructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        private void NewGame()
        {
            mResults = new MarkType[9];

            for (int i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }

            mPlayer1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Content = "";
                    button.Background = Brushes.AliceBlue;
                    button.Foreground = Brushes.Aquamarine;
                }
            );

            mGameEnded = false;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button) sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            if (mResults[index] != MarkType.Free)
                return;

            mResults[index] = mPlayer1Turn ? mResults[index] = MarkType.Cross : MarkType.Circle;

            button.Content = mPlayer1Turn ? "X" : "O";

            if (!mPlayer1Turn)
                button.Foreground = Brushes.LimeGreen;

            mPlayer1Turn ^= true;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            #region HorizontalWins
                //rows
                if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
                {
                    mGameEnded = true;

                    button0_0.Background = button1_0.Background = button2_0.Background = Brushes.Green;
                    Console.Beep();
                }
                if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
                {
                    mGameEnded = true;

                    button0_1.Background = button1_1.Background = button2_1.Background = Brushes.Green;
                    Console.Beep();
                }
                if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
                {
                    mGameEnded = true;

                    button0_2.Background = button1_2.Background = button2_2.Background = Brushes.Green;
                    Console.Beep();
                }
            #endregion

            #region VerticalWins
                //columns
                if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
                {
                    mGameEnded = true;
                    Console.Beep();

                    button0_0.Background = button0_1.Background = button0_2.Background = Brushes.Green;
                }
                if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
                {
                    mGameEnded = true;
                    Console.Beep();

                    button1_0.Background = button1_1.Background = button1_2.Background = Brushes.Green;
                }
                if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
                {
                    mGameEnded = true;
                    Console.Beep();

                    button2_0.Background = button2_1.Background = button2_2.Background = Brushes.Green;
                }
            #endregion

            #region DiagonalWins
                if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
                {
                    mGameEnded = true;

                    Console.Beep();
                    button0_0.Background = button1_1.Background = button2_2.Background = Brushes.Green;
                }
                if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
                {
                    mGameEnded = true;
                    Console.Beep();

                    button2_0.Background = button1_1.Background = button0_2.Background = Brushes.Green;
                }
            #endregion

            #region NoWins
            //none
            if (!mResults.Any(result => result == MarkType.Free))
                {
                    mGameEnded = true;

                    Container.Children.Cast<Button>().ToList().ForEach(
                        button =>
                        {
                            button.Background = Brushes.OrangeRed;
                        }
                    );
                    Console.Beep();
                }
            #endregion
        }
    }
}
