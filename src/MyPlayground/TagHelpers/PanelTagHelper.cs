using System.Threading.Tasks;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace MyPlayground.TagHelpers
{
  public class PanelTagHelper : TagHelper
  {
    public string Title { get; set; }

    public string Type { get; set; } = "default";

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = "div";
      output.Attributes["class"] = $" panel panel-{Type} {output.Attributes["class"]?.Value}";

      var childContent = (await context.GetChildContentAsync()).GetContent();

      var outputContent = "";
      if (Title != null)
      {
        outputContent += $@"<div class='panel-heading'>
            <h3 class='panel-title'>{Title}</h3>
           </div>";
      }
      outputContent += $@"<div class='panel-body'>
                            {childContent}
                          </div>";

      output.Content.SetContent(outputContent);
    }
  }
}
