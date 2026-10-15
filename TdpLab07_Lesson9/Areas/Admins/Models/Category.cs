using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TdpLab07_Lesson9.Areas.Admins.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống!")]
        [StringLength(100, ErrorMessage = "Tên danh mục tối đa là 100 ký tự!")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        [Display(Name = "Trạng thái")]
        public int Status { get; set; } = 1; 

        [DataType(DataType.Date)]
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; } = DateTime.Now; // Mặc định ngày hiện tại

        [StringLength(100)]
        [Display(Name = "Ảnh đại diện")]
        public string? Image { get; set; }

        [StringLength(350, ErrorMessage = "Mô tả tối đa 350 ký tự!")]
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }
    }
}
