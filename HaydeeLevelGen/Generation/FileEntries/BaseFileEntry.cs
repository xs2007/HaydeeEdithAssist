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
    
    private readonly string _name;
    private readonly Dictionary<string, string> _parameters;
    private readonly List<BaseFileEntry> _subEntries;

    /// <summary>
    /// Create a new BaseFileEntry with the given name.
    /// It does not hold any parameters or sub entries upon creation.
    /// </summary>
    /// <param name="name"></param>
    internal BaseFileEntry(string name) {
        this._name = name;
        this._parameters = new Dictionary<string, string>();
        this._subEntries = new List<BaseFileEntry>();
    }

    /// <summary>
    /// Put a parameter into this entry.
    /// If the parameter already exists, it is overwritten.
    /// </summary>
    internal void PutParameter(string key, string value) {
        this._parameters[key] = value;
    }

    /// <summary>
    /// Add additional sub-entries to be evaluated upon string generation.
    /// </summary>
    internal void AddSubEntries(params BaseFileEntry[] newEntries) {
        foreach (BaseFileEntry entry in newEntries) {
            this._subEntries.Add(entry);
        }
    }

    /// <summary>
    /// Use the given StringBuilder instance,
    /// and append the string representation of all contents in this BaseFileEntry to it.
    /// </summary>
    internal void AppendStringRepresentation(StringBuilder sb, int indent = 0) {
        AppendLineWithIndent(sb, indent, this._name);
        AppendLineWithIndent(sb, indent, "{");

        foreach (KeyValuePair<string, string> param in this._parameters)
            AppendLineWithIndent(sb, indent + 1, ToParamContentString(param));

        foreach (BaseFileEntry entry in this._subEntries)
            entry.AppendStringRepresentation(sb, indent + 1);

        AppendLineWithIndent(sb, indent, "}");
    }

    private static void AppendLineWithIndent(StringBuilder sb, int indent, string content) {
        sb.Append(new string('\t', indent));
        sb.AppendLine(content);
    }

    private static string ToParamContentString(KeyValuePair<string, string> entry) {
        return entry.Value != "" ? $"{entry.Key} {entry.Value};" : $"{entry.Key};";
    }
}