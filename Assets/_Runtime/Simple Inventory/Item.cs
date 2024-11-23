using UnityEngine;

public class Item : Entity
{
    [SerializeField] ItemData itemData;

    public ItemData ItemData
    {
        get
        {
            itemData.EntityData = EntityData;
            return itemData;
        }
        set => itemData = value;
    }

    public override void DestroyEntity()
    {
        base.DestroyEntity();
    }

    public override void ResetEntity()
    {
        base.ResetEntity();
    }
}

