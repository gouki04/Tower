using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Tower
{
    [ComVisible(true)]
    [Serializable]
    public struct MapPos
    {
        public int Row;
        public int Col;

        public MapPos(int row, int col)
        {
            Row = row;
            Col = col;
        }

        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                if (this.Row == 0)
                    return this.Col == 0;
                else
                    return false;
            }
        }

        public static bool operator ==(MapPos left, MapPos right)
        {
            if (left.Row == right.Row)
                return left.Col == right.Col;
            else
                return false;
        }

        public static bool operator !=(MapPos left, MapPos right)
        {
            return !(left == right);
        }

        public static MapPos operator -(MapPos left, MapPos right)
        {
            return new MapPos(left.Row - right.Row, left.Col - right.Col);
        }

        public static MapPos operator +(MapPos left, MapPos right)
        {
            return new MapPos(left.Row + right.Row, left.Col + right.Col);
        }

        public static MapPos operator +(MapPos left, int right)
        {
            return new MapPos(left.Row + right, left.Col + right);
        }

        public static MapPos operator -(MapPos left, int right)
        {
            return new MapPos(left.Row - right, left.Col - right);
        }

        public static MapPos operator /(MapPos left, int right)
        {
            return new MapPos(left.Row / right, left.Col / right);
        }

        public static MapPos operator *(MapPos left, int right)
        {
            return new MapPos(left.Row * right, left.Col * right);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MapPos))
                return false;
            var point = (MapPos)obj;
            if (point.Row == this.Row)
                return point.Col == this.Col;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return this.Row ^ this.Col;
        }

        public override string ToString()
        {
            return "{Row=" + this.Row.ToString((IFormatProvider)CultureInfo.CurrentCulture) + ",Col=" +
                   this.Col.ToString((IFormatProvider)CultureInfo.CurrentCulture) + "}";
        }
    }

    public struct MapRegion
    {
        public MapPos LB { get; set; }
        public MapPos RT { get; set; }

        public MapRegion(MapPos pos)
        {
            LB = RT = pos;
        }

        public MapRegion(MapPos lb, MapPos rt)
        {
            LB = lb;
            RT = rt;
        }

        public MapPos Pos
        {
            get
            {
                return LB;
            }
        }

        public bool IsPos
        {
            get
            {
                return LB == RT;
            }
        }

        public bool IsRegion
        {
            get
            {
                return LB != RT;
            }
        }

        public static bool operator ==(MapRegion left, MapRegion right)
        {
            if (left.LB == right.LB)
                return left.RT == right.RT;
            else
                return false;
        }

        public static bool operator !=(MapRegion left, MapRegion right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MapRegion))
                return false;
            var point = (MapRegion)obj;
            if (point.LB == this.LB)
                return point.RT == this.RT;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return this.LB.GetHashCode() ^ this.RT.GetHashCode();
        }

        public override string ToString()
        {
            if (IsPos)
            {
                return Pos.ToString();
            }
            else
            {
                return "{LB=" + this.LB.ToString() + ",RT=" + this.RT.ToString() + "}";
            }
        }
    }

    public class Object
    {
        public MapRegion Region { get; set; }
        public bool IsCollide { get; set; }
    }
}
