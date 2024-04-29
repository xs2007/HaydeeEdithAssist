using HaydeeLevelGen.Structure.Primitives;
using NLog;
using static HaydeeLevelGen.Structure.Walls.WallCornerType;
using static HaydeeLevelGen.Structure.Walls.WallOrientation;
using static HaydeeLevelGen.Structure.Primitives.Coords;

namespace HaydeeLevelGen.Structure.Walls;

/// <summary>
/// Represents a piece of wall within the scene definition.
/// Walls have an origin, a length and an orientation (front, back, right, left).
/// In addition to their relative position seen from the last wall, methods are available for the global (scene-wide)
/// location of certain points of interest within the wall.
/// Global positions are identical for left/right or front/back walls respectively,
/// while normal (local) positions are not.
/// </summary>
internal class Wall {
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    private readonly Coords _pos;
    private readonly Coords _globalPos;
    private readonly int _length;

    private readonly WallOrientation _orientation;

    private readonly WallCornerType _originCornerType;
    private readonly WallCornerType _upcomingCornerType;

    private readonly List<Door> _doors;
    private readonly List<Door> _doorsGlobalPos;

    private Wall(Coords pos, int length, WallOrientation orientation, WallCornerType originCornerType,
        WallCornerType upcomingCornerType, List<Door> doors) {
        _pos = pos;
        _length = length;
        _orientation = orientation;
        _originCornerType = originCornerType;
        _upcomingCornerType = upcomingCornerType;
        _doors = doors;

        _globalPos = _orientation switch {
            Front => _pos - Z(length),
            Left => _pos - X(length),
            _ => _pos
        };

        _doorsGlobalPos =
            _doors.Select(
                d => d.CopyWithNewPosition(GetCornerAdjustedWallLength() - d.Position - Door.Width)).ToList();
    }
    
    /// <summary>
    /// Creates a list of walls from the given wall and door descriptors.
    /// </summary>
    /// <param name="wallDescriptor">Space separated integers to describe the wall lengths
    /// and by that a counter-clockwise path on the outline of the room layout.
    /// The integers describe wall lengths alternating between horizontal and vertical, starting with horizontal.
    /// Positive values describe walls expanding to the south/right, while positive ones indicate north/left.
    /// Examples: "20 20 -20 -20" describes a 20x20 room, "10 20 10 10 -20 -30" describes an L-shaped room.</param>
    /// <param name="doorDescriptor">Space separated list of door descriptors. A door descriptor has the following form:
    /// <c>&lt;wall-id&gt;/&lt;position-in-wall&gt;/&lt;[S|C] for size&gt;/&lt;target-scene&gt;/&lt;target-entry&gt;</c>
    /// Example: "0/5/S/W_Start/E_Climb" describes an upright (standing size) door in the first wall,
    /// after 5 tiles within the wall, leading to scene W_Start and spawning at entry E_Climb in W_Start. </param>
    internal static List<Wall> GenerateWallsFromDescriptors(string wallDescriptor, string doorDescriptor) {
        Log.Info("Generating wall information from outline descriptor...");
        
        List<Wall> result = [];
        
        string[] wallDescriptors = wallDescriptor.Split(" ");
        List<int> lengths = wallDescriptors.Select(d => Convert.ToInt32(d)).ToList();
        
        List<Door> doorData = [];
        if (doorDescriptor != "") {
            string[] doorDescriptors = doorDescriptor.Split(" ");
            doorData.AddRange(doorDescriptors.Select(ds => new Door(ds)));
        }

        Coords currentPos = new Coords(0, 0);

        WallCornerType lastCornerType = GetCornerTypeXToZ(lengths.First(), lengths.Last());
        
        for (int i = 0; i < lengths.Count; i++) {
            Coords origin = currentPos;
            WallCornerType upcomingWallCornerType;

            int newLength = lengths[i];
            int nextLength = lengths[(i + 1) % lengths.Count];

            WallOrientation orientation;
            if (i % 2 == 1) { // X UP/DOWN
                currentPos += X(newLength);
                upcomingWallCornerType = (GetCornerTypeXToZ(newLength, nextLength));
                orientation = newLength > 0 ? Right : Left;
            } else { // Z LEFT/RIGHT
                currentPos += Z(newLength);
                upcomingWallCornerType = (GetCornerTypeZToX(newLength, nextLength));
                orientation = newLength > 0 ? Back : Front;
            }
            
            List<Door> doorsForWall = doorData.Where(d => d.WallId == i).ToList();

            result.Add(new Wall(origin,
                Math.Abs(newLength),
                orientation,
                lastCornerType,
                upcomingWallCornerType,
                doorsForWall));

            lastCornerType = upcomingWallCornerType;
        }
        
        
        return result;
    }
    
    private int GetCornerAdjustedWallLength() {
        int result = _length;
        if (GetGlobalLastCornerType() == Inner) {
            result -= 1;
        }
        if(GetGlobalUpcomingCornerType() == Inner) {
            result -= 1;
        }
        return result;
    }

    private WallCornerType GetGlobalLastCornerType() => _orientation switch {
        Front or Left => _upcomingCornerType,
        Back or Right => _originCornerType,
        _ => _originCornerType
    };

    private WallCornerType GetGlobalUpcomingCornerType() => _orientation switch {
        Front or Left => _originCornerType,
        Back or Right => _upcomingCornerType,
        _ => _upcomingCornerType
    };

    private static WallCornerType GetCornerTypeXToZ(int x, int z) => x switch {
        > 0 when z < 0 => Outer,
        > 0 when z > 0 => Inner,
        < 0 when z < 0 => Inner,
        _ => Outer
    };

    private static WallCornerType GetCornerTypeZToX(int z, int x) => z switch {
        > 0 when x < 0 => Inner,
        > 0 when x > 0 => Outer,
        < 0 when x < 0 => Outer,
        _ => Inner
    };
}