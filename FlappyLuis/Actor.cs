using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace FlappyBird {
    class Actor {
        int posX, posY;

        private ImageBrush teste = new ImageBrush();

        double velocityY = 0;

        public Actor(int positionX, int positionY) {
            this.posX = positionX;
            this.posY = positionY;
        }

        public void Update() {

            this.posY += (int)velocityY;
            velocityY += MainPage.gravity;
        }

        public Rectangle getDrawScheme() {
            var rect = new Rectangle() { Height = 48, Width = 48, Fill = new SolidColorBrush(Colors.Black) };
            Canvas.SetLeft(rect, posX);
            Canvas.SetTop(rect, posY);
            return rect;
        }

        public void Jump() {
            velocityY = -12;
        }

        public bool offLimits() {
            if (this.posY >= 0 && this.posY < (int) Windows.UI.Xaml.Window.Current.Bounds.Height - 24)
                return false;
            else
                return true;
        }
    }
}
