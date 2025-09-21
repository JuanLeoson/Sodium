namespace Sodium
{
    public class PluginInfo
    {
        public const string GUID = "0000.com.SodiumDevelopment.Sodium"; // 0 at beginning of UUID to run first in BepInEx load order
        public const string Name = "Sodium";
        public const string Description = "Performance Mod for Gorilla Tag";
        public const string Version = "1.6.1";
        public const string ConsoleVersion = "Sodium" + Version;
        public const string WriteAccessKey = "803b607b8cdd5571853047da83a41d5f522f52a290e530d580c70d5e8c08904e_" + Version;
    }
}
