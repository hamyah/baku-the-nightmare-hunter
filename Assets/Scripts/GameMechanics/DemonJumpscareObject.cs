using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonJumpscareObject : SpawnableObject
{
    public GameObject jumpscare;
    private int _timesSeen;
    private int _currentRoomIndex;

    void Start() {
        base.Start();

        RoomChanger.RoomChangedEvent.AddListener(UpdateCurrentRoomIndex);

        _timesSeen = 0;
    }

    public override void OnTimerTriggered() {
        //Deactivate();
    }

    void UpdateCurrentRoomIndex(int index) {
        _currentRoomIndex = index;
        if(_isActivated && transform.parent.parent.parent.GetChild(_currentRoomIndex) == transform.parent.parent) {
            if(++_timesSeen >= 2) {
                Instantiate(jumpscare);
                lifetimeOverEventChanel.RaiseEvent(transform.parent.parent.GetSiblingIndex());
            }
        }
    }

    void OnDestroy() {
        RoomChanger.RoomChangedEvent.RemoveListener(UpdateCurrentRoomIndex);
    }
}
