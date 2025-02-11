﻿// Copyright (c) 2021 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using DryIoc;
using FluentAssertions;
using Splat;
using Splat.DryIoc;
using Xunit;

namespace ReactiveUI.DI.Tests
{
    /// <summary>
    /// DryIoc ReactiveUI Dependency Tests.
    /// </summary>
    public class DryIocReactiveUIDependencyTests
    {
        /// <summary>
        /// Dries the ioc dependency resolver should register reactive UI creates command binding.
        /// </summary>
        [Fact]
        public void DryIocDependencyResolverShouldRegisterReactiveUI()
        {
            // Invoke RxApp which initializes the ReactiveUI platform.
            var container = new Container();
            container.UseDryIocDependencyResolver();
            Locator.CurrentMutable.InitializeReactiveUI();

            var converters = container.Resolve<IEnumerable<ICreatesCommandBinding>>().ToList();

            converters.Should().NotBeNull();
            converters.Should().Contain(x => x.GetType() == typeof(CreatesCommandBindingViaEvent));
            converters.Should().Contain(x => x.GetType() == typeof(CreatesCommandBindingViaCommandParameter));

            var convertersb = container.Resolve<IEnumerable<IBindingTypeConverter>>().ToList();

            convertersb.Should().NotBeNull();
            convertersb.Should().Contain(x => x.GetType() == typeof(StringConverter));
            convertersb.Should().Contain(x => x.GetType() == typeof(EqualityTypeConverter));
        }
    }
}
