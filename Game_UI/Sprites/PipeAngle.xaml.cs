using board_libs.Models;
using System.Windows.Controls;
using System.Windows.Media;
using utils_libs.Abstractions;

namespace Game_UI.Sprites
{
    /// <summary>
    /// Interaction logic for PipeAngle.xaml
    /// </summary>
    public partial class PipeAngle : UserControl, IBlock
    {
        Area area { get; set; }
        public PipeAngle(Area area)
        {
            InitializeComponent();
            this.area = area;
            int angle;
            switch (area.Shape)
            {
                case '╗': angle = 90; break;
                case '╝': angle = 180; break;
                case '╚': angle = 270; break;
                case '╔':
                default: angle = 0; break;
            }
            pipe_obstacle.RenderTransform = new RotateTransform(angle, 10, 10);
            SetValue(Canvas.LeftProperty, (double)area.Min.Y);
            SetValue(Canvas.TopProperty, (double)area.Min.X);
            SetValue(Canvas.WidthProperty, (double)area.Size);
            SetValue(Canvas.HeightProperty, (double)area.Size);
        }
        public bool Collide(IPlayer p) => area.Collide(p);
        public bool WillCollide(IPlayer p) => area.WillCollide(p);
        public IPosition GetCoord() => area.Coord;
        public bool Overlap(IPlayer p) => area.Overlap(p);
    }
}
