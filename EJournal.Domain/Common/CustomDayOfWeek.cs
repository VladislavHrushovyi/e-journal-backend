using EJournal.Domain.Common.Attributes;

namespace EJournal.Domain.Common;

public enum CustomDayOfWeek
{
    [StringValueAttr("Понеділок")]
    Monday = 0,
    
    [StringValueAttr("Вівторок")]
    Tuesday = 1,
    
    [StringValueAttr("Середа")]
    Wednesday = 2,
    
    [StringValueAttr("Четвер")]
    Thursday = 3,
    
    [StringValueAttr("П'ятниця")]
    Friday = 4,
    
    [StringValueAttr("Субота")]
    Saturday = 5,
    
    [StringValueAttr("Неділя")]
    Sunday = 6
}