namespace BlazorDictionary.Data;

public class DefinitionStore(MarkdownStore markdownStore)
{
    public IEnumerable<DefinitionItem> Definitions(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return markdownStore.GetDefinitions();
        }

        return markdownStore.GetDefinitions().Where(x =>
            x.Name.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
            || x.Summary.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase));
    }
}