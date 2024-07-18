using Work360.Services.Employee.Core.Exceptions;

namespace Work360.Services.Employee.Core.Entities;

public class AggregateId : IEquatable<AggregateId>
{
    public long PESEL { get; }

    public AggregateId(long pesel)
    {
        if (pesel is <= 30000000000 or > 99999999999)
        {
            throw new InvalidAggregateIdException();
        }
        
        PESEL = pesel;
    }
    
    public bool Equals(AggregateId? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || PESEL.Equals(other.PESEL);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((AggregateId)obj);
    }

    public override int GetHashCode()
    {
        return PESEL.GetHashCode();
    }
}