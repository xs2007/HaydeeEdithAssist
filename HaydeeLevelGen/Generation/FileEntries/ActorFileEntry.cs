namespace HaydeeLevelGen.Generation.FileEntries;

/// <summary>
/// Represents an actor file entry, used for all physical generic entities in the scene.
/// </summary>
internal class ActorFileEntry : BaseFileEntry {

    internal ActorFileEntry(string name) : base("Actor " + name) {
        PutDefaultParameters();
    }
    
    /// <summary>
    /// Sets the default behaviour of ActorFileEntries: not frozen, not hidden,
    /// enabled and at <see cref="BaseFileEntry.PutDefaultLocalsParameters">default position</see>.
    /// </summary>
    private void PutDefaultParameters() {
        PutParameter("frozen", "false");
        PutParameter("hidden", "false");
        PutDefaultLocalsParameters();
        PutDefaultFreezeParameters();
        PutParameter("enabled", "true");
    }
}