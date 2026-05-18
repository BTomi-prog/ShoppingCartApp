namespace ShoppingCartApp
{
    public class Discount
    {
        // percent: 0–100 között, különben ArgumentException
        // Példa: ApplyPercentage(200, 10) -> 180
        public double ApplyPercentage(double total, double percent)
        {
            if (percent>100 || percent < 0)
            {
                throw new ArgumentException("A számnak 0 és 100 között kell lennie");
            }
            else
            {
                double kedvezmeny = total * (percent / 100);
                return total - kedvezmeny;
            }
            
        }

        // Az eredmény soha nem lehet negatív — ha a kedvezmény nagyobb, 0-t ad vissza
        // Példa: ApplyFixed(100, 50) -> 50
        public double ApplyFixed(double total, double discountAmount)
        {
            if (discountAmount < 0)
            {
                throw new ArgumentException("A discountAmount nem lehet kisebb mint nulla");
            }
            double kedvezmenyesAr = total - discountAmount;
            if (kedvezmenyesAr < 0)
            {
                return 0;
            }
            else
            {
                return kedvezmenyesAr;
            }
            
            
        }

        // true ha discountValue > 0
        public bool IsValid(double discountValue)
        {
            if (discountValue > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
