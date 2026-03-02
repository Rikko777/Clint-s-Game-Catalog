namespace ClintsCatalog.Core.Models;

public class GameTitle
{
  public int Id { get; set; }
  public required string Title { get; set; }
  public required string Publisher { get; set; }
  public required string Developer { get; set; }
  public string? Barcode { get; set; }
  public MediaType Media { get; set; }
  public PackagingType Packaging { get; set; }
}

public enum MediaType
{
  CD,
  DVD,
  Bluray,
  Diskette,
  Tape
}

public enum PackagingType
{
  BigBox,
  SmallBox,
  JewelCase,
  Sleeve,
  None
}
