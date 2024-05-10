using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [Tooltip("Where the held item will be placed. If left empty will default to this component's parent")]
    public GameObject HoldLocation;
    [SerializeField]
    private Item _heldItem;

    public bool ClaimHeldItems = true;

    public Item HeldItem => _heldItem;
    public bool Empty => _heldItem == null;

    // Start is called before the first frame update
    void Awake()
    {
        if (HoldLocation == null) { HoldLocation = gameObject; }
        if (_heldItem != null) { PickUpItem(_heldItem); }
    }

    public void PickUpItem(Item item)
    {
        if (_heldItem != null) { DropCurrentItemOnGround(); }

        _heldItem = item;

        _heldItem.OnPickedUp(this);
        _heldItem.transform.SetParent(HoldLocation.transform);
        _heldItem.transform.localPosition = Vector3.zero;

        if (ClaimHeldItems) { _heldItem.Claimed = true; }
    }

    public void DropCurrentItemOnGround()
    {
        if (_heldItem != null) 
        {
            _heldItem.transform.SetParent(null, true);

            RemoveCurrentItem();
        }
    }

    public void RemoveCurrentItem()
    {
        if (_heldItem != null) 
        {
            _heldItem.OnPutDown();

            if (ClaimHeldItems) { _heldItem.Claimed = false; }

            _heldItem = null;
        }        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (HoldLocation != null)
        {
            //Gizmos.DrawSphere(HoldLocation.transform.position, 0.25f);
            Gizmos.DrawIcon(HoldLocation.transform.position, "Icon_ItemHolder");
        }
        else
        {
            //Gizmos.DrawSphere(transform.position, 0.25f);
            Gizmos.DrawIcon(transform.position, "Icon_ItemHolder");
        }
    }
}
