namespace Slack.Bot.Api.Behavior.Conditions;

public class It<TObject> where TObject : IEquatable<TObject>
{
    private readonly TObject _item;

    public It(TObject item)
    {
        _item = item;
    }

    public bool EqualsTo(TObject other)
    {
        return _item.Equals(other);
    }

    public bool NotEqualsTo(TObject other)
    {
        return _item.Equals(other) == false;
    }
}
