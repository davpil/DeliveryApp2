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
    public class ActivitySyncService : BaseSyncService<ActivityEntity, string>, ISynchronizable
    {
        public ActivitySyncService(IActivityRepository repository
           , ISynchroService synchro
           , ApplicationDbContext context) : base(repository, synchro, context)
        {
        }

        public override IEnumerable<ActivityEntity> GetEntities()
        {
            int ord = 0;
            List<ActivityEntity> acts = new List<ActivityEntity>
            {
                new ActivityEntity{ID=ActivityNames.Public, Order=1,
                Children=new List<ActivityEntity>{ }
                },
                new ActivityEntity{ ID = ActivityNames.Role, Order = 1,
                    Children = new List<ActivityEntity>{
                        new ActivityEntity { ID = ActivityNames.RoleInd, Order = (ord = 1)},
                        new ActivityEntity { ID = ActivityNames.RoleCr,  Order = ord++},
                        new ActivityEntity { ID = ActivityNames.RoleEd,  Order = ord++},
                        new ActivityEntity { ID = ActivityNames.RoleDel, Order = ord++},
                        new ActivityEntity { ID = ActivityNames.RoleDet, Order = ord++},
                    }
                },
                new ActivityEntity{ ID = ActivityNames.User, Order = 1,
                    Children = new List<ActivityEntity>{
                        new ActivityEntity { ID = ActivityNames.UserInd, Order = (ord = 1)},
                        new ActivityEntity { ID = ActivityNames.UserCr,  Order = ord++},
                        new ActivityEntity { ID = ActivityNames.UserEd,  Order = ord++},
                        new ActivityEntity { ID = ActivityNames.UserDel, Order = ord++},
                        new ActivityEntity { ID = ActivityNames.UserDet, Order = ord++},
                    }
                },
                 new ActivityEntity{ ID = ActivityNames.Employee, Order = 1,
                    Children = new List<ActivityEntity>{
                        new ActivityEntity { ID = ActivityNames.EmployeeInd, Order = (ord = 1)},
                        new ActivityEntity { ID = ActivityNames.EmployeeCr,  Order = ord++},
                        new ActivityEntity { ID = ActivityNames.EmployeeEd,  Order = ord++},
                        new ActivityEntity { ID = ActivityNames.EmployeeDel, Order = ord++},
                        new ActivityEntity { ID = ActivityNames.EmployeeDet, Order = ord++},
                    }
                },
                 new ActivityEntity{ ID = ActivityNames.Position, Order = 1,
                    Children = new List<ActivityEntity>{
                        new ActivityEntity { ID = ActivityNames.PositionInd, Order = (ord = 1)},
                        new ActivityEntity { ID = ActivityNames.PositionCr,  Order = ord++},
                        new ActivityEntity { ID = ActivityNames.PositionEd,  Order = ord++},
                        new ActivityEntity { ID = ActivityNames.PositionDel, Order = ord++},
                        new ActivityEntity { ID = ActivityNames.PositionDet, Order = ord++},
                    }
                },
            };

            return acts;
        }
    }
}
