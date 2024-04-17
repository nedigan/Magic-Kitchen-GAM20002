using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class RoomFinder
    {
        public static Room FindRoomAbove(GameObject gameObject)
        {
            GameObject nextParent = gameObject.transform.parent.gameObject; // why is Unity like this? why?
            Room foundRoom = null;

            while (foundRoom == null && nextParent != null)
            {
                if (nextParent.TryGetComponent(out foundRoom)) { break; }

                nextParent = nextParent.transform.parent.gameObject;
            }

            if (foundRoom == null)
            {
                Debug.LogWarning($"{gameObject} does not have a Current Room. Make sure it is below a Room in the Hierarchy and/or CurrentRoom is not null");
            }

            return foundRoom;
        }
    }
}
