namespace HaydeeLevelGen.Structure.Primitives;

/// <summary>
/// Describes an integer coordinate in 3-dimensional space.
/// </summary>
internal record Coords(int ValX, int ValY, int ValZ) {
    /// <summary>
    /// Creates a new X/Z Coords object with y=0.
    /// </summary>
    public Coords(int ValX, int ValZ) : this(ValX, 0, ValZ) { }

    /// <summary>
    /// Adds the values of the two coordinates
    /// and returns a new Coords object with the respective sums in each dimension.
    /// </summary>
    public static Coords operator +(Coords first, Coords second) {
        return new Coords(first.ValX + second.ValX, first.ValY + second.ValY, first.ValZ + second.ValZ);
    }

    /// <summary>
    /// Scales all dimensions uniformly with the given fixed scale.
    /// </summary>
    public static Coords operator *(Coords coords, int scale) {
        return new Coords(coords.ValX * scale, coords.ValY * scale, coords.ValZ * scale);
    }

    /// <summary>
    /// Returns a new Coords object with the given X value and y=z=0.
    /// </summary>
    public static Coords X(int x) {
        return new Coords(x, 0, 0);
    }

    /// <summary>
    /// Returns a new Coords object with the given Y value and x=z=0.
    /// </summary>
    public static Coords Y(int y) {
        return new Coords(0, y, 0);
    }

    /// <summary>
    /// Returns a new Coords object with the given Z value and x=y=0.
    /// </summary>
    public static Coords Z(int z) {
        return new Coords(0, 0, z);
    }
}