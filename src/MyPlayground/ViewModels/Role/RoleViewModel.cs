using System.Collections.Generic;

namespace MyPlayground.ViewModels
{
  public class RoleViewModel
  {
    public string Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<string> Rights { get; set; }
  }
}
