using Game_UI.Tools;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using utils_libs.Abstractions;
using utils_libs.Tools;

namespace Game_UI.Sprites
{
    /// <summary>
    /// Interaction logic for GhostSprite.xaml
    /// </summary>
    public partial class GhostSprite : UserControl, IUIPlayer
    {
        public IPosition LastPosition { get; set; }
        public IPlayer Player { get; }
        public GhostSprite(IPlayer Player)
        {
            this.Player = Player;

            SetValue(WidthProperty, (double)40);
            SetValue(HeightProperty, (double)40);

            InitializeComponent();

            RenderTransform = new TranslateTransform(-20, -20);
        }

        public void UpdatePosition()
        {
            LastPosition = new Position { X = Player.Position.X, Y = Player.Position.Y };
            //ghostBody.RenderTransform = new RotateTransform(DirectionType.ToAngle(Player.Direction), 10, 10);
            SetValue(Canvas.TopProperty, (double)Player.Position.X);
            SetValue(Canvas.LeftProperty, (double)Player.Position.Y);

            NominalAnimation();
        }

        public async void NominalAnimation()
        {
            await Task.Run(() => ghostBody.Refresh());
        }
    }
}

