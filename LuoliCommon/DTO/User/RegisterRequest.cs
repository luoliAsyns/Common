using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.User
{
    public class RegisterRequest
    {
        // 可以添加数据验证特性（如必填项校验）
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "电话不能为空")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "性别")]
        public bool Gender { get; set; } = true;
    }
}
