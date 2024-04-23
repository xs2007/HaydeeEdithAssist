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

    private const string MainMeshName = "Level";
    
    /// <summary>
    /// Generate the BaseFileEntry, that describes the given scene.
    /// </summary>
    internal static BaseFileEntry For(Scene scene) {
        BaseFileEntry content = new("content");

        ActorFileEntry levelActor = GenerateLevelActor(scene);
        content.AddSubEntries(levelActor);

        List<BaseFileEntry> doorActors = GenerateDoorActors(scene);
        doorActors.ForEach(content.AddSubEntry);

        List<ActorFileEntry> lightActors = GenerateHemiLightActors(scene);
        lightActors.ForEach(content.AddSubEntry);
        
        return content;
    }


    private static ActorFileEntry GenerateLevelActor(Scene scene) {
        ActorFileEntry level = new(MainMeshName);
        
        return level;
    }

    private static List<BaseFileEntry> GenerateDoorActors(Scene scene) {
        List<BaseFileEntry> doorActors = [];

        return doorActors;
    }

    private static List<ActorFileEntry> GenerateHemiLightActors(Scene scene) {
        List<ActorFileEntry> hemiLightActors = [];

        return hemiLightActors;
    }
}