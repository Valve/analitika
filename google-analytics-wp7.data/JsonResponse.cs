namespace Analitika.Data {
  public class JsonResponse<T>{
    public int totalResults { get; set; }
    public int startIndex { get; set; }
    public int itemsPerPage { get; set; }
    public T[] items { get; set; }
    public string[][] rows { get; set; }
  }
}
