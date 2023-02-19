using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PanBehavior : MonoBehaviour
{
    public Vector2 PointerPos { get; set; }

    [SerializeField] private Animator ani;
    [SerializeField] private Animator onionAni;
    [SerializeField] private float delay = 0.3f;
    private bool _atkBlocked;
    private static readonly int _attack = Animator.StringToHash("Attack");

    // Update is called once per frame
    void Update()
    {
        var t = transform;
        t.right = (PointerPos - (Vector2)t.position).normalized;
    }

    public void Attack()
    {
        if (_atkBlocked) return;
        
        ani.SetTrigger(_attack);
        onionAni.SetTrigger(_attack);
        _atkBlocked = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        _atkBlocked = false;
    }
}
