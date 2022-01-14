using System.Collections.Generic;
using System.Linq;
using TripServiceKata.Entity;
using TripServiceKata.Exception;
using TripServiceKata.Service;

namespace TripServiceKata
{
    public class TripService
    {
        private readonly IUserSession userSession;
        private readonly User user;

        public TripService(IUserSession userSession, User user)
        {
            this.userSession = userSession;
            this.user = user;
        }

        public List<Trip> GetTripsByUser()
        {
            var loggedUser = userSession.GetLoggedUser();
            if (loggedUser == null) throw new UserNotLoggedInException();

            if (user.IsFriend(loggedUser))
                return user.FindTripsByUser();

            return new List<Trip>();
        }
    }
}