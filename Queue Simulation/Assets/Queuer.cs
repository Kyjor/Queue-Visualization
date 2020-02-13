using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Queuer : MonoBehaviour
{
    public List<Task> RequestedTasks= new List<Task>();
    public Station[] NextStation;

    public Text numberInQText;
    QueueManager queueManager;
    private void Awake()
    {
        numberInQText = GetComponentInChildren<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        queueManager = FindObjectOfType<QueueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        numberInQText.text = "" + RequestedTasks.Count;
        if(RequestedTasks.Count > 0)
        {
            GetComponent<Renderer>().material.color = Color.green;

        }
        else if(RequestedTasks.Count == 0)
        {
            GetComponent<Renderer>().material.color = Color.gray;

        }
        if(RequestedTasks.Count == queueManager.totalObjects)
        { queueManager.timeScale = 0; }
    }

    public void RetrieveTask(int group, bool any)
    {
      //  int removeItem;
        foreach (Task task in RequestedTasks)
        {
           // print("HI");

            if (task.groupNumber == group && !any && group != 0)
            {
                NextStation[group - 1].ChooseNormalNumber(task.simple);
                NextStation[group - 1].currentTask = task;
                //print("Hello");
                RequestedTasks.Remove(task);
                break;
            }
            else if(group == 0 || group == 4 && !any)
            {
                if (group == 0)
                {
                    NextStation[group].ChooseNormalNumber(task.simple);
                    NextStation[group].currentTask = task;
                }
                if (group == 4)
                {
                    NextStation[1].ChooseNormalNumber(task.simple);
                    NextStation[1].currentTask = task;
                }

                //print("Hello");
                RequestedTasks.Remove(task);
                break;
            }
            else 
            {
                NextStation[group - 1].ChooseNormalNumber(task.simple);
                NextStation[group - 1].currentTask = task;
                //print("Hello");
                RequestedTasks.Remove(task);
                break;
            }
        }
    }
}
