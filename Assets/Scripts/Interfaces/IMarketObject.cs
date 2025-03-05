public interface IMarketObject
{
    ItemSCB ItemData { get; set; }
    void Init(ItemSCB itemData);
    void BuyMe();
    void SellMe();
}