namespace Domain.Entities
{
    public class PlanLocality
    {
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;
        public int LocalityId { get; set; }
        public Locality Locality { get; set; } = null!;
    }
}