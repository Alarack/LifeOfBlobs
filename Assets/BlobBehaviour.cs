using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlobBehaviour : MonoBehaviour{
    [Header("Navigation")]
    public NavMeshAgent _nav;
    public GameObject[] roamPositions;
    public Collider[] scentedFood;
    public LayerMask foodDetectLayer;
    [Header("Other")]
    public BlobStats _BS;
    public StatsChanger _SC;
    [Header("States")]
    public bool ROAMING;
    public bool WAITING;
    public bool FLEEING;
    public bool GETTING_FOOD;
    public bool EATING;
    int roamTarget;
    bool eatFlag;
    private void Start() {
        START_ROAM();
    }
    void Update() {
        CHECK_HUNGER();
    }
    void START_ROAM(){
        if(!FLEEING){
            scentedFood = null;
            ROAMING = true;
            roamTarget = Random.Range(0, roamPositions.Length);
            _nav.SetDestination(roamPositions[roamTarget].transform.position);
            StartCoroutine(ROAM_STATE());
        }
    }
    void CHECK_HUNGER(){
        if(_BS.hunger <= _BS.hungerSeekTreshold && !eatFlag){
            try{
                ROAMING = false;
                GETTING_FOOD = true;
                _nav.SetDestination(SCENTED_FOOD().transform.position);
                _nav.stoppingDistance = 1.5f;
                if(Vector3.Distance(transform.position, SCENTED_FOOD().transform.position) <= 2f && SCENTED_FOOD() != null){ 
                    StartCoroutine(EATING_STATE(SCENTED_FOOD()));
                    eatFlag = true;
                }
            } catch {
                GETTING_FOOD = false;
                START_ROAM();
                eatFlag = true;
            }
        }
    }
    GameObject SCENTED_FOOD(){
        Collider[] hits = Physics.OverlapSphere(transform.position, _BS.smell * 100, foodDetectLayer);
        scentedFood = hits;
        return scentedFood[CLOSEST_FOOD()].gameObject;
    }
    int CLOSEST_FOOD(){
        if(scentedFood.Length == 0) return -1;
        int closest = 0;
        float lastDist = Vector3.Distance(transform.position, scentedFood[0].transform.position);
        for (int i = 0; i < scentedFood.Length; i++){
            float thisDist = Vector3.Distance(transform.position, scentedFood[i].transform.position);
            if(lastDist > thisDist){
                lastDist = thisDist;
                closest = i;
            }
        }
        return closest;
    }
    IEnumerator EATING_STATE(GameObject food){
        while(GETTING_FOOD){
            EATING = true;
            yield return new WaitForSeconds(Random.Range(1f, 4f));
            _SC.EAT_FOOD(SCENTED_FOOD());
            EATING = false;
            START_ROAM();
            eatFlag = false;
            GETTING_FOOD = false;
            yield return null;
            yield break;
        }
    }
    public IEnumerator ROAM_STATE(){
        while(ROAMING){
            if(Vector3.Distance(transform.position, roamPositions[roamTarget].transform.position) < 10){
                _nav.stoppingDistance = Random.Range(6, 9);
                yield return new WaitForSeconds(Random.Range(2f, 10f));
                START_ROAM(); 
                yield return null;
                yield break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
