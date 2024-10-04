using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class BaseDomainModel
    {


        [Timestamp]
        public byte[] Version { get; set; } = [];

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}
