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
    public class GameObjectUnityEvent : UnityEvent<GameObject> { }

    [Serializable]
    [CreateAssetMenu(fileName = "GameObjectEvent", menuName = ComponentMenuConstants.EVENTS_FOLDER + "GameObject Event")]
    public class GameObjectEvent : ParameterisedEvent<GameObject>
    {
    }
}
