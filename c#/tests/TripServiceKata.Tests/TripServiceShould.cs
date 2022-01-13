using Moq;
using TripServiceKata.Entity;
using TripServiceKata.Service;
using Xunit;

namespace TripServiceKata.Tests
{
    public class TripServiceShould
    {
        [Fact]
        public void returns_an_empty_trip_list_when_user_have_not_trips()
        {
            var userSession = Mock.Of<IUserSession>();
            var tripsService = new TripService(userSession, new User());
            Mock.Get(userSession).Setup(us => us.GetLoggedUser()).Returns(new User());

            var trips = tripsService.GetTripsByUser();

            Assert.True(trips.Count == 0);
        }
    }
}
