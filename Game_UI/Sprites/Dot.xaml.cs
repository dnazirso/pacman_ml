using board_libs;
using Game_UI.Models;
using pacman_libs;
using System;
using System.Windows.Controls;

namespace Game_UI.Sprites
{
    /// <summary>
    /// Interaction logic for Dot.xaml
    /// </summary>
    public partial class Dot : UserControl, IBlock
    {
        Area area { get; set; }
        Board Board { get; set; }

        public Dot(int top, int left, int size, bool isBlocking, Board board)
        {
            InitializeComponent();
            this.Board = board;
            SetArea(top, left, size, isBlocking);
        }
        private void SetArea(int top, int left, int size, bool isblocking)
        {
            SetValue(Canvas.LeftProperty, (double)left);
            SetValue(Canvas.TopProperty, (double)top);
            SetValue(Canvas.WidthProperty, (double)size);
            SetValue(Canvas.HeightProperty, (double)size);
            area = Area.SetPositions(new Models.Position { X = (int)Canvas.GetTop(this), Y = (int)Canvas.GetLeft(this) }, size, isblocking);
        }
        public bool HasCollide(IPlayer p) => area.HasCollide(p.Position);
        public bool WillCollide(IPlayer p)
        {
            if (HasCollide(p))
            {
                EreaseDot();
            };
            return area.WillCollide(p);
        }
        public void EreaseDot()
        {
            if (dot.Children.Count > 0)
            {
                dot.Children.Remove(dott);
                Board.DotsLeft--;
            }
        }
    }
}
