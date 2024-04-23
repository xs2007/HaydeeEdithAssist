namespace HaydeeLevelGen.Generation.FileEntries;

/// <summary>
/// Component file entries compose each actor file entry.
/// </summary>
internal class ComponentFileEntry : BaseFileEntry {
    
    internal ComponentFileEntry(string type, string name) : base($"component {type} {name}") {
        PutDefaultParameters();
    }
    
    /// <summary>
    /// Sets the default behaviour of ComponentFileEntry: enabled, not editor-only, expanded in UI, not selected,
    /// not hidden and at <see cref="BaseFileEntry.PutDefaultLocalsParameters">default position</see>.
    /// </summary>
    private void PutDefaultParameters() {
        PutParameter("enabled", "true");
        PutParameter("editorOnly", "false");
        PutParameter("expanded", "true");
        PutParameter("selected", "false");
        PutParameter("hidden", "false");
        PutDefaultLocalsParameters();
        PutDefaultFreezeParameters();
    }
}