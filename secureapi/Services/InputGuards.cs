using System.Text.RegularExpressions;

namespace MyApi.Api.Services;

public static class InputGuards
{
    private static readonly Regex HtmlLike = new(@"<[^>]+>", RegexOptions.Compiled);

    public static bool ContainsHtml(string input) => HtmlLike.IsMatch(input);
}
