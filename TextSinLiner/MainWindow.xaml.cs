using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TextSinLiner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            editEndTimer = new Timer(EditEndCallback, null, Timeout.Infinite, Timeout.Infinite);
        }

        //EditEndCallbackメソッドのシグネチャには object? state
        ///としてstateパラメーターがありますが、メソッド内で使用
        ///されていないので、null許容を追加することで警告を解消できます。
        private void EditEndCallback(object? state)
        {
            Dispatcher.Invoke(() => {
                UpdateSinWave();
            });
        }


        private void UpdateSinWave()
        {
            string text = inputTextBox.Text;

            // Measure text width
            FormattedText formattedText = new FormattedText(
      text,
      System.Globalization.CultureInfo.CurrentCulture,
      FlowDirection.LeftToRight,
      new Typeface(inputTextBox.FontFamily, inputTextBox.FontStyle, inputTextBox.FontWeight, inputTextBox.FontStretch),
      inputTextBox.FontSize,
      Brushes.Black,
      1.0); // PixelsPerDipの値を1.0に設定

            double textWidth = formattedText.WidthIncludingTrailingWhitespace;

            // Draw sin wave based on text width            
            double amplitude = canvas1.ActualHeight /6; // Amplitude: サイン波の振幅（高さ）
            double frequency = 0.5; //Sin波の周期
            double phaseShift = 0;
            double offsetX = 18; //Sin Wave Start Position
            double offsetY = canvas1.ActualHeight / 20;
            double step = 0.01;
            double startX = offsetX;
            double endX = textWidth + offsetX;

            
            



            for (double x = startX; x <= endX; x += step)
            {
                double y = amplitude * Math.Sin(frequency * (x + phaseShift - offsetX));
                DrawPoint(x, offsetY + y);
            }
        }


        private void DrawPoint(double x, double y)
        {
            Ellipse ellipse = new Ellipse
            {
                Width = 2,
                Height = 2,
                Fill = Brushes.Black
            };

            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);

            canvas1.Children.Add(ellipse);
        }


        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {

          //  DrawSinWaveBasedOnStringLength();
            //    string text = inputTextBox.Text;
            //    double waveLength = text.Length * 5.3; // 波の周期を文字列の長さに基づいて設定

            //    DrawSineWave(waveLength);
            //}
        }

        System.Threading.Timer editEndTimer;

        private void inputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            editEndTimer.Change(3000, Timeout.Infinite); // タイマーを3秒後に開始
        }

       
    }
}
