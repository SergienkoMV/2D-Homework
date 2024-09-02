using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float _radius; //��������� ������
    [SerializeField, Range(0, 360)] private float _angle; //���� ������
    [SerializeField] private Player _player; //�����
    [SerializeField] private LayerMask _targetMask; //����� ��� ������
    [SerializeField] private LayerMask _obsticleMask; //����� �����������
    
    private bool _canSeePlayer; //������ ��������� ������

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
        WaitForSeconds wait = new WaitForSeconds(.2f); //� Unity ������������ ��� ���������� ����� �������� � yield return

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
