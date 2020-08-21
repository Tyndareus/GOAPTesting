
using SGoap;
using SGoap.Services;

public class WoodCutterDataProvider : SimulatorDataProvider
{
    public override void Setup()
    {
        BasicAgent agent = GetComponent<BasicAgent>();
        agent.Initialize();
    }

    public override void Clean()
    {
        TargetManager.Clear();
    }
}
