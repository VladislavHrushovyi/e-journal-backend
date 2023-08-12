namespace EJournal.Domain.Common.Attributes;

public class StringValueAttr : Attribute
{
    public string StringValue { get; protected set; }

    public StringValueAttr(string value)
    {
        this.StringValue = value;
    }
}