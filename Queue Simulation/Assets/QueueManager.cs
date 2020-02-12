using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueManager : MonoBehaviour
{
    public int groups = 3;
    public Queuer beginningQueue;
    public Queuer endQueue;
    public Text dayCountText;
    public Text dayTimeText;

    public int groupOneSimple;
    public int groupOneComplex; 
    
    public int groupTwoSimple;
    public int groupTwoComplex; 
    
    public int groupThreeSimple;
    public int groupThreeComplex;


    public float arrivalRate = .939220183f;
    public float timer = 0;
    public float elapsedTime;

    public int totalObjects;
    public List<GameObject> finishedTask;

    public int dayCount = 0;
    public float dayLength;
    public float dayTimer = 0;

   [Range(0,100f)] public float timeScale;

    // Start is called before the first frame update
    void Start()
    {
        totalObjects += groupOneSimple + groupOneComplex + groupTwoComplex + groupTwoSimple + groupThreeSimple + groupThreeComplex;
        dayCountText.text = "Day Count: " + dayCount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dayTimeText.text = "Time: " + dayTimer.ToString("#.#") + "/6.3";

        if (dayTimer < dayLength)
        {
            dayTimer += Time.deltaTime;
        }
        else if (dayTimer >= dayLength)
        {
            dayCount++;
            dayCountText.text = "Day Count: " + dayCount;

            dayTimer = 0;
        }
        if (endQueue.RequestedTasks.Count < totalObjects)
        {
            elapsedTime += Time.deltaTime;
        }
        Time.timeScale = timeScale;
        if (timer > arrivalRate)
        {
            if (groupOneComplex > 0 || groupOneSimple > 0 || groupTwoComplex > 0 || groupTwoSimple > 0 || groupThreeComplex > 0 || groupThreeSimple > 0)
            {
                AddItem();
                timer = 0;
                
            }
        }
        else 
        {
            timer += Time.deltaTime;
            
        }
    }

    void AddItem()
    {
       // print("AddItemAttempt");
        int groupChoice = Random.Range(0,3);
        int simpleOrComplex;
        switch (groupChoice) 
        {
            case 0:
                simpleOrComplex = Random.Range(0, 2);
                if (groupOneSimple > 0 && simpleOrComplex == 0)
                {
                    //create a new task
                    Task newTask = new Task();
                    newTask.groupNumber = 1;
                    newTask.simple = true;

                    beginningQueue.RequestedTasks.Add(newTask);
                    --groupOneSimple;
                    
                }
                else if (groupOneComplex > 0 && simpleOrComplex == 1)
                {
                    //create a new task
                    Task newTask = new Task();
                    newTask.groupNumber = 1;
                    newTask.simple = false;

                    beginningQueue.RequestedTasks.Add(newTask);
                    --groupOneComplex;
                 
                }
                else 
                {
                    AddItem();
                   
                }
                break;

            case 1:
                simpleOrComplex = Random.Range(0, 2);
                if (groupTwoSimple > 0 && simpleOrComplex == 0)
                {
                    //create a new task
                    Task newTask = new Task();
                    newTask.groupNumber = 2;
                    newTask.simple = true;

                    beginningQueue.RequestedTasks.Add(newTask);
                    --groupTwoSimple;

                }
                else if (groupTwoComplex > 0 && simpleOrComplex == 1)
                {
                    //create a new task
                    Task newTask = new Task();
                    newTask.groupNumber = 2;
                    newTask.simple = false;

                    beginningQueue.RequestedTasks.Add(newTask);
                    --groupTwoComplex;

                }
                else
                {
                    AddItem();

                }
                break;

            case 2:
                simpleOrComplex = Random.Range(0, 2);
                if (groupThreeSimple > 0 && simpleOrComplex == 0)
                {
                    //create a new task
                    Task newTask = new Task();
                    newTask.groupNumber = 3;
                    newTask.simple = true;

                    beginningQueue.RequestedTasks.Add(newTask);
                    --groupThreeSimple;

                }
                else if (groupThreeComplex > 0 && simpleOrComplex == 1)
                {
                    //create a new task
                    Task newTask = new Task();
                    newTask.groupNumber = 3;
                    newTask.simple = false;

                    beginningQueue.RequestedTasks.Add(newTask);
                    --groupThreeComplex;

                }
                else
                {
                    AddItem();

                }
                break;

        }

            

    }
}
