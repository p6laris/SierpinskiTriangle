using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Sierpinski
{
    public partial class MainWindow : Window
    {

        private Random random;
        //Random 
        private Point particles;

        //The three vertices of the Sierpinski triangle
        private Point a;
        private Point b;
        private Point c;


        public MainWindow()
        {
            InitializeComponent();
            random = new Random();
        }

        // Set up the initial positions of the three vertices of the Sierpinski triangle
        private void Setup()
        {
            a.X = canvas.ActualWidth / 2;
            a.Y = 0;

            b.X = 0;
            b.Y = canvas.ActualHeight;

            c.X = canvas.ActualWidth;
            c.Y = canvas.ActualHeight;
        }

        // Draw a point on the canvas with the specified color and coordinates
        private void DrawPoint(Point point, Color color)
        {

            var ellipse = new Ellipse
            {
                Width = 1,
                Height = 1,
                Fill = new SolidColorBrush(color),
            };

            Canvas.SetLeft(ellipse, point.X);
            Canvas.SetTop(ellipse, point.Y);

            Dispatcher.Invoke(() =>
            {
                canvas.Children.Add(ellipse);
            });


        }

        // Draw a specified number of particles using the Sierpinski triangle algorithm
        private async void DrawParticles(int seed)
        {

            for (int i = 1; i <= seed; i++)
            {
                // Randomly select one of the three vertices of the Sierpinski triangle
                var r = random.Next(3);

                // Calculate the new position of the particle
                if (r == 0)
                {
                    particles.X = (a.X + particles.X) / 2;
                    particles.Y = (a.Y + particles.Y) / 2;
                }
                else if (r == 1)
                {
                    particles.X = (b.X + particles.X) / 2;
                    particles.Y = (b.Y + particles.Y) / 2;
                }
                else
                {
                    particles.X = (c.X + particles.X) / 2;
                    particles.Y = (c.Y + particles.Y) / 2;
                }

                // Draw the particle on the canvas and update the seeds count int UI Thread
                Dispatcher.Invoke(() =>
                {
                    DrawPoint(particles, Colors.Red);
                    seedsTextBlock.Text = $"Seeds: {i}";
                });

                //Wait for one ms
                await Task.Delay(1);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Setup();

            DrawPoint(a, Colors.White);
            DrawPoint(b, Colors.White);
            DrawPoint(c, Colors.White);

            // Draw 100,000 particles using the Sierpinski triangle algorithm
            DrawParticles(100000);
        }
    }
}
