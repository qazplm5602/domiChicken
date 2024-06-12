using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CountertopTableSystem : DropResponse, IDomiItemHandler
{
    [SerializeField] Image icon;
    [SerializeField] DomiItem knife;
    [SerializeField] CountertopRecipeBookSO[] recipeGroups;

    DragRequest myDragSys;
    DomiItem currentItem = null;

    List<CountertopRecipe> recipes;

    private void Awake() {
        myDragSys = GetComponent<DragRequest>();

        recipes = new();
        foreach (var group in recipeGroups)
            foreach (var item in group.recipes)
                recipes.Add(item);
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
    public void Remove() {
        currentItem = null;
        UpdateTableUI();
    }

    protected override bool RequestItemDrop(bool winBoth, DragRequest dragRequest)
    {
        if (myDragSys == dragRequest) return false; // 자기 자신이 자기 자신을 놓음 ???

        bool isKnife = winBoth && dragRequest is CountertopKnifeDrag;
        
        DomiItem item = dragRequest.GetComponent<IDomiItemHandler>()?.GetDomiItem();
        if (!isKnife) {
            if (item == null) return false; // 아이템 못찾음
            
            if (currentItem == null) { // 조합에 아무것도 없으면 걍 됨 ( 소스 제외 )
                if (item.GetItemType() == ItemType.Source) return false;
                
                currentItem = item;
                UpdateTableUI();
                return true;
            }
        } else {
            if (currentItem == null) return false;
            item = knife;
        }


        // 조합법 찾고 할 예정
        CountertopRecipe recipe = recipes.Find(v => (currentItem.ItemEquals(v.item1) || currentItem.ItemEquals(v.item2)) && (item.ItemEquals(v.item1) || item.ItemEquals(v.item2)) && (v.item1.ItemEquals(v.item2) == currentItem.ItemEquals(item)));
        if (recipe.finalItem == null) return false;

        // 만약 소스면
        if (item.GetItemType() == ItemType.Source) {
            if ((item as SourceItem).GetCurrentSize() <= 0) return false; // 소스안에 소스가 없음
            RefrigeratorSystem.Instance.sourceUseDropped = true; // 소스 드랍 햇오
        }

        print($"조합 완료. {recipe.item1} + {recipe.item2} = {recipe.finalItem}");

        currentItem = recipe.finalItem;
        UpdateTableUI();

        return true;
    }
}
