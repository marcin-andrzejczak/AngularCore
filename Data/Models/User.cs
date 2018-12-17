using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularCore.Data.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Image ProfilePicture { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<UserFriends> UserFriends { get; set; } = new List<UserFriends>();

        public List<UserFriends> FriendUsers { get; set; } = new List<UserFriends>();

        [NotMapped]
        public List<User> Friends
        {
            get
            {
                var friends1 = UserFriends.Select( uf => uf.Friend );
                var friends2 = FriendUsers.Select( fu => fu.User );
                return friends1.Concat(friends2).Distinct().ToList();
            }
        }

        public List<Post> Posts { get; set; } = new List<Post>();

        public void AddFriend(User friend)
        {
            UserFriends.Add( new UserFriends( user: this, friend: friend ) );
        }
    }
}