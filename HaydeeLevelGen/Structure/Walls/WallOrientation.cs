using HaydeeLevelGen.Structure.Primitives;
using static HaydeeLevelGen.Structure.Walls.WallOrientation;

namespace HaydeeLevelGen.Structure.Walls;

/// <summary>
/// Used to describe the orientation of the wall in respect to the global scene.
/// </summary>
internal enum WallOrientation {
    Front, Back, Left, Right
}

internal static class WallOrientationExtensions {
    
    internal static int SizeFront(this WallOrientation orientation) {
        return orientation == Front ? 1 : 0;
    }
    
    internal static int SizeBack(this WallOrientation orientation) {
        return orientation == Back ? 1 : 0;
    }
    
    internal static int SizeLeft(this WallOrientation orientation) {
        return orientation == Left ? 1 : 0;
    }
    
    internal static int SizeRight(this WallOrientation orientation) {
        return orientation == Right ? 1 : 0;
    }

    /// <summary>
    /// Returns the orientation orthogonal (90° angled) in respect to the wall orientation.
    /// It can be used to calculate wall thickness.
    /// </summary>
    internal static Coords OrthogonalExtrudingDirection(this WallOrientation orientation) => orientation switch {
        Front => new Coords(1, 0),
        Back => new Coords(-1, 0),
        Left => new Coords(0, -1),
        _ => new Coords(0, 1)
    };

    /// <summary>
    /// Returns the global direction along the wall. Either (0,1) for horizontal walls, or (1,0) for vertical ones.
    /// </summary>
    internal static Coords GlobalDirection(this WallOrientation orientation) => orientation switch {
        Front or Back => new Coords(0, 1),
        _ => new Coords(1, 0)
    };
}