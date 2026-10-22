namespace atMISEDU.Contract.Enumerations
{
    public enum DateComparisonType
    {
        Equal, // Matches records with the same date
        LessThan, // Matches records with an earlier date
        GreaterThan, // Matches records with a later date
        LessThanOrEqual, // Matches records with the same or earlier date
        GreaterThanOrEqual // Matches records with the same or later date
    }

    public enum DateType
    {
        YEAR,
        MONTH,
        DATEBETWEEN
    }
}