﻿using Game_UI.Tools;
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
        bool toggle = false;

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
            SetValue(Canvas.TopProperty, (double)player.Position.X);
            SetValue(Canvas.LeftProperty, (double)player.Position.Y);
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
            pacBody.RenderTransform = new RotateTransform(DirectionType.ToAngle(player.Direction), 10, 10);
            await Task.Run(() => pacBody.Refresh());
        }
    }
}
