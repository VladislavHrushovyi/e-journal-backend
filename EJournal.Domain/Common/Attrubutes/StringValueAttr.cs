namespace EJournal.Domain.Common.Attrubutes;

public class StringValueAttr : Attribute
{
    public string StringValue { get; protected set; }

    public StringValueAttr(string value)
    {
        this.StringValue = value;
    }
}