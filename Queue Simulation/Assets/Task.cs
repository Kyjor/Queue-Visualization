using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task 

{
    //Length of task
    public float taskTime = 0;
    //Total Time in cycle
    public float elapsedTime = 0;
    //Time object is being worked on
    public float activeTime = 0;
    //Time object spends waiting in queue
    public float waitingTime = 0;


    //Record of time in station
    public float eaTime = 0;
    public float interestTime=0;
    public float loanTermsTime=0;
    public float finalIssueTime=0;
    
    //Waiting time
    public float wait1;
    public float wait2;
    public float wait3;
    public float wait4;

}
