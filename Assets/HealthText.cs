using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthText : MonoBehaviour
{
    // Start is called before the first frame update

    public RectTransform ts;
    
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(ts.DOAnchorPosY(10f, 2f));
        sequence.Append(ts.DOScale(0f, 2f));
        sequence.Play();
        sequence.OnComplete(() =>
        {
            // bo do pool
        });
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
