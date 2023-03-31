**Get started with signals using C#.NET**

In this tutorial for signals development using C# you'll submit sample signals to SAFE and create your own signal.

This tutorial will show you how to:

1. Run sample application that generates signals and submits to SAFE.
2. Check the results in SAFE.
3. Create your own signal.
4. Create a zip file for multiple signals and submit it to SAFE

**Pre-Requisites**

- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
- [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Safe Url
- REST API creds to access SAFE API's

**How to run sample application**

1. Check out the [signals repository](https://github.com/Safe-Security/signal) to your machine from Github.
2. Navigate to `signals\csharp\src\` folder and open [SignalsSampleApp.sln](https://github.com/Safe-Security/signal/blob/main/csharp/src/SignalsSampleApp.sln) file in Visual studio 2022.
3. Go to  App project inside the solution and open `appSettings.json`
4. Fill following properties in the json file
   1. **SafeUrl-**(*Required*) Base Url of the Safe Server.
   2. **ApiUsername and ApiPassword** *(Required)*- REST api credentials to access SAFE API's
   3. **ExamplesDirectoryPath**(*Optional*)- Directory path where sample signal json's reside. Path to be added in following manner `ExamplesDirectoryPath-C:\\Users\\Username\\Desktop\\samples` If this path is not set then [default](https://github.com/Safe-Security/signal/tree/main/examples/samples) examples directory path will be picked. 
   4. Click on Save
5. In Visual studio click on Build and select Rebuild solution.
6. Once successfully build, start the application by clicking on App button.
7. Console should show the status and names of signals successfully submitted.

**Check results on SAFE**

1. Login to SAFE and click on Technology->Assets from left navigation view.
2. Search for asset `MyVirtualMachine.acme.com `and click on the asset.
3. Scroll down to see list of controls that were sent using signal.

**Create your own signal**

See [Signal](https://github.com/Safe-Security/signal/blob/main/csharp/src/Signals.Library/Models/Signal.cs) specification

1. Create instance of signal as per specification.
2. Call library method `SubmitSignal()` using the signal created in step 1.

**Create a zip file for multiple signals and submit it to SAFE**

In a real world scenario to get an overall risk posture of an asset, it must have multiple security controls attached to it. To submit this information to SAFE API have to be called multiple times, to prevent this SAFE also have the ability to accept all the signals for an entity/asset in one go by just creating a zip file comprising of collection of signals that are applicable to that asset.
Please follow the below steps:-

1. Create different signals as per [signal]((https://github.com/Safe-Security/signal/blob/main/csharp/src/Signals.Library/Models/Signal.cs)) specification applicable to an asset and save them as individual .`json` files.

2. Create a directory and copy all the signal `json's` into it.

3. Create `config.json` in the same directory with following code snippet

   ```json
   {
   	"assetMatchingCriteria": [
   		"fqdn",
   		"assetName",
   		"ipAddress"
   	],
   	"shouldImportAssets": true
   }
   ```

   

Here asset matching criteria will be used to match existing asset in SAFE based on the priority `fqdn,assetName,ipAddress`.

 `shouldImportAssets` will inform SAFE whether to import this asset on SAFE portal.

4. Click on save.
5. Now create a zip file that consist of all the files in the directory including config.json. Make sure no subfolders are present inside zip.
6. Now using the instance of communication class call SubmitSignalZip() with filepath as argument to this method.
7. Zip should be posted successfully to the API.
