namespace Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        // Revenue Model test
        RevenueLogic rev = new RevenueLogic();
        rev._revenueList = new List<RevenueModel>();
        RevenueModel rev1 = new RevenueModel(1, "TestField", 10);
        rev._revenueList.Add(rev1);
        Assert .AreEqual (10, rev.GetById(1).Money);
    }
}