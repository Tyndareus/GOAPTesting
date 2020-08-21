using System.Collections;
using System.Collections.Generic;
using SGoap.Services;
using UnityEngine;

public class GoToResource : AstarNavAction
{
    public GatherAction gatherAction;
    private Resource resource;

    public override bool IsUsable()
    {
        resource = ObjectManager<Resource>.FindClosest(AgentData.Position);

        if (resource == null)
        {
            return false;
        }

        TargetTransform = resource.transform;
        AgentData.Target = TargetTransform;

        return base.IsUsable();
    }

    public override bool PostPerform()
    {
        gatherAction.resource = resource;

        return base.PostPerform();
    }
}
