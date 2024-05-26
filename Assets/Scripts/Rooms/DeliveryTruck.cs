using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Room))]
public class DeliveryTruck : MonoBehaviour
{
    private Room _room;
    private TaskManager _taskManager;
    private MoneyHandler _moneyHandler;
    
    public StoreRoom StoreRoom;

    public int Cost = 100;
    public KeyCode Key = KeyCode.P;

    private bool _delivering = false;
    private List<Item> _missingIngredients;

    [SerializeField]
    private Vector2 _deliveriesArea;
    [SerializeField]
    private Vector3 _deliveriesPosition;

    public List<Animal> Deliverers = new();
    public List<Station> ExitSpots;

    private int _deliverersReadyToLeave = 0;
    private int _deliverersSent = 0;

    [SerializeField]
    private SimpleMover _mover;

    public Transform TablePosition;
    public Transform WaitPosition;

    // Start is called before the first frame update
    void Start()
    {
        _room = GetComponent<Room>();
        _taskManager = FindFirstObjectByType<TaskManager>();
        _mover.transform.position = WaitPosition.position - _mover.Offset;
        _moneyHandler = FindFirstObjectByType<MoneyHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            RequestDelivery();
            //GenerateDeliverTasks();
        }
    }

    public void RequestDelivery()
    {
        if (_delivering) { return; }

        _missingIngredients = SpawnMissingIngredients();

        if (_missingIngredients.Count == 0) { return; }

        _delivering = true;

        _moneyHandler.RemoveMoney(Cost);

        _deliverersReadyToLeave = 0;

        DriveToTable();
    }

    private void OnReachedTable(object sender, EventArgs eventArgs)
    {
        _mover.ReachedDestination -= OnReachedTable;
        GenerateDeliverTasks();
    }

    private void OnReachedWaitPosition(object sender, EventArgs eventArgs)
    {
        _mover.ReachedDestination -= OnReachedWaitPosition;
        _delivering = false;
    }

    private List<Item> SpawnMissingIngredients()
    {
        List<Item> missings = StoreRoom.GenerateMissingStockItems();

        foreach (Item newItem in missings)
        {
            newItem.Claimed = true;

            newItem.transform.position =
                transform.position + _deliveriesPosition +
                new Vector3(
                    Random.Range(-_deliveriesArea.x, _deliveriesArea.x),
                    0,
                    Random.Range(-_deliveriesArea.y, _deliveriesArea.y)
                    );

            newItem.SetCurrentRoom(_room);
        }

        return missings;
    }

    private void GenerateDeliverTasks()
    {
        //int tasksGenerated = 0;
        //Stack<Item> itemStack = new Stack<Item>();
        //foreach (Item item in _missingIngredients)
        //{
        //    itemStack.Push(item);

        //    if (itemStack.Count >= Deliverers[0].ItemHolder.MaxItems)
        //    {
        //        DelivererCollect delivererCollect = ScriptableObject.CreateInstance<DelivererCollect>();
        //        delivererCollect.SetUp(this, item);

        //        _taskManager.ManageTask(delivererCollect);

        //        tasksGenerated++;
        //    }            
        //}

        //List <Item[]> = _missingIngredients.Select((item, index) => new { index, item })
        //               .GroupBy(x => x.index % parts)
        //               .Select(x => x.Select(y => y.item));

        int parts = (int)Math.Ceiling((float)_missingIngredients.Count / (float)Deliverers[0].ItemHolder.MaxItems);

        List<IEnumerable<Item>> chunks = _missingIngredients.Select((item, index) => new { index, item }).GroupBy(x => x.index % parts).Select(x => x.Select(y => y.item)).ToList();

        foreach (IEnumerable chunk in chunks)
        {
            Stack<Item> stack = new Stack<Item>();
            foreach (Item item in chunk)
            {
                stack.Push(item);
                _moneyHandler.RemoveMoney(item.Price);
            }

            Item first = stack.Pop();

            DelivererCollect delivererCollect = ScriptableObject.CreateInstance<DelivererCollect>();
            delivererCollect.SetUp(this, first, stack);

            _taskManager.ManageTask(delivererCollect);
        }


        _deliverersSent = Math.Clamp(Deliverers.Count, 0, chunks.Count);

        // set up deliverer Default Tasks
        foreach (Animal deliverer in Deliverers)
        {
            DelivererReturnToTruck delivererReturnToTruck = ScriptableObject.CreateInstance<DelivererReturnToTruck>();
            delivererReturnToTruck.Setup(deliverer, this);

            deliverer.TaskHolder.DefaultTask = delivererReturnToTruck;
        }
    }

    public void DelivererReadyToLeave()
    {
        _deliverersReadyToLeave += 1;

        if (_deliverersReadyToLeave >= _deliverersSent)
        {
            DriveAway();
        }
    }

    private void DriveAway()
    {
        Debug.Log("Truck is driving away!");
        _mover.GoTo(WaitPosition);
        _mover.ReachedDestination += OnReachedWaitPosition;

    }

    private void DriveToTable()
    {
        _mover.GoTo(TablePosition);
        _mover.ReachedDestination += OnReachedTable;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(transform.position + _deliveriesPosition, new Vector3(_deliveriesArea.x * 2, 0.5f, _deliveriesArea.y * 2));
    }
}
