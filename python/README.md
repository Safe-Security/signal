# Overview

This package contains the definitions of common objects used in safe-security signal and a python program to submit [sample signals](../examples/samples) to Safe.

# Signal specification

See [Signal](app/dataclass/signal.py)

# Run sample program

```
Make sure `python3` and `pip3` are installed in the system.
Add Safe server URL and REST API Creds in the file config.ini.
pip3 install -r requirements.txt
python3 app/main.py
```
## Result

Once the sample program is run successfully, assets and assessments can be seen in the Safe UI.