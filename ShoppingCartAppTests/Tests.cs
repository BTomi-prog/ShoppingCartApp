using ShoppingCartApp;

namespace ShoppingCartAppTests
{
    [TestClass]
    public class CartItemTests
    {
        
        [TestMethod]
        public void Constructor_ValidArguments()
        {
            var item = new CartItem("Apple", 1.50, 3);
            Assert.AreEqual("Apple", item.Name);
            Assert.AreEqual(1.50, item.UnitPrice);
            Assert.AreEqual(3, item.Quantity);
        }

        [TestMethod]
        public void Constructore_NullName()
        {
            Assert.ThrowsException<ArgumentException>(() => new CartItem(null, 1, 1));// TODO: null/üres name -> ArgumentException
        }

        [TestMethod]
        public void Constructor_InvalidUsitPrice()
        {
            Assert.ThrowsException<ArgumentException>(() => new CartItem("testNev", 0, 1));// TODO: unitPrice <= 0 -> ArgumentException
        }

        [TestMethod]
        public void Constructor_InvalidQuantity()
        {
            Assert.ThrowsException<ArgumentException>(() => new CartItem("testNev", 1, 0));// TODO: quantity <= 0 -> ArgumentException
        }


        [TestMethod]
        public void GetTotal_MultipleQuantity()
        {
            var item = new CartItem("Banana", 0.75, 4);
            Assert.AreEqual(3.00, item.GetLineTotal());
        }

        [TestMethod]
        public void UpdateQuantity_ValidValue()
        {
            var item = new CartItem("Milk", 1.20, 1);
            item.UpdateQuantity(5);
            Assert.AreEqual(5, item.Quantity);
        }
        

        [TestMethod]
        public void UpdateQuantity_InvalidQuantity()
        {
            CartItem upd = new CartItem("testNev", 1, 1);
            Assert.ThrowsException<ArgumentException>(() => upd.UpdateQuantity(0));// TODO: quantity <= 0 -> ArgumentException
        }
    }

