using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CGameObject : MonoBehaviour {

    //private Transform _transform;
    //private Vector3 mPos;
    private Vector3 _vel;
    private Vector3 _accel;

    private bool _isDead = false;

    private int _state = 0;
    private float _timeState = 0.0f;

    private string _name;

    private float _radius = .6f;

    private int _type;

    private float _width = 100;
    private float _height = 100;

    private float _friction = 1.0f;

    private float _maxSpeed = 10000.0f;

    private float _mass = 1.0f;

    public bool _usesCharacterController = false;

    private CharacterController _controller;

    private bool _useUnaffectedDeltaTime = false;

    public delegate void DamageDelegate(int currentHP, int maxHP);
    public delegate void BoolDelegate(bool aBool);

    protected BoolDelegate OnFlipped;

    public void SubscribeOnFlipped(BoolDelegate action)
    {
        UnSubscribeOnFlipped(action);
        OnFlipped += action;
    }

    public void UnSubscribeOnFlipped(BoolDelegate action)
    {
        OnFlipped -= action;
    }

    void Awake()
    {
        ApiAwake();
    }

    public virtual void ApiAwake()
    {
        //_transform = GetComponent<Transform>();
        //mPos = new CVector();
        //mPos._vector = _transform.position;
        _vel = new Vector3();
        _accel = new Vector3();
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            _usesCharacterController = false;
        }
    }

    public CharacterController GetController()
    {
        return _controller;
    }

    public void SetX(float aX)
    {
        transform.position = new Vector3(aX, transform.position.y, transform.position.z);
        //mPos.setX(aX);
    }

    public void SetY(float aY)
    {
        transform.position = new Vector3(transform.position.x, aY, transform.position.z);
        //mPos.setY(aY);
    }

    public void SetZ(float aZ)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, aZ);
        //mPos.setZ(aZ);
    }

    public void SetXY(float aX, float aY)
    {
        transform.position = new Vector3(aX, aY, transform.position.z);
        //mPos.setX(aX);
        //mPos.setY(aY);
    }

    public void SetXZ(float aX, float aZ)
    {
        transform.position = new Vector3(aX, transform.position.y, aZ);
        //mPos.setX(aX);
        //mPos.setY(aY);
    }

    public void SetPos(Vector3 aPos)
    {
        transform.position = aPos;
        //mPos = aPos;
    }

    public void SetLocalPos(Vector3 aPos)
    {
        transform.localPosition = aPos;
        //mPos = aPos;
    }

    public float GetX()
    {
        return transform.position.x;
        //return mPos.x();
    }

    public float GetY()
    {
        return transform.position.y;
        //return mPos.y();
    }

    public float GetZ()
    {
        return transform.position.z;
        //return mPos.z();
    }

    public Vector3 GetPos()
    {
        //return mPos._vector;
        return transform.position;
    }

    public Vector3 GetProjectedPos(float height)
    {
        //Find angle at point C
        float cA = 70;
        //find a. a / sinA = c / sinC
        float a = ((transform.position.y - height) / CMath.Sin(cA)) * CMath.Sin(90);
        Vector3 v = transform.position + new Vector3(0, CMath.Sin(-cA), CMath.Cos(-cA)) * a;
        Debug.DrawLine(transform.position, v);
        return v;
    }

    public Vector3 GetProjectedPos(Vector3 fromPosition, float height)
    {
        //Find angle at point C
        float cA = 70;
        //find a. a / sinA = c / sinC
        float a = ((fromPosition.y - height) / CMath.Sin(cA)) * CMath.Sin(90);
        Vector3 v = fromPosition + new Vector3(0, CMath.Sin(-cA), CMath.Cos(-cA)) * a;
        //Debug.DrawLine(transform.position, v);
        return v;
    }

    public void SetVelX(float aVelX)
    {
        _vel = new Vector3(aVelX, _vel.y, _vel.z);
        _vel.Clamp(_maxSpeed);
    }

    public void SetVelY(float aVelY)
    {
        _vel = new Vector3(_vel.x, aVelY, _vel.z);
        _vel.Clamp(_maxSpeed);
    }

    public void SetVelXY(float aVelX, float aVelY)
    {
        _vel = new Vector3(aVelX, aVelY, _vel.z);
        _vel.Clamp(_maxSpeed);
    }

    public void SetVelXZ(float aVelX, float aVelZ)
    {
        _vel = new Vector3(aVelX, _vel.y, aVelZ);
        _vel.Clamp(_maxSpeed);
    }

    public void SetVelZ(float aVelZ)
    {
        _vel = new Vector3(_vel.x, _vel.y, aVelZ);
        _vel.Clamp(_maxSpeed);
    }

    public void SetVel(Vector3 aVel)
    {
        _vel = aVel;
        _vel.Clamp(_maxSpeed);
    }

    public float GetVelX()
    {
        return _vel.x;
    }

    public float GetVelY()
    {
        return _vel.y;
    }

    public float GetVelZ()
    {
        return _vel.z;
    }

    public Vector3 GetVel()
    {
        return _vel;
    }

    public Vector3 GetControllerVel()
    {
        if (_usesCharacterController)
        {
            return GetController().velocity;
        }
        else return _vel;
    }

    public void SetAccel(Vector3 aAccel)
    {
        _accel = aAccel;
    }

    public void SetAccelX(float aAccelX)
    {
        _accel = new Vector3(aAccelX, _accel.y, _accel.z);
    }

    public void SetAccelY(float aAccelY)
    {
        _accel = new Vector3(_accel.x, aAccelY, _accel.z);
    }

    public void SetAccelZ(float aAccelZ)
    {
        _accel = new Vector3(_accel.x, _accel.y, aAccelZ);
    }

    public void SetAccelXY(float aAccelX, float aAccelY)
    {
        _accel = new Vector3(aAccelX, aAccelY, _accel.z);

    }

    public void SetAccelXZ(float aAccelX, float aAccelZ)
    {
        _accel = new Vector3(aAccelX, _accel.y, aAccelZ);

    }

    public float GetAccelX()
    {
        return _accel.x;
    }

    public float GetAccelY()
    {
        return _accel.y;
    }

    public float GetAccelZ()
    {
        return _accel.z;
    }

    public Vector3 GetAccel()
    {
        return _accel;
    }

    private void Update()
    {
        if (GameData.IsPause)
        {
            return;
        }
        ApiUpdate();
    }

    virtual public void ApiUpdate()
    {
        if (GameData.IsPause)
        {
            return;
        }
        float dTime = Time.deltaTime;
        if (_useUnaffectedDeltaTime)
        {
            dTime = Time.unscaledDeltaTime;
        }
        _timeState = _timeState + dTime;

        _vel = _vel + _accel * dTime;
        _vel = _vel * _friction;

        // Clamp velocity.
        if (_vel.magnitude > _maxSpeed)
        {
            _vel = _vel.normalized * _maxSpeed;
        }
        if (_vel.magnitude < GameData.MinVelClamp)
        {
            _vel = Vector3.zero;
        }

        if (_usesCharacterController && _controller.enabled)
        {
            _controller.Move(_vel * dTime);
        }
        else
        {
            transform.position = transform.position + _vel * dTime;
        }
    }

    virtual public void Render()
    {
    }

    /*virtual public void destroy()
    {
        mPos.destroy();
        mPos = null;
        mVel.destroy();
        mVel = null;
        mAccel.destroy();
        mAccel = null;
    }*/

    virtual public void SetState(int aState)
    {
        _state = aState;
        _timeState = 0.0f;
    }

    public int GetState()
    {
        return _state;
    }

    public float GetStateTime()
    {
        return _timeState;
    }

    public virtual void SetDead(bool aIsDead)
    {
        _isDead = aIsDead;
    }

    public bool IsDead()
    {
        return _isDead;
    }

    public void SetRadius(int aRadius)
    {
        _radius = aRadius;
    }

    public float GetRadius()
    {
        return _radius;
    }

    public void SetType(int aType)
    {
        _type = aType;
    }

    public int GetObjType()
    {
        return _type;
    }

    virtual public void SetName(string aName)
    {
        _name = aName;
    }

    virtual public string GetName()
    {
        return _name;
    }

    public void SetWidth(float aWidth)
    {
        _width = aWidth;
    }

    public float GetWidth()
    {
        return _width;
    }

    public void SetHeight(float aHeight)
    {
        _height = aHeight;
    }

    public float GetHeight()
    {
        return _height;
    }

    public void StopMove()
    {
        SetVel(Vector3.zero);
        SetAccel(Vector3.zero);
    }

    public bool Collides(CGameObject aGameObject)
    {
        if (CMath.dist(GetX(), GetY(), aGameObject.GetX(), aGameObject.GetY()) < (GetRadius() + aGameObject.GetRadius()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetFriction(float aFriction)
    {
        _friction = aFriction;
    }

    public float GetFriction()
    {
        return _friction;
    }

    public void SetMaxSpeed(float aMaxSpeed)
    {
        _maxSpeed = aMaxSpeed;
    }

    public float GetMaxSpeed()
    {
        return _maxSpeed;
    }

    public float GetMass()
    {
        return _mass;
    }

    public void SetMass(float aMass)
    {
        _mass = aMass;
    }

    public void SetScale(Vector3 aScale)
    {
        transform.localScale = aScale;
    }

    public Vector3 GetScale()
    {
        return transform.localScale;
    }

    public void SetRotation(Vector3 aAngles)
    {
        transform.rotation = Quaternion.identity;
        transform.Rotate(aAngles);
    }

    public void SetUseUnaffectedDeltaTime(bool aUse)
    {
        _useUnaffectedDeltaTime = aUse;
    }
}

public delegate void GenericDelegate();
