using UnityEngine;

namespace GearFactory
{
    public class StaticMouseDrag : MonoBehaviour
    {
		public Rigidbody2D body;
        private Rigidbody2D connectedBody;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //raycast from mouse pointer
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    //if we hit something...
                    if (hit.collider.GetComponent<Rigidbody2D>())
                    {
                        connectedBody = hit.collider.GetComponent<Rigidbody2D>();
						connectedBody.isKinematic = true;
                    }
                }
				
				connectedBody = body;
				connectedBody.isKinematic = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (connectedBody != null)
                {
					connectedBody.isKinematic = false;
                    connectedBody = null;
                }
            }
        }

        void FixedUpdate()
        {
            if (connectedBody)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                connectedBody.transform.position = pos;
            }
        }

    }
}