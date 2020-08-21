using System.Collections;
using System.Collections.Generic;
using SGoap;
using SGoap.Services;
using UnityEditor;
using UnityEngine;

public class WoodCutter : BasicAgent
{
    public string WorkToDoState;

    private void Update()
    {
        if (ObjectManager<Resource>.Count == 0)
        {
            States.RemoveState(WorkToDoState);
        }
        else
        {
            States.SetState(WorkToDoState, 1);
        }
    }
}
