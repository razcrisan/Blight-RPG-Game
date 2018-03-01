using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
		Layer.Enemy, 
        Layer.Walkable
    };

	//serializefield keeps it private, 
    [SerializeField] float distanceToBackground = 100f; //if you get as far as 100m, give up
    Camera viewCamera; 

    RaycastHit m_hit; //defining a member variable called m_hit, its tipe RaycastHit
    public RaycastHit hit //if we do things this way, we can access m_hit from outside
    {
        get { return m_hit; }
    }

    Layer m_layerHit;  //
    public Layer layerHit
    {
        get { return m_layerHit; }
    }

    void Start() // TODO Awake?
    {
        viewCamera = Camera.main;
    }

    void Update() 
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                m_hit = hit.Value;
                m_layerHit = layer;
                return;
            }
        }

        // Otherwise return background hit, so we hit the background in the game
        m_hit.distance = distanceToBackground;
        m_layerHit = Layer.RaycastEndStop;
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
