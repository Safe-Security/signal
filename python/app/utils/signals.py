import os
import json

from communication import Communication
from dataclass.signal import Signal

from utils.constants import BASE_URL, POST_SIGNALS_API

"""
Reads sample signal jsons and submit it to Safe
"""
def submitSampleSignals():
    signalSampleDir = "../examples/samples/"
    isExist = os.path.exists(signalSampleDir)
    if not isExist:
        print("Signals sample directory doesnot exists. Exiting.")
        return
    signal_json_files = [signal_json for signal_json in os.listdir(signalSampleDir) if signal_json.endswith('.json')]
    for signal_json in signal_json_files:
        signalFile = signalSampleDir+signal_json
        with open(signalFile, 'rb') as signalFile:
            try:
                signalData = signalFile.read()
                signal = Signal.from_json(signalData)
                client = Communication()
                headers = {'Accept' : 'application/json', 'Content-Type' : 'application/json'}
                response = client.post(BASE_URL+POST_SIGNALS_API, body=signal.to_json(), extra_headers=headers)
                print(response.text)
            except Exception as err:
                print(
                    "Error while submitting signal json {signalFile.name}. Error: {err}".format(
                        signalFile=signalFile, err=err
                    )
                )

