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
        public Area Aria { get; set; }
        public Obstacle()
        {
            InitializeComponent();

            SetValue(Canvas.LeftProperty, (double)200);
            SetValue(Canvas.TopProperty, (double)100);
            SetValue(Canvas.WidthProperty, (double)20);
            SetValue(Canvas.HeightProperty, (double)20);

            RenderTransform = new TranslateTransform(20, 20);

            Aria = Area.SetPositions(new Models.Position { X = (int)Canvas.GetTop(this), Y = (int)Canvas.GetLeft(this) }, 20, 20);
        }

        public bool HasCollide(IPlayer p)
        {
            return Aria.HasCollide(p);
        }

        public bool WillCollide(IPlayer p)
        {
            return Aria.WillCollide(p);
        }
    }
}
