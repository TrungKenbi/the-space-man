using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using ECS;
using ObjectPooling;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyBossBrain : BaseComponent
    {
        public NavMeshAgent navMeshAgent;
        public float scanningRadius = 5f;
        
        public float patrollingSpeed = 1f;
        public float chasingSpeed = 5f;
        
        public float attackSpeed = 1f;
        public int attackDamage = 1;
        public Transform attackPoint;
        public float attackRange = 0.5f;
        public float attackForce = 5f;

        public PooledObject bulletPrefab;
        public GameObject childEnemy;

        [SerializeField] private BehaviorTree bTree;

        private bool _foundTarget;
        private Transform _target;
        private BaseEntity _targetEntity;
        
        private Vector3 _originPoint;
        private Vector3 _destPoint;

        private float _attackInterval;

        private bool _isEven;

        private void OnValidate()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Awake()
        {
            bTree = new BehaviorTreeBuilder(gameObject)
                .Selector()
                    .Sequence()
                        .Condition("Close enough to attack?", ShouldAttack)
                        .Do("Attack", Attack)
                    .End()
                
                    .Sequence()
                        .Condition("Lost or killed player?", () => !_foundTarget)
                        .Do("Patrol", Patrol)
                    .End()
                    
                    .Sequence()
                        .Condition("Player on sight?", () => _foundTarget)
                        .Do("Chase", Chase)
                    .End()
                .End()
                .Build();
        }

        private void Start()
        {
            navMeshAgent.autoBraking = false;
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;

            _originPoint = transform.position;
            
            GotoNextPoint();
        }

        public override void DoUpdate()
        {
            bTree.Tick();
        }

        private void FixedUpdate()
        {
            FindingTarget();
        }

        private bool ShouldAttack()
        {
            if (!_foundTarget)
                return false;
            
            float distance = Vector3.Distance(transform.position, _target.position);
            return distance <= attackRange;
        }

        private TaskStatus Attack()
        {
            if (_attackInterval > 0f)
            {
                _attackInterval -= Time.deltaTime;
                return TaskStatus.Continue;
            }

            bool isReached = false;
            
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        isReached = true;
                    }
                }
            }

            if (!isReached)
                return TaskStatus.Continue;
            
            _attackInterval = attackSpeed;

            int skill = Random.Range(0, 15);
            if (skill == 0)
                BossSkill2();
            else
                BossSkill1();

            return TaskStatus.Success;
        }

        private TaskStatus Patrol()
        {
            navMeshAgent.speed = patrollingSpeed;
            
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                GotoNextPoint();
            
            return TaskStatus.Success;
        }
        
        private TaskStatus Chase()
        {
            navMeshAgent.speed = chasingSpeed;
            navMeshAgent.SetDestination(_target.position);
            return TaskStatus.Success;
        }
 
        private void FindingTarget()
        {
            if (_foundTarget)
                return;

            Collider2D collision =
                Physics2D.OverlapCircle(transform.position, scanningRadius, LayerMask.GetMask("Player"));
            if (collision)
            {
                _foundTarget = true;
                _target = collision.transform;
                _targetEntity = collision.gameObject.GetComponent<BaseEntity>();
                _targetEntity.OnDeath += ResetTarget;
            }
        }

        private void ResetTarget()
        {
            _foundTarget = false;
            _target = null;
            _targetEntity = null;
            
            // Move to origin position
            navMeshAgent.destination = _originPoint;
        }
        
        private void GotoNextPoint()
        {
            float x = Random.Range(_originPoint.x - scanningRadius, _originPoint.x + scanningRadius);
            float y = Random.Range(_originPoint.y - scanningRadius, _originPoint.y + scanningRadius);
            
            _destPoint = new Vector3(x, y, 0);

            NavMeshPath path = new NavMeshPath();
            navMeshAgent.CalculatePath(_destPoint, path);
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                GotoNextPoint();
                return;
            }
            
            navMeshAgent.destination = _destPoint;
        }

        private void BossSkill1()
        {
            int angle = 30;
            Debug.Log("Boss attack");
            for (int i = 0; i <= 360; i += angle)
            {
                var bulletRotation = Quaternion.AngleAxis((float)i + (_isEven ? 15 : 0), Vector3.forward) * attackPoint.rotation;
                var bullet = Pool.Instance.Spawn(bulletPrefab, attackPoint.position, bulletRotation);
                var rb2d = bullet.As<Bullet.BulletController>().rb2d;
                rb2d.AddForce(rb2d.transform.up * attackForce, ForceMode2D.Impulse);
                bullet.FinishDelayed(2f);
            }

            _isEven = !_isEven;
        }
        
        private void BossSkill2()
        {
            for(int i = 0; i < 2; i++)
                Instantiate(childEnemy, transform.position, Quaternion.identity);
        }

        private void OnDrawGizmosSelected()
        {
            // Draw scanning radius
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, scanningRadius);
            
            // Draw attack range
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
        
        private void OnDrawGizmos()
        {
            DrawNavMeshPath(navMeshAgent);
        }

        private static void DrawNavMeshPath(NavMeshAgent agent, bool showPath = true)
        {
            if (Application.isPlaying)
            {
                if (showPath && agent.hasPath)
                {
                    var corners = agent.path.corners;
                    if (corners.Length < 2)
                    {
                        return;
                    }

                    int i = 0;
                    for (; i < corners.Length - 1; i++)
                    {
                        Debug.DrawLine(corners[i], corners[i + 1], Color.blue);
                        Gizmos.color = Color.blue;
                        Gizmos.DrawSphere(agent.path.corners[i + 1], 0.03f);
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
                    }

                    Debug.DrawLine(corners[0], corners[1], Color.blue);
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(agent.path.corners[1], 0.03f);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(agent.path.corners[0], agent.path.corners[1]);
                }
            }
        }
    }
}
