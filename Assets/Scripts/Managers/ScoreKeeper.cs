using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public GameObject levelEndMenuUI;
    public GameObject gameEndMenuUI;

    public GameObject[] targets;
    public int targetCount;

    private void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Target");
        targetCount = targets.Length;
        CheckTargetsRemaining();
    }

    public void CheckTargetsRemaining()
    {
        print(targetCount + " targets remaining");
    }

}


