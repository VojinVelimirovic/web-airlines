using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Sistem_za_rezervaciju_avio_karata.Models;

namespace Sistem_za_rezervaciju_avio_karata.Models
{
    public class Users
    {
        private static readonly string jsonFilePath = HttpContext.Current.Server.MapPath("~/App_Data/users.json");
        public static List<User> UsersList { get; set; }

        public static List<User> LoadUsers()
        {
            if (!File.Exists(jsonFilePath))
            {
                return new List<User>();
            }

            var json = File.ReadAllText(jsonFilePath);
            var retVal = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
            return retVal;
        }

        public static void SaveUsers()
        {
            var json = JsonConvert.SerializeObject(UsersList, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
        }

        public static User FindByUsername(string username)
        {
            return UsersList.Find(item => item.Username == username);
        }
        public static User AddUser(User user)
        {
            if(user.Reservations == null)
            {
                user.Reservations = new List<Reservation>();
            }
            user.UserType = UserType.Traveler;
            UsersList.Add(user);
            SaveUsers();
            return user;
        }
        public static void RemoveUser(User user)
        {
            UsersList.Remove(user);
            SaveUsers();
        }

        public static void UpdateUser(User updatedUser)
        {
            var existingUser = FindByUsername(updatedUser.Username);
            if (existingUser != null)
            {
                existingUser.Password = updatedUser.Password;
                existingUser.FirstName = updatedUser.FirstName;
                existingUser.LastName = updatedUser.LastName;
                existingUser.Email = updatedUser.Email;
                existingUser.DateOfBirth = updatedUser.DateOfBirth;
                existingUser.Gender = updatedUser.Gender;
                existingUser.UserType = updatedUser.UserType;
            }
        }
    }
}