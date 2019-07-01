namespace Discord
{
    public class AuditLogFilters
    {
        internal Property<long> UserIdProperty = new Property<long>();
        public long UserId
        {
            get { return UserIdProperty; }
            set { UserIdProperty.Value = value; }
        }

        internal Property<AuditLogActionType> ActionTypeProperty = new Property<AuditLogActionType>();
        public AuditLogActionType ActionType
        {
            get { return ActionTypeProperty; }
            set { ActionTypeProperty.Value = value; }
        }

        internal Property<long> BeforeIdProperty = new Property<long>();
        public long BeforeId
        {
            get { return BeforeIdProperty; }
            set { BeforeIdProperty.Value = value; }
        }

        internal Property<int> LimitProperty = new Property<int>();
        public int Limit
        {
            get { return LimitProperty; }
            set { LimitProperty.Value = value; }
        }
    }
}
