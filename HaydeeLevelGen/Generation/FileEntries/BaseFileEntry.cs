using System.Dynamic;
using System.Text;

namespace HaydeeLevelGen.Generation.FileEntries;

/// <summary>
/// Represents the base class for all (nested) entries within an Edith scene string representation.
/// More specific entry types inherit from this one.
/// </summary>
internal class BaseFileEntry {
    
    internal const int TileSize = 16;

    internal const string ParamRotationX = "localx";
    internal const string ParamRotationY = "localy";
    internal const string ParamRotationZ = "localz";
    internal const string ParamLocation = "localw";

    private const string FreezeX = "freezex";
    private const string FreezeY = "freezey";
    private const string FreezeZ = "freezez";
    private const string FreezeW = "freezew";
    
    private readonly string _name;
    private readonly Dictionary<string, string> _parameters;
    private readonly List<BaseFileEntry> _subEntries;

    /// <summary>
    /// Create a new BaseFileEntry with the given name.
    /// It does not hold any parameters or sub entries upon creation.
    /// </summary>
    /// <param name="name"></param>
    internal BaseFileEntry(string name) {
        _name = name;
        _parameters = new Dictionary<string, string>();
        _subEntries = new List<BaseFileEntry>();
    }

    /// <summary>
    /// Puts the default location parameters into the parameter map of this BaseFileEntry.
    /// Initial position will be (0,0,0).
    /// </summary>
    protected void PutDefaultLocalsParameters() {
        PutParameter(ParamRotationX, "1.0 0.0 0.0");
        PutParameter(ParamRotationY, "0.0 1.0 0.0");
        PutParameter(ParamRotationZ, "0.0 0.0 1.0");
        PutParameter(ParamLocation, "0.0 0.0 0.0");
    }

    /// <summary>
    /// Puts the default freeze parameters for this BaseFileEntry.
    /// Initial freeze position will be (0,0,0).
    /// </summary>
    protected void PutDefaultFreezeParameters() {
        PutParameter(FreezeX, "1.0 0.0 0.0");
        PutParameter(FreezeY, "0.0 1.0 0.0");
        PutParameter(FreezeZ, "0.0 0.0 1.0");
        PutParameter(FreezeW, "0.0 0.0 0.0");
    }

    /// <summary>
    /// Put a parameter into this entry.
    /// If the parameter already exists, it is overwritten.
    /// </summary>
    internal void PutParameter(string key, string value) {
        _parameters[key] = value;
    }

    /// <summary>
    /// Add additional sub-entries to be evaluated upon string generation.
    /// </summary>
    internal void AddSubEntries(params BaseFileEntry[] newEntries) {
        foreach (BaseFileEntry entry in newEntries) {
            _subEntries.Add(entry);
        }
    }
    
    /// <summary>
    /// Adds one additional sub-entry to be evaluated upon string generation.
    /// </summary>
    internal void AddSubEntry(BaseFileEntry newEntry) {
        AddSubEntries(newEntry);
    }

    /// <summary>
    /// Use the given StringBuilder instance,
    /// and append the string representation of all contents in this BaseFileEntry to it.
    /// </summary>
    internal void AppendStringRepresentation(StringBuilder sb, int indent = 0) {
        AppendLineWithIndent(sb, indent, _name);
        AppendLineWithIndent(sb, indent, "{");
        
        foreach ((string key, string value) in _parameters)
            AppendLineWithIndent(sb, indent + 1, ToParamContentString(key, value));

        foreach (BaseFileEntry entry in _subEntries)
            entry.AppendStringRepresentation(sb, indent + 1);

        AppendLineWithIndent(sb, indent, "}");
    }

    private static void AppendLineWithIndent(StringBuilder sb, int indent, string content) {
        sb.Append(new string('\t', indent));
        sb.AppendLine(content);
    }

    private static string ToParamContentString(string key, string value) {
        return value != "" ? $"{key} {value};" : $"{key};";
    }
}