using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomChanger : MonoBehaviour
{
    static public IntUnityEvent RoomChangedEvent;
    [SerializeField] private StereoAudioManager audioManager;
    
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
                audioManager.PlaySound(
                    nextRoom == 1 ?
                        StereoAudioManager.AudioTargetPosition.RIGHT :
                        StereoAudioManager.AudioTargetPosition.LEFT
                );
            } else {
                SetRoom(Mathf.Clamp(roomHolder.childCount - nextRoom, 0, roomHolder.childCount-1));
                audioManager.PlaySound(
                    nextRoom == 1 ?
                        StereoAudioManager.AudioTargetPosition.RIGHT :
                        StereoAudioManager.AudioTargetPosition.LEFT
                );
            }

            lastChangeTime = Time.time;
        }
    }

    void SetRoom(int i) {
        if((i > currentRoom && !(currentRoom == 0 && i == roomHolder.childCount-1)) || (currentRoom == roomHolder.childCount-1 && i == 0)) {
            //Debug.Log("rotate right");
            roomHolder.GetComponent<Animator>().Play("RotateRight");
        } else {
            //Debug.Log("rotate left");
            roomHolder.GetComponent<Animator>().Play("RotateLeft");
        }

        StartCoroutine("RoomChangedEventTrigger");


        currentRoom = i;
        _isMoving = -1;
    }

    public void GoToRoom(int roomId) {
        RoomManager roomManager = GetComponent<RoomManager>();
        /*AnimationClip clip = new AnimationClip();
        clip.SetCurve("Room Holder", typeof(Transform), "m_LocalRotation.z", AnimationCurve.EaseInOut(0, roomHolder.rotation.eulerAngles.z, 2, 360/roomManager.roomPrefabs.Count*roomId));

        AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController(roomHolder.GetComponent<Animator>().runtimeAnimatorController);
        roomHolder.GetComponent<Animator>().runtimeAnimatorController = animatorOverrideController;

        animatorOverrideController["GoTo"] = clip;
        roomHolder.GetComponent<Animator>().Play("GoTo");

        Debug.Log("go to room " + roomId + " " + 360/roomManager.roomPrefabs.Count*roomId);*/

        LeanTween.rotateZ(roomHolder.gameObject,360/roomManager.roomPrefabs.Count*roomId, 0.5f).setIgnoreTimeScale(true);

    }

    IEnumerator RoomChangedEventTrigger() {
        yield return new WaitForSeconds(0.2f);

        ThrowRoomChangedEvent();
    }

    public void ThrowRoomChangedEvent() {
        Debug.Log("room changed");
        RoomChangedEvent.Invoke(currentRoom);
    }

    public int MovingToIndex() {
        return _isMoving;
    }


    
}
