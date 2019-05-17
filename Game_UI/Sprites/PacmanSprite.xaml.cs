using Game_UI.Tools;
using System.Collections.Generic;
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
    public partial class PacmanSprite : UserControl, IUIPLayer
    {
        bool _toggle;
        int _tickMoveCounter;
        readonly List<Dot> dots;
        public IPosition LastPosition { get; set; }
        public IPlayer Player { get; }
        public PacmanSprite(IPlayer Player, List<Dot> dots)
        {
            this.Player = Player;
            this.dots = dots;

            SetValue(WidthProperty, (double)40);
            SetValue(HeightProperty, (double)40);

            InitializeComponent();

            RenderTransform = new TranslateTransform(-20, -20);
        }

        public void UpdatePosition()
        {
            LastPosition = new Position { X = Player.Position.X, Y = Player.Position.Y };
            pacBody.RenderTransform = new RotateTransform(DirectionType.ToAngle(Player.Direction), 15, 15);
            SetValue(Canvas.TopProperty, (double)Player.Position.X);
            SetValue(Canvas.LeftProperty, (double)Player.Position.Y);

            NominalAnimation();
            EreaseDots();
        }

        private void EreaseDots() => dots.ForEach(x => x.EreaseDot(Player));

        public async void NominalAnimation()
        {
            _tickMoveCounter++;
            if (_tickMoveCounter >= 5)
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
                _tickMoveCounter = 0;
                await Task.Run(() => pacBody.Refresh());
            }
        }
    }
}
