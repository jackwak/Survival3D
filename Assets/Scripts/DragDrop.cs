using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas _canvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    public static GameObject ItemBeingDragged;
    private Vector3 _startPosition;
    private Transform _startParent;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _canvas = transform.root.GetComponent<Canvas>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 0.6f;
        // So the ray cast will ignore the item itself
        _canvasGroup.blocksRaycasts = false;
        _startPosition = transform.position;
        _startParent = transform.parent;

        transform.SetParent(transform.root);
        ItemBeingDragged = gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ItemBeingDragged = null;

        if (transform.parent == _startParent || transform.parent == transform.root)
        {
            transform.position = _startPosition;
            transform.SetParent(_startParent);
        }

        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }
}
