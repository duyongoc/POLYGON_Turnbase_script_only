using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// this class is used for counting time do something
[System.Serializable]
public class Timer
{

    // [private]
    [SerializeField] private float _timer = 0f;
    [SerializeField] private float _duration = 0f;
    [SerializeField] private bool _isPause = false;


    // [properties]
    public float GetTimer { get => _timer; }
    public float GetDuration { get => _duration; }
    public bool Pause { get => _isPause; set => _isPause = value; }



    public void Init(float time)
    {
        Reset();
        SetDuration(time);
    }


    public void SetDuration(float time)
    {
        _timer = time;
        _duration = time;
    }


    public void UpdateTime(float deltaTime)
    {
        if (_timer == 0 || _isPause)
        {
            _timer = 0;
            return;
        }

        _timer -= deltaTime;
        if (_timer < 0)
        {
            _timer = 0;
        }
    }


    public bool IsDone()
    {
        if (_timer == 0)
            return true;

        return false;
    }


    public void SetDone()
    {
        _timer = 0;
    }


    public void Reset()
    {
        _isPause = false;
        _timer = _duration;
    }

}
