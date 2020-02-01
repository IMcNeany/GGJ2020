using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Sprite openedOpenerSprite;
    public List<OpenerAndDoor> openersAndRespectiveDoors = new List<OpenerAndDoor>();

    public GameObject ReturnDoorToOpen(GameObject opener)
    {
        var i = openersAndRespectiveDoors.FindIndex(x => x.opener == opener);
        return openersAndRespectiveDoors[i].doorToOpen;
    }
}
[Serializable]
public class OpenerAndDoor
{
    public GameObject doorToOpen;
    public GameObject opener;
}

