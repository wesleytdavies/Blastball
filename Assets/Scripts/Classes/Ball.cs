using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum BallType { Blastball, Burstball };

    private const float throwForce = 15f; //velocity the ball should be thrown at
    private const float throwVerticalMagnitude = 1f; //vertical velocity to add throw arc

    #region getters/setters
    public BallType Type
    {
        get
        {
            return _type;
        }
        private set
        {
            _type = value;
        }
    }
    private BallType _type;
    #endregion

    private Rigidbody rbBall;
    [SerializeField] private GameObject blastParticles;

    void Start()
    {
        if (gameObject.GetComponent<Blastball>())
        {
            Type = BallType.Blastball;
        }else if (gameObject.GetComponent<Burstball>())
        {
            Type = BallType.Burstball;
        }
        rbBall = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    public void ThrowBall(Vector3 lookDirection)
    {
        Vector3 throwDirection = lookDirection;
        throwDirection.y += throwVerticalMagnitude;
        rbBall.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        if (Type == BallType.Blastball)
        {
            GetComponent<Blastball>().ChangeState(GetComponent<Blastball>().stateThrown);
        }else if (Type == BallType.Burstball)
        {
            GetComponent<Burstball>().ChangeState(GetComponent<Burstball>().stateThrown);
        }
    }

    //explosion code based on Unity documentation: https://docs.unity3d.com/ScriptReference/Rigidbody.AddExplosionForce.html
    public void Blast(Vector3 epicenter, float radius, float magnitude, float lift)
    {
        Collider[] explodedColliders = Physics.OverlapSphere(epicenter, radius);
        foreach (Collider exploded in explodedColliders)
        {
            Rigidbody rb = exploded.GetComponent<Rigidbody>();
            if (exploded.GetComponent<Ball>())
            {
                Instantiate(blastParticles, exploded.transform.position, Quaternion.identity);
                Destroy(exploded.gameObject);
            }
            if (rb != null)
            {
                rb.AddExplosionForce(magnitude, epicenter, radius, lift);
            }
        }
    }
}
