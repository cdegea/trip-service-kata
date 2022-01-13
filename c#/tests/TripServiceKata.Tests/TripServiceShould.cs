using Moq;
using System.Collections.Generic;
using TripServiceKata.Entity;
using TripServiceKata.Service;
using Xunit;

namespace TripServiceKata.Tests
{
    public class TripServiceShould
    {
        [Fact]
        public void returns_an_empty_trip_list_when_user_not_exists()
        {
            var userSession = Mock.Of<IUserSession>();
            var tripsService = new TripService(userSession, new User());
            Mock.Get(userSession).Setup(us => us.GetLoggedUser()).Returns(new User());

            var trips = tripsService.GetTripsByUser();

            Assert.True(trips.Count == 0);
        }

        [Fact]
        public void returns_two_trips_when_user_exists_and_its_in_friends_list()
        {
            var userSession = Mock.Of<IUserSession>();
            var user = Mock.Of<User>();
            var tripsService = new TripService(userSession, user);
            var expectedTrips = new List<Trip>
            {
                new Trip(), new Trip()
            };
            var expectedFriends = new List<User>
            {
                new User(), user
            };

            Mock.Get(userSession).Setup(us => us.GetLoggedUser()).Returns(user);
            Mock.Get(user).Setup(us => us.GetFriends()).Returns(expectedFriends);
            Mock.Get(user).Setup(us => us.FindTripsByUser()).Returns(expectedTrips);

            var trips = tripsService.GetTripsByUser();

            Assert.True(trips.Count == 2);
        }
    }
}
