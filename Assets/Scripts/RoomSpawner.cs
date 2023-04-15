using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomSpawner : MonoBehaviour
{
    private List<SpawnableObject> roomObjects;

    private GameObject _currentSpawnedObject;
    private int _numObjects;

    private void Start()
    {
        roomObjects = new();
        roomObjects.AddRange(transform.GetComponentsInChildren<SpawnableObject>());
        Debug.Log(transform.GetComponentsInChildren<SpawnableObject>().Length);
        Debug.Log(roomObjects.Count);
        
        
        _numObjects = roomObjects.Count;
        Debug.Log(gameObject);
    }

    public void OnSpawnObject(float objectLifetime, int roomIndex)
    {
        Debug.Log(gameObject);
        Debug.Log(roomObjects.Count);
        //List<SpawnableObject> nonActivatedSpawnableObjects = roomObjects.Where(obj => !obj.IsActivated()).ToList();
        List<SpawnableObject> nonActivatedSpawnableObjects = new();
        foreach (SpawnableObject obj in roomObjects) {
            if(!obj.IsActivated()) nonActivatedSpawnableObjects.Add(obj);
        }

        if(nonActivatedSpawnableObjects.Count == 0) {
            Debug.Log("Max objects in room reached");
            return;
        }


        int index = Random.Range(0, nonActivatedSpawnableObjects.Count);
        Debug.Log(index);
        //_currentSpawnedObject = Instantiate(objectPrefabs[index], GameObject.Find("Item Holder " + roomIndex).transform);

        nonActivatedSpawnableObjects[index].GetComponent<SpawnableObject>().Activate(objectLifetime);
    }



}
