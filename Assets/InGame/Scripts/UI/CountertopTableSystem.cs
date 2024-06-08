using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountertopTableSystem : DropResponse, IDomiItemHandler
{
    DragRequest myDragSys;
    [SerializeField] Image icon;

    DomiItem currentItem = null;

    private void Awake() {
        myDragSys = GetComponent<DragRequest>();
    }

    public DomiItem GetDomiItem() => currentItem;

    public void UpdateTableUI() {
        if (currentItem == null) {
            icon.color = new Color(1,1,1,0);
            return;
        }

        icon.color = Color.white;
        icon.sprite = currentItem.GetImage(true);
    }
    
    public void Sliced() {
        print("Sliced!!");
    }

    public void Remove() {
        currentItem = null;
        UpdateTableUI();
    }

    protected override bool RequestItemDrop(bool winBoth, DragRequest dragRequest)
    {
        if (myDragSys == dragRequest) return false; // 자기 자신이 자기 자신을 놓음 ???
        
        if (winBoth && dragRequest is CountertopKnifeDrag) {
            Sliced();
            return true;
        }

        DomiItem item = dragRequest.GetComponent<IDomiItemHandler>()?.GetDomiItem();
        if (item == null) return false; // 아이템 못찾음
        
        if (currentItem == null) { // 조합에 아무것도 없으면 걍 됨 ( 소스 제외 )
            if (item.GetItemType() == ItemType.Source) return false;
            
            currentItem = item;
            UpdateTableUI();
            return true;
        }

        // 조합법 찾고 할 예정

        return false;
    }
}