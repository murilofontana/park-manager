namespace ParkManager.Application.Establishments.Summary.DailySummaryGroupedByHour;

public class DailySummaryGroupedByHourResponse
{
  public DailySummaryGroupedByHourResponse(Guid establishmentId, DateOnly date, List<HourSummaryResponse> hourSummaries)
  {
    EstablishmentId = establishmentId;
    Date = date;
    HourSummaries = hourSummaries;
  }

  public Guid EstablishmentId { get; set; }
  public DateOnly Date { get; set; }
  public List<HourSummaryResponse> HourSummaries { get; set; } = new List<HourSummaryResponse>();
}

public class HourSummaryResponse
{
  public HourSummaryResponse(int hour, int totalCarsEntry, int totalCarsExit, int totalMotorcyclesEntry, int totalMotorcyclesExit)
  {
    Hour = hour;
    TotalCarsEntry = totalCarsEntry;
    TotalCarsExit = totalCarsExit;
    TotalMotorcyclesEntry = totalMotorcyclesEntry;
    TotalMotorcyclesExit = totalMotorcyclesExit;
  }
  public int Hour { get; set; }
  public int TotalCarsEntry { get; set; }
  public int TotalCarsExit { get; set; }
  public int TotalMotorcyclesEntry { get; set; }
  public int TotalMotorcyclesExit { get; set; }
  public int TotalGeneralEntry => TotalCarsEntry + TotalMotorcyclesEntry;
  public int TotalGeneralExit => TotalCarsExit + TotalMotorcyclesExit;
}
