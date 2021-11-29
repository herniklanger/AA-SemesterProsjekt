using System;
using Autofac.Extras.Moq;
using InterfacesLib;
using System.Threading;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Route = Route.DataBaseLayre.Models.Route;

namespace Route.Test.Unitest
{
    public class UpdateRouteTest
    {
        public void UpdateRoute_DBEkseption()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<DataBaseLayre.Models.Route, int>>().Setup<int>(x =>
                    x.UpsertAsync(It.IsAny<DataBaseLayre.Models.Route>(), It.IsAny<CancellationToken>()))
                    .Throws<Exception>();
            }
        }
    }
}