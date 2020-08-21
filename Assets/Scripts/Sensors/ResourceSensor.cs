using System.Collections;
using System.Collections.Generic;
using SGoap;
using UnityEngine;

public class ResourceSensor : MonoBehaviour, IDataBind<AgentBasicData>
{
    public Color GizmozColor = Color.red;

    public float MaxRange = 8;
    public float MinRange = 1;

    [Effect]
    public State State;

    private AgentBasicData _agentData;

    private void Update()
    {
        if (_agentData.Target == null)
        {
            _agentData.Agent.States.RemoveState(State.Key);
            return;
        }
        
        if (_agentData.DistanceToTarget <= MaxRange)
        {
            _agentData.Agent.States.SetState(State.Key, 1);
        }
        else
        {
            _agentData.Agent.States.RemoveState(State.Key);
        }
    }

    public void Bind(AgentBasicData data)
    {
        _agentData = data;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmozColor;
        Gizmos.DrawWireSphere(transform.position, MinRange);
        Gizmos.DrawWireSphere(transform.position, MaxRange);
    }
}
