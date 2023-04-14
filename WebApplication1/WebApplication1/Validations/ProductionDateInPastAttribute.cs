using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Validations
{
    public class ProductionDateInPastAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
            => value is DateTime productionDate && productionDate < DateTime.Now;
    }
}
