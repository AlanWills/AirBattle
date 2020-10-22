using AirBattle.Level;
using AirBattle.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AirBattle.UserInterface
{
    [AddComponentMenu(ComponentMenuConstants.USER_INTERFACE_FOLDER + "/UI Manager")]
    public class UIManager : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField]
        private IntValue score;

        [SerializeField]
        private FloatValue timeLeft;

        [SerializeField]
        private Text scoreText;

        [SerializeField]
        private Text timeLeftText;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Debug.AssertFormat(scoreText != null, "Score Text is not set on UIManager on GameObject {0}", gameObject.name);
            Debug.AssertFormat(score != null, "Score is not set on UIManager on GameObject {0}", gameObject.name);
            Debug.AssertFormat(timeLeftText != null, "Time Left Text is not set on UIManager on GameObject {0}", gameObject.name);
            Debug.AssertFormat(timeLeft != null, "Time Left is not set on UIManager on GameObject {0}", gameObject.name);
        }

        private void Update()
        {
            if (scoreText != null)
            {
                scoreText.text = string.Format("Score: {0}", score != null ? score.value : 0);
            }

            if (timeLeftText)
            {
                timeLeftText.text = string.Format("Time Left: {0:0}s", timeLeft != null ? timeLeft.value : 0);
            }
        }

        #endregion
    }
}