    [TestClass]
    public class ShoppingCartTests
    {
        private ShoppingCart CreateCartWithItems()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 1.00, 3);
            cart.AddItem("Bread", 2.50, 1);
            return cart;
        }

        [TestMethod]
        public void AddItem_NewItem()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 1.00, 2);
            Assert.AreEqual(1, cart.GetItemCount());
        }

        [TestMethod]
        public void AddItem_SameName()
        {
            ShoppingCart cart = new ShoppingCart();
            cart.AddItem("Pineapple", 2, 3);
            cart.AddItem("Pineapple", 1, 1);
            CartItem find = cart.GetItems().Where(x => x.Name == "Pineapple").FirstOrDefault();
            Assert.AreEqual(4, find.Quantity);// TODO: ugyanolyan nevű item hozzáadása, mennyiséget növel annál az adott item-nél (nincs új item)
        }

        [TestMethod]
        public void AddItem_InvalidArg()
        {
            ShoppingCart cart = new ShoppingCart();// TODO: érvénytelen argumentumok -> ArgumentException
            Assert.ThrowsException<ArgumentException>(() => cart.AddItem("", 1, 1));
            Assert.ThrowsException<ArgumentException>(() => cart.AddItem("test", 0, 1));
            Assert.ThrowsException<ArgumentException>(() => cart.AddItem("test", 1, 0));
            
            
        }

        
        

        [TestMethod]
        public void RemoveItem_ExistingItem()
        {
            var cart = CreateCartWithItems();
            bool result = cart.RemoveItem("Apple");
            Assert.IsTrue(result);
            Assert.AreEqual(1, cart.GetItemCount());
        }
        [TestMethod]
        public void RemoveItem_False()
        {
            ShoppingCart cart = new ShoppingCart();// TODO: nem létező item -> false
            bool eredmeny = cart.RemoveItem("sldk");
            Assert.AreEqual(false, eredmeny);
        }

        [TestMethod]
        public void Remove_KicsiNagy()
        {
            ShoppingCart cart = new ShoppingCart();
            cart.AddItem("Apple", 1, 1);
            bool torles = cart.RemoveItem("apple");
            Assert.AreEqual(true, torles);// TODO: törlés kis-nagybetű független-e ("apple" törli az "Apple"-t)
        }


        
        

        [TestMethod]
        public void GetTotal_MultipleItems()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Apple", 1.00, 3);  // 3.00
            cart.AddItem("Bread", 2.50, 2);  // 5.00
            Assert.AreEqual(8.00m, cart.GetTotal());
        }

        [TestMethod]
        public void GetTotal_Ures()
        {
            ShoppingCart cart = new ShoppingCart();
            Decimal eredmeny = cart.GetTotal();
            Assert.AreEqual(0, eredmeny);// TODO: üres kosár -> 0
        }

        [TestMethod]
        public void GetTotal_TorlesUtan()
        {
            var cart = new ShoppingCart();
            cart.AddItem("Alma", 2, 3);
            cart.AddItem("Banán", 4, 4);
            cart.RemoveItem("Alma");
            Assert.AreEqual(16, cart.GetTotal());// TODO: item törlése után helyes-e az összeg


        }
        

        [TestMethod]
        public void Clear_CartWithItems()
        {
            var cart = CreateCartWithItems();
            cart.Clear();
            Assert.AreEqual(0, cart.GetItemCount());
            Assert.AreEqual(0m, cart.GetTotal());
        }

        [TestMethod]
        public void Clear_NotThrowExeption()
        {
            var cart = new ShoppingCart();       
            cart.Clear();// TODO: üres kosáron Clear() nem dob kivételt
        }

        
    }

    [TestClass]
    public class DiscountTests
    {
        [TestMethod]
        public void ApplyPercentage_TenPercent()
        {
            var discount = new Discount();
            Assert.AreEqual(180, discount.ApplyPercentage(200, 10));
        }
        [TestMethod]
        public void ApplyPercentage_ValtozatlanOszz()
        {
            var discount = new Discount();
            var Ar = discount.ApplyPercentage(100, 0);
            Assert.AreEqual(Ar, 100);// TODO: 0% -> változatlan összeg
        }
        [TestMethod]
        public void ApplyPercentage_Nullaba()
        {
            var discount = new Discount();
            var Ar = discount.ApplyPercentage(100, 100);
            Assert.AreEqual(Ar, 0);// TODO: 100% -> 0
        }
        [TestMethod]
        public void ApplyPercentage_Nagyobb100()
        {
            var discount = new Discount();
            
            Assert.ThrowsException<ArgumentException>(() => discount.ApplyPercentage(100, 110));// TODO: percent > 100 -> ArgumentException
        }


        

        [TestMethod]
        public void ApplyFixed_AmountLessThanTotal()
        {
            var discount = new Discount();
            Assert.AreEqual(75, discount.ApplyFixed(100, 25));
        }

        [TestMethod]
        public void ApplyFixed_NemNegativ()
        {
            var discount = new Discount();
            var nemNeg = discount.ApplyFixed(90, 100);
            Assert.AreEqual(nemNeg, 0);// TODO: kedvezmény > total -> 0 (nem negatív)

        }
        [TestMethod]
        public void ApplyFixed_NegativDis()
        {
            var discount = new Discount();
            Assert.ThrowsException<ArgumentException>(() => discount.ApplyFixed(100, -100));// TODO: negatív discountAmount -> ArgumentException
        }
        

        [TestMethod]
        public void IsValid_PositiveValue()
        {
            var discount = new Discount();
            Assert.IsTrue(discount.IsValid(15));
        }

        [TestMethod]
        public void IsValid_Null()
        {
            var discount = new Discount();
            var nulla = discount.IsValid(0);
            Assert.IsFalse(nulla);// TODO: 0 -> false
        }
        [TestMethod]
        public void IsValid_Negativ()
        {
            var discount = new Discount();  
            var negativ = discount.IsValid(-100);
            Assert.IsFalse(negativ);// TODO: negatív -> false
        }
        
    }
}
