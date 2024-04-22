using System.Text;
using HaydeeLevelGen.Generation.FileEntries;
using HaydeeLevelGen.Structure;
using NLog;

namespace HaydeeLevelGen.Generation;

/// <summary>
/// Used to interact with the Haydee Edith editor file format.
/// This static class offers methods to generate Edith-compatible file strings.
/// </summary>
public static class EdithFileGenerator {
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();


    private const string FileHeader = "HD_DATA_TXT 300";

    /// <summary>
    /// Converts the given Scene object into the respective edith file string representation.
    /// </summary>
    public static string ConvertSceneToEdithFileString(Scene scene) {
        Log.Info("Starting conversion of scene to Edith scene file...");

        BaseFileEntry root = new("edith");

        BaseFileEntry content = EdithFileContentGenerator.For(scene);
        BaseFileEntry var = GenerateDefaultVarEntry();
        BaseFileEntry view = GenerateDefaultViewEntry();
        BaseFileEntry director = GenerateDefaultDirectorEntry();

        root.AddSubEntries(content, var, view, director);

        StringBuilder sb = new(FileHeader + Environment.NewLine);
        root.AppendStringRepresentation(sb);
        string result = sb.ToString();

        Log.Info("Conversion to Edith scene file completed.");
        return result;
    }

    private static BaseFileEntry GenerateDefaultVarEntry() {
        BaseFileEntry var = new("var");

        var.PutParameter("actorCounter", "0");
        var.PutParameter("censor", "false");
        var.PutParameter("outline", "false");
        var.PutParameter("ambientOcclusion", "false");
        var.PutParameter("lightUp", "false");
        var.PutParameter("animating", "false");
        var.PutParameter("dollHeight", "6.0");
        var.PutParameter("dollRadius", "4.0");
        var.PutParameter("csgCounter", "0");
        var.PutParameter("componentCounter", "0");
        var.PutParameter("renderSprites", "true");

        return var;
    }

    private static BaseFileEntry GenerateDefaultViewEntry() {
        BaseFileEntry view = new("view");

        view.PutParameter("origin", "80.0 48.0 80.0");
        view.PutParameter("yaw", "45.0");
        view.PutParameter("pitch", "0.0");
        view.PutParameter("zoom", "76.59613");

        return view;
    }

    private static BaseFileEntry GenerateDefaultDirectorEntry() {
        BaseFileEntry director = new("director");

        director.PutParameter("x", "-130");
        director.PutParameter("y", "-60");
        director.PutParameter("nodes", "");
        director.PutParameter("links", "");

        return director;
    }
}