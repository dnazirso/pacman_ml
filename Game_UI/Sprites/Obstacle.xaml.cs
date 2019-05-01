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
        public Obstacle()
        {
            InitializeComponent();

            SetValue(Canvas.LeftProperty, (double)20);
            SetValue(Canvas.TopProperty, (double)20);
            SetValue(Canvas.WidthProperty, (double)20);
            SetValue(Canvas.HeightProperty, (double)20);

            RenderTransform = new TranslateTransform(-10, -10);
        }
    }
}
