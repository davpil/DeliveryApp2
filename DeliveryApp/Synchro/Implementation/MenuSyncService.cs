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
                 new DynamicMenuEntity{Order=order++, Title="Position", Api="Position", Icon="position",
                      DynamicMenuActivityEntities=new List<DynamicMenuActivityEntity>
                      {
                          new DynamicMenuActivityEntity{ActivityEntityID=ActivityNames.Position},
                          new DynamicMenuActivityEntity{ActivityEntityID=ActivityNames.PositionCr},
                          new DynamicMenuActivityEntity{ActivityEntityID=ActivityNames.PositionDel},
                          new DynamicMenuActivityEntity{ActivityEntityID=ActivityNames.PositionDet},
                          new DynamicMenuActivityEntity{ActivityEntityID=ActivityNames.PositionEd},
                          new DynamicMenuActivityEntity{ActivityEntityID=ActivityNames.PositionInd},
                      }
                },
                  new DynamicMenuEntity{ Order = order++, Title = "Role", Api = "Role", Icon="role",
                        DynamicMenuActivityEntities = new List<DynamicMenuActivityEntity>
                        {
                            new DynamicMenuActivityEntity{ ActivityEntityID = ActivityNames.Role},
                            new DynamicMenuActivityEntity{ ActivityEntityID = ActivityNames.RoleCr},
                            new DynamicMenuActivityEntity{ ActivityEntityID = ActivityNames.RoleDel},
                            new DynamicMenuActivityEntity{ ActivityEntityID = ActivityNames.RoleDet},
                            new DynamicMenuActivityEntity{ ActivityEntityID = ActivityNames.RoleEd},
                            new DynamicMenuActivityEntity{ ActivityEntityID = ActivityNames.RoleInd},
                        }
                    },

                  new DynamicMenuEntity{ Order = order++, Title = "User", Api = "User", Icon="user",
                        DynamicMenuActivityEntities = new List<DynamicMenuActivityEntity>
                        {
                            new DynamicMenuActivityEntity{ ActivityEntityID = ActivityNames.User},
                            new DynamicMenuActivityEntity{ ActivityEntityID = ActivityNames.UserCr},
                            new DynamicMenuActivityEntity{ ActivityEntityID = ActivityNames.UserDel},
                            new DynamicMenuActivityEntity{ ActivityEntityID = ActivityNames.UserDet},
                            new DynamicMenuActivityEntity{ ActivityEntityID = ActivityNames.UserEd},
                            new DynamicMenuActivityEntity{ ActivityEntityID = ActivityNames.UserInd},
                        }
                    },
            };
            return menu;
        }
    }
}
