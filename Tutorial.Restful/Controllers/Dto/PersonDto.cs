using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tutorial.Restful.Controllers.Dto
{
    public class PersonDto : IValidatableObject
    {
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "姓名"), Required, MaxLength(20, ErrorMessage = "{0}的长度不可超过{1}")]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Code { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Code.Length < Name.Length)
                yield return new ValidationResult("编号的长度不能小于姓名的长度", new[] { nameof(Code), nameof(Name) });
            yield return ValidationResult.Success;
        }
    }
}