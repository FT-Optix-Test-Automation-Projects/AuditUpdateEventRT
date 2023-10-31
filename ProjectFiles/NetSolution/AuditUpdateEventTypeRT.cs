#region Using directives
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.Core;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.WebUI;
using FTOptix.Alarm;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.EventLogger;

#endregion


public class AuditUpdateEventTypeRT: BaseNetLogic
{

    [ExportMethod]
    public void generateProject()
    {

        var projectRoot = Project.Current;
        var mainWindow = projectRoot.Get("UI").Get("MainWindow");
        var window = Owner.Get("UI").Get<WindowType>("MainWindow");
        window.Width = 1500;
        window.Height = 600;

        var styleSheet = InformationModel.MakeObject<StyleSheet>("StyleSheet");

        var user = InformationModel.MakeObject<User>("Guest");
        var usersFolder = projectRoot.Get("Security").Get("Users");
        usersFolder.Add(user);

        var uiFolder = projectRoot.Get("UI");
        var webPresentationEngine = InformationModel.MakeObject<WebUIPresentationEngine>("WebPresentationEngine");
        webPresentationEngine.Protocol = FTOptix.WebUI.Protocol.HTTP;
        webPresentationEngine.StyleSheet = styleSheet.NodeId;
        webPresentationEngine.StartingUser = user.NodeId;
        webPresentationEngine.StartWindow = window;
        webPresentationEngine.Hostname = "localhost";
        webPresentationEngine.Port = 8080;

        uiFolder.Add(styleSheet);
        uiFolder.Add(webPresentationEngine);

        var modelFolder = projectRoot.Get("Model");
        var variable1 = InformationModel.MakeVariable("Variable1", OpcUa.DataTypes.Int32);
        modelFolder.Add(variable1);

        var label1 = InformationModel.MakeObject<Label>("Label1");
        label1.TextVariable.SetDynamicLink(variable1);
        label1.TopMargin = 500;
        label1.LeftMargin = 50;
        mainWindow.Add(label1);

        var textBox1 = InformationModel.MakeObject<TextBox>("TextBox1");
        textBox1.TextVariable.SetDynamicLink(variable1);
        textBox1.TopMargin = 500;
        textBox1.LeftMargin = 100;
        mainWindow.Add(textBox1);

        Log.Info("Project Generation Complete");
    }
}
