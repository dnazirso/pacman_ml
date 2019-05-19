using board_libs.Models;
using Game_UI.Tools;
using System.Threading.Tasks;
using System.Windows.Controls;
using utils_libs.Abstractions;

namespace Game_UI.Sprites
{
    /// <summary>
    /// Interaction logic for Dot.xaml
    /// </summary>
    public partial class Dot : UserControl, IBlock
    {
        Area Area { get; set; }
        public Dot(Area area)
        {
            InitializeComponent();
            this.Area = area;
            SetValue(Canvas.LeftProperty, (double)area.Min.Y);
            SetValue(Canvas.TopProperty, (double)area.Min.X);
            SetValue(Canvas.WidthProperty, (double)area.Size);
            SetValue(Canvas.HeightProperty, (double)area.Size);
        }
        public bool Collide(IPlayer p) => Area.Collide(p);
        public bool WillCollide(IPlayer p) => Area.WillCollide(p);
        public async void EreaseDot(IPacman p)
        {
            if (Area.EreaseDot(p) && dot.Children.Count > 0)
            {
                dot.Children.Remove(dott);
                await Task.Run(() => dot.Refresh());
            }
        }
    }
}
