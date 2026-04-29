using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    // public event EventHandler<OnTestSuccessfulEventArgs> OnTest;
    public event EventHandler OnLMBDown;
    private Action TimerCallback;
    private float timer;
    public void LMBDown(object sender, EventArgs e)
    {
        Debug.Log("LMB Down by " + sender);
    }
    public void SetTimer(float duration, Action callback)
    {
        timer = duration;
        TimerCallback = callback;
    }
    // public class OnTestSuccessfulEventArgs : EventArgs
    // {
    //     public int num;
    // }
    // private int myNum;

    // void OnTestSuccessful(object sender, OnTestSuccessfulEventArgs e)
    // {
    //     Debug.Log("Test successful! , " + e.num);
    // }
    void Start()
    {
        // OnTest += OnTestSuccessful;
    }
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                TimerCallback();
            }
        }
    }
}
