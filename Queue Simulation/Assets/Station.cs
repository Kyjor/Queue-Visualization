using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Station : MonoBehaviour
{
    public Queuer lastQueue;
    public Queuer nextQueue;
    public float stationTimeSimple;
    public float stationSDSimple;
    public float stationTimeComplex;
    public float stationSDComplex;
    public float minimumTime;
    public float maximumTime;
    public bool working;
    public bool waiting;
    public float maxDay;
    public float dayTimer = 0;

    public bool acceptAnyTask;

    public Task currentTask;

    public int groupNumber;

    Color currentColor;

    float timer = 0;
    public Text timerText;
    int currentDay = 0;
     QueueManager queueManager;
    private void Awake()
    {
        queueManager = FindObjectOfType<QueueManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dayTimer < maxDay)
        {
            dayTimer += Time.deltaTime;

        }
        else if (dayTimer >= maxDay)
        {
            //timer = 0;
            GetComponent<Renderer>().material.color = Color.grey;

            waiting = true;
        }
        if(currentDay < queueManager.dayCount)
        {
            currentDay = queueManager.dayCount;
            dayTimer = 0;
            waiting = false;
        }


        timerText.text = timer.ToString("#.##");
        if (currentTask == null && !waiting)
        {
            GetComponent<Renderer>().material.color = Color.cyan;

            lastQueue.RetrieveTask(groupNumber,acceptAnyTask);
        }
        if (currentTask != null && !waiting)
        {
           
            if(currentTask.simple)
            {
                GetComponent<Renderer>().material.color = Color.red;

                if (timer < stationTimeSimple)
                {
                    timer += Time.deltaTime;
                }
                else if (timer >= stationTimeSimple)
                {
                    MoveToNextQueue();
                    timer = 0;
                }
            }
            else if (!currentTask.simple)
            {
                GetComponent<Renderer>().material.color = Color.yellow;

                if (timer < stationTimeComplex)
                {
                    timer += Time.deltaTime;
                }
                else if (timer >= stationTimeComplex)
                {
                    MoveToNextQueue();
                    timer = 0;
                }
            }

        }
        //print (currentTask);
    }

    void MoveToNextQueue()
    {
        nextQueue.RequestedTasks.Add(currentTask);
        currentTask = null;
    }

    void SetTime()
    {

    }
}
