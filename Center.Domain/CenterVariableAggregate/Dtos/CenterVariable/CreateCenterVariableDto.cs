namespace Center.Domain.CenterVariableAggregate.Dtos.CenterVariable
{
    public class CreateCenterVariableDto
    {
        public  int Id { get;  set; }
        public string Name { get;  set; }
        public string EnName { get;  set; }
        public int ParentId { get;  set; }
        public int Code { get;  set; }
        public int Sort { get;  set; }
        public int InternalUsage { get;  set; }
        public int AppType { get;  set; }
        public string Address { get; set; }
        public string ShortKey { get; set; }
        public byte[]? Icon { get; set; }

        // public List<ActivityDto> Activities { get; set; }
    }
}
