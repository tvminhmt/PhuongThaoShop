using Newtonsoft.Json;
using System.IO;

namespace PTS.Shared.Helper
{
    public static class ExtensionMethods
    {
        // Deep clone
        public static T DeepClone<T>(this T obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static byte[] ReadFileToBytes(string path)
        {
            try
            {
                byte[] buffer = null;
                FileInfo f = new FileInfo(path);
                using (FileStream fs = f.OpenRead())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        fs.CopyTo(memoryStream);
                        buffer = memoryStream.ToArray();
                    }
                }
                return buffer;
            }
            catch
            {
                return null;
            }
        }
    }
}
