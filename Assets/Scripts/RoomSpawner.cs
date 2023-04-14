using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectPrefabs;

    private GameObject _currentSpawnedObject;
    private int _numObjects;

    private void Start()
    {
        _numObjects = objectPrefabs.Count;
    }


    public void OnSpawnObject(float objectLifetime)
    {
        int index = Random.Range(0, _numObjects);
        _currentSpawnedObject = Instantiate(objectPrefabs[index]);
        
        _currentSpawnedObject.GetComponent<SpawnableObject>().SetLifetime(objectLifetime);
    }
    


}
