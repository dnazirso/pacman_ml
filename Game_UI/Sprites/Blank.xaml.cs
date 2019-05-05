using Game_UI.Models;
using pacman_libs;
using System.Windows.Controls;
using System.Windows.Media;

namespace Game_UI.Sprites
{
    /// <summary>
    /// Interaction logic for Blank.xaml
    /// </summary>
    public partial class Blank : UserControl, IBlock
    {
        Area area { get; set; }
        public Blank(int top, int left, int size, bool isBlocking)
        {
            InitializeComponent();
            SetArea(top, left, size, isBlocking);
        }
        private void SetArea(int top, int left, int size, bool isblocking)
        {
            SetValue(Canvas.LeftProperty, (double)left);
            SetValue(Canvas.TopProperty, (double)top);
            SetValue(Canvas.WidthProperty, (double)size);
            SetValue(Canvas.HeightProperty, (double)size);
            area = Area.SetPositions(new Models.Position { X = (int)Canvas.GetTop(this), Y = (int)Canvas.GetLeft(this) }, size, size, isblocking);
        }
        public bool HasCollide(IPlayer p) => area.HasCollide(p);
        public bool WillCollide(IPlayer p) => area.WillCollide(p);
    }
}
