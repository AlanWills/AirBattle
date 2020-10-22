using AirBattle.Level;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAirBattle.Mocks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace TestAirBattle.Level
{
    [Timeout(20000)]
    public class TimeManagerTests
    {
        #region Properties and Fields

        private TimeManager timeManager;
        private MockEventListener roundOverListener = new MockEventListener();
        private bool sceneLoaded = false;
        private float originalRoundTime;

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

            GameObject timeManagerGameObject = GameObject.Find(TestConstants.TIME_MANAGER_NAME);
            Assert.IsNotNull(timeManagerGameObject);

            timeManager = timeManagerGameObject.GetComponent<TimeManager>();
            Assert.IsNotNull(timeManager);

            originalRoundTime = timeManager.roundTime;
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
            roundOverListener.Reset();
        }

        [TearDown]
        public void EnemyManagerTests_TearDown()
        {
            LogAssert.ignoreFailingMessages = false;
            timeManager.OnRoundOver.RemoveEventListener(roundOverListener);
            timeManager.roundTime = originalRoundTime;
        }

        #endregion

        #region Utility Methods/Properties

        private IEnumerator SetUpTimeManager()
        {
            yield return new WaitUntil(() => { return sceneLoaded; });

            timeManager.EndRound();
        }

        #endregion

        #region Round Over Tests

        [UnityTest]
        public IEnumerator RoundTimeElapsed_TriggersRoundOverEvent()
        {
            yield return SetUpTimeManager();

            timeManager.roundTime = 3;
            timeManager.OnRoundOver.AddEventListener(roundOverListener);
            timeManager.BeginRound();

            Assert.IsFalse(roundOverListener.WasEventRaised);

            yield return new WaitForSeconds(3);

            Assert.IsTrue(roundOverListener.WasEventRaised);
        }

        #endregion
    }
}
