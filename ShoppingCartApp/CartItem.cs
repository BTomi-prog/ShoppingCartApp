namespace ShoppingCartApp
{
    public class CartItem
    {
        public string Name { get; }
        public double UnitPrice { get; }
        public int Quantity { get; private set; }

        // name nem lehet null/üres, unitPrice > 0, quantity >= 1
        public CartItem(string name, double unitPrice, int quantity)
        {
            if (name == null || name == "")
            {
                throw new ArgumentException("Nem lett név megadva");
            }
            if (unitPrice <= 0 )
            {
                throw new ArgumentException("A unit price-nak 0-nál nagyobb számnak kell lennie");
            }
            if (quantity < 1)
            {
                throw new ArgumentException("A quantity 1 vagy 1-nél nagyobb számnak kell lennie");
            }
            Name = name;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        // UnitPrice * Quantity
        public double GetLineTotal()
        {
            return UnitPrice * Quantity;
        }

        // quantity >= 1, különben ArgumentException
        public void UpdateQuantity(int quantity)
        {
            if (quantity < 1)
            {
                throw new ArgumentException("A quantity 1 vagy 1-nél nagyobb számnak kell lennie");
            }
            else
            {
                Quantity = quantity;
            }
        }
    }
}
