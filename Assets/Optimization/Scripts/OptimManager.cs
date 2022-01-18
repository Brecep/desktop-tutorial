using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimManager : MonoBehaviour
{
    public OptimUnit OptimPrefab;
    public int InstanceCount;
    public Vector3 Size;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < InstanceCount; ++i)
        {
            Vector3 position = new Vector3(
            Random.Range(-Size.x, Size.x),
            Random.Range(-Size.y, Size.y),
            Random.Range(-Size.z, Size.z));

            var newUnit = Instantiate(OptimPrefab, position, Quaternion.identity);
            newUnit.SetAreaSize(Size);
        }
    }
}
