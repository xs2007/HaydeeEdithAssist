namespace HaydeeLevelGen.Structure;

/// <summary>
/// Describes a scene in the Edith Haydee level editor. This is the top level entity.
/// </summary>
public class Scene
{
    private readonly int _ceilingHeight;
    private readonly List<Wall> _walls;
    private readonly Floor _floor;
    private readonly string _name;

    /// <summary>
    /// Creates a scene based on the given descriptors.
    /// </summary>
    /// <param name="wallString"></param>
    /// <param name="doorString"></param>
    /// <param name="name"></param>
    /// <param name="ceilingHeight"></param>
    public Scene(string wallString, string doorString, string name, int ceilingHeight = 11)
    {
        this._name = name;
        this._ceilingHeight = ceilingHeight;
        
        this._walls = Wall.GenerateWallsFromDescriptors(wallString, doorString);
        this._floor = Floor.GenerateFloorFromWalls(this._walls);
    }
}