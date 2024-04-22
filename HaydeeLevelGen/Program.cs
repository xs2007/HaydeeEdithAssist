using HaydeeLevelGen.Generation;
using HaydeeLevelGen.Structure;

namespace HaydeeLevelGen;

/// <summary>
/// Static class for main entry for console-based usage.
/// </summary>
public static class StartUp
{
    /// <summary>
    /// Console based main method.
    /// </summary>
    /// <param name="args">
    /// Generation parameters. -w for wall descriptors, -d for door descriptors, -n for level name, -s for save path.
    /// </param>
    public static void Main(string[] args)
    {
        HandleArgs(args, out string wallString, out string doorString, out string levelName,
            out string savePath);
        
        Scene scene = new Scene(wallString, doorString, levelName);
        string levelData = EdithFileGenerator.ConvertToEdithSceneFileString(scene);
        WriteToFile(savePath, levelData);
    }
        
    private static void WriteToFile(string path, string content) {
        File.WriteAllText(path, content);
    }

    /// <summary>
    /// Handles all command line parameters and places their content into the respective out-variables.
    /// </summary>
    /// <exception cref="CommandLineArgumentException">When an unrecognized args string array is passed.</exception>
    private static void HandleArgs(string[] args, out string wallString, out string doorString, out string levelName,
        out string savePath)
    {
        wallString = "";
        doorString = "";
        levelName = "";
        savePath = "";
        
        for (int i = 0; i < args.Length; i++)
        {
            if (i + 1 >= args.Length)
                throw new CommandLineArgumentException("Invalid parameter length detected.");
            
            switch (args[i])
            {
                case "-w": wallString = args[i + 1];
                    break;
                case "-d": doorString = args[i + 1];
                    break;
                case "-n": levelName = args[i + 1];
                    break;
                case "-s": savePath = args[i + 1];
                    break;
                default: throw new CommandLineArgumentException("Invalid flag detected.");
            }

            i++;
        }
    }

    private class CommandLineArgumentException(string msg) : Exception(msg);
}