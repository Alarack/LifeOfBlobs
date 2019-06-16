using UnityEngine;
using UnityEngine.AI;
public class BlobStats: MonoBehaviour{
    [Header("Family")]
    public GameObject[] kids;
    public GameObject[] parents;
    public bool isChild;
    public bool isParent;
    public bool isFertile;
    [Header("Needs")]
    public float hunger;
    public float hungerSeekTreshold;
    public float energy;
    [Header("Physical")]
    public float weight;
    public float strength;
    public float speed;
    public float attraction;
    public float health;
    [Header("Mental")]
    public float intelligence;
    public float sight;
    public float smell;
    [Header("Other")]
    public SkinnedMeshRenderer skin;
    public NavMeshAgent _nav;

    void Start(){
        if(!isChild && !isParent){
            GENERATE_DNA();
        }
    }
    void GENERATE_DNA(){
        int temp = Random.Range(0, 10);
        if(temp <= 5){
            isFertile = true;
        }
        energy = 1f;
        weight = Random.Range(0f, 1f);
        strength = Random.Range(0f, 1f);
        speed = strength - (weight / 2);
        speed = Mathf.Clamp(speed, 0.1f, 1f);
        _nav.speed = speed * 10;
        attraction = Random.Range(0f, 1f) * strength;
        health = 1;
        intelligence = Random.Range(0f, 1f);
        sight = Random.Range(0f, 1f);
        smell = Random.Range(0f, 1f);
        hungerSeekTreshold = intelligence;

        skin.SetBlendShapeWeight(0, weight * 100);
        skin.SetBlendShapeWeight(1, sight * 100);
        skin.SetBlendShapeWeight(2, strength * 100);
        float stupidity = 1f - intelligence;
        skin.SetBlendShapeWeight(3, stupidity*100);
    }
}
