

using System;
using UnityEngine;

public struct GridPosition : IEquatable<GridPosition>
{
    public int x;
    public int z;

    public GridPosition(int x, int z)
    {
        this.x = x;
        this.z = z;
        
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return "x : " + x + " z : " + z;
    }

    public static bool operator ==( GridPosition a , GridPosition b)
    {
        return a.x == b.x && a.z == b.z;
    }

    public static bool operator != (GridPosition a, GridPosition b)
    {
        return !(a == b);
    }

}