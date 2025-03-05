using UnityEngine;

public interface IDraggable
{
    void OnDragStart();
    void OnDrag(Vector3 newPos);
    void OnDragEnd(Cell cell);
}