using System.Drawing;

namespace LebUpwork.Api.Validators
{
    public class FileValidation
    {
        public  bool IsImageValid(IFormFile file)
        {
            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                var signatures = _ImageSignatures.Values.SelectMany(x => x).ToList();  // flatten all signatures to single list
                var headerBytes = reader.ReadBytes(_ImageSignatures.Max(m => m.Value.Max(n => n.Length)));
                bool result = signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));
                return result;
            }
        }

        private static readonly Dictionary<string, List<byte[]>> _ImageSignatures = new()
        {
    { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
    { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
    { ".jpeg", new List<byte[]>
        {
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
        }
    },
    { ".jpeg2000", new List<byte[]> { new byte[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A } } },

    { ".jpg", new List<byte[]>
        {
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xEE },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
        }
    },

   // { ".pdf", new List<byte[]> { new byte[] { 0x25, 0x50, 0x44, 0x46 } } },
   
};
        public  bool IsFileValid(IFormFile file)
        {
            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                var signatures = _fileSignatures.Values.SelectMany(x => x).ToList();  // flatten all signatures to single list
                var headerBytes = reader.ReadBytes(_fileSignatures.Max(m => m.Value.Max(n => n.Length)));
                bool result = signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));
                return result;
            }
        }

        private static readonly Dictionary<string, List<byte[]>> _fileSignatures = new()
        {


    { ".pdf", new List<byte[]> { new byte[] { 0x25, 0x50, 0x44, 0x46 } } },
   
};
    }
}
