namespace knightstale.Models
{
  public class Castle
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string CreatorId { get; set; }

    public Profile Creator { get; set; }

  }
}