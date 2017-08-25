﻿using System;
using FakeItEasy;
using NUnit.Framework;
using Serializer = NServiceBus.AzureServiceBusForwarder.Serializers;

namespace NServiceBus.AzureServiceBusForwarder.Tests
{
    [TestFixture]
    public class ForwarderTests
    {
        private IEndpointInstance endpointFake;

        [SetUp]
        public void Setup()
        {
            endpointFake = A.Fake<IEndpointInstance>();
        }

        [Test]
        public void when_creating_a_forwarder_the_source_configuration_is_required()
        {
            Assert.Throws<ArgumentNullException>(() => new Forwarder(
                null,
                "DestinationQueue",
                endpointFake,
                message => typeof(TestMessage),
                null));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void when_creating_a_forwarder_the_destination_queue_is_required(string destinationQueue)
        {
            Assert.Throws<ArgumentException>(() => new Forwarder(
                new ForwarderSourceConfiguration("ConnectionString", "TestTopic"),
                destinationQueue,
                endpointFake,
                message => typeof(TestMessage),
                new Serializer.JsonSerializer()));
        }

        [Test]
        public void when_creating_a_forwarder_an_endpoint_is_required()
        {
            Assert.Throws<ArgumentNullException>(() => new Forwarder(
                new ForwarderSourceConfiguration("ConnectionString", "TestTopic"),
                "DestinationQueue",
                null,
                message => typeof(TestMessage),
                new Serializer.JsonSerializer()));
        }

        [Test]
        public void when_creating_a_forwarder_a_message_mapper_is_required()
        {
            Assert.Throws<ArgumentNullException>(() => new Forwarder(
                new ForwarderSourceConfiguration("ConnectionString", "TestTopic"),
                "DestinationQueue",
                endpointFake,
                null,
                new Serializer.JsonSerializer()));
        }

        [Test]
        public void when_creating_a_forwarder_a_serializer_is_required()
        {
            Assert.Throws<ArgumentNullException>(() => new Forwarder(
                new ForwarderSourceConfiguration("ConnectionString", "TestTopic"),
                "DestinationQueue", 
                endpointFake, 
                message => typeof(TestMessage),
                null));
        }
    }
}
