public class Category
{
  private string _name;
  private string _type;
  public Category(string name, string type)
  {
    _name = name;
    _type = type;
  }
  static void AddCategory(string name, string type)
  {
  }
  static List<Category> GetCategories(string type)
  {
    return new List<Category>();
  }
}
