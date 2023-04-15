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
    private int _isMoving = -1;

    private void Awake()
    {
        RoomChangedEvent = new();
    }

    void Update() {
        int axis = (int) Input.GetAxisRaw("Horizontal");
        if(axis != 0 && changeCooldown <= Time.time - lastChangeTime) {
            int nextRoom = currentRoom + axis;
            _isMoving = nextRoom;
            if(nextRoom < roomHolder.childCount && nextRoom >= 0) {
                SetRoom(nextRoom);
            } else {
                SetRoom(Mathf.Clamp(roomHolder.childCount - nextRoom, 0, roomHolder.childCount-1));
            }

            lastChangeTime = Time.time;
        }
    }

    void SetRoom(int i) {
        if((i > currentRoom && !(currentRoom == 0 && i == roomHolder.childCount-1)) || (currentRoom == roomHolder.childCount-1 && i == 0)) {
            Debug.Log("rotate right");
            roomHolder.GetComponent<Animator>().Play("RotateRight");
        } else {
            Debug.Log("rotate left");
            roomHolder.GetComponent<Animator>().Play("RotateLeft");
        }

        currentRoom = i;
        RoomChangedEvent.Invoke(currentRoom);
        _isMoving = -1;
    }

    public int MovingToIndex() {
        return _isMoving;
    }
}
