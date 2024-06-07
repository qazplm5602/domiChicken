using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FryerSystem : DropResponse
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI status;
    [SerializeField] Image icon;

    DomiItem currentItem;
    float time;


    protected override bool RequestItemDrop(bool winBoth, DragRequest dragRequest)
    {
        if (currentItem != null) return false; // 튀김기에서 꺼내야 함니다~~
        
        bool tryItem = dragRequest.TryGetComponent<IDomiItemHandler>(out var itemHandler);
        if (!tryItem) return false;

        IngredientItem item = itemHandler.GetDomiItem() as IngredientItem;
        if (item?.ingredientType != IngredientItemType.RawChicken) return false;

        currentItem = item;
        time = 0;

        title.text = item.GetName();
        status.text = "튀겨지는 중...";
        icon.sprite = item.GetImage(true);
        
        print($"RequestItemDrop {winBoth} {dragRequest}");
        return true;
    }

    private void Update() {
        if (currentItem == null) return;
        
        time += Time.deltaTime;
        if (time >= 5) {
            status.text = "끝...";
        }
    }
}
