using AirBattle.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirBattle.Level
{
    [AddComponentMenu(ComponentMenuConstants.LEVEL_FOLDER + "Score Manager")]
    public class ScoreManager : MonoBehaviour, IRoundManager
    {
        #region Properties and Fields

        [SerializeField]
        private IntValue score;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            BeginRound();
        }

        #endregion

        #region Score Utility Methods

        public void IncrementScore()
        {
            ++score.value;
        }

        #endregion

        #region Round Begin/End

        public void BeginRound()
        {
            score.value = 0;
        }

        public void EndRound()
        {
        }

        #endregion
    }
}
