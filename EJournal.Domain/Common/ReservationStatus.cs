using EJournal.Domain.Common.Attrubutes;

namespace EJournal.Domain.Common;

public enum ReservationStatus
{
    [StringValueAttr("Вільно")]
    Free = 0,
    
    [StringValueAttr("Тимчасово заброньовано")]
    TemporaryHold = 1,
    
    [StringValueAttr("Заброньовно")]
    Reserved = 2,
    
    [StringValueAttr("Виконано")]
    Done = 3
}