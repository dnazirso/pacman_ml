using Game_UI.Models;
using pacman_libs;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game_UI.Sprites
{
    /// <summary>
    /// Interaction logic for Obstable.xaml
    /// </summary>
    public partial class Obstacle : UserControl
    {
        Area area { get; set; }
        public Obstacle(int top, int left, int size)
        {
            InitializeComponent();
            SetArea(top, left, size);
        }
        private void SetArea(int top, int left, int size)
        {
            SetValue(Canvas.LeftProperty, (double)left);
            SetValue(Canvas.TopProperty, (double)top);
            SetValue(Canvas.WidthProperty, (double)size);
            SetValue(Canvas.HeightProperty, (double)size);
            RenderTransform = new TranslateTransform(size, size);
            area = Area.SetPositions(new Models.Position { X = (int)Canvas.GetTop(this), Y = (int)Canvas.GetLeft(this) }, size, size);
        }
        public bool HasCollide(IPlayer p)
        {
            return area.HasCollide(p);
        }
        public bool WillCollide(IPlayer p)
        {
            return area.WillCollide(p);
        }
    }
}
