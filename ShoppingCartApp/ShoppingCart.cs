namespace ShoppingCartApp
{
    public class ShoppingCart
    {
        private readonly List<CartItem> _items;

        public ShoppingCart()
        {
            _items = new List<CartItem>();
        }

        // Ha az item neve már szerepel (kis-nagybetű független), növeli a mennyiségét
        public void AddItem(string name, double unitPrice, int quantity)
        {
            CartItem foundItem = _items.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (foundItem != null)
            {
                foundItem.UpdateQuantity(foundItem.Quantity + quantity); 
            }
            else
            {
                CartItem uj = new CartItem(name, unitPrice, quantity);
                _items.Add(uj);
            }
        }

        // true ha megtalálta és törölte, false ha nem szerepelt
        public bool RemoveItem(string name)
        {
            CartItem talal = _items.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (talal != null)
            {
                _items.Remove(talal);
                return true;
            }
            else { return false; }
        }

        public int GetItemCount()
        {
            return _items.Count;
        }

        // Összeg = minden item (UnitPrice * Quantity) összege
        public decimal GetTotal()
        {
            return (decimal)/*castolás tipus átalakítás*/ _items.Sum(x => x.GetLineTotal());
        }

        public IReadOnlyList<CartItem> GetItems()
        {
            return _items;
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
