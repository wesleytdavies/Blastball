using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject blastballPrefab;
    [SerializeField] private GameObject burstballPrefab;
    private GameObject blastball;
    [SerializeField] private Vector3 blastballDropPos;
    [SerializeField] private float burstballDropY;
    [SerializeField] private int maxBurstballCount;
    private GameObject burstball;

    void Start()
    {
        Instantiate(blastballPrefab, blastballDropPos, Quaternion.identity);
        Vector3 burstballDropPos = new Vector3(Random.Range(-30f, 30f), burstballDropY, Random.Range(-30f, 30f)); //TODO: get rid of these magic numbers
        Instantiate(burstballPrefab, burstballDropPos, Quaternion.identity);
    }

    void Update()
    {
        blastball = GameObject.Find("Blastball(Clone)");
        if (blastball == null)
        {
            Instantiate(blastballPrefab, blastballDropPos, Quaternion.identity);
        }
        burstball = GameObject.Find("Burstball(Clone)");
        if (burstball == null)
        {
            Vector3 burstballDropPos = new Vector3(Random.Range(-30f, 30f), burstballDropY, Random.Range(-30f, 30f)); //TODO: get rid of these magic numbers
            Instantiate(burstballPrefab, burstballDropPos, Quaternion.identity);
        }
    }
}
