using HaydeeLevelGen.Structure.Primitives;

namespace HaydeeLevelGen.Structure.Walls;

/// <summary>
/// Used to describe the orientation of the wall in respect to the global scene.
/// </summary>
internal enum WallOrientation {
    Front, Back, Left, Right
}

internal static class WallOrientationExtensions {
    internal static int SizeFront(this WallOrientation orientation) {
        return orientation == WallOrientation.Front ? 1 : 0;
    }
    
    internal static int SizeBack(this WallOrientation orientation) {
        return orientation == WallOrientation.Back ? 1 : 0;
    }
    
    internal static int SizeLeft(this WallOrientation orientation) {
        return orientation == WallOrientation.Left ? 1 : 0;
    }
    
    internal static int SizeRight(this WallOrientation orientation) {
        return orientation == WallOrientation.Right ? 1 : 0;
    }

    /// <summary>
    /// Returns the orientation orthogonal (90° angled) in respect to the wall orientation.
    /// It can be used to calculate wall thickness.
    /// </summary>
    internal static Coords OrthogonalExtrudingDirection(this WallOrientation orientation) {
        switch (orientation) {
            case WallOrientation.Front: return new Coords(1, 0);
            case WallOrientation.Back: return new Coords(-1, 0);
            case WallOrientation.Left: return new Coords(0, -1);
            case WallOrientation.Right: default: return new Coords(0, 1);
        }
    }

    /// <summary>
    /// Returns the global direction along the wall. Either (0,1) for horizontal walls, or (1,0) for vertical ones.
    /// </summary>
    internal static Coords GlobalDirection(this WallOrientation orientation) {
        switch (orientation) {
            case WallOrientation.Front: case WallOrientation.Back:
                return new Coords(0, 1);
            case WallOrientation.Left: case WallOrientation.Right: default:
                return new Coords(1, 0);
        }
    }
}