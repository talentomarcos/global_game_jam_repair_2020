using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CShakeManager))]
public class CCamera2D : CGameObject
{
    private Camera _cam;

    public Vector3 _offset;

    public float _minXbound;
    public float _maxXbound;
    public float _maxYBound;
    public float _minYBound;

    private float _camHeight = 0;
    private float _camWidth = 0;
    private float _camSize;

    [Range(5, 30)]
    public float _followStrength = 5;
    private CShakeManager _shaker;

    private GameObject _followTarget;

    // Zoom variables
    private bool _doZoom = false;
    private float _initZoom;
    private float _endZoom;
    private float _zoomTime;
    private float _elapsedZoomTime;

    void Awake()
    {
        base.ApiAwake();
        _cam = Camera.main;
        _camHeight = 2f * _cam.fieldOfView;
        _camWidth = _camHeight * _cam.aspect;
        _camSize = _cam.fieldOfView;

        _shaker = GetComponent<CShakeManager>();
    }

    void Update()
    {
        if (!GameData.IsPause)
        {
            ApiUpdate();
        }
    }

    public override void ApiUpdate()
    {
        base.ApiUpdate();

        UpdateZoom();
    }

    void LateUpdate()
    {
        if (_followTarget == null)
        {
            Debug.LogWarning("No camera follow target");
            return;
        }
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        Vector3 targetPos = Vector3.Scale(_followTarget.transform.position,
            new Vector3(1, 1, 0));

        #region CHECK BOUNDS
        bool inBounds = CMath.pointInRect2D(targetPos.x, targetPos.y, _minXbound, _minYBound, _maxXbound - _minXbound, _maxYBound - _minYBound);
        //bool inBounds = CExtensions.IsPointInRectangleXZ(transform.position.XZ(), new Vector2(_minXbound, _minZbound), _maxZbound - _minZbound, _maxXbound - _minXbound);
        if (!inBounds)
        {
            if (targetPos.x < _minXbound)
            {
                targetPos = new Vector3(_minXbound, targetPos.y, 0);
            }
            else if (targetPos.x > _maxXbound)
            {
                targetPos = new Vector3(_maxXbound, targetPos.y, 0);
            }

            if (targetPos.y < _minYBound)
            {
                targetPos = new Vector3(targetPos.x, _minYBound, 0);
            }
            else if (targetPos.y > _maxYBound)
            {
                targetPos = new Vector3(targetPos.x, _maxYBound, 0);
            }
        }
        #endregion
        targetPos = targetPos + _offset;
        transform.position = Vector3.Lerp(transform.position, targetPos,
            Time.unscaledDeltaTime * _followStrength)
            + _shaker.SmoothShake(2);
    }
    public void AddShake(float intensity, float duration)
    {
        _shaker.AddShake(intensity, duration);
    }
    public void DoScreenShake(float aStrenght, float aTime = 1f, bool aIsInfinite = false)
    {
        if (aTime == 0)
            return;

        _shaker.AddShake(aStrenght, aTime, aIsInfinite);
    }
    public void UpdateZoom()
    {
        if (_doZoom)
        {
            _elapsedZoomTime += Time.deltaTime;
            float value = Mathfx.Hermite(_initZoom, _endZoom, _elapsedZoomTime / _zoomTime);
            _cam.fieldOfView = value;
            if (_elapsedZoomTime >= _zoomTime)
            {
                _doZoom = false;
                _cam.fieldOfView = _endZoom;
            }
        }
    }
    /// <summary>
    /// Zooms the default zoom value of the camera.
    /// </summary>
    /// <param name="aTime"> The time to lerp the zoom in or out </param>
    /// <param name="aMultip"> The multiplier for the zoom. Ex: 2 will zoom in, 1/2 will zoom out </param>
    public void Zoom(float aTime, float aMultip)
    {
        _zoomTime = aTime;
        _doZoom = true;
        _initZoom = _cam.fieldOfView;
        _endZoom = _camSize / aMultip;
        _elapsedZoomTime = 0;
    }

    /// <summary>
    /// Returns teh camera zoom back to default
    /// </summary>
    /// <param name="aTime"></param>
    public void ZoomOff(float aTime)
    {
        if (_cam.fieldOfView == _camSize)
        {
            _doZoom = false;
            return;
        }
        _zoomTime = aTime;
        _doZoom = true;
        _initZoom = _cam.fieldOfView;
        _endZoom = _camSize;
        _elapsedZoomTime = 0;
    }

    public void SetFollow(GameObject target)
    {
        _followTarget = target;
    }
}
