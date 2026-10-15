using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TdpLab07_Lesson9.Areas.Admins.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        [StringLength(150, ErrorMessage = "Tên sản phẩm tối đa là 150 ký tự!")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm không được để trống!")]
        [Display(Name = "Giá bán")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Display(Name = "Ảnh đại diện")]
        [StringLength(250)]
        public string? Image { get; set; }

        [Display(Name = "Mô tả sản phẩm")]
        [StringLength(500)]
        public string? Description { get; set; }

        [Display(Name = "Trạng thái")]
        public int Status { get; set; } = 1; // Mặc định hiển thị (1)

        [Display(Name = "Ngày tạo")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // --- KHÓA NGOẠI LIÊN KẾT VỚI BẢNG CATEGORIES ---
        [Required(ErrorMessage = "Vui lòng chọn danh mục sản phẩm!")]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}