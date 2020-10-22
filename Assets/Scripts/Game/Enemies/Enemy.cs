using AirBattle.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Event = AirBattle.Events.Event;

namespace AirBattle.Enemies
{
    [AddComponentMenu(ComponentMenuConstants.ENEMIES_FOLDER + "Enemy")]
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField]
        private float horizontalSpeed = 1;

        [SerializeField]
        private Event onDeathEvent;

        [SerializeField]
        private BoxCollider2D enemyCollider;

        [SerializeField]
        private Rigidbody2D enemyRigidbody;

        #endregion

        #region Initialization

        public Vector3 Initialize(Vector3 spawnPosition)
        {
            Vector2 halfSize = enemyCollider.size * 0.5f;
            spawnPosition.y -= halfSize.y;
            transform.position = spawnPosition + new Vector3(-halfSize.x, 0, 0);
            spawnPosition.y -= halfSize.y;  // Add on the extra half the height of this plane AFTER we set the position so it is centred correctly

            enemyRigidbody.velocity = transform.right * horizontalSpeed;

            return spawnPosition;
        }

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            enemyCollider = enemyCollider == null ? GetComponent<BoxCollider2D>() : enemyCollider;
            enemyRigidbody = enemyRigidbody == null ? GetComponent<Rigidbody2D>() : enemyRigidbody;
        }

        #endregion

        #region Trigger Handling

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.LogFormat("OnTriggerEnter2D called from {0} by colliding with {1}", gameObject.name, collision.gameObject.name);
            if (collision.gameObject.CompareTag(Tags.PLAYER))
            {
                gameObject.SetActive(false);

                if (onDeathEvent != null)
                {
                    onDeathEvent.Raise();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.LogFormat("OnTriggerExit2D called from {0} by leaving collision with {1}", gameObject.name, collision.gameObject.name);
            if (collision.gameObject.CompareTag(Tags.BOUNDARY))
            {
                Vector3 position = transform.position;
                position.x = collision.transform.position.x - collision.bounds.extents.x - enemyCollider.bounds.extents.x;
                transform.position = position;
            }
        }

        #endregion
    }
}
