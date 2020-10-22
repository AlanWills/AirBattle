using AirBattle;
using AirBattle.Level;
using AirBattle.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirBattle.Enemies
{
    [AddComponentMenu(ComponentMenuConstants.ENEMIES_FOLDER + "Enemy Manager")]
    public class EnemyManager : MonoBehaviour, IRoundManager
    {
        #region Properties and Fields

        [SerializeField]
        private GameObjectAllocator enemyAllocator;

        [SerializeField, Min(1)]
        private uint minWaveSize = 3;
        public uint MinWaveSize { get { return minWaveSize; } }

        [SerializeField, Min(1)]
        private uint maxWaveSize = 5;
        public uint MaxWaveSize { get { return maxWaveSize; } }

        [SerializeField, Min(0)]
        private float enemySpacing = 0.1f;

        private List<GameObject> currentWave = new List<GameObject>();

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            enemyAllocator = enemyAllocator == null ? GetComponent<GameObjectAllocator>() : enemyAllocator;
            maxWaveSize = System.Math.Min(maxWaveSize, enemyAllocator.Capacity);
            minWaveSize = System.Math.Min(minWaveSize, maxWaveSize);
        }

        public void Update()
        {
            if (currentWave.Count == 0)
            {
                SpawnWave();
            }
            else
            {
                for (int i = currentWave.Count; i > 0; --i)
                {
                    if (!currentWave[i - 1].activeSelf)
                    {
                        currentWave.RemoveAt(i - 1);
                    }
                }
            }
        }

        #endregion

        #region Enemy Spawning Methods

        public void SpawnWave()
        {
            currentWave.Clear();

            uint waveSize = (uint)Random.Range((int)minWaveSize, (int)maxWaveSize);
            bool canAllocate = enemyAllocator.CanAllocate(waveSize);
            Debug.AssertFormat(canAllocate, "Unable to allocate {0} enemies for wave in EnemyManager on GameObject {1}", waveSize, gameObject.name);

            if (canAllocate)
            {
                Vector3 previousPosition = transform.position;

                for (uint i = 0; i < waveSize; ++i)
                {
                    GameObject enemyGameObject = enemyAllocator.Allocate();
                    Enemy enemy = enemyGameObject.GetComponent<Enemy>();
                    Debug.AssertFormat(enemy != null, "Enemy {0} spawned in EnemyManager is missing an Enemy script.  Please add one (enemies without this script will not be spawned)", 
                        enemy.name);

                    if (enemy != null)
                    {
                        // GameObject MUST be active or changes to the physics components won't work
                        enemyGameObject.SetActive(true);

                        previousPosition.y -= enemySpacing;
                        previousPosition = enemy.Initialize(previousPosition);
                        currentWave.Add(enemyGameObject);
                    }
                }
            }
        }

        public void ClearWave()
        {
            enemyAllocator.DeallocateAll();
            currentWave.Clear();
        }

        #endregion

        #region Round Begin/End

        public void BeginRound()
        {
        }

        public void EndRound()
        {
            ClearWave();
        }

        #endregion
    }
}
