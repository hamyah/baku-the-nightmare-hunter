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
    [SerializeField] private GameManagerAudioManager audioManager;

    
    public GameObject gameOver;
    
    public float totalTime;
    
    private float _startTime;
    private float _elapsedTime;
    private bool _timerPlaying;
    private bool _anomaliesStarted;

    private int _currentRoomIndex;
    private List<RoomSpawner> _rooms = new();
    private List<int> _visitedRooms = new();

    
    private void OnEnable()
    {
        lifetimeOverEventChanel.OnEventRaised += OnLifetimeOver;
    }

    private void OnDisable()
    {
        lifetimeOverEventChanel.OnEventRaised -= OnLifetimeOver;
    }

    void Start() 
    {
        _visitedRooms.Add(0);
        GetRoomsList();
        RoomChanger.RoomChangedEvent.AddListener(UpdateCurrentRoomIndex);
    }
    
    void Update()
    {
        if (!_timerPlaying)
            return;
        
        if(_timerPlaying && totalTime <= Time.time - _startTime) {
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
            
            if (objectSpawnInterval - objectSpawnIntervalDecreaseAmount > minimumObjectSpawnInterval) {
                objectSpawnInterval -= objectSpawnIntervalDecreaseAmount;
            }

            SpawnObject();
        }

    }

    void StartTimer()
    {
        Debug.Log("start timer");
        _timerPlaying = true;
        _startTime = Time.time;
        Time.timeScale = 1;
    }

    void SpawnObject() {
        if (!_anomaliesStarted) {
            audioManager.PlayFirstObjectSpawn();
            _anomaliesStarted = true;
        }

        int index;
        do {
            index = Random.Range(0, _rooms.Count);
        } while(index == _currentRoomIndex);
        
        _rooms[index].OnSpawnObject(objectLifetime, index);
    }

    public void UpdateCurrentRoomIndex(int index)
    {
        _currentRoomIndex = index;
        UpdateVisitedRooms(_currentRoomIndex);
    }

    private void OnLifetimeOver()
    {
        GameOver();
    }

    private void GameOver()
    {
        Instantiate(gameOver);
        _timerPlaying = false;
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
    
    private void UpdateVisitedRooms(int roomIndex)
    {
        if (!_visitedRooms.Contains(roomIndex))
            _visitedRooms.Add(roomIndex);
        
        if (_visitedRooms.Count == _rooms.Count)  // Start timer for spawning objects once all rooms have been visited at least once
            StartTimer();
    }
}
