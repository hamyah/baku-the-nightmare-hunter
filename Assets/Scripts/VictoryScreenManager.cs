using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject celestialHolder;
    private bool sunCameUp = false;
    private Animator animator;


    void Awake()
    {
        animator = celestialHolder.GetComponent<Animator>();
    }

    void Update()
    {
        /*if(Input.anyKeyDown && !sunCameUp) {
            animator.Play("NightToDay");
            sunCameUp = true;
        }
        else if(Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(0);
        }*/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }
}


