﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoTest.TestRunners.Shared;
using AutoTest.TestRunners.NUnit;
using System.IO;

namespace AutoTest.TestRunners.NUnit.Tests
{
    [TestFixture]
    public class RunnerTests
    {
        [Test]
        public void Should_run_test()
        {
            var options = new RunnerOptions("nunit");
            options.AddAssembly(new AssemblyOptions(
                Path.GetFullPath(@"AutoTest.TestRunners.NUnit.Tests.TestResource.dll")));
            var runner = new Runner();
            var results = runner.Run(options);
            Assert.That(results.Count(), Is.EqualTo(5));

            Assert.That(results.ElementAt(0).Assembly, Is.EqualTo(Path.GetFullPath(@"AutoTest.TestRunners.NUnit.Tests.TestResource.dll")));
            Assert.That(results.ElementAt(0).TestFixture, Is.EqualTo("AutoTest.TestRunners.NUnit.Tests.TestResource.Fixture1"));
            Assert.That(results.ElementAt(0).State, Is.EqualTo(Shared.TestState.Failed));
            Assert.That(results.ElementAt(0).TestName, Is.EqualTo("AutoTest.TestRunners.NUnit.Tests.TestResource.Fixture1.Should_fail"));
            Assert.That(results.ElementAt(0).StackLines.Count(), Is.EqualTo(1));
            Assert.That(File.Exists(results.ElementAt(0).StackLines.ElementAt(0).File), Is.True);
            Assert.That(results.ElementAt(0).StackLines.ElementAt(0).Method, Is.EqualTo("AutoTest.TestRunners.NUnit.Tests.TestResource.Fixture1.Should_fail()"));
            Assert.That(results.ElementAt(0).StackLines.ElementAt(0).Line, Is.EqualTo(21));

            Assert.That(results.ElementAt(1).Assembly, Is.EqualTo(Path.GetFullPath(@"AutoTest.TestRunners.NUnit.Tests.TestResource.dll")));
            Assert.That(results.ElementAt(1).TestFixture, Is.EqualTo("AutoTest.TestRunners.NUnit.Tests.TestResource.Fixture1"));
            Assert.That(results.ElementAt(1).State, Is.EqualTo(Shared.TestState.Ignored));
            Assert.That(results.ElementAt(1).TestName, Is.EqualTo("AutoTest.TestRunners.NUnit.Tests.TestResource.Fixture1.Should_ignore"));
            Assert.That(results.ElementAt(1).StackLines.Count(), Is.EqualTo(1));
            Assert.That(File.Exists(results.ElementAt(0).StackLines.ElementAt(0).File), Is.True);
            Assert.That(results.ElementAt(1).StackLines.ElementAt(0).Method, Is.EqualTo("AutoTest.TestRunners.NUnit.Tests.TestResource.Fixture1.Should_ignore()"));
            Assert.That(results.ElementAt(1).StackLines.ElementAt(0).Line, Is.EqualTo(27));

            Assert.That(results.ElementAt(2).Assembly, Is.EqualTo(Path.GetFullPath(@"AutoTest.TestRunners.NUnit.Tests.TestResource.dll")));
            Assert.That(results.ElementAt(2).TestFixture, Is.EqualTo("AutoTest.TestRunners.NUnit.Tests.TestResource.Fixture1"));
            Assert.That(results.ElementAt(2).State, Is.EqualTo(Shared.TestState.Passed));
            Assert.That(results.ElementAt(2).TestName, Is.EqualTo("AutoTest.TestRunners.NUnit.Tests.TestResource.Fixture1.Should_pass"));
            Assert.That(results.ElementAt(2).StackLines.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Should_run_tests_from_type()
        {
            var options = new RunnerOptions("nunit");
            options.AddAssembly(new AssemblyOptions(
                Path.GetFullPath(@"AutoTest.TestRunners.NUnit.Tests.TestResource.dll")));
            options.Assemblies.ElementAt(0).AddMember("AutoTest.TestRunners.NUnit.Tests.TestResource.Fixture2");
            var runner = new Runner();
            var results = runner.Run(options);
            Assert.That(results.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Should_run_tests_from_namespace()
        {
            var options = new RunnerOptions("nunit");
            options.AddAssembly(new AssemblyOptions(
                Path.GetFullPath(@"AutoTest.Runners.NUnit.Tests.TestResource.dll")));
            options.Assemblies.ElementAt(0).AddNamespace("AutoTest.Runners.NUnit.Tests.TestResource");
            var runner = new Runner();
            var results = runner.Run(options);
            Assert.That(results.Count(), Is.EqualTo(5));
        }

        [Test]
        public void Should_run_single_test_and_type()
        {
            var options = new RunnerOptions("nunit");
            options.AddAssembly(new AssemblyOptions(
                Path.GetFullPath(@"AutoTest.Runners.NUnit.Tests.TestResource.dll")));
            options.Assemblies.ElementAt(0).AddTest("AutoTest.Runners.NUnit.Tests.TestResource.Fixture1.Should_ignore");
            options.Assemblies.ElementAt(0).AddMember("AutoTest.Runners.NUnit.Tests.TestResource.Fixture2");
            var runner = new Runner();
            var results = runner.Run(options);
            Assert.That(results.Count(), Is.EqualTo(3));
        }
    }
}
