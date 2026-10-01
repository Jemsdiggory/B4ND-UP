using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class DraggableClothUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Camera worldCamera;
    [SerializeField] LayerMask dropZoneLayer;
    [SerializeField] string category = "Cloth";
    [SerializeField] string characterId = "AOI";
    [SerializeField] GameObject linkedOnBodyObject;

    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    Canvas rootCanvas;

    Transform originalParent;
    int originalSiblingIndex;
    Vector2 originalAnchoredPosition;

    void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        rootCanvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = rectTransform.parent;
        originalSiblingIndex = rectTransform.GetSiblingIndex();
        originalAnchoredPosition = rectTransform.anchoredPosition;

        canvasGroup.blocksRaycasts = false;
        rectTransform.SetParent(rootCanvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform parentRect = rectTransform.parent as RectTransform;
        Camera cam = rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : rootCanvas.worldCamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            eventData.position,
            cam,
            out Vector2 localPoint);

        rectTransform.anchoredPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        Vector3 worldPos = worldCamera.ScreenToWorldPoint(eventData.position);
        Collider2D zoneHit = Physics2D.OverlapPoint(worldPos, dropZoneLayer);
        if (zoneHit != null && zoneHit.TryGetComponent(out DropZone zone) && zone.CharacterId == characterId)
        {
            zone.Equip(linkedOnBodyObject, category);
        }

        rectTransform.SetParent(originalParent, false);
        rectTransform.SetSiblingIndex(originalSiblingIndex);
        rectTransform.anchoredPosition = originalAnchoredPosition;
    }
}