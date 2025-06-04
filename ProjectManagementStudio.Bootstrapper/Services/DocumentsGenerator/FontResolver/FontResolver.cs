using PdfSharp.Fonts;
using System.IO;

namespace ProjectManagementStudio.Bootstrapper.Services.DocumentsGenerator.FontResolver;

internal class FontResolver : IFontResolver
{
    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        if (familyName.Equals("OpenSans", StringComparison.OrdinalIgnoreCase))
        {
            if (isBold) return new FontResolverInfo("Source/Fonts/OpenSans-Bold.ttf");
            if (isItalic) return new FontResolverInfo("Source/Fonts/OpenSans-Italic.ttf");
            return new FontResolverInfo("Source/Fonts/OpenSans-Bold.ttf");
        }
        return null;
    }

    public byte[] GetFont(string faceName)
    {
        return File.ReadAllBytes(faceName);
    }
}
