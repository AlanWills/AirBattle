using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace AirBattle.Events
{
    [Serializable]
    public class FloatUnityEvent : UnityEvent<float> { }

    [Serializable]
    [CreateAssetMenu(fileName = "FloatEvent", menuName = ComponentMenuConstants.EVENTS_FOLDER + "Float Event")]
    public class FloatEvent : ParameterisedEvent<float>
    {
    }
}
