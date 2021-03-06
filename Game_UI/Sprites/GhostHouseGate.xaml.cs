using board_libs.Models;
using System.Windows.Controls;
using System.Windows.Media;
using utils_libs.Abstractions;
using utils_libs.Models;

namespace Game_UI.Sprites
{
    /// <summary>
    /// Interaction logic for GhostHouseGate.xaml
    /// </summary>
    public partial class GhostHouseGate : UserControl, IBlock
    {
        Area area { get; set; }
        public GhostHouseGate(Area area)
        {
            InitializeComponent();
            this.area = area;
            int angle = 90;
            gate.RenderTransform = new RotateTransform(angle, 10, 10);
            SetValue(Canvas.LeftProperty, (double)area.Min.Y);
            SetValue(Canvas.TopProperty, (double)area.Min.X);
            SetValue(Canvas.WidthProperty, (double)area.Size);
            SetValue(Canvas.HeightProperty, (double)area.Size);
        }
        public bool Collide(IPlayer p) => area.Collide(p);
        public bool WillCollide(IPlayer p) => area.WillCollide(p);
        public Position GetCoord() => area.Coord;
        public bool Overlap(IPlayer p) => area.Overlap(p);
    }
}
