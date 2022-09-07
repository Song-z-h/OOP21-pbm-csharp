namespace PaoloPietrelli;

public class MoneyBox
{
    public string NameMoneyBox { get; set; }
    public List<Transaction> Transactions { get; set; }
        
    public List<AssetValue>  getTotalAmount()
    {
        List<AssetValue> totalAmount = new List<AssetValue>();
            
        foreach (var transaction in Transactions)
        {
            if (totalAmount.Any(asset => asset.SymbolAsset == transaction.Currency))
            {
                foreach (var asset in totalAmount)
                {
                    if (asset.SymbolAsset == transaction.Currency)
                    {
                        asset.Amount += transaction.Amount;
                    }
                }
            }
            else
            {
                AssetValue newAssetValue = new AssetValue()
                {
                    Amount = transaction.Amount,
                    SymbolAsset = transaction.Currency
                };
                    
                totalAmount.Add(newAssetValue);

            }
        }

        return totalAmount;
    }

    public bool transactionIsPossible(double amount, string currency)
    {
        var totalAmount = getTotalAmount();
        if (totalAmount.Any(asset => asset.SymbolAsset == currency))
        {
            foreach (var value in totalAmount)
            {
                if (value.SymbolAsset == currency)
                {
                    return (value.Amount + amount) >= 0 ? true : false;
                }
            }
                
        }
        return (amount >= 0);
    }
}