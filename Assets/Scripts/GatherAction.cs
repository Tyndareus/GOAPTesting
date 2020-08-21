using System.Collections;
using Pathfinding;
using SGoap;
using SGoap.Services;
using UnityEngine;

public class GatherAction : BasicAction
{
    public string action;
    public override float StaggerTime => 0;
    public override float CooldownTime => 0;

    public Resource resource;

    public override bool PrePerform()
    {
        AIPath aiPath = AgentData.Agent.GetComponent<AIPath>();
        if (resource == null || !aiPath.reachedEndOfPath) return false;

        StartCoroutine(GatherRoutine());

        return base.PrePerform();
    }

    private IEnumerator GatherRoutine()
    {
        AIPath aiPath = AgentData.Agent.GetComponent<AIPath>();
        
        while (resource != null && resource.value >= 1)
        {
            if (!aiPath.reachedEndOfPath)
            {
                yield return null;
            }
            else
            {
                for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime) yield return null;
                
                resource.TakeResource();
                States.ModifyState(action, 1);
            }
        }

        ObjectManager<Resource>.Remove(resource);
        Destroy(resource.gameObject);
        resource = null;
        AgentData.Target = null;
    }

    public override EActionStatus Perform()
    {
        return resource == null ? EActionStatus.Success : EActionStatus.Running;
    }
}
