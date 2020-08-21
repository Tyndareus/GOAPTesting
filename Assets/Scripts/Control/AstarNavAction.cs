using Pathfinding;
using SGoap;
using UnityEngine;

public class AstarNavAction : BasicAction
{
    public AIPath Agent;
    public Transform TargetTransform;
    public Vector3 TargetPosition;

    public float StopDistance = 2;
    public float Duration = 0;

    public override bool TrackStopWatch => false;

    public override bool PrePerform()
    {
        Vector3 target = Vector3.zero;
        if (TargetTransform != null)
        {
            target = TargetTransform.position;
        }
        else if (TargetPosition != Vector3.zero)
        {
            target = TargetPosition;
        }

        if (target == Vector3.zero) return false;

        Agent.destination = target;

        return base.PrePerform();
    }

    public override EActionStatus Perform()
    {
        Vector3 target = Vector3.zero;
        if (TargetTransform != null)
        {
            target = TargetTransform.position;
        }
        else if (TargetPosition != Vector3.zero)
        {
            target = TargetPosition;
        }

        if (target == Vector3.zero) return EActionStatus.Failed;

        Agent.destination = target;

        if (HasBegun())
            Stopwatch.Start();

        if (Stopwatch.IsRunning && Stopwatch.Elapsed.TotalSeconds >= Duration)
            return EActionStatus.Success;
        
        return EActionStatus.Running;
    }

    private bool HasBegun()
    {
        float distance = Vector3.Distance(Agent.transform.position,
            TargetTransform != null ? TargetTransform.position : TargetPosition);
        return distance <= StopDistance + 0.2f && !Stopwatch.IsRunning;
    }
}
