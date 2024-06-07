using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FryerSystem : DropResponse, IDomiItemHandler
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI status;
    [SerializeField] Image icon;
    
    [SerializeField] ChickenItem changeItem;
    [SerializeField] ChickenItem failItem;

    DomiItem currentItem;
    float time;

    public DomiItem GetDomiItem() => currentItem;

    protected override bool RequestItemDrop(bool winBoth, DragRequest dragRequest)
    {
        if (currentItem != null) return false; // 튀김기에서 꺼내야 함니다~~

        bool tryItem = dragRequest.TryGetComponent<IDomiItemHandler>(out var itemHandler);
        if (!tryItem) return false;

        IngredientItem item = itemHandler.GetDomiItem() as IngredientItem;
        if (item?.ingredientType != IngredientItemType.RawChicken) return false;

        currentItem = item;
        time = 0;

        UpdateItemUI();
        status.text = "튀겨지는 중...";
        icon.color = Color.white;

        return true;
    }

    public void Reset()
    {
        currentItem = null;
        time = 0;
        
        title.text = "없음";
        status.text = "아무일도 없는 중...";
        icon.sprite = null;
        icon.color = new Color(1,1,1,0);
    }

    void UpdateItemUI() {
        title.text = currentItem.GetName();
        icon.sprite = currentItem.GetImage(true);
    }

    private void Update() {
        if (currentItem == null) return;

        bool isFry = currentItem is ChickenItem;
        
        time += Time.deltaTime;
        if (!isFry && time >= 5) { // 치킨으로 바꾸기
            currentItem = Instantiate(changeItem);
            status.text = "<color=red>과하게</color> 튀겨지는중...";
            
            UpdateItemUI();
        } else if (isFry && (currentItem as ChickenItem).GetChickenType() != ChickenItemType.WTF && time >= 30) { // 아니 오버쿡 ㄷㄷ
            currentItem = Instantiate(failItem);
            status.text = "너무 오랫동안 튀겼습니다.";

            UpdateItemUI();
        }
    }
}
