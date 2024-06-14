using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryDrop : DropResponse
{
    System.Action<List<DomiItem>> callback;

    public void Init(System.Action<List<DomiItem>> cb) {
        callback = cb;
    }

    protected override bool RequestItemDrop(bool winBoth, DragRequest dragRequest)
    {
        if (winBoth) return false;

        var itemHandler = dragRequest.GetComponent<IDomiItemHandler>();
        DomiItem item = itemHandler?.GetDomiItem();
        
        // 포장키트가 아니믄~~
        if (item == null || item.GetItemType() != ItemType.Other || (item as OtherItem)?.otherType != OtherItemType.Package) return false;
        
        callback.Invoke((item as OtherItem).packItems);
        callback = null;
        Destroy(transform.parent.gameObject);

        return true;
    }
}
