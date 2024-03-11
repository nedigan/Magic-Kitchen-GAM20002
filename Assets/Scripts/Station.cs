using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StationType
{
    Stove, //for cooking
    Shelf, //for grabbing ingredients
    Table //for serving food
}
public class Station : MonoBehaviour
{
    public StationType _type;
}
