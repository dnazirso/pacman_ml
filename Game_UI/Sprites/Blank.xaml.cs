using board_libs.Models;
using System.Windows.Controls;
using utils_libs.Abstractions;

namespace Game_UI.Sprites
{
    /// <summary>
    /// Interaction logic for Blank.xaml
    /// </summary>
    public partial class Blank : UserControl, IBlock
    {
        Area area { get; set; }
        public Blank(Area area)
        {
            InitializeComponent();
            this.area = area;
            SetValue(Canvas.LeftProperty, (double)area.Min.Y);
            SetValue(Canvas.TopProperty, (double)area.Min.X);
            SetValue(Canvas.WidthProperty, (double)area.Size);
            SetValue(Canvas.HeightProperty, (double)area.Size);
        }
        public bool Collide(IPlayer p) => area.Collide(p);
        public bool WillCollide(IPlayer p) => area.WillCollide(p);
        public IPosition GetCoord() => area.Coord;
    }
}
