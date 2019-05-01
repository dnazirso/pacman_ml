using Game_UI.Tools;
using pacman_libs;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Game_UI.Sprites
{
    /// <summary>
    /// Interaction logic for Pacman.xaml
    /// </summary>
    public partial class PacmanSprite : UserControl
    {
        bool toggle = false;

        IPlayer player { get; }
        public PacmanSprite(IPlayer player)
        {
            this.player = player;
            SetValue(WidthProperty, (double)40);
            SetValue(HeightProperty, (double)40);
            SetValue(Canvas.LeftProperty, (double)0);
            SetValue(Canvas.TopProperty, (double)0);

            InitializeComponent();
            NominalAnimation();
        }

        public async void NominalAnimation()
        {
            if (toggle)
            {
                pacMouth.SetValue(GeometryDrawing.GeometryProperty, Geometry.Parse(Properties.Resources.mouthOpen));
            }
            else
            {
                pacMouth.SetValue(GeometryDrawing.GeometryProperty, Geometry.Parse(Properties.Resources.mouthClose));
            }
            toggle = !toggle;
            await Task.Run(() => pacBody.Refresh());
        }

        public async void rotate()
        {
            int dir = 0;
            switch (DirectionType.ToKey(player.Direction))
            {
                case Key.Left:
                    dir = 180;
                    break;
                case Key.Up:
                    dir = 270;
                    break;
                case Key.Right:
                    dir = 0;
                    break;
                case Key.Down:
                    dir = 90;
                    break;
                default:
                    break;
            }
            pacBody.RenderTransform = new RotateTransform(dir, 10, 10);
            await Task.Run(() => pacBody.Refresh());
        }
    }
}
