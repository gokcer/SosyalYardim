using DevExpress.ExpressApp;
using DevExpress.ExpressApp.AspNetCore;
using DevExpress.ExpressApp.AspNetCore.WebApi;
using DevExpress.Persistent.BaseImpl;

namespace SosyalYardim.WebApi.Core;

public class WebApiApplicationSetup : IWebApiApplicationSetup {
    public void SetupApplication(AspNetCoreApplication application) {
        application.Modules.Add(new SosyalYardim.Module.SosyalYardimModule());
        //application.CreateCustomModelDifferenceStore += Application_CreateCustomModelDifferenceStore;

#if DEBUG
        if(System.Diagnostics.Debugger.IsAttached) {
            application.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
        }
#endif
        //application.DatabaseVersionMismatch += (s, e) => {
        //    e.Updater.Update();
        //    e.Handled = true;
        //};
    }
    //private void Application_CreateCustomModelDifferenceStore(object? sender, CreateCustomModelDifferenceStoreEventArgs e) {
    //    e.Store = new ModelDifferenceDbStore((XafApplication)sender!, typeof(ModelDifference), true, "Win");
    //    e.Handled = true;
    //}
}
