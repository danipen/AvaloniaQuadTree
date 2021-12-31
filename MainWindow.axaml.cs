using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace AvaloniaQuadTree
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            Content = new DrawingPanel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        class DrawingPanel : Panel
        {
            protected override Size ArrangeOverride(Size finalSize)
            {
                Size result = base.ArrangeOverride(finalSize);

                mQuadTree = new QuadTree(new Rect(0, 0, result.Width, result.Height), 4);

                for (int i = 0; i < 1000; i++)
                {
                    mQuadTree.Insert(new Point(
                        mRandom.Next(0, (int)result.Width),
                        mRandom.Next(0, (int)result.Height)));
                }

                mClipRectangle = new Rect(100, 100, 200, 100);

                return result;
            }

            protected override void OnPointerPressed(PointerPressedEventArgs e)
            {
                base.OnPointerPressed(e);

                /*mQuadTree.Insert(e.GetPosition(this));
                InvalidateVisual();*/
            }

            protected override void OnPointerMoved(PointerEventArgs e)
            {
                base.OnPointerMoved(e);

                PointerPoint p = e.GetCurrentPoint(this);

                if (!p.Properties.IsLeftButtonPressed)
                    return;

                mClipRectangle = new Rect(p.Position.X, p.Position.Y, mClipRectangle.Width, mClipRectangle.Height);

                /*for (int i = 0; i < 5; i++)
                {
                    mQuadTree.Insert(new Point(
                        p.Position.X + mRandom.Next(-25, 25),
                        p.Position.Y + mRandom.Next(-25, 25)));
                }*/

                InvalidateVisual();
            }

            public override void Render(DrawingContext context)
            {
                base.Render(context);

                context.FillRectangle(Brushes.Black, new Rect(0, 0, Bounds.Width, Bounds.Height));

                mQuadTree.Render(context);

                context.DrawRectangle(null, new Pen(Brushes.Red, 4), mClipRectangle);

                List<Point> points = mQuadTree.Query(mClipRectangle);

                foreach (Point p in points)
                    context.FillRectangle(Brushes.LimeGreen, new Rect(p.X - 2, p.Y - 2, 4, 4));
            }

            QuadTree mQuadTree;
            Random mRandom = new Random();
            Rect mClipRectangle;
        }
    }
}