using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class BaseDomainModel
    {
        public int Id { get; set; }

        [Timestamp]
        public byte[] Version { get; set; } = [];

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}
