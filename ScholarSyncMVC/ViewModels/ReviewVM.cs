namespace ScholarSyncMVC.ViewModels
{
    public class ReviewVM
    {
        public int Id { get; set; }
        public string? Message { get; set; }

        public float? Rating { get; set; }
        public string UserPhoto { get; set; }
        public string FullName { get; set; }
        public string DepartmentName { get; set; }
    }
}
