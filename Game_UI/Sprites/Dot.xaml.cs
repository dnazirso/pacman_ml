using board_libs;
using board_libs.Models;
using System.Windows.Controls;
using utils_libs.Abstractions;

namespace Game_UI.Sprites
{
    /// <summary>
    /// Interaction logic for Dot.xaml
    /// </summary>
    public partial class Dot : UserControl, IBlock
    {
        Area area { get; set; }
        public Dot(Area area)
        {
            InitializeComponent();
            this.area = area;
            SetValue(Canvas.LeftProperty, (double)area.Min.Y);
            SetValue(Canvas.TopProperty, (double)area.Min.X);
            SetValue(Canvas.WidthProperty, (double)area.Size);
            SetValue(Canvas.HeightProperty, (double)area.Size);
        }
        public bool Collide(IPlayer p) => area.Collide(p);
        public bool WillCollide(IPlayer p)
        {
            if (Collide(p))
            {
                EreaseDot();
            };
            return area.WillCollide(p);
        }
        public void EreaseDot()
        {
            if (dot.Children.Count > 0)
            {
                dot.Children.Remove(dott);
            }
        }
    }
}
