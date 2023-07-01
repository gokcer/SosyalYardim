using System.Configuration;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Win.ApplicationBuilder;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraEditors;
using System.Net;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Design;

namespace SosyalYardim.Win;

public class ApplicationBuilder : IDesignTimeApplicationFactory {
    public static WinApplication BuildApplication() {
        var builder = WinApplication.CreateBuilder();
        builder.UseApplication<SosyalYardimWindowsFormsApplication>();
        builder.Modules
            .AddAuditTrailXpo(options => {
                options.AuditDataItemPersistentType = typeof(DevExpress.Persistent.BaseImpl.AuditDataItemPersistent);
            })
            .AddCharts()
            .AddCloningXpo()
            .AddConditionalAppearance()
            .AddDashboards(options => {
                options.DashboardDataType = typeof(DevExpress.Persistent.BaseImpl.DashboardData);
                options.DesignerFormStyle = DevExpress.XtraBars.Ribbon.RibbonFormStyle.Ribbon;
            })
            .AddFileAttachments()
            .AddNotifications()
            .AddOffice()
            .AddPivotChart()
            .AddPivotGrid()
            .AddReports(options => {
                options.EnableInplaceReports = true;
                options.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.ReportDataV2);
                options.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
            })
            .AddScheduler()
            .AddStateMachine(options => {
                options.StateMachineStorageType = typeof(DevExpress.ExpressApp.StateMachine.Xpo.XpoStateMachine);
            })
            .AddTreeListEditors()
            .AddValidation(options => {
                options.AllowValidationDetailsAccess = false;
            })
            .AddViewVariants(options => {
                options.ShowAdditionalNavigation = true;
            })
            .Add<SosyalYardim.Module.SosyalYardimModule>()
        	.Add<SosyalYardimWinModule>();
        builder.ObjectSpaceProviders
            .AddNonPersistent();
        builder.Security
            .UseMiddleTierMode(options => {
                options.BaseAddress = new Uri("https://localhost:44319/");
                options.Events.OnHttpClientCreated = client => client.DefaultRequestHeaders.Add("Accept", "application/json");
                options.Events.OnCustomAuthenticate = (sender, security, args) => {
                    args.Handled = true;
                    HttpResponseMessage msg = args.HttpClient.PostAsJsonAsync("api/Authentication/Authenticate", (AuthenticationStandardLogonParameters)args.LogonParameters).GetAwaiter().GetResult();
                    string token = (string)msg.Content.ReadFromJsonAsync(typeof(string)).GetAwaiter().GetResult();
                    if(msg.StatusCode == HttpStatusCode.Unauthorized) {
                        throw new UserFriendlyException(token);
                    }
                    msg.EnsureSuccessStatusCode();
                    args.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                };
            })
            .UsePasswordAuthentication();
        builder.AddBuildStep(application => {
        });
        var winApplication = builder.Build();
        return winApplication;
    }

    XafApplication IDesignTimeApplicationFactory.Create() {
        DevExpress.ExpressApp.Security.ClientServer.MiddleTierClientSecurity.DesignModeUserType = typeof(SosyalYardim.Module.BusinessObjects.ApplicationUser);
        DevExpress.ExpressApp.Security.ClientServer.MiddleTierClientSecurity.DesignModeRoleType = typeof(DevExpress.Persistent.BaseImpl.PermissionPolicy.PermissionPolicyRole);
        return BuildApplication();
    }
}
