namespace HaydeeLevelGen.Structure.Walls;

/// <summary>
/// Describes a door within a wall.
/// Doors can either be full-sized (standing) or crouch-sized.
/// </summary>
internal class Door {
    internal const int Width = 2;
    internal const int HeightFull = 4;
    internal const int HeightCrouch = 2;

    private readonly string _targetScene;
    private readonly string _targetEntryObject;

    private readonly bool _crouch;

    internal int Position { get; }
    internal int WallId { get; }


    internal Door(string descriptor) {
        string[] rawData = descriptor.Split("/");
        if(rawData.Length != 5)
            throw new ArgumentException(descriptor);

        WallId = Convert.ToInt32(rawData[0]);
        Position = Convert.ToInt32(rawData[1]);
        _crouch = rawData[2].ToLower().Equals("c");
        _targetScene = rawData[3];
        _targetEntryObject = rawData[4];
    }

    private Door(string targetScene, string targetEntryObject, bool crouch, int position, int wallId) {
        _targetScene = targetScene;
        _targetEntryObject = targetEntryObject;
        _crouch = crouch;
        Position = position;
        WallId = wallId;
    }

    internal int GetHeight() {
        return _crouch ? HeightCrouch : HeightFull;
    }

    internal Door CopyWithNewPosition(int position) {
        return new Door(_targetScene, _targetEntryObject, _crouch, position, WallId);
    }
    
}