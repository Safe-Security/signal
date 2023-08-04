import requests

from config import Config
from utils.auth import get_basic_auth_header
from utils.common import mergeDict
from utils.constants import BASIC_AUTH_API

class Communication:

    def __init__(self):
        self.access_token = None

    def get_auth_token(self):
        # can be improved with using access token expiry
        if self.access_token is None:
            url = Config.base_url+BASIC_AUTH_API
            headers = {
                "Authorization": get_basic_auth_header()
            }
            response = requests.post(url, data=None, headers=headers)
            if response.status_code == 200:
                auth_response = response.json()
                self.access_token = auth_response['accessToken']
        return self.access_token


    def get(self, url, body=None, extra_headers=None):
        headers = {"Authorization": "Bearer {token}".format(token=Communication.get_auth_token(self))}
        if extra_headers:
            headers = mergeDict(extra_headers, headers)

        res = requests.get(url, params=body, headers=headers, timeout=60)
        response_status_code = res.status_code
        print(
            "GET response Status Code {response_status_code}".format(
                response_status_code=response_status_code
            )
        )
        return res


    def post(self, url, body=None, extra_headers=None, files=None):
        headers = {"Authorization": "Bearer {token}".format(token=Communication.get_auth_token(self))}
        if extra_headers:
            headers = mergeDict(extra_headers, headers)

        res = requests.post(
            url,
            data=body,
            headers=headers,
            files=files,
            timeout=60,
        )
        response_status_code = res.status_code
        print(
            "POST response Status Code {response_status_code}".format(
                response_status_code=response_status_code
            )
        )
        return res
