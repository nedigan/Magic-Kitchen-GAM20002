using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    // this should only be used to aid in setting up test scenes
    // it should never be used in a finalised scene bc its way too slow
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
