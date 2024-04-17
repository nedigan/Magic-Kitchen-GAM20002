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
    }
}
