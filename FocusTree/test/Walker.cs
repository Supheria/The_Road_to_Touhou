using LocalUtilities.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace test;

public enum Direction
{
    Left,
    Top,
    Right,
    Bottom,
    LeftTop,
    TopRight,
    LeftBottom,
    BottomRight,
}

public class Walker(int x, int y) : RosterItem<(int X, int Y)>((x, y))
{
    public static Rectangle MapBounds { get; set; } = new();

    public int X => Signature.X;

    public int Y => Signature.Y;

    public Dictionary<Direction, (int X, int Y)> Neighbor { get; } = new();

    public Dictionary<Direction, int> ConnetNumber { get; } = new();

    public int Height
    {
        get
        {
            if (_height is -1)
            {
                if (!ConnetNumber.TryGetValue(Direction.Top, out var top))
                    top = 0;
                if (!ConnetNumber.TryGetValue(Direction.Bottom, out var bottom))
                    bottom = 0;
                if (!ConnetNumber.TryGetValue(Direction.Left, out var left))
                    left = 0;
                if (!ConnetNumber.TryGetValue(Direction.Right, out var right))
                    right = 0;
                if (!ConnetNumber.TryGetValue(Direction.LeftTop, out var leftTop))
                    leftTop = 0;
                if (!ConnetNumber.TryGetValue(Direction.BottomRight, out var bottomRight))
                    bottomRight = 0;
                if (!ConnetNumber.TryGetValue(Direction.TopRight, out var topRight))
                    topRight = 0;
                if (!ConnetNumber.TryGetValue(Direction.LeftBottom, out var leftBottom))
                    leftBottom = 0;
                _height = Math.Max(
                    Math.Max(
                        top + bottom - Math.Abs(top - bottom),
                        left + right - Math.Abs(left - right)),
                    Math.Max(
                        leftTop + bottomRight - Math.Abs(leftTop - bottomRight),
                        topRight + leftBottom - Math.Abs(topRight - leftBottom))
                    );
            }
            return _height;
        }
        set => _height = value;
    }

    int _height = -1;

    public Walker() : this(
        new Random().Next(MapBounds.Left, MapBounds.Right + 1),
        new Random().Next(MapBounds.Top, MapBounds.Bottom + 1)
        )
    {

    }

    public bool CheckStuck(Dictionary<(int, int), Walker> stuckedPoint)
    {
        var left = Math.Max(X - 1, MapBounds.Left);
        var top = Math.Max(Y - 1, MapBounds.Top);
        var right = Math.Min(X + 1, MapBounds.Right);
        var bottom = Math.Min(Y + 1, MapBounds.Bottom);
        bool isStucked = false;
        if (stuckedPoint.ContainsKey((X, Y)))
            return false;
        if (stuckedPoint.TryGetValue((left, Y), out var walker))
        {
            Neighbor[Direction.Left] = (left, Y);
            walker.Neighbor[Direction.Right] = (X, Y);
            isStucked = true;
        }
        if (stuckedPoint.TryGetValue((right, Y), out walker))
        {
            Neighbor[Direction.Right] = (right, Y);
            walker.Neighbor[Direction.Left] = (X, Y);
            isStucked = true;
        }
        if (stuckedPoint.TryGetValue((X, top), out walker))
        {
            Neighbor[Direction.Top] = (X, top);
            walker.Neighbor[Direction.Bottom] = (X, Y);
            isStucked = true;
        }
        if (stuckedPoint.TryGetValue((X, bottom), out walker))
        {
            Neighbor[Direction.Bottom] = (X, bottom);
            walker.Neighbor[Direction.Top] = (X, Y);
            isStucked = true;
        }
        if (stuckedPoint.TryGetValue((left, top), out walker))
        {
            Neighbor[Direction.LeftTop] = (left, top);
            walker.Neighbor[Direction.BottomRight] = (X, Y);
            isStucked = true;
        }
        if (stuckedPoint.TryGetValue((left, bottom), out walker))
        {
            Neighbor[Direction.LeftBottom] = (left, bottom);
            walker.Neighbor[Direction.TopRight] = (X, Y);
            isStucked = true;
        }
        if (stuckedPoint.TryGetValue((right, top), out walker))
        {
            Neighbor[Direction.TopRight] = (right, top);
            walker.Neighbor[Direction.LeftBottom] = (X, Y);
            isStucked = true;
        }
        if (stuckedPoint.TryGetValue((right, bottom), out walker))
        {
            Neighbor[Direction.BottomRight] = (right, bottom);
            walker.Neighbor[Direction.LeftTop] = (X, Y);
            isStucked = true;
        }
        
        return isStucked;
    }

    public void Walk()
    {
        int x = X, y = Y;
        switch (new Random().Next(0, 8))
        {
            case 0: // left
                x--;
                break;
            case 1: // right
                x++;
                break;
            case 2: // up
                y--;
                break;
            case 3: // down
                y++;
                break;
            case 4: // left up
                x--;
                y--;
                break;
            case 5: // up right
                x++;
                y--;
                break;
            case 6: // bottom right
                x++;
                y++;
                break;
            case 7: // left bottom
                x--;
                y++;
                break;
        }
        if (x < MapBounds.Left || x > MapBounds.Right ||
            y < MapBounds.Top || y > MapBounds.Bottom)
        {
            x = new Random().Next(MapBounds.Left, MapBounds.Right + 1);
            y = new Random().Next(MapBounds.Top, MapBounds.Bottom + 1);
        }
        Signature = (x, y);
    }
}

