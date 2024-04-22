using HaydeeLevelGen.Generation.FileEntries;
using HaydeeLevelGen.Structure;

namespace HaydeeLevelGen.Generation;

/// <summary>
/// Provides means to generate a content string from a scene definition.
/// </summary>
internal static class EdithFileContentGenerator {
    
    private const string DoorNamePrefix = "Door_";
    private const string GrateNamePrefix = "Grate_";
    private const string BoolSwitchNamePrefix = "Bool_";
    private const string BoolSwitchNameSuffix = "_Unlocked";
    
    /// <summary>
    /// Generate the BaseFileEntry, that describes the given scene.
    /// </summary>
    internal static BaseFileEntry For(Scene scene) {
        BaseFileEntry content = new("content");

        // TODO: Implement
        
        return content;
    }
}