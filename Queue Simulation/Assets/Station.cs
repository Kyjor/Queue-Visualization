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

    public Task currentTask;

    public int groupNumber;

    Color currentColor;

    float timer = 0;
    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = timer.ToString("#.##");
        if (currentTask == null)
        {
            GetComponent<Renderer>().material.color = Color.cyan;

            lastQueue.RetrieveTask(groupNumber);
        }
        if (currentTask != null)
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
