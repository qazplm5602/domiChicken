using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DropResponseData {
    public bool result;
    public WindowType window;
}

public class DragDropManager : MonoSingleton<DragDropManager>
{
    DropResponseData responseData;

    public void SetResponseData(WindowType winType, bool result) {
        responseData.window = winType;
        responseData.result = result;
    }

    public ref DropResponseData GetResponseData() {
        return ref responseData;
    }
}
