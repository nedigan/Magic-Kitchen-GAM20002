using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IRoomObject
    {
        public Room CurrentRoom { get; set; }
        public Vector3 Destination { get; }

        // Handle setting the current Room
        // and place the object in the correct place in the Hierarchy if needed
        public void SetCurrentRoom(Room room);
    }
}
