using System;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Windows.UI;

namespace FlappyBird {
    public sealed partial class MainPage : Page {
        
        Actor bird = new Actor(20, (int) Window.Current.Bounds.Height/2 - 20);
        Obstacle obstacle = new Obstacle();
        Canvas canvas = new Canvas();
        
        private DispatcherTimer timer = new DispatcherTimer() {Interval = new TimeSpan(200)};
        private DispatcherTimer timer_spawn = new DispatcherTimer() { Interval = new TimeSpan(0,0,3) };
        private DispatcherTimer timer_score = new DispatcherTimer() { Interval = new TimeSpan(0,0,0,0,10) };

        static public double gravity = 0.77;
        double score = 0.01;

        public MainPage() {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.timer.Tick += Timer_Tick;
            this.timer_spawn.Tick += Timer_spawn_Tick;
            this.timer_score.Tick += Timer_score_Tick;
            this.timer.Start();
            this.timer_spawn.Start();
            this.timer_score.Start();
        }

        private void Draw() {
            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(bird.getDrawScheme());
            LayoutRoot.Children.Add(obstacle.inferiorRect);
            LayoutRoot.Children.Add(obstacle.superiorRect);
            var scoreString = score.ToString();
            if (score.ToString().Length >= 4) {
                scoreString = score.ToString().Substring(0, 4);
            } else if(score.ToString().Length >= 3) {
                scoreString = score.ToString().Substring(0, 3) + "0";
            }else if(score.ToString().Length == 1) {
                scoreString = score.ToString() + ".00";
            }

            LayoutRoot.Children.Add(new TextBlock() {
                Text = scoreString, FontSize = 24,
                Foreground = new SolidColorBrush(Colors.Red)
            });
        }

        private void Timer_spawn_Tick(object sender, object e) {
            obstacle.Reset();
        }

        private void Timer_Tick(object sender, object e) {
            Draw();
            bird.Update();
            obstacle.Update();

            if (obstacle.Intersect(bird.getDrawScheme()) || bird.offLimits())
                Game_Over();
        }

        private void Game_Over() {
            timer.Stop();
            timer_spawn.Stop();
            var Game_Over_Text = new TextBlock() {
                Text = "GAME OVER",
                FontSize = 67,
                Foreground = new SolidColorBrush(Colors.MidnightBlue)
            };
            Canvas.SetLeft(Game_Over_Text, 12);
            Canvas.SetTop(Game_Over_Text, (int)Window.Current.Bounds.Height / 2 - 20);
            LayoutRoot.Children.Add(Game_Over_Text);
        }

        private void Restart() {
            bird = new Actor(20, 210);
            obstacle.Reset();
            timer.Start();
            timer_spawn.Start();
            score = 0;
        }

        private void MainOutline_Tapped(object sender, PointerRoutedEventArgs e) {
            if (timer.IsEnabled)
                bird.Jump();
            else
                Restart();
        }

        private void Timer_score_Tick(object sender, object e) {
            this.score += 0.01;
        }
    }
}
