using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Errors
{
    public static class StudentErrors
    {
        public static readonly Error SameStudent = new Error(
            "Student.SameUser", "Can't follow yourself");

        public static readonly Error NonPublicProfile = new Error(
            "Followers.NonPublicProfile", "Can't follow non-public profiles");

        public static readonly Error AlreadyFollowing = new Error(
            "Followers.AlreadyFollowing", "Already following");
    }

}
