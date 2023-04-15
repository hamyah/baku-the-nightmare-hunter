using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> roomPrefabs;
    public List<GameObject> rooms;
    public Transform roomHolder;

    void Awake() {
        GameObject newRoom, newRoomItemHolder;
        //int i = 0;
        foreach (GameObject room in roomPrefabs) {
            newRoom = Instantiate(room);
            newRoom.transform.SetParent(roomHolder);

            /*
            newRoomItemHolder = new GameObject("Item Holder " + i);
            newRoomItemHolder.transform.SetParent(roomHolder);
            newRoomItemHolder.AddComponent<Canvas>();
            newRoomItemHolder.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            */

            roomHolder.Rotate(0, 0, 360/roomPrefabs.Count);
            //i++;

            rooms.Add(newRoom);
        }
    }
    
}
