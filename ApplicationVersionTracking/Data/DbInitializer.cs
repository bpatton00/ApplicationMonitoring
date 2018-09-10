using System;
using System.Linq;
using ApplicationVersionTracking.Models;

namespace ApplicationVersionTracking.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            // Look for any application.
            if (context.Applications.Any())
            {
                return;   // DB has been seeded
            }



            var projects = new Project[]
            {
                new Project{Name="TSDS"},
                new Project{Name="eGrants"}
            };
            foreach (Project p in projects)
            {
                context.Projects.Add(p);
            }
            context.SaveChanges();



            var EventTypes = new EventType[]
            {
                new EventType{Description="Install"},
                new EventType{Description="Uninstall"}
            };
            foreach (EventType e in EventTypes)
            {
                context.EventTypes.Add(e);
            }
            context.SaveChanges();



            var EnvironmentTypes = new EnvironmentType[]
            {
                new EnvironmentType{Description="Development"},
                new EnvironmentType{Description="Test"},
                new EnvironmentType{Description="Production"},
                new EnvironmentType{Description="Training" }
            };
            foreach (EnvironmentType e in EnvironmentTypes)
            {
                context.EnvironmentTypes.Add(e);
            }
            context.SaveChanges();



            var StatusTypes = new StatusType[]
            {
                new StatusType{Description="Active"},
                new StatusType{Description="Inactive"}
            };
            foreach (StatusType s in StatusTypes)
            {
                context.StatusTypes.Add(s);
            }
            context.SaveChanges();



            var applications = new Application[]
            {
            new Application{Name="Test",ProjectID=1,StatusTypeID=1,ActiveTFRelease="rel1234"},
            new Application{Name="Test2",ProjectID=1,StatusTypeID=1,ActiveTFRelease="rel1234"},
            new Application{Name="TestProj2",ProjectID=2,StatusTypeID=1,ActiveTFRelease="rel1234"},
            new Application{Name="TestDisabled",ProjectID=1,StatusTypeID=2,ActiveTFRelease="rel1234"},
            };
            foreach (Application a in applications)
            {
                context.Applications.Add(a);
            }
            context.SaveChanges();



            var Servers = new Server[]
            {
                new Server{Name="TEST"},
                new Server{Name="TEST2"}
            };
            foreach (Server s in Servers)
            {
                context.Servers.Add(s);
            }
            context.SaveChanges();

            
            context.SaveChanges();
        }
    }
}
