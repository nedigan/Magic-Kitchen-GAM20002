using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] 
    private Door _door;

    public List<Animal> Animals = new List<Animal>();
    public List<Item> Items = new List<Item>();
    public List<Station> Stations = new List<Station>();

    public Door Door => _door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
