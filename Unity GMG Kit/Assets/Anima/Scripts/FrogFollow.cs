using UnityEngine;
using System.Collections;
using System;

/// Author: Matthew Mazan [Masterio]
public class PetFollow : MonoBehaviour
{
    public Transform target = null;                    // follow by target
    public float reactDelay = 0.5f;                    // how fast it react to target moves [edit it in edit mode only]
    public float recordFPS = 15f;                    // how many record data in one second [edit it in edit mode only]
    [Range(0.1f,2f)]
    public float followSpeed = 5f;                    // follow speed
    [Range(0f, 7f)]
    public float targetDistance = 0f;                // don't move closer than this value
    public bool targetDistanceY = false;            // if near then update y only
    public bool targetPositionOnStart = false;        // set the same position as target on start
    public bool start = false;                        // start or stop follow  
    private Animator animator;
    SpriteRenderer sprite;
#if UNITY_EDITOR
    public bool gizmo = true;                        // draw gizmos
#endif

    [Serializable]
    public class TargetRecord
    {
        public Vector3 position;                    // world position

        // we can add more data if we need here

        public TargetRecord(Vector3 position)
        {
            this.position = position;
        }
    }

    private TargetRecord[] _records = null;            // keeps target data in time
    private float _t = 0f;
    private int _i = 0;                                // keeps current index for recorder
    private int _j = 1;                                // keeps current index for follower
    private float _interval = 0f;
    private TargetRecord _record = null;            // current record
    private bool _recording = true;                    // stop recording if true
    private int _arraySize = 1;

    public void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        target = target.transform;

        Initialize();
    }

    public void Initialize()
    {
        if (targetPositionOnStart)
            transform.position = target.position;

        _interval = 1 / recordFPS;

        _arraySize = Mathf.CeilToInt(recordFPS * reactDelay);
        if (_arraySize == 0)
            _arraySize = 1;

        _records = new TargetRecord[_arraySize];
    }

    // update this transform data
    public void LateUpdate()
    {
        if (start)
        {
            // can be move into the Update or LateUpdate if needed
            RecordData(Time.deltaTime);

            // move to the target
            if (targetDistance <= 0f)
            {
                if (_record != null)
                {
                    transform.position = Vector3.Lerp(transform.position, _record.position, Time.deltaTime * followSpeed);
                    //Debug.Log("1");
                    animator.SetBool("Moving", true);
                }
            }
            else if ((target.position - transform.position).magnitude > targetDistance)
            {
                if (!_recording)
                {
                    ResetRecordArray();
                    _recording = true;
                }

                if (_record != null)
                {
                    transform.position = Vector3.Lerp(transform.position, _record.position, Time.deltaTime * followSpeed);
                    if(transform.position.x < _record.position.x && animator.GetBool("Moving"))
                    {
                        sprite.flipX = false;
                    }
                    else
                    {
                        sprite.flipX = true;
                    }
                    //Debug.Log("2");
                    animator.SetBool("Moving", true);
                }
                }
            else if (targetDistanceY && Mathf.Abs(target.position.y - transform.position.y) > 0.05f)
            {
                if (_record != null)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, target.position.y, transform.position.z), Time.deltaTime * followSpeed);
                    //Debug.Log("3");
                    animator.SetBool("Moving", true);
                }
                }
            else
            {
                animator.SetBool("Moving", false);
                _recording = false;
            }
        }
    }

    private void RecordData(float deltaTime)
    {
        if (!_recording)
            return;

        // check intervals
        if (_t < _interval)
        {
            _t += deltaTime;
        }
        // record this frame
        else
        {
            // record target data
            _records[_i] = new TargetRecord(target.position);

            // set next record index
            if (_i < _records.Length - 1)
                _i++;
            else
                _i = 0;

            // set next follow index
            if (_j < _records.Length - 1)
                _j++;
            else
                _j = 0;

            // handle current record
            _record = _records[_j];

            _t = 0f;
        }
    }

    // used if distance is small
    private void ResetRecordArray()
    {
        _i = 0;
        _j = 1;
        _t = 0f;

        _records = new TargetRecord[_arraySize];

        for (int i = 0; i < _records.Length; i++)
        {
            _records[i] = new TargetRecord(transform.position);
        }

        _record = _records[_j];
    }

    /// <summary>
    /// Gets the current record.
    /// </summary>
    public TargetRecord currentRecord
    {
        get
        {
            return _record;
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        if (gizmo)
        {
            if (_records == null || _records.Length < 2)
                return;

            Gizmos.color = Color.red;
            for (int i = 0; i < _i - 1; i++)
            {
                if (_records[i] != null && _records[i + 1] != null)
                    Gizmos.DrawLine(_records[i].position, _records[i + 1].position);
            }

            //Gizmos.color = Color.green;
            for (int j = _j; j < _records.Length - 1; j++)
            {
                if (_records[j] != null && _records[j + 1] != null)
                    Gizmos.DrawLine(_records[j].position, _records[j + 1].position);
            }

            //Gizmos.color = Color.yellow;
            if (_records[0] != null && _records[_records.Length - 1] != null)
                Gizmos.DrawLine(_records[0].position, _records[_records.Length - 1].position);

            Gizmos.color = Color.white;
            if (_record != null)
                Gizmos.DrawLine(_record.position, transform.position);
        }
    }

#endif
}
