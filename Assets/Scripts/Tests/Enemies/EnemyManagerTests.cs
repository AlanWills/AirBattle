using System.Collections;
using System.Collections.Generic;
using AirBattle.Enemies;
using AirBattle.Memory;
using NUnit.Framework;
using TestAirBattle.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace TestAirBattle.Enemies
{
    [Timeout(20000)]
    public class EnemyManagerTests
    {
        #region Properties and Fields

        private Collider2D levelCollider;
        private EnemyManager enemyManager;
        private bool sceneLoaded = false;

        #endregion

        #region Set Up/Tear Down

        [OneTimeSetUp]
        public void EnemyManagerTests_OneTimeSetUp()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.LoadScene(TestConstants.SPARSE_TEST_SCENE, LoadSceneMode.Single);
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            sceneLoaded = true;

            GameObject roundManagerGameObject = GameObject.Find(TestConstants.ROUND_MANAGER_NAME);
            Assert.IsNotNull(roundManagerGameObject);

            levelCollider = roundManagerGameObject.GetComponent<Collider2D>();
            Assert.IsNotNull(levelCollider);
            
            GameObject enemyManagerGameObject = GameObject.Find(TestConstants.ENEMY_MANAGER_NAME);
            Assert.IsNotNull(enemyManagerGameObject);

            enemyManager = enemyManagerGameObject.GetComponent<EnemyManager>();
            Assert.IsNotNull(enemyManager);
        }

        [OneTimeTearDown]
        public void EnemyManagerTests_OneTimeTearDown()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        [SetUp]
        public void EnemyManagerTests_SetUp()
        {
            LogAssert.ignoreFailingMessages = true;
        }

        [TearDown]
        public void EnemyManagerTests_TearDown()
        {
            LogAssert.ignoreFailingMessages = false;
        }

        #endregion

        #region Utility Methods/Properties

        private uint NumActiveEnemies
        {
            get
            {
                uint total = 0;
                Transform enemyManagerTransform = enemyManager.transform;

                for (int i = 0; i < enemyManagerTransform.childCount; ++i)
                {
                    if (enemyManagerTransform.GetChild(i).gameObject.activeSelf)
                    {
                        ++total;
                    }
                }

                return total;
            }
        }

        private GameObject FirstActiveEnemy
        {
            get
            {
                Transform enemyManagerTransform = enemyManager.transform;

                for (int i = 0; i < enemyManagerTransform.childCount; ++i)
                {
                    if (enemyManagerTransform.GetChild(i).gameObject.activeSelf)
                    {
                        return enemyManagerTransform.GetChild(i).gameObject;
                    }
                }

                return null;
            }
        }

        private IEnumerator SetUpEnemyManager()
        {
            yield return new WaitUntil(() => { return sceneLoaded; });

            enemyManager.EndRound();
            enemyManager.BeginRound();
        }

        #endregion

        #region Enemy Spawning Tests

        [UnityTest]
        public IEnumerator NoEnemiesInWave_SpawnsWave()
        {
            yield return SetUpEnemyManager();

            Assert.AreEqual(0, NumActiveEnemies);

            yield return null;

            Assert.AreNotEqual(0, NumActiveEnemies);
        }

        [UnityTest]
        public IEnumerator SpawningWave_CreatesRandomNumberOfEnemies_InCorrectMinMaxBounds()
        {
            yield return SetUpEnemyManager();

            Assert.AreEqual(0, NumActiveEnemies);

            for (int i = 0; i < 10; ++i)
            {
                enemyManager.SpawnWave();

                Assert.GreaterOrEqual(NumActiveEnemies, enemyManager.MinWaveSize);
                Assert.LessOrEqual(NumActiveEnemies, enemyManager.MaxWaveSize);

                enemyManager.ClearWave();
            }
        }

        #endregion

        #region Enemy Movement Tests

        [UnityTest]
        public IEnumerator UponReachingRHS_EnemyRepositionsAtLHS()
        {
            yield return SetUpEnemyManager();
            enemyManager.SpawnWave();

            Assert.AreNotEqual(0, NumActiveEnemies);

            GameObject firstActiveEnemy = FirstActiveEnemy;
            
            Assert.IsNotNull(firstActiveEnemy);

            Collider2D enemyCollider = firstActiveEnemy.GetComponent<Collider2D>();

            Assert.IsNotNull(enemyCollider);

            // Allow unity to pump it's various messages and update physics
            yield return new WaitForSeconds(0.2f);

            // Position the enemy squarely in the level
            Vector3 position = firstActiveEnemy.transform.position;
            position.x = levelCollider.transform.position.x;
            firstActiveEnemy.transform.position = position;

            // Allow unity to pump it's various messages and update physics
            yield return new WaitForSeconds(0.2f);

            // Now position the enemy at the far right
            position = firstActiveEnemy.transform.position;
            position.x = levelCollider.transform.position.x + levelCollider.bounds.extents.x + enemyCollider.bounds.extents.x;
            firstActiveEnemy.transform.position = position;

            // Finally, allow unity to update again - the enemy should now be moved over to the other side
            yield return new WaitForSeconds(0.2f);

            // A definite calculation is difficult here because the physics simulation will have already moved the enemy
            // We could maybe change the implementation of the sideways movement to manually set the transform in a component
            // But doing a rough bounds check here is probably ok
            float expectedXPos = -levelCollider.transform.position.x - levelCollider.bounds.extents.x - enemyCollider.bounds.extents.x;
            
            Assert.GreaterOrEqual(firstActiveEnemy.transform.position.x, expectedXPos);
            Assert.LessOrEqual(firstActiveEnemy.transform.position.x, expectedXPos * 0.5f);
        }

        #endregion
    }
}
