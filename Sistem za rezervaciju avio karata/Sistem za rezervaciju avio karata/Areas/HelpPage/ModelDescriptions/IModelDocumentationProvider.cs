using System;
using System.Reflection;

namespace Sistem_za_rezervaciju_avio_karata.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}