import os

from communication import Communication
from config import Config
from dataclass.signal import Signal

from utils.constants import POST_SIGNALS_API, POST_SIGNALS_ZIP_API

"""
Reads sample signal jsons and submit it to Safe
"""
def submitSampleSignals():
    signalSampleDir = "../examples/samples/"
    isExist = os.path.exists(signalSampleDir)
    if not isExist:
        print("Signals sample directory doesnot exists. Exiting.")
        return
    signalJsonFiles = [signalJson for signalJson in os.listdir(signalSampleDir) if signalJson.endswith('.json')]
    for signalJson in signalJsonFiles:
        signalFile = signalSampleDir+signalJson
        with open(signalFile, 'rb') as signalFile:
            try:
                signalData = signalFile.read()
                signal = Signal.from_json(signalData)
                client = Communication()
                headers = {'Accept' : 'application/json', 'Content-Type' : 'application/json'}
                response = client.post(Config.base_url+POST_SIGNALS_API, body=signal.to_json(), extra_headers=headers)
                print(response.text)
            except Exception as err:
                print(
                    "Error while submitting signal json {signalFile.name}. Error: {err}".format(
                        signalFile=signalFile, err=err
                    )
                )

    signalZipFiles = [signalZip for signalZip in os.listdir(signalSampleDir) if signalZip.endswith('.zip')]
    for signalZip in signalZipFiles:
        signalZipFile = signalSampleDir+signalZip
        with open(signalZipFile, 'rb') as signalZipFile:
            try:
                client = Communication()
                files = {'file': (signalZip, signalFile)}
                response = client.post(Config.base_url+POST_SIGNALS_ZIP_API, files=files)
                print(response.text)
            except Exception as err:
                print(
                    "Error while submitting signal zip {signalZip.name}. Error: {err}".format(
                        signalFile=signalFile, err=err
                    )
                )
    