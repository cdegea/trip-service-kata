using System.Collections.Generic;
using TripServiceKata.Entity;
using TripServiceKata.Exception;
using TripServiceKata.Service;

namespace TripServiceKata
{
    public class TripService
    {
        private readonly IUserSession userSession;

        public TripService(IUserSession userSession)
        {
            this.userSession = userSession;
        }

        public List<Trip> GetTripsByUser(User user)
        {
            var tripList = new List<Trip>();
            var loggedUser = userSession.GetLoggedUser();
            var isFriend = false;
            if (loggedUser != null)
            {
                foreach (var friend in user.GetFriends())
                    if (friend.Equals(loggedUser))
                    {
                        isFriend = true;
                        break;
                    }

                if (isFriend) tripList = user.FindTripsByUser();

                return tripList;
            }

            throw new UserNotLoggedInException();
        }
    }
}