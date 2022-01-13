using System.Collections.Generic;
using TripServiceKata.Exception;

namespace TripServiceKata.Entity
{
    public class User
    {
        private readonly List<User> friends = new List<User>();

        public virtual List<User> GetFriends()
        {
            return friends;
        }

        public void AddFriend(User user)
        {
            friends.Add(user);
        }

        public virtual List<Trip> FindTripsByUser()
        {
            throw new DependendClassCallDuringUnitTestException(
                "TripDAO should not be invoked on an unit test.");
        }
    }
}