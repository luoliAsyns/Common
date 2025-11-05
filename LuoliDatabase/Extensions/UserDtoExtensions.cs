using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.DTO.User;
using LuoliDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliDatabase.Extensions
{
    public static class UserDtoExtensions
    {
        public static UserEntity ToEntity(this UserDTO dto)
        {
            if (dto is null)
                return null;
            // 映射属性（处理命名差异和类型转换）

            var entity = new UserEntity();

            entity.email = dto.Email;
            entity.phone = dto.Phone;
            entity.gender = dto.Gender;
            entity.last_login_ip = dto.LastLoginIP;
            entity.last_login_time = dto.LastLoginTime;
            entity.username = dto.UserName;
            entity.password = dto.Password;
            entity.real_name = dto.RealName;
            entity.remark = dto.Remark;


            entity.birthday = null;
            entity.is_deleted = false;
            entity.dept_id = 1;

            return entity;
        }
    }
}
