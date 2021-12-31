using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;

namespace AvaloniaQuadTree
{
    public class QuadTree
    {
        public QuadTree(Rect bounds, int capacity)
        {
            mBounds = bounds;
            mCapacity = capacity;
            mPoints = new List<Point>();
            mBrush = new SolidColorBrush(Colors.White);
            /*new SolidColorBrush(Color.FromRgb(
                (byte)mRandom.Next(0, 255),
                (byte)mRandom.Next(0, 255),
                (byte)mRandom.Next(0, 255)));*/
        }

        public bool Insert(Point point)
        {
            if (!mBounds.Contains(point))
                return false;

            if (mPoints.Count < mCapacity - 1)
            {
                mPoints.Add(point);
                return true;
            }

            if (!mIsDivided)
            {
                Subdivide();
            }

            if (mNorthEast.Insert(point))
                return true;

            if (mNorthWest.Insert(point))
                return true;

            if (mSouthEast.Insert(point))
                return true;

            if (mSouthWest.Insert(point))
                return true;

            return false;
        }

        public List<Point> Query(Rect range)
        {
            List<Point> result = new List<Point>();

            if (!mBounds.Intersects(range))
                return result;

            foreach (Point p in mPoints)
            {
                if (range.Contains(p))
                    result.Add(p);
            }

            if (mIsDivided)
            {
                result.AddRange(mNorthEast.Query(range));
                result.AddRange(mNorthWest.Query(range));
                result.AddRange(mSouthEast.Query(range));
                result.AddRange(mSouthWest.Query(range));
            }

            return result;
        }

        void Subdivide()
        {
            Rect nw = new Rect(mBounds.X, mBounds.Y, mBounds.Width / 2, mBounds.Height / 2);
            Rect ne = new Rect(mBounds.X + mBounds.Width / 2, mBounds.Y, mBounds.Width / 2, mBounds.Height / 2);
            Rect sw = new Rect(mBounds.X, mBounds.Y + mBounds.Height / 2, mBounds.Width / 2, mBounds.Height / 2);
            Rect se = new Rect(mBounds.X + mBounds.Width / 2, mBounds.Y + mBounds.Height / 2, mBounds.Width / 2, mBounds.Height / 2);

            mNorthWest = new QuadTree(nw, mCapacity);
            mNorthEast = new QuadTree(ne, mCapacity);
            mSouthWest = new QuadTree(sw, mCapacity);
            mSouthEast = new QuadTree(se, mCapacity);

            mIsDivided = true;
        }

        internal void Render(DrawingContext context)
        {
            context.DrawRectangle(null, new Pen(mBrush, 0.5), mBounds);

            if (mIsDivided)
            {
                mNorthWest.Render(context);
                mNorthEast.Render(context);
                mSouthWest.Render(context);
                mSouthEast.Render(context);
            }

            foreach (Point p in mPoints)
            {
                context.FillRectangle(mBrush, new Rect(p.X - 2, p.Y - 2, 4, 4));
            }
        }

        Rect mBounds;
        int mCapacity;
        List<Point> mPoints = new List<Point>();
        bool mIsDivided;
        QuadTree mNorthWest;
        QuadTree mNorthEast;
        QuadTree mSouthWest;
        QuadTree mSouthEast;
        Brush mBrush;
        static Random mRandom = new Random();
    }
}