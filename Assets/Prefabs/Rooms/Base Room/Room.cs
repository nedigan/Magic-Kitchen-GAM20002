using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Kitchen,
    Storage,
    Dining
};
public class Room : MonoBehaviour
{
    [SerializeField] 
    private Door _door;

    public List<Animal> Animals = new List<Animal>();
    public List<Item> Items = new List<Item>();
    public List<Station> Stations = new List<Station>();

    public RoomType RoomType;
    public Door Door => _door;
}
