using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirBattle.Projectiles
{
    [AddComponentMenu(ComponentMenuConstants.PROJECTILES_FOLDER + "Bullet")]
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class Bullet : MonoBehaviour
    {
        #region Trigger Handling

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.LogFormat("OnTriggerEnter2D called from {0} by colliding with {1}", gameObject.name, collision.gameObject.name);
            if (collision.gameObject.CompareTag(Tags.ENEMY))
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.LogFormat("OnTriggerExit2D called from {0} by leaving collision with {1}", gameObject.name, collision.gameObject.name);
            if (collision.gameObject.CompareTag(Tags.BOUNDARY))
            {
                gameObject.SetActive(false);
            }
        }

        #endregion
    }
}
