using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum BallType { Blastball, Burstball };
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
    private BallType _type; //backing variable for ballType

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
        rbBall.AddForce(throwDirection * 20, ForceMode.Impulse); //TODO: get rid of this magic number
        if (this.Type == BallType.Blastball)
        {
            this.gameObject.GetComponent<Blastball>().ChangeState(this.gameObject.GetComponent<Blastball>().stateThrown);
        }
    }
}
