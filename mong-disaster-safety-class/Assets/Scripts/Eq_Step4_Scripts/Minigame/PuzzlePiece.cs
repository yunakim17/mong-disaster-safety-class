using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 originalPosition;
    private bool isLocked = false;
    private bool hasBeenMoved = false;

    [HideInInspector]
    public string correctSlotName;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (isLocked) return;
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        if (isLocked) return;
        transform.position = GetMouseWorldPosition() + offset;
        hasBeenMoved = true; 
    }

    private void OnMouseUp()
    {
        if (isLocked || !hasBeenMoved) return;

        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.CompareTag("Slot") &&
                col.gameObject.name == correctSlotName)
            {
                transform.position = col.transform.position;
                isLocked = true;
                FindObjectOfType<PuzzleManager>().CheckPuzzleCompletion();
                return;
            }
        }

        transform.position = originalPosition;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screenMousePos = Input.mousePosition;
        screenMousePos.z = 10f;
        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }

    public bool IsLocked()
    {
        return isLocked;
    }
}
