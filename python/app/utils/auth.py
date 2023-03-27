import base64
from config import Config

def get_basic_auth_header() -> str:
    creds = Config.api_username+":"+Config.api_password
    creds_bytes = creds.encode("ascii")
    creds_base64_bytes = base64.b64encode(creds_bytes)
    base64_string = creds_base64_bytes.decode("ascii")
    return "Basic "+base64_string
