using System.Text.RegularExpressions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BlazorDictionary.Data;

public class MarkdownStore(IHostEnvironment env)
{
    public IEnumerable<DefinitionItem> GetDefinitions()
    {
        var directoryPath = Path.Combine(env.ContentRootPath, "Definitions");
        if (!Directory.Exists(directoryPath))
        {
            throw new DirectoryNotFoundException($"Directory {directoryPath} not found.");
        }

        var filePaths = Directory.GetFiles(directoryPath, "*.md");

        var definitions = new List<DefinitionItem>();
        foreach (var path in filePaths)
        {
            var content = File.ReadAllText(path);
            var file = ParseFrontMatter<DefinitionFile>(content);

            if (file != null)
            {
                definitions.Add(new DefinitionItem { Name = file.Name, Summary = file.Summary });
            }
        }

        return definitions;
    }

    private T? ParseFrontMatter<T>(string markdownContent)
    {
        var yamlDeserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var regexMatch = Regex.Match(markdownContent, @"---\s*(.*?)(---\s*)(.*)", RegexOptions.Singleline);
        if (!regexMatch.Success)
        {
            return default;
        }

        var frontMatter = regexMatch.Groups[1].Value;

        try
        {
            return yamlDeserializer.Deserialize<T>(frontMatter);
        }
        catch (Exception ex)
        {
            return default;
        }
    }

    private class DefinitionFile
    {
        [YamlMember(Alias = "name")] public string Name { get; set; }

        [YamlMember(Alias = "summary")] public string Summary { get; set; }
    }
}