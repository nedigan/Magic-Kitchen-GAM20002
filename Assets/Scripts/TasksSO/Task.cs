using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Task : ScriptableObject
{
    public RoomType Room; // rooms this task is completed in
    public StationType Station; // stations this task is completed at
    public float Duration; // how long this task
}
