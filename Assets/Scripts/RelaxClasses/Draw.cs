using UnityEngine;
using UnityEngine.InputSystem;

public class Draw : MonoBehaviour
{
    public Camera camera;
    public GameObject brush;

    private InputAction clickAction;
    private Vector2 lastPos;
    private LineRenderer lineRenderer;
    private InputAction pointAction;

    private void Awake()
    {
        clickAction = new InputAction("Click", binding: "<Mouse>/leftButton");
        pointAction = new InputAction("Point", binding: "<Mouse>/position");

        clickAction.started += ctx => CreateBrush();
        clickAction.canceled += ctx => lineRenderer = null;

        clickAction.Enable();
        pointAction.Enable();
    }

    private void Update()
    {
        if (lineRenderer == null)
            return;

        var pos = pointAction.ReadValue<Vector2>();
        if (pos != lastPos)
        {
            AddPoint(camera.ScreenToWorldPoint(pos));
            lastPos = pos;
        }
    }

    private void OnDestroy()
    {
        clickAction.Disable();
        pointAction.Disable();
    }

    private void CreateBrush()
    {
        var brushObject = Instantiate(brush);
        lineRenderer = brushObject.GetComponent<LineRenderer>();
        Vector2 mousePos = camera.ScreenToWorldPoint(pointAction.ReadValue<Vector2>());
        lineRenderer.SetPosition(0, mousePos);
        lineRenderer.SetPosition(1, mousePos);
    }

    private void AddPoint(Vector2 point)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, point);
    }


    public void clear()
    {
        foreach (var line in FindObjectsOfType<LineRenderer>())
            if (line.gameObject != brush)
                Destroy(line.gameObject);
    }

    public void changeColor(Color color)
    {
        brush.GetComponent<LineRenderer>().startColor = color;
        brush.GetComponent<LineRenderer>().endColor = color;
    }

    public void onBlackButtonClicked()
    {
        changeColor(Color.black);
    }

    public void onRedButtonClicked()
    {
        changeColor(Color.red);
    }

    public void onGreenButtonClicked()
    {
        changeColor(Color.green);
    }

    public void onBlueButtonClicked()
    {
        changeColor(Color.blue);
    }
}