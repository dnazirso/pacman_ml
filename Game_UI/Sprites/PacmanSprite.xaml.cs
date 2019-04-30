using Game_UI.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game_UI.Sprites
{
    /// <summary>
    /// Interaction logic for Pacman.xaml
    /// </summary>
    public partial class PacmanSprite : UserControl
    {
        const string mouthOpened = "M 148.85938,169.78496 A 50,50 0 0 1 93.456152,184.63022 50,50 0 0 1 60.557159,137.64558 50,50 0 0 1 93.456152,90.660953 50,50 0 0 1 148.85938,105.5062 l -38.30222,32.13938 z";
        const string mouthClosed = "m 160.54954,138.5182 a 50,50 0 0 1 -50.64686,49.1231 50,50 0 0 1 -49.343617,-50.43204 50,50 0 0 1 50.216257,-49.5632 50,50 0 0 1 49.78184,49.99952 h -50 z";
        public PacmanSprite()
        {
            InitializeComponent();
            PacmanNominalAnimation();
        }

        async void PacmanNominalAnimation()
        {
            bool toggle = false;
            while (true)
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
                await Task.Run(() => pacBody.Refresh(200));
            }
        }
    }
}
