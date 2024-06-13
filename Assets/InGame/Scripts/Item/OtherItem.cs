using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum OtherItemType {
    Knife,
    Package // 포장 도구
}

[CreateAssetMenu(menuName = "SO/Item/Other")]
public class OtherItem : DomiItem
{
    [field: SerializeField] public OtherItemType otherType { get; private set; }

    public override bool ItemEquals(DomiItem target)
    {
        OtherItem item = target as OtherItem;
        if (item == null) return false;

        return otherType == item.otherType;
    }

    // 포장 기능 관련
    public List<DomiItem> packItems = new();
    public override string GetDesc()
    {
        if (otherType != OtherItemType.Package || packItems.Count == 0)
            return base.GetDesc();
    
        StringBuilder builder = new();
        builder.AppendLine("아래와 같이 포장되어 있습니다.");
        
        for (int i = 0; i < packItems.Count; i++)
            builder.AppendLine($"    {packItems[i].GetName()}");

        return builder.ToString();
    }
}
