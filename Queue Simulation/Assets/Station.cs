using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Station : MonoBehaviour
{
    public Queuer lastQueue;
    public Queuer nextQueue;

    public double stationTimeSimple;
    
    public double stationTimeMeanSimple;
    public double stationSDSimple;

    public double stationTimeComplex;

    public double stationTimeMeanComplex;
    public double stationSDComplex;


    public double minimumTime;
    public double maximumTime;
    public bool working;
    public bool waiting;
    public double maxDay;
    public double dayTimer = 0;

    public bool acceptAnyTask;

    public Task currentTask;

    public int groupNumber;

    Color currentColor;

    double timer = 0;
    public Text timerText;
    int currentDay = 0;
     QueueManager queueManager;

    public double waitingTimeElapsed = 0;
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
        if (currentTask == null && !waiting /*&& !currentTask.isActive*/)
        {
            waitingTimeElapsed += Time.deltaTime;
            GetComponent<Renderer>().material.color = Color.cyan;
          
            RetrieveTask();
        }
        if (currentTask != null && !waiting /*&& currentTask.isActive*/)
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
                print("Hello");

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

    void RetrieveTask()
    {
        //  int removeItem;
        foreach (Task task in lastQueue.RequestedTasks)
        {
            // print("HI");

            if (acceptAnyTask)
            {
                ChooseNormalNumber(task.simple);
                currentTask = task;
                //print("Hello");
                lastQueue.RequestedTasks.Remove(task);
                break;
            }
            else if (!acceptAnyTask)
            {
                if(task.groupNumber == groupNumber)
                {
                    ChooseNormalNumber(task.simple);
                    currentTask = task;
                    lastQueue.RequestedTasks.Remove(task);
                    break;
                }
                else if(task.groupNumber != groupNumber)
                {
                    continue;
                }
            }
            
        }
    }
    void ChooseNormalNumber(double mean, double stdDev, double min, double max, ref double times)
    {
        System.Random rand = new System.Random(); //reuse this if you are generating many
        double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = System.Math.Sqrt(-2.0 * System.Math.Log(u1)) *
                     System.Math.Sin(2.0 * System.Math.PI * u2); //random normal(0,1)
        double randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

        randNormal = System.Math.Min(randNormal, max);
        randNormal = System.Math.Max(randNormal, min);

        times = randNormal;


        
    }

    public void ChooseNormalNumber(bool simple)
    {
        double mean = 0,  stdDev = 0,  min = minimumTime,  max = maximumTime,  times;
        if(simple)
        {
            mean = stationTimeMeanSimple;
            stdDev = stationSDSimple;
        }
        else if(!simple)
        {
            mean = stationTimeMeanComplex;
            stdDev = stationSDComplex;
        }
        System.Random rand = new System.Random(); //reuse this if you are generating many
        double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = System.Math.Sqrt(-2.0 * System.Math.Log(u1)) *
                     System.Math.Sin(2.0 * System.Math.PI * u2); //random normal(0,1)
        double randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

        randNormal = System.Math.Min(randNormal, max);
        randNormal = System.Math.Max(randNormal, min);

        if (simple)
        {
            stationTimeSimple = randNormal;
        }
        else if (!simple)
        {
            stationTimeComplex = randNormal;
        }


        }
}
