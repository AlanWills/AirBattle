using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirBattle.Parameters
{
    [CreateAssetMenu(fileName = "IntReference", menuName = ComponentMenuConstants.PARAMETERS_FOLDER + "Int Reference")]
    public class IntReference : ParameterReference<int, IntValue, IntReference>
    {
    }
}
