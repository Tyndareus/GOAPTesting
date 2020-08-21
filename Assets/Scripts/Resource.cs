using SGoap;
using SGoap.Services;
using UnityEngine;
using Random = UnityEngine.Random;

public class Resource : MonoBehaviour
{
    public enum ResourceType
    {
        Wood,
        Food,
        Stone
    }
    
    public CharacterStatusController status;
    public int value;
    
    public static GameObject t;

    private void Awake()
    {
        value = Random.Range(2, 8);
        status.Hp = status.MaxHp = value;
        
        t = GameObject.FindGameObjectWithTag("Finish");
    }

    private void OnEnable()
    {
        ObjectManager<Resource>.Add(this);
    }

    private void OnDisable()
    {
        ObjectManager<Resource>.Remove(this);
        
        if (AstarPath.active.isScanning) return;
        
        Collider c = t.GetComponent<Collider>();
        AstarPath.active.UpdateGraphs(c.bounds);
    }
    
    public void TakeResource()
    {
        if (status != null) status.TakeDamage(1);

        value--;
    }
}
