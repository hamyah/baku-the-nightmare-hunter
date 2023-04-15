using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonObject : SpawnableObject
{
    public override void OnTimerTriggered() {
        Destroy(gameObject);
    }
}
