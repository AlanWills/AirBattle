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
    [AddComponentMenu(ComponentMenuConstants.USER_INTERFACE_FOLDER + "/Round Over Dialog")]
    public class RoundOverDialog : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField]
        private IntValue score;

        [SerializeField]
        private Text scoreText;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            Debug.AssertFormat(scoreText != null, "Score Text is not set on RoundOverDialog on GameObject {0}", gameObject.name);
            Debug.AssertFormat(score != null, "Score is not set on RoundOverDialog on GameObject {0}", gameObject.name);

            if (scoreText != null)
            {
                scoreText.text = string.Format("Score: {0}", score != null ? score.value : 0);
            }
        }

        #endregion
    }
}
