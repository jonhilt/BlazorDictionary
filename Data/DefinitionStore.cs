namespace BlazorDictionary.Data;

public class DefinitionStore(MarkdownStore markdownStore)
{
    public IEnumerable<DefinitionItem> Definitions(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return markdownStore.ListDefinitions();
        }

        return markdownStore.ListDefinitions().Where(x =>
            x.Name.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
            || x.Summary.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase));
    }
}