using DeliveryApp.Constants;
using DeliveryApp.Data;
using DeliveryApp.Models;
using DeliveryApp.Repositories.Interfaces;
using DeliveryApp.Services.Interfaces;
using DeliveryApp.Synchro.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Synchro.Implementation
{
    public class MenuSyncService : BaseSyncService<DynamicMenuEntity, int>, ISynchronizable
    {
        public MenuSyncService(IDynamicMenuRepository repository
          , ISynchroService synchro
          , ApplicationDbContext context) : base(repository, synchro, context)
        {
        }

        public override IEnumerable<DynamicMenuEntity> GetEntities()
        {
            int order = 0;
            List<DynamicMenuEntity> menu = new List<DynamicMenuEntity>
            {
                new DynamicMenuEntity{Order=order++, Title="Employee", Api="Employee", Icon="employee",
                      DynamicMenuActivityEntities=new List<DynamicMenuActivityEntity>
                      {
                          new DynamicMenuActivityEntity{ActivityEntityID=ActivityNames.Employee},
                          new DynamicMenuActivityEntity{ActivityEntityID=ActivityNames.EmployeeCr},
                          new DynamicMenuActivityEntity{ActivityEntityID=ActivityNames.EmployeeDel},
                          new DynamicMenuActivityEntity{ActivityEntityID=ActivityNames.EmployeeDet},
                          new DynamicMenuActivityEntity{ActivityEntityID=ActivityNames.EmployeeEd},
                          new DynamicMenuActivityEntity{ActivityEntityID=ActivityNames.EmployeeInd},
                      }
                },
            };
            return menu;
        }
    }
}
