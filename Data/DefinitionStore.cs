namespace BlazorDictionary.Data;

public class DefinitionStore
{
    List<DefinitionItem> definitions = new()
    {
        new()
        {
            Name = "Server-side rendering",
            Summary = "Render your Blazor components once, on the server, and return plain HTML to the browser"
        },
        new()
        {
            Name = "Streaming Rendering",
            Summary =
                "Slow database call? Make your page load quickly by returning an initial payload of HTML, then stream the rest of the data when it becomes available."
        }
    };

    public IEnumerable<DefinitionItem> Definitions(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
            return definitions;

        return definitions.Where(x =>
            x.Name.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
            || x.Summary.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase));
    }
}