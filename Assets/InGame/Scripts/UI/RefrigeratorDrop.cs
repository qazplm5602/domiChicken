using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorDrop : DropResponse
{
    [SerializeField] RefrigeratorSystem _system;

    protected override bool RequestItemDrop(bool winBoth, DragRequest dragRequest)
    {
        if (winBoth) return false; // 같은 창인디

        bool tryResult = dragRequest.TryGetComponent<IDomiItemHandler>(out var itemHandler);
        if (!tryResult) return false; // 아이템 불러오기 실패
    
        var item = itemHandler.GetDomiItem();
        if (item == null) return false; // 아이템 왜 없냐..

        _system.GiveInventory(item);
        
        return true;
    }
}
