using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryTruck : MonoBehaviour
{
    public StoreRoom StoreRoom;

    public int Cost = 100;

    private bool _delivering = false;
    private List<Item> _missingIngredients;

    private Vector2 _deliveriesArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            RequestDelivery();
        }
    }

    public void RequestDelivery()
    {
        if (_delivering) return;

        _delivering = true;

        MoneyHandler.RemoveMoney(Cost);

        _missingIngredients = SpawnMissingIngredients();
    }

    private List<Item> SpawnMissingIngredients()
    {
        List<Item> prefabs = StoreRoom.GetMissingStock();
        List<Item> missings = new();

        foreach (Item prefab in prefabs)
        {
            Item newItem = Instantiate(prefab);

            newItem.Claimed = true;

            newItem.transform.localPosition = new Vector3(Random.Range(-_deliveriesArea.x, _deliveriesArea.x), 0, Random.Range(-_deliveriesArea.y, _deliveriesArea.y));

            missings.Add(newItem);
        }

        return missings;
    }

    private void GenerateDeliverTasks()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(transform.position, new Vector3(_deliveriesArea.x * 2, 0.5f, _deliveriesArea.y * 2));
    }
}
