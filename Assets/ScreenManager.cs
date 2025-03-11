using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] GameObject topCollider;
    [SerializeField] GameObject bottomCollider;
    [SerializeField] GameObject leftCollider;
    [SerializeField] GameObject rightCollider;
    [SerializeField] Camera mainCamera;
    private Vector3 _screenTouchPosition;
    public static event Action<Vector2> OnClick;

    private void Update()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            ConvertAndLogWorldPosition(touchPosition);
        }

        // Mouse kontrol (Sol tık için)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            ConvertAndLogWorldPosition(mousePosition);
        }
    }

    private void ConvertAndLogWorldPosition(Vector2 screenPosition)
    {
        _screenTouchPosition = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, mainCamera.nearClipPlane));
        OnClick?.Invoke(_screenTouchPosition);
        Debug.Log("World Position: " + _screenTouchPosition);
    }
    private void Start()
    {
        PositionColliders();
    }

    private void PositionColliders()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Vector2 topLeft = cam.ScreenToWorldPoint(new Vector3(0, screenHeight, 0));
        Vector2 bottomRight = cam.ScreenToWorldPoint(new Vector3(screenWidth, 0, 0));

        float worldWidth = bottomRight.x - topLeft.x;
        float worldHeight = topLeft.y - bottomRight.y;

        // Üst Kenar
        topCollider.transform.position = new Vector2(0, topLeft.y);
        topCollider.transform.localScale = new Vector2(worldWidth, 0.1f);
        SetBoxColliderSize(topCollider, worldWidth, 0.1f);

        // Alt Kenar
        bottomCollider.transform.position = new Vector2(0, bottomRight.y);
        bottomCollider.transform.localScale = new Vector2(worldWidth, 0.1f);
        SetBoxColliderSize(bottomCollider, worldWidth, 0.1f);

        // Sol Kenar
        leftCollider.transform.position = new Vector2(topLeft.x, 0);
        leftCollider.transform.localScale = new Vector2(0.1f, worldHeight);
        SetBoxColliderSize(leftCollider, 0.1f, worldHeight);

        // Sağ Kenar
        rightCollider.transform.position = new Vector2(bottomRight.x, 0);
        rightCollider.transform.localScale = new Vector2(0.1f, worldHeight);
        SetBoxColliderSize(rightCollider, 0.1f, worldHeight);
    }

    private void SetBoxColliderSize(GameObject obj, float width, float height)
    {
        BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
        if (col != null)
        {
            col.size = new Vector2(width, height);
        }
    }

}