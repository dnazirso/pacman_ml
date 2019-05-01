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
        const string mouthOpened = "M 148.85938,169.78496 A 50,50 0 0 1 93.456152,184.63022 50,50 0 0 1 60.557159,137.64558 50,50 0 0 1 93.456152,90.660953 50,50 0 0 1 148.85938,105.5062 l -38.30222,32.13938 z";
        const string mouthClosed = "m 160.54954,138.5182 a 50,50 0 0 1 -50.64686,49.1231 50,50 0 0 1 -49.343617,-50.43204 50,50 0 0 1 50.216257,-49.5632 50,50 0 0 1 49.78184,49.99952 h -50 z";
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
                pacMouth.SetValue(GeometryDrawing.GeometryProperty, Geometry.Parse(mouthOpened));
            }
            else
            {
                pacMouth.SetValue(GeometryDrawing.GeometryProperty, Geometry.Parse(mouthClosed));
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
