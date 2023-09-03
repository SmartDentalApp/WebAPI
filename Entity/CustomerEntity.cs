namespace smart_dental_webapi.Entity
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string Email {  get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public DateTime BirthDate { get; set; }
        public string RG { get; set; }
        public string Address { get; set; }
        public DateTime TreatmentStartDate { get; set; }
    }
}
