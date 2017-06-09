using System.Collections.Generic;
using System.Linq;

namespace Lib.Helpers
{
    public class MimeHelper
    {
        private static readonly Dictionary<string, string> MimeTypeMappings = new Dictionary<string, string>
        {
            { "gif", "image/gif" },
            { "jpeg", "image/jpeg" },
            { "jpg", "image/jpeg" },
            { "png", "image/png" }
        };

        public string GetMimeType(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return string.Empty;
            }
            string value;
            var extension = filename.Split('.').Last();
            return MimeTypeMappings.TryGetValue(extension, out value) ? value : string.Empty;
        }
        public string GetMimeType(byte[] contents)
        {
            if (IsGif(contents))
            {
                return "image/gif";
            }
            if (IsJpg(contents))
            {
                return "image/jpeg";
            }
            if (IsPng(contents))
            {
                return "image/png";
            }
            return string.Empty;
        }
        private static bool IsGif(byte[] contents)
        {
            return contents[0] == 0x47 && contents[1] == 0x49 && contents[2] == 0x46;
        }
        private static bool IsJpg(byte[] contents)
        {
            return contents[0] == 0xFF && contents[1] == 0xD8;
        }
        private static bool IsPng(byte[] contents)
        {
            return contents[0] == 0x89 && contents[1] == 0x50 && contents[2] == 0x4E && contents[3] == 0x47;
        }
    }
}
