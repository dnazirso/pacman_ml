using Game_UI.Models;
using pacman_libs;
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
            //ObstacleText.Text = $"UL : X {Aria.GetPosition("UL").X}, Y : {Aria.GetPosition("UL").Y} | BR : X {Aria.GetPosition("BR").X}, Y : {Aria.GetPosition("BR").Y}";
        }

        public bool HasCollide(IPlayer p)
        {
            return Aria.HasCollide(p);
        }
    }
}
