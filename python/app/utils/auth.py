import base64

from utils.constants import PASSWORD, USERNAME


def get_basic_auth_header() -> str:
    creds = USERNAME+":"+PASSWORD
    creds_bytes = creds.encode("ascii")
    creds_base64_bytes = base64.b64encode(creds_bytes)
    base64_string = creds_base64_bytes.decode("ascii")
    return "Basic "+base64_string
