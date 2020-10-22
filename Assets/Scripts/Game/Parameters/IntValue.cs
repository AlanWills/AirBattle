using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirBattle.Parameters
{
    [CreateAssetMenu(fileName = "IntValue", menuName = ComponentMenuConstants.PARAMETERS_FOLDER + "Int Value")]
    public class IntValue : ParameterValue<int>
    {
    }
}
