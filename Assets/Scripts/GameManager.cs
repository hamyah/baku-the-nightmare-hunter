using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private EventChannelSO lifetimeOverEventChanel;
    [SerializeField] private float objectLifetime = 30;
    [SerializeField] private float objectSpawnInterval = 30;
    [SerializeField] private float objectSpawnIntervalDecreaseAmount = 2;
    [SerializeField] private float minimumObjectSpawnInterval = 10;

    
    public GameObject gameOver;
    
    public float totalTime;
    
    private float _startTime;
    private float _elapsedTime;
    private bool _gameFinished;
    
    private int _currentRoomIndex;
    private List<RoomSpawner> _rooms = new();

    
    private void OnEnable()
    {
        lifetimeOverEventChanel.OnEventRaised += OnLifetimeOver;
    }

    private void OnDisable()
    {
        lifetimeOverEventChanel.OnEventRaised -= OnLifetimeOver;
    }

    void Start() {
        _startTime = Time.time;
        _gameFinished = false;
        Time.timeScale = 1;

        GetRoomsList();
        RoomChanger.RoomChangedEvent.AddListener(UpdateCurrentRoomIndex);
    }
    
    void Update()
    {
        if (_gameFinished)
            return;
        
        if(!_gameFinished && totalTime <= Time.time - _startTime) {
            // Timer expired
            GameOver();
            return;
        }

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= objectSpawnInterval)
        {
            if (_rooms.Count == 0)
                return;
            
            // Spawn an object in a random room (that is not current room)
            _elapsedTime = 0;
            
            if (objectSpawnInterval - objectSpawnIntervalDecreaseAmount > minimumObjectSpawnInterval)
                objectSpawnInterval -= objectSpawnIntervalDecreaseAmount;

            int index;
            do {
                index = Random.Range(0, _rooms.Count);
            } while(index == _currentRoomIndex);
            
            _rooms[index].OnSpawnObject(objectLifetime, index);
        }

    }

    public void UpdateCurrentRoomIndex(int index)
    {
        _currentRoomIndex = index;
    }

    private void OnLifetimeOver()
    {
        GameOver();
    }

    private void GameOver()
    {
        Instantiate(gameOver);
        _gameFinished = true;
        Time.timeScale = 0;
    }

    private void GetRoomsList()
    {
        var roomObjects = GameObject.Find("Room Manager").GetComponent<RoomManager>().rooms;

        foreach (var roomObject in roomObjects)
        {
            _rooms.Add(roomObject.GetComponent<RoomSpawner>());
        }
    }
}
