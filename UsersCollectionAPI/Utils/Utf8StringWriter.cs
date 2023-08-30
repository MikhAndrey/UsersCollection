using System.Text;

namespace UsersCollectionAPI.Utils;

public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding
    {
        get => new UTF8Encoding(false);
    }
}
