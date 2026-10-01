using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterClickable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string characterId = "AOI";

    public void OnPointerClick(PointerEventData eventData)
    {
        FittingRoomPanelManager.Instance.ShowPanelFor(characterId);
    }
}