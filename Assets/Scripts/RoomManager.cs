using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> rooms;
    public Transform roomHolder;
    void Awake() {
        /*GameObject newRoom;
        foreach (GameObject room in rooms) {
            newRoom = Instantiate(room, roomHolder);
            roomHolder.Rotate(0, 0, 45);
        }*/
    }
}
