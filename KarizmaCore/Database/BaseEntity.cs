using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KarizmaPlatform.Core.Database
{
    public class BaseEntity
    {
        [JsonInclude][Column("id", Order = 0), Required, Key] public long Id { get; set; }
        [JsonInclude][Column("created_at"), Required] public DateTimeOffset CreatedDate { get; set; }
        [JsonIgnore][Column("updated_at")] public DateTimeOffset UpdatedDate { get; set; }
        [JsonIgnore][Column("deleted_at")] public DateTimeOffset? DeletedDate { get; set; }
        [JsonIgnore][NotMapped] public bool IsDeleted => DeletedDate != null;

    }
}
