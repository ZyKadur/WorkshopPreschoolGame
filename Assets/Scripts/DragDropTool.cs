using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragDropTool : MonoBehaviour
{
    public enum ToolType
    {
        Arrosoir,
        Seed,
        Shovel
    }

    Collider2D collider2D;
    Vector3 offset;

    [SerializeField]
    private ToolType toolType;
    [SerializeField]
    private Plant seedType;

    private Vector3 startPosition;
    
    void Start ()
    {
        collider2D = GetComponent<Collider2D>();
        startPosition = transform.position;
    }

    Vector3 MouseWorldPosition ()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
    }

    public void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    public void OnMouseUp()
    {
        collider2D.enabled = false;
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit2D hitInfos;

        if (hitInfos = Physics2D.Raycast(rayOrigin, rayDirection))
            if (hitInfos.collider.gameObject.GetComponent<Crop>() != null)
                FindObjectOfType<DropInteractionManager>().DoInteraction(toolType, hitInfos.collider.gameObject.GetComponent<Crop>(), seedType);
            

        collider2D.enabled=true;

        transform.position = startPosition;
    }
}
