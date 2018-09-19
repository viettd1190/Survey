using System;
using System.ComponentModel.DataAnnotations;

namespace Survey.WebApp.Models
{
    public class FaqModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Câu hỏi không được để trống")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Nội dung trả lời không được để trống")]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDisplay { get; set; }
    }
}
