using LuoliCommon.DTO.User;
using LuoliDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliDatabase.Extensions
{
     public static class UserEntityExtensions
    {
        public static UserDTO ToDTO(this UserEntity entity)
        {
            if (entity is null)
                return null;
            // 映射属性（处理命名差异和类型转换）

            var dto = new UserDTO();

            dto.Email = entity.email;
            dto.Phone = entity.phone;
            dto.Gender = entity.gender;
            dto.LastLoginIP = entity.last_login_ip;
            dto.LastLoginTime = entity.last_login_time.HasValue ? entity.last_login_time.Value : DateTime.Now;
            dto.UserName = entity.username;
            dto.Password = entity.password;
            dto.RealName = entity.real_name;
            dto.Remark = entity.remark;

            return dto;
        }
    }
}
