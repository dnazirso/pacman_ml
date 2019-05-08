using Game_UI.Models;
using pacman_libs;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game_UI.Sprites
{
    /// <summary>
    /// Interaction logic for PipeStraight.xaml
    /// </summary>
    public partial class PipeStraight : UserControl, IBlock
    {
        Area area { get; set; }
        public PipeStraight(int top, int left, int size, bool isBlocking, int angle)
        {
            InitializeComponent();
            SetArea(top, left, size, isBlocking);
            pipe_obstacle.RenderTransform = new RotateTransform(angle, 10, 10);
        }
        private void SetArea(int top, int left, int size, bool isblocking)
        {
            SetValue(Canvas.LeftProperty, (double)left);
            SetValue(Canvas.TopProperty, (double)top);
            SetValue(Canvas.WidthProperty, (double)size);
            SetValue(Canvas.HeightProperty, (double)size);
            area = Area.SetPositions(new Models.Position { X = (int)Canvas.GetTop(this), Y = (int)Canvas.GetLeft(this) }, size, isblocking);
        }
        public bool HasCollide(IPlayer p) => area.HasCollide(p.Position);
        public bool WillCollide(IPlayer p) => area.WillCollide(p);
    }
}
