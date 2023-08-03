**Get started with signals using Python**

In this tutorial for signals development using Python3 you'll create a sample signals and submit them to SAFE.

This tutorial will show you how to:

1. Run sample application that generates signals and submits to SAFE.
2. Check the results in SAFE.
3. Create your own signal.

**Pre-Requisites**

- Python3 and PIP3
- Safe Url
- REST API creds to access SAFE API's

**How to run sample application**

1. Check out the [signals repository](https://github.com/Safe-Security/signal) to your machine from Github.
2. Navigate to `signal/python` folder.
3. Fill following properties in the [config file](config.ini)
   1. **SafeUrl-**(*Required*) Base Url of the Safe Server.
   2. **ApiUsername and ApiPassword** *(Required)*- REST api credentials to access SAFE API's.
4. Run `pip3 install -r requirements.txt` which will install the required [dependencies](requirements.txt).
5. Run `python3 app/main.py` to run the program which will submit [sample signals](../examples/samples) to Safe.

**Check results on SAFE**

1. Login to SAFE and click on Technology->Assets from left navigation view.
2. Search for asset `MyVirtualMachine.acme.com `and click on the asset.
3. Scroll down to see list of controls that were sent using signal.

**Create your own signal**

See [Signal](app/dataclass/signal.py) specification

1. Follow [Signal](app/dataclass/signal.py) specification to add new signal jsons in the [directory](../examples/samples).
2. Run `python3 app/main.py` to submit the signals to Safe.