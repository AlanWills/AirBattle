using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TestAirBattle.Utils
{
    public class AutoDestroyer<T> : System.IDisposable where T : Object
    {
        public T Object { get; private set; }

        public AutoDestroyer(T obj)
        {
            Object = obj;
        }

        public void Dispose()
        {
            GameObject.Destroy(Object);
        }
    }
}
