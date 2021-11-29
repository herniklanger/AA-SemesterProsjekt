﻿using System;
using Autofac.Extras.Moq;
using InterfacesLib;
using System.Threading;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Moq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Route.Controllers;
using ServiceStack.OrmLite;
using Xunit;

namespace Route.Test.Unitest
{
    public class UpdateRouteTest
    {
        [Fact]
        public async void UpdateRoute_DBEkseption()
        {
            
            //Arrange
            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<DataBaseLayre.Models.Route, int>>().Setup(x =>
                    x.UpsertAsync(It.IsAny<DataBaseLayre.Models.Route>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(new Exception()); 
                RouteController controller = mock.Create<RouteController>();
                //Act
                ActionResult result = await controller.Put(new DataBaseLayre.Models.Route()) as ActionResult;
                //Assert
                var statusCodeResult = result as StatusCodeResult;
                ActionContext test = new ActionContext();
                statusCodeResult.StatusCode.Should().Be(304);
            }
        }
    }
}