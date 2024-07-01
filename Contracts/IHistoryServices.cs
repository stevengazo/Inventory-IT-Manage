namespace InventoryIT.Contracts
{
    public interface IHistoryServices<T> : IControllerServices<T> where T : class
    {
        List<T> HistoriesOfComputer(int id);
        List<T> HistoriesOfSmartphones(int id);
        List<T> HistoriesOfPeripherals(int id);

    }
}
