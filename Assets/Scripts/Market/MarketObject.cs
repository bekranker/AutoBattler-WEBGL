using DG.Tweening;
using TMPro;
using UnityEngine;

public abstract class MarketObject : MonoBehaviour, IMarketObject, IDraggable
{
    [SerializeField] private float _cost;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private Collider2D _collider;
    private Vector3 _initialPosition;
    public ItemSCB ItemData { get; set; }
    private bool _canBuy;
    private Transform _targetSeat;
    public void BuyMe()
    {
        Debug.Log($"Purchased, Cost is: {_cost}");
        ItemData.BuyEffect?.ForEach((button)=>{
            if(!button.ExecuteEffect(_targetSeat)){
                RejectPurchase();
                return;
            }
        });
        bool tryBuy = MoneyHandler.Instance.DecreaseMoney(_cost);
        if (!tryBuy)
        {
            RejectPurchase();
            return;
        }
        Destroy(gameObject, .3f);
    }
    void OnDestroy()
    {
        DOTween.Kill(transform);
    }
    public void Init(ItemSCB itemData)
    {
        ItemData = itemData;
        _initialPosition = transform.position;
        DOTween.Kill(transform);
        transform.DOPunchScale(DoTweenProps.Instance.PunchScale, DoTweenProps.Instance.NotEnoughMoneyDuration);
        _cost = ItemData.Cost;
        _costText.text = _cost.ToString();
        _spriteRenderer.sprite = ItemData.ItemSprite;
    }

    public void SellMe()
    {
    }
    public void OnDrag(Vector3 newPos)
    {
        if(!_canBuy) return;

        transform.position = Vector3.Lerp(transform.position, newPos, DoTweenProps.Instance.ItemAimMovementSpeed * Time.deltaTime);
    }
    public void OnDragEnd(Cell cell)
    {
        if(!_canBuy) return;
        if (cell == null)
        {
            DOTween.Kill(transform);
            transform.DOMove(_initialPosition, DoTweenProps.Instance.MoveSpeed).SetEase(Ease.Linear);
            return;
        }
        _collider.enabled = false;
        
        transform.DOMove(cell.CellT.position, DoTweenProps.Instance.PurchasedItemSpeed).SetEase(Ease.Linear).OnComplete(()=>
        {
            _targetSeat = cell.CellT;
            BuyMe();
            transform.DOPunchScale(DoTweenProps.Instance.PurchasedPunchScale * Vector3.one, DoTweenProps.Instance.MoveSpeed);
        });
    }
    private void RejectPurchase()
    {
        DOTween.Kill(transform);
        transform.DOPunchPosition(DoTweenProps.Instance.NotEnoughMoneyPunch, DoTweenProps.Instance.MoveSpeed);
    }
    public void OnDragStart()
    {
        DOTween.Kill(transform);
        if (MoneyHandler.Instance.CurrnetMoney < _cost)
        {
            _canBuy = false;
            transform.position = _initialPosition;
            RejectPurchase();
            return;
        }
        _canBuy = true;
        print("Dragging Item is Started");
    }
}