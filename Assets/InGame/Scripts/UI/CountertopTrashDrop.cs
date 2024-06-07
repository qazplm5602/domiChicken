using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountertopTrashDrop : DropResponse
{
    protected override bool RequestItemDrop(bool winBoth, DragRequest dragRequest)
    {
        return true; // 모든걸 다 받고 아무것도 안하면 됨 ㅅㄱ
    }
}
