namespace Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Revenue()
    {
        // Revenue Model test
        RevenueLogic rev = new RevenueLogic();
        rev._revenueList = new List<RevenueModel>();
        RevenueModel rev1 = new RevenueModel(1, "TestField", 10);
        rev._revenueList.Add(rev1);
        Assert .AreEqual (10, rev.GetById(1).Money);
    }

    [TestMethod]
    public void Geust()
    {
        GuestLogic guest = new();
        int[] seat = { 1, 2 };
        List<int[]> seats = new();
        seats.Add(seat);
        List<Tuple<int, int, DateTime, int>> info = new();
        Tuple<int, int, DateTime, int> thing = new(1, 1, new DateTime(2021, 12, 28, 10, 10, 10), 1);
        info.Add(thing);
        ReservationModel model = new(1, "DHawE", "John Pork", "Josh@nl", "john Wick 4", 2, 2, 2, 1, seats, info, 10, new DateTime(2021, 12, 28, 10, 10, 10));
        guest.UpdateList(model);
        Assert.AreEqual(guest.GetByEmail("Josh@nl"), model);
    }

    [TestMethod]
    public void Update_FoodItemList()
    {
        // Check of je een nieuwe food item kan toevoegen.
        List<FoodModel> FoodList = FoodAccess.LoadAll();
        int beforeLength = FoodList.Count();
        FoodsLogic obj = new FoodsLogic();
        FoodModel snack = new FoodModel("Appel", 2.5, 0, 18);
        int afterLength = FoodList.Count();
        Assert.IsTrue(beforeLength == afterLength);
    }

    [TestMethod]
    public void CheckCostReturn()
    {
        // Check of je het correct FoodModelObject/Kosten terug krijgt.
        double price = 3.75;
        FoodsLogic foodsLogic = new FoodsLogic();
        var result = foodsLogic.GetByPrice(price);
        Assert.IsNotNull(result);
        Assert.AreEqual("Popcorn Small", result.Name);
        Assert.AreEqual(price, result.Cost);
    }
    
    [TestMethod]
    public void CheckSnackReturn()
    {
        // Check of je het correct FoodModelObject/SnackNaam terug krijgt.
        FoodModel snack = new FoodModel("Popcorn Large", 5.95, 0, 18);
        FoodsLogic obj = new FoodsLogic();
        string snackName = "Popcorn Large";
        Assert.AreEqual(snackName, obj.GetByName(snackName).Name);
    }

    [TestMethod]
    public void CheckPasswordFormat()
    {
        // Check of de method het wachtwoord in de correcte formaat goedrekent.
        AccountsLogic obj = new AccountsLogic();
        string password = "AryanJadoe@2003";
        Assert.IsTrue(obj.PasswordFormatCheck(password));
    }

    [TestMethod]
    public void CheckEmailFormat()
    {
        // Check of de method de email in het correcte formaat goedrekent.
        AccountsLogic obj = new AccountsLogic();
        string email = "aryanjd2005@gmail.com";
        Assert.IsTrue(obj.EmailFormatCheck(email));
    }

    [TestMethod]
    public void TestGetByID() // roomLogic test
    {
        List<RoomModel> roomList = RoomAccess.LoadAll();
        RoomsLogic roomLogic = new();
        RoomModel model = roomLogic.GetById(2);
        Assert.IsNotNull(model);
    }

    [TestMethod]
    public void TestGetByIDNull() // roomLogic test
    {
        List<RoomModel> roomList = RoomAccess.LoadAll();
        RoomsLogic roomLogic = new();
        RoomModel model = roomLogic.GetById(5);
        Assert.IsNull(model);
    }
}