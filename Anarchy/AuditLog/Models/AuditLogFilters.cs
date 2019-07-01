namespace Discord
{
    public class AuditLogFilters
    {
        public long? UserId { get; set; }
        public AuditLogActionType? ActionType { get; set; }
        public long? BeforeId { get; set; }
        public int? Limit { get; set; }
    }
}
