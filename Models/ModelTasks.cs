using System.Net.NetworkInformation;

namespace SoftlineTest.Models
{
    public class ModelTasks
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StatusID { get; set; }

        public ModelStatus Status { get; set; }
    }
}
