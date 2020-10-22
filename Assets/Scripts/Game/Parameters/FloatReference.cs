using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirBattle.Parameters
{
    [CreateAssetMenu(fileName = "FloatReference", menuName = ComponentMenuConstants.PARAMETERS_FOLDER + "Float Reference")]
    public class FloatReference : ParameterReference<float, FloatValue, FloatReference>
    {
    }
}
