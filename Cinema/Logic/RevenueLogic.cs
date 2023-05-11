public class RevenueLogic
{
    public List<RevenueModel> _revenueList;

    public RevenueLogic()
    {
        _revenueList = RevenueAcces.LoadAll();
    }

    public void UpdateList(RevenueModel revenue)
    {
        //Find if there is already an model with the same id
        int index = _revenueList.FindIndex(s => s.Id == revenue.Id);

        if (index != -1)
        {
            //update existing model
            _revenueList[index] = revenue;
        }
        else
        {
            //add new model
            _revenueList.Add(revenue);
        }
        RevenueAcces.WriteAll(_revenueList);
    }

    public RevenueModel GetById(int id)
    {
        return _revenueList.Find(i => i.Id == id)!;
    }

    public void RemoveRevenue(int revenueId)
    {
        //Find if there is already an model with the same id
        int index = _revenueList.FindIndex(s => s.Id == revenueId);

        if (index != -1)
        {
            _revenueList.RemoveAt(index);
            RevenueAcces.WriteAll(_revenueList);
        }
        else
        {
            Console.WriteLine($"Not found!?");
        }
    }

    public decimal TotalRevenue()
    {
        decimal total = 0;
        foreach (RevenueModel revenue in _revenueList)
        {
            total += revenue.Money;
        }
        return total;
    }
}