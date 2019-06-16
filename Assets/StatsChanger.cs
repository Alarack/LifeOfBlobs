using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsChanger : MonoBehaviour
{
    public BlobStats _BS;
    public SkinnedMeshRenderer skin;
    private void Start() {
        StartCoroutine(PASS_TIME());
    }
    public IEnumerator PASS_TIME(){
        while(true){
            _BS.hunger -= _BS.weight / 2;
            _BS.hunger = Mathf.Clamp(_BS.hunger, -1, 1);
            _BS.energy -= _BS.weight / 2;
            _BS.energy = Mathf.Clamp(_BS.energy, -1, 1);
            if(_BS.hunger <= 0){
                _BS.weight -= 0.1f;
            }
            _BS.weight -= 0.01f;
            _BS.weight = Mathf.Clamp(_BS.weight, -1, 1);
            skin.SetBlendShapeWeight(0, _BS.weight*100);
            if(_BS.hunger <= 0 || _BS.energy <= 0 || _BS.weight <= 0){
                _BS.health -= 0.1f;
            }
            yield return new WaitForSeconds(30);
        }
    }
    public void EAT_FOOD(GameObject food){
        Destroy(food);
        _BS.hunger += (1 - _BS.weight)/2;
        _BS.energy += (1 - _BS.weight)/2;
        _BS.weight += 0.01f;
        skin.SetBlendShapeWeight(0, _BS.weight*100);
    }
}
