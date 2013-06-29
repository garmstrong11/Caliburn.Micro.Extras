﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;

namespace Samples.Validation {
    public class AppBootstrapper : BootstrapperBase {
        private SimpleContainer container;

        public AppBootstrapper() {
            Start();
        }

        protected override void Configure() {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.PerRequest<IShell, ShellViewModel>();

            container.Instance(new Company
                {
                    Name = "The Company",
                    Address = "Some Road",
                    Website = "http://thecompany.com",
                });
        }

        protected override object GetInstance(Type service, string key) {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new Exception("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<IShell>();
        }
    }
}
