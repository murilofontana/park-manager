namespace ParkManager.Application.Establishments.Summary.TotalSumary;

public class TotalSummaryResponse
{
  public TotalSummaryResponse(int totalCarsEntry, int totalCarsExit, int totalMotorcyclesEntry, int totalMotorcyclesExit, int totalGeneralEntry, int totalGeneralExit)
  {
    TotalCarsEntry = totalCarsEntry;
    TotalCarsExit = totalCarsExit;
    TotalMotorcyclesEntry = totalMotorcyclesEntry;
    TotalMotorcyclesExit = totalMotorcyclesExit;
    TotalGeneralEntry = totalGeneralEntry;
    TotalGeneralExit = totalGeneralExit;
  }

  public int TotalCarsEntry { get; set; }
  public int TotalCarsExit { get; set; }
  public int TotalMotorcyclesEntry { get; set; }
  public int TotalMotorcyclesExit { get; set; }
  public int TotalGeneralEntry { get; set; }
  public int TotalGeneralExit { get; set; }
}