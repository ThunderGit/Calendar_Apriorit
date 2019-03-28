﻿using Calendar_Apriorit.DAL.Interfaces;
using Unity;
using Calendar_Apriorit.DAL.Repositories;
using System;
using Calendar_Apriorit.Infastructure;
using Calendar_Apriorit.BLL;

namespace Calendar_Apriorit.Initialazer
{
    public class UnityDependencyRegister
    {
        private static IUnityContainer _container;

        public static void RegisterDependencyTypes(IUnityContainer container)
        {
            _container = container;

            RegisterContexts();
            RegisterBusinessTypes();
            RegisterDataTypes();
        }

        private static void RegisterContexts()
        {
            _container.RegisterType<IWebContext, WebContext>();
            _container.RegisterType<IBusinessContext, BusinessContext>();
        }

        private static void RegisterBusinessTypes()
        {
            throw new NotImplementedException();
        }

        private static void RegisterDataTypes()
        {
            _container.RegisterType<IUnitOfWork, EFUnitOfWork>();
            
        }
    }
}