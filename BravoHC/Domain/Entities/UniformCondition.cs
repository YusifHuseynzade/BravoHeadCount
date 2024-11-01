using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UniformCondition: BaseEntity
    {
        public int PositionId { get; set; }
        public Position Position { get; set; } 
        public string FunctionalArea { get; set; } 
        public string UniName { get; set; }
        public Gender Gender { get; set; } 
        public int CountUniform { get; set; } 
        public UniType UniType { get; set; } 
        public int UsageDuration { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public void InitializeUniformCondition(int positionId, Position position, string functionalArea, string uniName, Gender gender, int countUniform, UniType uniType, int usageDuration, string createdBy, DateTime createdDate)
        {
            PositionId = positionId;
            Position = position;
            FunctionalArea = functionalArea;
            UniName = uniName;
            Gender = gender;
            CountUniform = countUniform;
            UniType = uniType;
            UsageDuration = usageDuration;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
        }

    }
}
