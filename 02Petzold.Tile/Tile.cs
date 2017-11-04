using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _02Petzold.Tile 
{
    public class Tile : Canvas
    {
        const int SIZE = 64; // 2/3 inch
        const int BORD = 6;  // 1/16 inch
        private TextBlock txtBlck;
        public Tile()
        {
            Width = SIZE;
            Height = SIZE;

            // Border on left and right sides
            Polygon poly = new Polygon();
            poly.Points = new PointCollection(new Point[]
                 {
                     new Point(0, 0), new Point(SIZE, 0),
                     new Point(SIZE-BORD, BORD),
                     new Point(BORD, BORD),
                     new Point(BORD, SIZE-BORD), new Point(0, SIZE) 
                                                      
                 });
            poly.Fill = SystemColors.ControlLightLightBrush;
            Children.Add(poly);

            // Field for text
            Border bord = new Border();
            bord.Width = SIZE - 2*BORD;
            bord.Height = SIZE - 2*BORD;
            bord.Background = SystemColors.ControlBrush;
            Children.Add(bord);
            SetLeft(bord, BORD);
            SetTop(bord, BORD);

            // Taping text
            txtBlck = new TextBlock();
            txtBlck.FontSize = 32;
            txtBlck.Foreground = SystemColors.ControlTextBrush;
            txtBlck.HorizontalAlignment = HorizontalAlignment.Center;
            txtBlck.VerticalAlignment = VerticalAlignment.Center;
            bord.Child = txtBlck;

        }

        // Opened property for text setting
        public string Text
        {
            set { txtBlck.Text = value; }
            get { return txtBlck.Text; }
        }

    }

    class Empty: System.Windows.FrameworkElement
    {
        
    }

    public class Play : Window
    {
        const int NumberRows = 4;
        const int NumberCols = 4;

        UniformGrid unigrid;
        int xEmpty, yEmpty, iCounter;
        Key[] keys = {Key.Left, Key.Right, Key.Up, Key.Down};
        Random rand;
        UIElement elEmptySpare = new Empty();

        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new Play());

        }

        public Play()
        {
            Title = "Jeu de Tacquin";

            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            Background = SystemColors.ControlBrush;

            // Creating StackPanel obj as window's content
            StackPanel stack = new StackPanel();
            Content = stack;

            // Creatin button in top side of window
            Button btn = new Button();
            btn.Content = "_Scramble";
            btn.Margin = new Thickness(10);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Click += ScrambleOnClick;
            stack.Children.Add(btn);

            // Creating obj Border for decor
            Border bord = new Border();
            bord.BorderBrush = SystemColors.ControlDarkDarkBrush;
            bord.BorderThickness = new Thickness(1);
            stack.Children.Add(bord);

            // Creatin UniformGrid obj as children of Border
            unigrid = new UniformGrid();
            unigrid.Rows = NumberRows;
            unigrid.Columns = NumberCols;
            bord.Child = unigrid;

            // Creating Tile objects 
            for (int i = 0; i < NumberRows*NumberCols -1; i++)
            {
                Tile tile = new Tile();
                tile.Text = (i + 1).ToString();
                tile.MouseLeftButtonDown += TileOnMouseLeftButtonDown;
                unigrid.Children.Add(tile);
            }

            // Creating Empty obj for last field
            unigrid.Children.Add(new Empty());
            xEmpty = NumberCols - 1;
            yEmpty = NumberRows - 1;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.Key)
            {
                case Key.Right: MoveTile(xEmpty -1, yEmpty);
                    break;
                case Key.Left: MoveTile(xEmpty+1, yEmpty);
                    break;
                case Key.Down: MoveTile(xEmpty, yEmpty - 1);
                    break;
                case Key.Up:MoveTile(xEmpty, yEmpty +1);
                    break;
            }
        }

        private void TileOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tile tile = sender as Tile;
            int iMove = unigrid.Children.IndexOf(tile);
            int xMove = iMove % NumberCols;
            int yMove = iMove / NumberCols;

            if (xMove == xEmpty)
                while (yMove != yEmpty)
                {
                    MoveTile(xMove, yEmpty + (yMove - yEmpty)/
                                    Math.Abs(yMove - yEmpty));
                }
            if (yMove == yEmpty)
                while (xMove != xEmpty)
                {
                    MoveTile(xEmpty + (xMove - xEmpty) /
                        Math.Abs(xMove - xEmpty), yMove);
                }
            

        }

        void MoveTile(int xTile, int yTile)
        {
            if ((xTile == xEmpty && yTile == yEmpty) ||
                xTile < 0 || xTile >= NumberCols ||
                yTile < 0 || yTile >= NumberRows)
                return;
            int iTile = NumberCols*yTile + xTile;
            int iEmpty = NumberCols*yEmpty + xEmpty;

            UIElement elTile = unigrid.Children[iTile];
            UIElement elEmpty = unigrid.Children[iEmpty];
            unigrid.Children.RemoveAt(iTile);
            unigrid.Children.Insert(iTile, elEmptySpare);
            unigrid.Children.RemoveAt(iEmpty);
            unigrid.Children.Insert(iEmpty, elTile);

            xEmpty = xTile;
            yEmpty = yTile;
            elEmptySpare = elEmpty;
        }

        void ScrambleOnClick(object sender, RoutedEventArgs e)
        {
            rand = new Random();
            iCounter = 16*NumberCols*NumberRows;
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromMilliseconds(10);
            tmr.Tick += TimerOnTick;
            tmr.Start();
        }

        void TimerOnTick(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                MoveTile(xEmpty, yEmpty + rand.Next(3)-1);
                MoveTile(xEmpty + rand.Next(3)-1, yEmpty);
            }
            if (0==iCounter--)(sender as DispatcherTimer).Stop();
        }
    }
}
