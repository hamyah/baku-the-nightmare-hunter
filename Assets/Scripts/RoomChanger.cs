using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomChanger : MonoBehaviour
{
    static public IntUnityEvent RoomChangedEvent;
    
    public Transform roomHolder;
    public int currentRoom = 0;
    public float changeCooldown = 0.5f;
    private float lastChangeTime = 0;

    private void Awake()
    {
        RoomChangedEvent = new();
    }

    void Update() {
        int axis = (int) Input.GetAxisRaw("Horizontal");
        if(axis != 0 && changeCooldown <= Time.time - lastChangeTime) {
            int nextRoom = currentRoom + axis;
            if(nextRoom < roomHolder.childCount && nextRoom >= 0) {
                SetRoom(nextRoom);
            } else {
                SetRoom(Mathf.Clamp(roomHolder.childCount - nextRoom, 0, roomHolder.childCount-1));
            }

            lastChangeTime = Time.time;
        }
    }

    void SetRoom(int i) {
        // FIXME: this is gonna be changed for animation
        Debug.Log(roomHolder.childCount);
        Debug.Log(360/roomHolder.childCount);
        Quaternion newAngle = Quaternion.identity;
        newAngle.eulerAngles = new Vector3(0, 0, (360/roomHolder.childCount)*i);
        roomHolder.rotation = newAngle;

        currentRoom = i;
        RoomChangedEvent.Invoke(currentRoom);
    }
}
