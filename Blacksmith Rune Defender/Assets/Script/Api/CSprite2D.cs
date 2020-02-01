using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSprite2D : MonoBehaviour {

    private SpriteRenderer _renderer;
    public int _offset = 0;
    public bool _staticObject = false;

    void Start()
    {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _renderer.sortingOrder = (int)(-transform.position.y * 100) + _offset;
        if (_staticObject)
        {
            enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        Sort();
	}

    private void Sort()
    {
        int order = (int)(-transform.position.y * 100) + _offset;
        _renderer.sortingOrder = order;
    }

    public int GetSortingOrder()
    {
        return _renderer.sortingOrder;
    }

    public void SetOffset(int aOffset)
    {
        _offset = aOffset;
    }

    public int GetOffset()
    {
        return _offset;
    }
}
