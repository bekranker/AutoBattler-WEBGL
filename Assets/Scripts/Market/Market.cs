using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;
public class Market : MonoBehaviour
{
    [Header("----Components")]
    [SerializeField] private MoneyHandler _moneyHandler;
    [SerializeField] private WaveSystem _waveSystem;
    [Header("----Raycast Props")]
    [SerializeField] private LayerMask _raycastLayerItems;
    [SerializeField] private LayerMask _raycastLayerCollectables;
    [SerializeField] private LayerMask _raycastPutPlace;
    [Header("----Item Props")]
    [SerializeField] private List<ItemSCB> _marketObjectsThatcanSell;
    [SerializeField] private MarketObject _marketObjectPrefab;
    private IDraggable _currentDraggable;
    private RaycastHit2D _hit2D, _hit2DCollectable;
    [SerializeField] private GenericInventory _inventory;
    [SerializeField] private GenericInventory _players;

    private List<MarketObject> _currentMarketObjects = new();
    public static Market Instance;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        InitializeMarket();
    }
    void Update()
    {
        Collectable();
        if (_currentDraggable != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            _currentDraggable.OnDrag(mousePos);
        }
        ItemDrag();
    }
    void OnEnable()
    {
        _waveSystem.OnPassingnNextWave += ReDesignMarketItems;
    }
    void OnDisable()
    {
        _waveSystem.OnPassingnNextWave -= ReDesignMarketItems;
    }
    [Button("---Re Design Market Items")]
    private void ReDesignMarketItems()
    {
        if (_waveSystem.CurrentWave == null) return;
        if (_waveSystem.CurrentWave.NewItemsForMarket.Count == 0) return;
        _waveSystem.CurrentWave.NewItemsForMarket?.ForEach((item) =>
        {
            if (!_marketObjectsThatcanSell.Contains(item))
            {
                _marketObjectsThatcanSell.Add(item);
            }
        });
    }
    [Button("---Initialize Market---")]
    private void InitializeMarket()
    {
        ReDesignMarketItems();
        for (int i = 0; i < _inventory.Cells.Count; i++)
        {
            MarketObject spawnedSCB = Instantiate(_marketObjectPrefab, _inventory.Cells[i].CellT.position, Quaternion.identity);
            _currentMarketObjects.Add(spawnedSCB);
            spawnedSCB.Init(_marketObjectsThatcanSell[Random.Range(0, _marketObjectsThatcanSell.Count)]);
            spawnedSCB.transform.DOPunchScale(DoTweenProps.Instance.PunchScale, DoTweenProps.Instance.PunchDuration);
            _inventory.Cells[i].SetCell(spawnedSCB);
        }
    }
    private void ItemDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 100, _raycastLayerItems);
            if (_hit2D.collider != null)
            {
                if (_hit2D.collider.TryGetComponent(out IDraggable draggable))
                {
                    _currentDraggable = draggable;
                    _currentDraggable.OnDragStart();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_currentDraggable != null)
            {
                _hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 100, _raycastPutPlace);
                _currentDraggable.OnDragEnd(_players.GetCell((_hit2D.collider == null) ? null : _hit2D.collider.transform));

                _currentDraggable = null;
            }
        }
    }
    private void Collectable()
    {
        _hit2DCollectable = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 100, _raycastLayerCollectables);
        if (_hit2DCollectable.collider != null)
        {
            if (_hit2DCollectable.collider.TryGetComponent<ICollectable>(out ICollectable collectable))
            {
                collectable.CollectMe();
            }
        }
    }
    [Button("----- Delete Market -----")]
    public void ResetMarket(){

        foreach (MarketObject marketObject in _currentMarketObjects)
        {
            CreateDissapierSequence(marketObject);
        }
        _currentMarketObjects?.Clear();

    }
    private void CreateDissapierSequence(MarketObject item){
        if(item == null) return;
        Sequence sequence = DOTween.Sequence();

        sequence.Append(item.transform.DOShakePosition(DoTweenProps.Instance.NotEnoughMoneyDuration, DoTweenProps.Instance.NotEnoughMoneyPunch));
        sequence.AppendCallback(()=>{
            sequence.Join(item.transform.DOMoveY(-1, DoTweenProps.Instance.PurchasedItemSpeed));
            sequence.Join(item.GetComponentInChildren<SpriteRenderer>().DOFade(0, .1f));
        });
        sequence.AppendCallback(()=>{
            Destroy(item);
        });
        sequence.Play();
    }
}