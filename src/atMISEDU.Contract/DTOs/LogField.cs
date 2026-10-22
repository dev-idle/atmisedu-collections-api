namespace atMISEDU.Contract.DTOs
{
    public class LogField
    {
        public string Name { set; get; }
        public object NewValue { set; get; }
        public object OldValue { set; get; }
        public bool isSticked { set; get; }

        public LogField()
        {
            isSticked = false;
        }

        public LogField(string fldName, object fldValue)
        {
            Name = fldName;
            NewValue = fldValue;
            isSticked = false;
        }

        public LogField(string fldName, object newfldValue, object oldfldValue)
        {
            Name = fldName;
            NewValue = newfldValue;
            OldValue = oldfldValue;
            isSticked = false;
        }

        public LogField(string fldName, object newfldValue, object oldfldValue, bool isStick)
        {
            Name = fldName;
            NewValue = newfldValue;
            OldValue = oldfldValue;
            isSticked = isStick;
        }
    }
}