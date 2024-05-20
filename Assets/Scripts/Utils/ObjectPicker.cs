using System;
using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    private Command command;
    private GameObject selectedObj;

    private void Awake() => command = new PickCommand();
    private void Start() => command.Execute(this);
    private void Update() => selectedObj.transform.position = transform.position;

    public void SetObject(GameObject obj) => selectedObj = obj;
    public GameObject GetObject() => selectedObj;
    public void ChangeCommand(Command _command) => command = _command;
}

public abstract class Command
{
    public abstract void Execute(ObjectPicker picker);
}

public class PickCommand : Command
{
    public override void Execute(ObjectPicker picker)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.transform)
            {
                picker.SetObject(hit.transform.gameObject);
                picker.ChangeCommand(new ReleaseCommand());
            }
        }
    }
}

public class ReleaseCommand : Command
{
    public override void Execute(ObjectPicker picker)
    {
        var ray = new Ray(picker.GetObject().transform.position, Vector3.down);

        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.transform.CompareTag("Dockable"))
            {
                picker.SetObject(null);
                picker.ChangeCommand(new PickCommand());
            }
        }
    }
}