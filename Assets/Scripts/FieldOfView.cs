using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float _radius; //дальность обзора
    [SerializeField, Range(0, 360)] private float _angle; //угол обзора
    [SerializeField] private Player _player; //игрок
    [SerializeField] private LayerMask _targetMask; //маска для игрока
    [SerializeField] private LayerMask _obsticleMask; //маска препятствий
    
    private bool _canSeePlayer; //статус видимости игрока

    public bool CanSeePlayer => _canSeePlayer;
    public float Radius => _radius;
    public float Angle => _angle;
    public Player Player => _player;

    void Start()
    {
        StartCoroutine(FOVRountine());        
    }

    IEnumerator FOVRountine()
    {
        WaitForSeconds wait = new WaitForSeconds(.2f); //в Unity документации эта переменная сразу задается в yield return

        while(true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] collidersForCheck = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        if(collidersForCheck.Length > 0)
        {
            for (int i = 0; i < collidersForCheck.Length; i++)
            {
                Transform target = collidersForCheck[i].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obsticleMask))
                    {
                        _canSeePlayer = true;
                    }
                    else
                    {
                        _canSeePlayer = false;
                    }
                }
                else
                {
                    _canSeePlayer = false;
                }
            }
        }
        else if(_canSeePlayer)
        {
            _canSeePlayer = false;
        }
    }
}
