public class TotalManager
{
    private static TotalManager _instance;
    private static readonly object _lock = new object();
    public int Total { get; private set; }

    private TotalManager() { }

    public static TotalManager Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new TotalManager();
                }
                return _instance;
            }
        }
    }

    public void AddToTotal(int value)
    {
        Total += value;
    }

    public void SubToTotal(int value) {
        Total -= value;
    }

    public void ResetTotal()
    {
        Total = 0;
    }
    public string GetTotalAsBinary()
    {
        return Convert.ToString(Total, 2);
    }
}
