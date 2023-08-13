using System.Reflection;
using EJournal.Domain.Common;
using EJournal.Domain.Common.Attributes;

namespace EJournal.Domain.Extension;

public static class GetReservationStatusStringValue
{
    public static string GetStringValue(this ReservationStatus status)
    {
        Type type = status.GetType();
        FieldInfo? fieldInfo = type.GetField(status.ToString());

        StringValueAttr[]? stringValueAttr = fieldInfo?.GetCustomAttributes(typeof(StringValueAttr), false) as StringValueAttr[];

        return stringValueAttr != null && stringValueAttr.Length > 0 ? stringValueAttr[0].StringValue : "";
    }
    
    public static string GetStringValue(this CustomDayOfWeek status)
    {
        Type type = status.GetType();
        FieldInfo? fieldInfo = type.GetField(status.ToString());

        StringValueAttr[]? stringValueAttr = fieldInfo?.GetCustomAttributes(typeof(StringValueAttr), false) as StringValueAttr[];

        return stringValueAttr != null && stringValueAttr.Length > 0 ? stringValueAttr[0].StringValue : "";
    }
}