namespace knightstale.Models
{
  public class Knight
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }
    public string CastleId { get; set; }
    public string CreatorId { get; set; }
  }
}