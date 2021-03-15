using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//auto destroy code based off this: https://answers.unity.com/questions/219609/auto-destroying-particle-system.html
public class ParticleSystemAutoDestroy : MonoBehaviour
{
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
