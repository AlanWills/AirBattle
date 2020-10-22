using AirBattle.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirBattle.Level
{
    [AddComponentMenu(ComponentMenuConstants.LEVEL_FOLDER + "Time Manager")]
    public class TimeManager : MonoBehaviour, IRoundManager
    {
        #region Properties and Fields

        [SerializeField]
        private FloatValue timeLeft;

        public float roundTime;

        [SerializeField]
        private Events.Event onRoundOver;
        public Events.Event OnRoundOver { get { return onRoundOver; } }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            BeginRound();
        }

        private void Update()
        {
            timeLeft.value = Math.Max(0, timeLeft.value - Time.deltaTime);

            if (timeLeft.value == 0 && onRoundOver != null)
            {
                onRoundOver.Raise();
            }
        }

        #endregion

        #region Round Begin/End

        public void BeginRound()
        {
            timeLeft.value = roundTime;
        }

        public void EndRound()
        {
        }

        #endregion
    }
}
