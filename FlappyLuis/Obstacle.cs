using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;

namespace FlappyBird {
    class Obstacle {
        public Rectangle superiorRect;
        public Rectangle inferiorRect;
        Random randomNumber = new Random(DateTime.Now.Millisecond);
        int inferiorHeight;
        int posX = (int) Windows.UI.Xaml.Window.Current.Bounds.Width;
        double velocityX = -7;
        private TextBlock Game_Over_Text = new TextBlock();

        public Obstacle() {
            inferiorHeight = randomNumber.Next(30, 300);
            this.superiorRect = new Rectangle() {Width = 60, Height = inferiorHeight, Fill = new SolidColorBrush(Colors.Black)};
            Canvas.SetLeft(superiorRect, posX);
            Canvas.SetTop(superiorRect, 0);
            this.inferiorRect = new Rectangle() { Width = 60, Height = 500, Fill = new SolidColorBrush(Colors.Black) };
            Canvas.SetLeft(inferiorRect, posX);
            Canvas.SetTop(inferiorRect, inferiorHeight + 160);
        }

        public void Update() {
            this.posX += (int) velocityX;
            Canvas.SetLeft(superiorRect, posX);
            Canvas.SetLeft(inferiorRect, posX);
        }

        public bool Intersect(Rectangle actor) {
            Rect target = new Rect(Canvas.GetLeft(actor), Canvas.GetTop(actor), actor.Width, actor.Height);
            Rect supRect = new Rect(Canvas.GetLeft(superiorRect), Canvas.GetTop(superiorRect), superiorRect.Width, superiorRect.Height);
            Rect infRect = new Rect(Canvas.GetLeft(inferiorRect), Canvas.GetTop(inferiorRect), inferiorRect.Width, inferiorRect.Height);
            if (supRect.X < target.X + target.Width && 
                supRect.X + supRect.Width > target.X &&
                supRect.Y < target.Y + target.Height &&
                supRect.Height + supRect.Y > target.Y) {
                return true;
            } else if (infRect.X < target.X + target.Width &&
                infRect.X + infRect.Width > target.X &&
                infRect.Y < target.Y + target.Height &&
                infRect.Height + infRect.Y > target.Y) {
                return true;
            } else {
                return false;
            }
        }

        public void Reset() {
            posX = (int)Windows.UI.Xaml.Window.Current.Bounds.Width;
            inferiorHeight = randomNumber.Next(30, 300);
            this.superiorRect = new Rectangle() { Width = 60, Height = inferiorHeight, Fill = new SolidColorBrush(Colors.Black) };
            Canvas.SetLeft(superiorRect, posX);
            Canvas.SetTop(superiorRect, 0);
            this.inferiorRect = new Rectangle() { Width = 60, Height = 500, Fill = new SolidColorBrush(Colors.Black) };
            Canvas.SetLeft(inferiorRect, posX);
            Canvas.SetTop(inferiorRect, inferiorHeight + 160);
        }
    }
}

