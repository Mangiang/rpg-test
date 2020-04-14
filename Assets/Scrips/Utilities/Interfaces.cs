public interface ISelectable
{
    void OnSelect(PlayerHolder playerHolder);
}

public interface IDeselectable
{
    void OnDeselect(PlayerHolder playerHolder);
}

public interface IHighlight
{
    void OnHighlight(PlayerHolder playerHolder);
}

public interface IDehighlight
{
    void OnDehighlight(PlayerHolder playerHolder);
}

public interface IDetectableByMouse
{
    Node OnDetec();
}