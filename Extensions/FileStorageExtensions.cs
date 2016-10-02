using System.IO;

using PCLExt.FileStorage;

namespace PokeD.SCON.Extensions
{
    public static class FileStorageExtensions
    {

        public static bool LoadLog(string filename, out string content)
        {
            content = string.Empty;

            using (var stream = Storage.LogFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists).Result.OpenAsync(FileAccess.ReadAndWrite).Result)
            using (var writer = new StreamWriter(stream))
            {
                try { writer.Write(content); }
                catch (IOException) { return false; }
            }

            return true;
        }
        public static bool SaveLog(string filename, string content)
        {
            using (var stream = Storage.LogFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists).Result.OpenAsync(FileAccess.ReadAndWrite).Result)
            using (var writer = new StreamWriter(stream))
            {
                try { writer.Write(content); }
                catch (IOException) { return false; }
            }

            return true;
        }
    }
}
