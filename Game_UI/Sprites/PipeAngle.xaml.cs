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
    /// Interaction logic for PipeAngle.xaml
    /// </summary>
    public partial class PipeAngle : UserControl, IBlock
    {
        Area area { get; set; }
        public PipeAngle(int top, int left, int size, bool isBlocking, int angle)
        {
            InitializeComponent();
            SetArea(top, left, size, isBlocking);
            pipe_obstacle.RenderTransform = new RotateTransform(angle, 10, 10);
        }
        private void SetArea(int top, int left, int size, bool isblocking)
        {
            SetValue(Canvas.LeftProperty, (double)left);
            SetValue(Canvas.TopProperty, (double)top);
            SetValue(Canvas.WidthProperty, (double)size);
            SetValue(Canvas.HeightProperty, (double)size);
            area = Area.SetPositions(new Models.Position { X = (int)Canvas.GetTop(this), Y = (int)Canvas.GetLeft(this) }, size, isblocking);
        }
        public bool HasCollide(IPlayer p) => area.HasCollide(p);
        public bool WillCollide(IPlayer p) => area.WillCollide(p);
    }
}
