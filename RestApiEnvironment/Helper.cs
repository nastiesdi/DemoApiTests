using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using RestApiEnvironment.Models;

namespace RestApiEnvironment
{
    public static class Helper
    {
        public static void AssertOrderIsAscendingById(List<UserModel> userList)
        {
            var sortedList = userList.OrderBy(userModel => int.Parse(userModel.Id));
            Assert.AreEqual(sortedList, userList);
        }

        public static string GenerateRandomString(int length)
        {
            Random random = new Random();
            var rString = "";
            for (var i = 0; i < length; i++)
            {
                rString += ((char)(random.Next(1, 26) + 64)).ToString().ToLower();
            }

            return rString;
        }
        
        public static string LoadJson(string fileName)
        {
            using var r = new StreamReader(fileName);
            return r.ReadToEnd();
        }
    }
}