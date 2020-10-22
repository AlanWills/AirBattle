﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirBattle.Parameters
{
    [CreateAssetMenu(fileName = "FloatValue", menuName = ComponentMenuConstants.PARAMETERS_FOLDER + "Float Value")]
    public class FloatValue : ParameterValue<float>
    {
    }
}
