using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.DevGroup.Projectname.Models
{
    /// <summary>
    /// 待办事项
    /// </summary>
    public class Todo
    {
        /// <summary>
        /// 特性标签Key, 标记Id为实例的唯一标识符, 即主键，数据库自动增长列
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(50)] //varchar 必须指定长度, 否则默认长度为max
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }
    }
}
