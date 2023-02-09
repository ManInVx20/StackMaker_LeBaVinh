using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action<int> OnGainStack;

    [SerializeField]
    private float _moveSpeed = 20.0f;
    [SerializeField]
    private LayerMask _wallLayer;
    [SerializeField]
    private PlayerStack _playerStackPrefab;
    [SerializeField]
    private Transform _stacksTransform;
    [SerializeField]
    private Transform _modelTransform;
    [SerializeField]
    private Animator _animator;

    private Vector3 _targetPosition;
    private float _horizontalInput = 0.0f;
    private float _verticalInput = 0.0f;
    private bool _isMoving = false;
    private bool _canControl = true;
    
    private int _idleHash;
    private int _jumpHash;
    private int _happyHash;

    private Stack<PlayerStack> _stacks = new Stack<PlayerStack>();
    private int _gainedStacks = 0;

    public int StacksCount => _stacks.Count;

    private void Awake()
    {
        _idleHash = Animator.StringToHash("Idle");
        _jumpHash = Animator.StringToHash("Jump");
        _happyHash = Animator.StringToHash("Happy");
    }

    private void OnEnable()
    {
        LevelManager.OnStartLevel += LevelManager_OnStartLevel;
        LevelManager.OnFinishLevel += LevelManager_OnFinishLevel;
        LevelManager.OnStartEndLevel += LevelManager_OnStartEndLevel;
    }

    private void OnDisable()
    {
        LevelManager.OnStartLevel -= LevelManager_OnStartLevel;
        LevelManager.OnFinishLevel -= LevelManager_OnFinishLevel;
        LevelManager.OnStartEndLevel -= LevelManager_OnStartEndLevel;
    }

    private void Update()
    {
        if (!_canControl)
        {
            return;
        }

        if (!_isMoving)
        {
            _horizontalInput = 0.0f;
            _verticalInput = 0.0f;

            if (SwipeInput.SwipedRight)
            {
                _isMoving = true;
                _horizontalInput = 1.0f;
            }
            else if (SwipeInput.SwipedLeft)
            {
                _isMoving = true;
                _horizontalInput = -1.0f;
            }
            else if (SwipeInput.SwipedUp)
            {
                _isMoving = true;
                _verticalInput = 1.0f;
            }
            else if (SwipeInput.SwipedDown)
            {
                _isMoving = true;
                _verticalInput = -1.0f;
            }

            if (_horizontalInput != 0.0f || _verticalInput != 0.0f)
            {
                UpdateTargetPosition(_horizontalInput, _verticalInput);
            }
        }

        if (_isMoving)
        {
            if (Vector3.Distance(transform.position, _targetPosition) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = _targetPosition;
                _isMoving = false;

                if (_targetPosition == LevelManager.Instance.EndPoint)
                {
                    LevelManager.Instance.StartEndLevel();
                }
            }
        }
    }

    private void UpdateTargetPosition(float horizontal, float vertical)
    {
        Vector3 offset = new Vector3(horizontal, 0.0f, vertical);

        while (!CheckWall(_targetPosition + offset))
        {
            _targetPosition += offset;
        }
    }

    private bool CheckWall(Vector3 position)
    {
        Vector3 offset = Vector3.up * 2.0f;

        if (Physics.Linecast(position - offset * 5.0f, position + offset, out RaycastHit hit, _wallLayer))
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Stack stack = other.GetComponent<Stack>();

        if (stack != null)
        {
            if (stack is RoadStack)
            {
                PushStack(stack);
            }
            else if (stack is BridgeStack)
            {
                PopStack(stack);
            }
        }

        // if (other.gameObject.name == "JumpPoint")
        // {
        //     _animator.SetTrigger(_jumpHash);
        // }
    }

    private void PushStack(Stack roadStack)
    {
        roadStack.Activate(this);

        PlayerStack playerStack = Instantiate(_playerStackPrefab, _stacksTransform);
        playerStack.Setup(this);

        _stacks.Push(playerStack);
        _gainedStacks += 1;

        OnGainStack?.Invoke(_gainedStacks);

        _modelTransform.localPosition = new Vector3(0.0f, playerStack.StackHeight * _stacks.Count, 0.0f);
    }

    private void PopStack(Stack bridgeStack)
    {
        if (_stacks.Count == 0 || bridgeStack.IsActive)
        {
            return;
        }

        bridgeStack.Activate(this);

        PlayerStack playerStack = _stacks.Pop();

        if (_stacks.Count >= 1)
        {
            _modelTransform.localPosition = new Vector3(0.0f, playerStack.StackHeight * _stacks.Count, 0.0f);
        }

        playerStack.Activate(this);
    }

    private void ClearStacks()
    {
        if (_stacks.Count == 0)
        {
            return;
        }

        float stackHeight = 0.0f;

        foreach (var stack in _stacks)
        {
            stackHeight = stack.StackHeight;
            stack.Activate(this);
        }

        _stacks.Clear();

        _modelTransform.localPosition = new Vector3(0.0f, stackHeight, 0.0f);
    }

    private void LevelManager_OnStartLevel()
    {
        transform.position = LevelManager.Instance.StartPosition;
        _targetPosition = transform.position;
        _animator.SetTrigger(_idleHash);
        ClearStacks();
    }

    private void LevelManager_OnFinishLevel()
    {
        _moveSpeed *= 0.75f;
    }

    private void LevelManager_OnStartEndLevel()
    {
        _canControl = false;
        _animator.SetTrigger(_happyHash);
    }
}
