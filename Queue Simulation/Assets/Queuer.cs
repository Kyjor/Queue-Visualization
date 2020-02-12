using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Queuer : MonoBehaviour
{
    public List<Task> RequestedTasks= new List<Task>();
    public Station[] NextStation;

    public Text numberInQText;
    private void Awake()
    {
        numberInQText = GetComponentInChildren<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void RetrieveTask(int group)
    {
      //  int removeItem;
        foreach (Task task in RequestedTasks)
        {
           // print("HI");

            if (task.groupNumber == group)
            {
                
                NextStation[group - 1].currentTask = task;
                //print("Hello");
                RequestedTasks.Remove(task);
                break;
            }
            else if(group == 0 || group == 4)
            {
                if(group == 0)
                NextStation[group].currentTask = task;
                if(group == 4)
                NextStation[1].currentTask = task;

                //print("Hello");
                RequestedTasks.Remove(task);
                break;
            }
        }
    }
}
