using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    public Transform roomHolder;
    public float currentRoom = 0;
    public float changeCooldown = 0.5f;
    private float lastChangeTime = 0;
    void Update() {
        float axis = Input.GetAxisRaw("Horizontal");
        if(axis != 0 && changeCooldown <= Time.time - lastChangeTime) {
            float nextRoom = currentRoom + axis;
            if(nextRoom < roomHolder.childCount && nextRoom >= 0) {
                SetRoom(nextRoom);
            } else {
                SetRoom(Mathf.Clamp(roomHolder.childCount - nextRoom, 0, roomHolder.childCount-1));
            }

            lastChangeTime = Time.time;
        }
    }

    void SetRoom(float i) {
        //FIXME: this is gonna be changed for animation
        Debug.Log(roomHolder.childCount);
        Debug.Log(360/roomHolder.childCount);
        Quaternion newAngle = Quaternion.identity;
        newAngle.eulerAngles = new Vector3(0, 0, (360/roomHolder.childCount)*i);
        roomHolder.rotation = newAngle;

        currentRoom = i;
    }
}
