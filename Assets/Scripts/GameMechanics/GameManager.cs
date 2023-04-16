using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO lifetimeOverEventChanel;
    [SerializeField] private float objectLifetime = 30;
    [SerializeField] private float objectSpawnInterval = 30;
    [SerializeField] private float objectSpawnIntervalDecreaseAmount = 2;
    [SerializeField] private float minimumObjectSpawnInterval = 10;
    [SerializeField] private SingleSourceAudioManager firstSpawnAudioManager;
    [SerializeField] private SingleSourceAudioManager spawnAudioManager;

    
    public GameObject gameOver;
    
    public float totalTime;
    public GameObject memorizeText;
    public GameObject firstAnomalyText;
    
    private float _startTime;
    private float _elapsedTime;
    private bool _timerPlaying;

    private bool _gameFinished;
    private bool _anomaliesStarted = false;
    
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
        Time.timeScale = 1;
        
        _visitedRooms.Add(0);
        GetRoomsList();
        RoomChanger.RoomChangedEvent.AddListener(UpdateCurrentRoomIndex);

        StartCoroutine("StartingTexts");
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

    System.Collections.IEnumerator StartingTexts() {
        yield return new WaitForSeconds(1.5f);
        
        Instantiate(memorizeText);
    }

    void StartTimer()
    {
        Debug.Log("start timer");
        _timerPlaying = true;
        _startTime = Time.time;
    }

    void SpawnObject() {
        if (!_anomaliesStarted) {
            firstSpawnAudioManager.PlaySound();
            _anomaliesStarted = true;

            GameObject text = Instantiate(firstAnomalyText);
        }

        spawnAudioManager.PlaySound();

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

    private void OnLifetimeOver(int roomId)
    {
        GameObject.Find("Room Manager").GetComponent<RoomChanger>().GoToRoom(roomId);


        
        GameOver();
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 5f).setOnComplete(DelayGameOverScreen).setIgnoreTimeScale(true);
    }

    void DelayGameOverScreen() {
        Instantiate(gameOver);
    }

    private void GameOver()
    {
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
