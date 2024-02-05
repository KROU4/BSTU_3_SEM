using System;

public partial class Human : Entity, IComparable<Human>
{
    public Human(string name, int yearOfBirth, AdditionalInfo info) : base(name, yearOfBirth, info)
    {
    }

    public int CompareTo(Human other)
    {
        if (other == null)
        {
            return 1; // Любой экземпляр не является равным NULL
        }

        return YearOfBirth.CompareTo(other.YearOfBirth);
    }
}