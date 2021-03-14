using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum BallType { Blastball, Burstball };

    private const float throwForce = 10f; //velocity the ball should be thrown at

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

    void Start()
    {
        if (this.gameObject.GetComponent<Blastball>())
        {
            Type = BallType.Blastball;
        }else if (this.gameObject.GetComponent<Burstball>())
        {
            Type = BallType.Burstball;
        }
        rbBall = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    public void ThrowBall(Vector3 throwDirection)
    {
        rbBall.AddForce(throwDirection * throwForce, ForceMode.Impulse); //TODO: get rid of this magic number
        if (this.Type == BallType.Blastball)
        {
            this.gameObject.GetComponent<Blastball>().ChangeState(this.gameObject.GetComponent<Blastball>().stateThrown);
        }
    }
}
