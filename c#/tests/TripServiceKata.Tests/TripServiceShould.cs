﻿using System.Collections.Generic;
using Moq;
using TripServiceKata.Entity;
using TripServiceKata.Exception;
using TripServiceKata.Service;
using Xunit;

namespace TripServiceKata.Tests
{
    public class TripServiceShould
    {
        private readonly List<User> expectedFriends;
        private readonly List<Trip> expectedTrips;
        private readonly TripService tripsService;
        private readonly User user = Mock.Of<User>();
        private readonly IUserSession userSession = Mock.Of<IUserSession>();

        public TripServiceShould()
        {
            tripsService = new TripService(userSession, user);
            expectedTrips = new List<Trip>
            {
                new Trip(), new Trip()
            };
            expectedFriends = new List<User>
            {
                new User(), user
            };
        }

        [Fact]
        public void returns_two_trips_when_user_exists_and_its_in_friends_list()
        {
            Mock.Get(userSession).Setup(us => us.GetLoggedUser()).Returns(user);
            Mock.Get(user).Setup(us => us.GetFriends()).Returns(expectedFriends);
            Mock.Get(user).Setup(us => us.FindTripsByUser()).Returns(expectedTrips);

            var trips = tripsService.GetTripsByUser();

            Assert.True(trips.Count == 2);
        }

        [Fact]
        public void returns_an_empty_trip_list_when_user_exists_and_its_not_in_friends_list()
        {
            var friends = new List<User>
            {
                new User(), new User()
            };

            Mock.Get(userSession).Setup(us => us.GetLoggedUser()).Returns(user);
            Mock.Get(user).Setup(us => us.GetFriends()).Returns(friends);
            Mock.Get(user).Setup(us => us.FindTripsByUser()).Returns(expectedTrips);

            var trips = tripsService.GetTripsByUser();

            Assert.True(trips.Count == 0);
        }

        [Fact]
        public void returns_an_empty_trip_list_when_user_not_exists()
        {
            Mock.Get(userSession).Setup(us => us.GetLoggedUser()).Returns((User) null);
            Mock.Get(user).Setup(us => us.GetFriends()).Returns(expectedFriends);
            Mock.Get(user).Setup(us => us.FindTripsByUser()).Returns(expectedTrips);

            Assert.Throws<UserNotLoggedInException>(
                () => tripsService.GetTripsByUser()
            );
        }
    }
}