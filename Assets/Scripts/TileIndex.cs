using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tower
{
    public struct TileIndex : IEquatable<TileIndex>
    {
        //
        // Summary:
        //     Represents an invalid tile index.
        public static readonly TileIndex invalid = new TileIndex(-1, -1);

        //
        // Summary:
        //     Index of first tile in system.
        public static readonly TileIndex zero = new TileIndex(0, 0);
        
        //
        // Summary:
        //     Zero-based column index.
        public int Column;

        //
        // Summary:
        //     Zero-based row index.
        public int Row;

        public TileIndex(int r, int c)
        {
            Row = r;
            Column = c;
        }

        public override bool Equals(object obj)
        {
            if (obj is TileIndex) {
                var other = (TileIndex)obj;
                return other.Row == Row && other.Column == Column;
            } else {
                return false;
            }
        }

        public bool Equals(TileIndex other)
        {
            return other.Row == Row && other.Column == Column;
        }

        public override int GetHashCode()
        {
            return Row * 1000 + Column;
        }

        public override string ToString()
        {
            return string.Format("(row = {0}, column = {1})", Row, Column);
        }

        public static TileIndex operator +(TileIndex lhs, TileIndex rhs)
        {
            return new TileIndex(lhs.Row + rhs.Row, lhs.Column + rhs.Column);
        }

        public static TileIndex operator -(TileIndex value)
        {
            return new TileIndex(-value.Row, -value.Column);
        }

        public static TileIndex operator -(TileIndex lhs, TileIndex rhs)
        {
            return new TileIndex(lhs.Row - rhs.Row, lhs.Column - rhs.Column);
        }

        public static bool operator ==(TileIndex lhs, TileIndex rhs)
        {
            return lhs.Row == rhs.Row && lhs.Column == rhs.Column;
        }

        public static bool operator !=(TileIndex lhs, TileIndex rhs)
        {
            return lhs.Row != rhs.Row || lhs.Column != rhs.Column;
        }
    }
}
