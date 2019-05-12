using Game_UI.Tools;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using utils_libs.Abstractions;
using utils_libs.Tools;

namespace Game_UI.Sprites
{
    /// <summary>
    /// Interaction logic for Pacman.xaml
    /// </summary>
    public partial class PacmanSprite : UserControl
    {
        bool _toggle;
        int _tickMoveCounter;
        public IPosition lastPosition { get; private set; }

        IPlayer player { get; }
        public PacmanSprite(IPlayer player)
        {
            this.player = player;
            SetValue(WidthProperty, (double)40);
            SetValue(HeightProperty, (double)40);

            InitializeComponent();

            RenderTransform = new TranslateTransform(-20, -20);
        }

        public void UpdatePosition()
        {
            lastPosition = new Position { X = player.Position.X, Y = player.Position.Y };
            pacBody.RenderTransform = new RotateTransform(DirectionType.ToAngle(player.Direction), 10, 10);
            SetValue(Canvas.TopProperty, (double)player.Position.X);
            SetValue(Canvas.LeftProperty, (double)player.Position.Y);

            _tickMoveCounter++;
            if (_tickMoveCounter >= 20)
            {
                NominalAnimation();
                _tickMoveCounter = 0;
            }
        }

        public async void NominalAnimation()
        {
            if (_toggle)
            {
                pacMouth.SetValue(GeometryDrawing.GeometryProperty, Geometry.Parse(Properties.Resources.mouthOpen));
            }
            else
            {
                pacMouth.SetValue(GeometryDrawing.GeometryProperty, Geometry.Parse(Properties.Resources.mouthClose));
            }
            _toggle = !_toggle;
            await Task.Run(() => pacBody.Refresh());
        }
    }
}
