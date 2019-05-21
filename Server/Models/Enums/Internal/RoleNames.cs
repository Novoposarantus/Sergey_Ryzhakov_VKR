using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Enums.Internal
{
    public static class RoleNames
    {
        public const string Admin = "admin";
        public const string User = "user";

        public static string FromId(int roleId)
        {
            switch (roleId)
            {
                case (int)RoleEnum.Admin:
                    return Admin;
                case (int)RoleEnum.User:
                    return User;
                default:
                    throw new NotSupportedException();
            }
        }

        public static string FromId(RoleEnum roleId)
        {
            return FromId((int)roleId);
        }

        public static RoleEnum FromName(string roleName)
        {
            switch (roleName)
            {
                case Admin:
                    return RoleEnum.Admin;
                case User:
                    return RoleEnum.User;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
